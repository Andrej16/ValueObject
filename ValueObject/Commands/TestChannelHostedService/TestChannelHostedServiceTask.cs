using Api.ChannelHostedService;

namespace Api.Commands.TestChannelHostedService;

public class TestChannelHostedServiceTask : BackgroundTask
{
    private TestChannelHostedServiceTask(params WorkItem[] workItems)
        : base(workItems)
    {

    }

    public static TestChannelHostedServiceTask Create(string firstOperationData, string secondOperationData)
    {
        var taskId = Guid.NewGuid();

        var backgroundTask = new TestChannelHostedServiceTask(
            new WorkItem(EWorkItemType.FirstOperation, taskId, firstOperationData),
            new WorkItem(EWorkItemType.FailOperation, taskId),
            new WorkItem(EWorkItemType.SecondOperation, taskId, secondOperationData));

        return backgroundTask;
    }
}
