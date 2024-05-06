using Api.ChannelHostedService;
using Domain.Common;

namespace Api.Processors;

public class FailOperationProcessor : WorkItemProcessor
{
    public override async Task<Result<WorkItem, Error>> ProcessAsync(
        WorkItem workItem, 
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"FailOperationProcessor.Process task with id: {workItem.TaskId}, " +
            $"and type {workItem.Type}");

        return await Task.FromResult(Errors.General.InternalServerError("Test fail"));
    }
}
