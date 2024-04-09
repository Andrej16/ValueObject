using Api.Services.Implementations;

namespace Api.Services.Abstractions
{
    public interface IOperationHandler
    {
        Task HandleAsync(OperationHandlerContext context);
    }
}
