using Api.ChannelHostedService;
using Domain.Common;

namespace Api.Processors
{
    public class TestWorkItemProcessor
    {
        public async Task<Result<WorkItem, Error>> ProcessAsync(WorkItem work, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);

            Console.WriteLine($"TestWorkItemProcessor.ProcessAsync: {work.TaskId}");

            return work;
        }
    }
}
