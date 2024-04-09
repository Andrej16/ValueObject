using Api.Services.Abstractions;

namespace Api.Services
{
    public class OperationFailure
    {
        private static readonly OperationFailure _explicitFailure = new() { FailCalled = true };

        private OperationFailure() { }

        public bool FailCalled { get; private set; }

        public IEnumerable<IOperationContract> FailedRequirements { get; private set; }
            = Array.Empty<IOperationContract>();

        public IEnumerable<OperationFailureReason> FailureReasons { get; private set; }
            = Array.Empty<OperationFailureReason>();

        public static OperationFailure ExplicitFail() => _explicitFailure;

        public static OperationFailure Failed(IEnumerable<OperationFailureReason> reasons)
            => new OperationFailure
            {
                FailCalled = true,
                FailureReasons = reasons
            };

        public static OperationFailure Failed(IEnumerable<IOperationContract> failed)
            => new OperationFailure
            {
                FailedRequirements = failed
            };
    }
}
