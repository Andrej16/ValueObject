using Api.Abstractions;
using System.Threading.Channels;

namespace Api.ChannelHostedService;

public sealed class TasksQueue : ITasksQueue
{
    private readonly Channel<WorkItem> _channel;

    public TasksQueue()
    {
        _channel = Channel.CreateUnbounded<WorkItem>();
    }

    public async Task QueueBackgroundTaskAsync(BackgroundTask backgroundTask, CancellationToken cancellationToken)
    {
        if (!backgroundTask.WorkItems.Any())
            throw new ArgumentException();

        foreach (var workItem in backgroundTask.WorkItems)
        {
            await _channel.Writer.WriteAsync(workItem, cancellationToken);
        }
    }

    public async Task<WorkItem> DequeueAsync(CancellationToken cancellationToken)
    {
        var workItem = await _channel.Reader.ReadAsync(cancellationToken);

        return workItem;
    }

    public Guid? GetNextId()
    {
        _channel.Reader.TryPeek(out var workItem);
            
        return workItem?.TaskId;
    }

    public async Task<int> ClearWorkItemsInTaskAsync(Guid taskId, CancellationToken cancellationToken)
    {
        int count = default;

        while(_channel.Reader.TryPeek(out var peekedWorkItem))
        {
            if (peekedWorkItem.TaskId != taskId)
                break;

            _ = await DequeueAsync(cancellationToken);
            count++;
        }

        return count;
    }
}
