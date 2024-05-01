namespace Api.ChannelHostedService;

public record WorkItem(EWorkItemType Type, Guid TaskId, object? State = null);
