using Api.ChannelHostedService;

namespace Api.Abstractions;

public interface ITasksQueue
{
    Task QueueBackgroundTaskAsync(BackgroundTask backgroundTask, CancellationToken cancellationToken);

    Task<WorkItem> DequeueAsync(CancellationToken cancellationToken);

    Guid? GetNextId();

    Task<int> ClearWorkItemsInTaskAsync(Guid taskId, CancellationToken cancellationToken);
}
