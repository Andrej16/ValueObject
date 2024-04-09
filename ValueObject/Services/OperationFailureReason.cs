using Api.Services.Abstractions;

namespace Api.Services
{
    public class OperationFailureReason
    {
        public OperationFailureReason(IOperationHandler handler, string message)
        {
            Handler = handler;
            Message = message;
        }

        public string Message { get; }

        public IOperationHandler Handler { get; }
    }
}
