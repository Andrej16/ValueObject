using Api.Abstractions;
using Api.Processors;

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
            EWorkItemType.FirstOperation => typeof(TestWorkItemProcessor),
            EWorkItemType.SecondOperation => typeof(TestWorkItemProcessor),

            _ => throw new NotImplementedException($"Processor for {type} is not implemented")
        };

        return _processors.First(p => p.GetType() == processorType);
    }
}
