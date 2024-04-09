using Api.Services.Abstractions;

namespace Api.Services.Implementations
{
    public class OperationHandlerContextFactory : IOperationHandlerContextFactory
    {
        public virtual OperationHandlerContext CreateContext(
            IEnumerable<IOperationContract> requirements)
        {
            return new OperationHandlerContext(requirements);
        }
    }
}
