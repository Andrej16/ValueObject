using Api.ChannelHostedService;

namespace Api.Abstractions;

public interface IBackgroundProcessorsFactory
{
    WorkItemProcessor GetProcessor(EWorkItemType type);
}