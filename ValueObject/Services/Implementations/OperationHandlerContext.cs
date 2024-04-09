using Api.Services.Abstractions;

namespace Api.Services.Implementations
{
    public class OperationHandlerContext
    {
        private readonly HashSet<IOperationContract> _pendingRequirements;
        private List<OperationFailureReason>? _failedReasons;
        private bool _failCalled;
        private bool _succeedCalled;

        public OperationHandlerContext(IEnumerable<IOperationContract> requirements)
        {
            Requirements = requirements;
            _pendingRequirements = new HashSet<IOperationContract>(requirements);
        }

        public virtual IEnumerable<IOperationContract> Requirements { get; }

        public virtual object? Resource { get; }

        public virtual IEnumerable<IOperationContract> PendingRequirements => _pendingRequirements;

        public virtual IEnumerable<OperationFailureReason> FailureReasons =>
            _failedReasons ?? Enumerable.Empty<OperationFailureReason>();

        public virtual bool HasFailed => _failCalled;

        public virtual bool HasSucceeded
        {
            get
            {
                return !_failCalled && _succeedCalled && !PendingRequirements.Any();
            }
        }

        public virtual void Succeed(IOperationContract requirement)
        {
            _succeedCalled = true;
            _pendingRequirements.Remove(requirement);
        }

        public virtual void Fail() => _failCalled = true;

        public virtual void Fail(OperationFailureReason reason)
        {
            Fail();
            if (reason != null)
            {
                if (_failedReasons == null)
                {
                    _failedReasons = new List<OperationFailureReason>();
                }

                _failedReasons.Add(reason);
            }
        }
    }
}