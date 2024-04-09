using Api.Services.Implementations;

namespace Api.Services.Abstractions
{
    public interface IOperationEvaluator
    {
        OperationResult Evaluate(OperationHandlerContext context);
    }
}
