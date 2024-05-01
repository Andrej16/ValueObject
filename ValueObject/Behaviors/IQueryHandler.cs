using MediatR;

namespace Api.Behaviors
{
    public interface IQueryHandler<TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}
