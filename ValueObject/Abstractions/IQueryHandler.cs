using MediatR;

namespace Api.Abstractions
{
    public interface IQueryHandler<TQuery, TResponse> 
        : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}
