using Api.Abstractions;
using System.Transactions;

namespace Api.ChannelHostedService;

public class WorkItemsHostedService : BackgroundService
{
    private readonly ITasksQueue _tasksQueue;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WorkItemsHostedService> _logger;

    public WorkItemsHostedService(
        ITasksQueue tasksQueue,
        IServiceProvider serviceProvider,
        ILogger<WorkItemsHostedService> logger)
    {
        _tasksQueue = tasksQueue;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var processorsFactory = scope.ServiceProvider.GetRequiredService<IBackgroundProcessorsFactory>();

        while (!cancellationToken.IsCancellationRequested)
        {
            //Todo: transaction must envelope work items with the same taskId
            //using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var workItem = await _tasksQueue.DequeueAsync(cancellationToken);

            var processor = processorsFactory.GetProcessor(workItem.Type);

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

            //transactionScope.Complete();
        }
    }
}
