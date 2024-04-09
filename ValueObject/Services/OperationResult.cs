namespace Api.Services
{
    public class OperationResult
    {
        private static readonly OperationResult _succeededResult
            = new()
            {
                Succeeded = true
            };

        private static readonly OperationResult _failedResult
            = new()
            {
                Failure = OperationFailure.ExplicitFail()
            };

        private OperationResult() { }

        public bool Succeeded { get; private set; }

        public OperationFailure? Failure { get; private set; }

        public static OperationResult Success() => _succeededResult;

        public static OperationResult Failed(OperationFailure failure)
            => new OperationResult { Failure = failure };

        public static OperationResult Failed() => _failedResult;
    }
}
