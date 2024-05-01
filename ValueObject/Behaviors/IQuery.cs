using MediatR;

namespace Api.Behaviors
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {

    }
}
