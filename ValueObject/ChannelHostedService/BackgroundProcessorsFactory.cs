using Api.Abstractions;

namespace Api.ChannelHostedService;

public sealed class BackgroundProcessorsFactory : IBackgroundProcessorsFactory
{
    private readonly IEnumerable<WorkItemProcessor> _processors;

    public BackgroundProcessorsFactory(IEnumerable<WorkItemProcessor> processors)
    {
        _processors = processors;
    }

    public WorkItemProcessor GetProcessor(EWorkItemType type)
    {
        Type processorType = type switch
        {
            EWorkItemType.FirstOperation => typeof(WorkItemProcessor),

            _ => throw new NotImplementedException($"Processor for {type} is not implemented")
        };

        return _processors.First(p => p.GetType() == processorType);
    }
}
