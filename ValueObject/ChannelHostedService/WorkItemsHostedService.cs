using Api.Abstractions;
using System.Transactions;

namespace Api.ChannelHostedService;

public class WorkItemsHostedService : BackgroundService
{
    private readonly IBackgroundProcessorsFactory _processorsFactory;
    private readonly ITasksQueue _tasksQueue;
    private readonly ILogger<WorkItemsHostedService> _logger;

    public WorkItemsHostedService(
        IBackgroundProcessorsFactory processorsFactory,
        ITasksQueue tasksQueue,
        ILogger<WorkItemsHostedService> logger)
    {
        _processorsFactory = processorsFactory;
        _tasksQueue = tasksQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        while (await _tasksQueue.WaitToReadAsync(cancellationToken))
        {
            var workItem = _tasksQueue.Dequeue();

            var processor = _processorsFactory.GetProcessor(workItem.Type);

            var operationResult = await processor.ProcessAsync(workItem, cancellationToken);
            if (operationResult.IsFailure)
            {
                _logger.LogError(
                    "Background {Processor} failed be {Reason}",
                    processor.ToString(),
                    operationResult.Error.Message);
            }

            var nextWorkItemId = _tasksQueue.GetNextId();
            if (workItem.TaskId == nextWorkItemId)
                continue;

            transactionScope.Complete();
        }
    }
}
