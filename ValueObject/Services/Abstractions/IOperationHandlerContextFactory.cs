using Api.Services.Implementations;

namespace Api.Services.Abstractions
{
    public interface IOperationHandlerContextFactory
    {
        OperationHandlerContext CreateContext(IEnumerable<IOperationContract> requirements);
    }
}
