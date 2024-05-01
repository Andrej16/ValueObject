using MediatR;

namespace Api.Behaviors
{
    public interface ICommandHandler<in TCommand, TResponse>
        : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
    }
}
