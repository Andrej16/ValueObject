using Domain.Common;

namespace Api.ChannelHostedService;

public abstract class WorkItemProcessor
{
    public abstract Task<Result<WorkItem, Error>> ProcessAsync(WorkItem workItem, CancellationToken cancellationToken);
}