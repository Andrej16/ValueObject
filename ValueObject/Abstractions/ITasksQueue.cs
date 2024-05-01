using Api.ChannelHostedService;

namespace Api.Abstractions;

public interface ITasksQueue
{
    void QueueBackgroundTask(BackgroundTask backgroundTask, CancellationToken cancellationToken);
    
    Task QueueAsync(WorkItem workItem, CancellationToken cancellationToken);
    
    ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken);

    WorkItem Dequeue();

    Guid? GetNextId();
}
