using Api.Abstractions;
using Api.Behaviors;
using Domain.Common;

namespace Api.Commands.TestChannelHostedService;

public record TestChannelHostedServiceCommand(
    string FirstOperation,
    string SecondOperation) : ICommand<Result<TestChannelHostedServiceResponse, Error>>;

public class TestChannelHostedServiceHandler(ITasksQueue tasksQueue)
    : ICommandHandler<TestChannelHostedServiceCommand, Result<TestChannelHostedServiceResponse, Error>>
{
    private readonly ITasksQueue _tasksQueue = tasksQueue;

    public async Task<Result<TestChannelHostedServiceResponse, Error>> Handle(
        TestChannelHostedServiceCommand command,
        CancellationToken cancellationToken)
    {
        var testChannelHostedServiceTask = TestChannelHostedServiceTask.Create(
            command.FirstOperation,
            command.SecondOperation);

        await _tasksQueue.QueueBackgroundTaskAsync(testChannelHostedServiceTask, cancellationToken);

        return new TestChannelHostedServiceResponse();
    }
}