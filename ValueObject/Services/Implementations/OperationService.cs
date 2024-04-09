using Api.Services.Abstractions;

namespace Api.Services.Implementations
{
    public class OperationService : IOperationService
    {
        private readonly IOperationHandlerContextFactory _contextFactory;
        private readonly IOperationHandlerProvider _handlersProvider;
        private readonly IOperationEvaluator _evaluator;

        public OperationService(
            IOperationHandlerProvider handlers,
            IOperationHandlerContextFactory contextFactory,
            IOperationEvaluator evaluator)
        {
            _handlersProvider = handlers;
            _evaluator = evaluator;
            _contextFactory = contextFactory;
        }

        public virtual async Task<OperationResult> ExecuteAsync(
            IEnumerable<IOperationContract> requirements)
        {
            var authContext = _contextFactory.CreateContext(requirements);

            var handlers = _handlersProvider.GetHandlers(authContext);

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(authContext);
            }

            var result = _evaluator.Evaluate(authContext);

            return result;
        }
    }
}
