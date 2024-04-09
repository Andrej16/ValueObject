using Api.Services.Abstractions;

namespace Api.Services.Implementations
{
    public class OperationEvaluator : IOperationEvaluator
    {
        public OperationResult Evaluate(OperationHandlerContext context)
        {
            if (context.HasSucceeded)
            {
                return OperationResult.Success();
            }
            else if (context.HasFailed)
            {
                return OperationResult.Failed(OperationFailure.Failed(context.FailureReasons));
            }
            
            return OperationResult.Failed(OperationFailure.Failed(context.PendingRequirements));
        }
    }
}
