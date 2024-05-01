namespace Api.ChannelHostedService;

public abstract class BackgroundTask(IEnumerable<WorkItem> workItems)
{
    public readonly IReadOnlyList<WorkItem> WorkItems = workItems.ToList();
}
