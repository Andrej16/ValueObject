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

    public void QueueBackgroundTask(BackgroundTask backgroundTask, CancellationToken cancellationToken)
    {
        if (!backgroundTask.WorkItems.Any())
            throw new ArgumentException();

        var tasks = new List<Task>();
        foreach (var workItem in backgroundTask.WorkItems)
        {
            tasks.Add(QueueAsync(workItem, cancellationToken));
        }

        Task.WaitAll([.. tasks], cancellationToken);
    }

    public async Task QueueAsync(WorkItem workItem, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(workItem, cancellationToken);
    }

    public async ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return false;

        return await _channel.Reader.WaitToReadAsync(cancellationToken);
    }

    public WorkItem Dequeue()
    {
        while (_channel.Reader.TryRead(out var work))
            return work;

        throw new InvalidOperationException("No work items available");
    }

    public Guid? GetNextId()
    {
        if (_channel.Reader.TryPeek(out var work))
            return work.TaskId;

        return null;
    }
}
