using Api.Services.Implementations;

namespace Api.Services.Abstractions
{
    public interface IOperationHandlerProvider
    {
        IEnumerable<IOperationHandler> GetHandlers(OperationHandlerContext context);
    }
}
