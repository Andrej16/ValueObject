using Api.ChannelHostedService;
using Domain.Common;

namespace Api.Processors
{
    public class TestWorkItemProcessor : WorkItemProcessor
    {
        public override async Task<Result<WorkItem, Error>> ProcessAsync(WorkItem work, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);

            Console.WriteLine($"TestWorkItemProcessor.Process task with id: {work.TaskId}, and type {work.Type}");

            return work;
        }
    }
}
