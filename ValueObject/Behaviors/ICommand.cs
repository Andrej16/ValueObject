using MediatR;

namespace Api.Behaviors
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }
}
