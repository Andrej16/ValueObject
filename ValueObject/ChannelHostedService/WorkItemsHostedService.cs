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
        Guid taskId = default;
        TransactionScope? transactionScope = default;
        using var scope = _serviceProvider.CreateScope();
        var processorsFactory = scope.ServiceProvider.GetRequiredService<IBackgroundProcessorsFactory>();

        while (!cancellationToken.IsCancellationRequested)
        {
            var workItem = await _tasksQueue.DequeueAsync(cancellationToken);
            if (taskId != workItem.TaskId)
            {
                transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                taskId = workItem.TaskId;
            }

            var processor = processorsFactory.GetProcessor(workItem.Type);

            var operationResult = await processor.ProcessAsync(workItem, cancellationToken);
            if (operationResult.IsFailure)
            {
                transactionScope?.Dispose();
                await _tasksQueue.ClearWorkItemsInTaskAsync(workItem.TaskId, cancellationToken);

                _logger.LogError(
                    "Background {Processor} failed be {Reason}",
                    processor.ToString(),
                    operationResult.Error.Message);

                continue;
            }

            if (workItem.TaskId != _tasksQueue.GetNextId())
                transactionScope!.Complete();
        }
    }
}
