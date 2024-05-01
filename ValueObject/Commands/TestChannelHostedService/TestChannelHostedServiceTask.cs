using Api.ChannelHostedService;

namespace Api.Commands.TestChannelHostedService;

public class TestChannelHostedServiceTask : BackgroundTask
{
    private TestChannelHostedServiceTask(IEnumerable<WorkItem> workItems)
        : base(workItems)
    {

    }

    public static TestChannelHostedServiceTask Create(string firstOperationData, string secondOperationData)
    {
        var taskId = Guid.NewGuid();
        var firstWorkItem = new WorkItem(EWorkItemType.FirstOperation, taskId, firstOperationData);
        var secondWorkItem = new WorkItem(EWorkItemType.SecondOperation, taskId, secondOperationData);

        var workItems = new WorkItem[]
        {
            firstWorkItem,
            secondWorkItem
        };

        var backgroundTask = new TestChannelHostedServiceTask(workItems);

        return backgroundTask;
    }
}
