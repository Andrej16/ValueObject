using Api.Services.Abstractions;

namespace Api.Services.Implementations
{
    public class OperationHandlerProvider : IOperationHandlerProvider
    {
        private readonly IEnumerable<IOperationHandler> _handlers;

        public OperationHandlerProvider(IEnumerable<IOperationHandler> handlers)
        {
            _handlers = handlers;
        }

        public IEnumerable<IOperationHandler> GetHandlers(OperationHandlerContext context)
            => _handlers;
    }
}
