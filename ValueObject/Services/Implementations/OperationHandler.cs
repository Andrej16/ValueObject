using Api.Services.Abstractions;

namespace Api.Services.Implementations
{
    public abstract class OperationHandler<TRequirement> : IOperationHandler
        where TRequirement : IOperationContract
    {
        public virtual async Task HandleAsync(OperationHandlerContext context)
        {
            foreach (var req in context.Requirements.OfType<TRequirement>())
            {
                await HandleAsync(context, req);
            }
        }

        protected abstract Task HandleAsync(
            OperationHandlerContext context, TRequirement requirement);
    }
}
