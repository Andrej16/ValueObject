using System.Runtime.Serialization;

namespace Domain.Common
{
    internal readonly struct ResultCommonLogic<E>
    {
        internal static class Messages
        {
            public static readonly string ErrorIsInaccessibleForSuccess = "You attempted to access the Error property for a successful result. A successful result has no Error.";
            
            public static readonly string ValueIsInaccessibleForFail = "You attempted to access the Value property for a unsuccessful result. A unsuccessful result has no Value.";

            public static readonly string ErrorObjectIsNotProvidedForFailure = "You attempted to create a failure result, which must have an error, but a null error object (or empty string) was passed to the constructor.";

            public static readonly string ErrorObjectIsProvidedForSuccess = "You attempted to create a success result, which cannot have an error, but a non-null error object was passed to the constructor.";

            public static readonly string ConvertFailureExceptionOnSuccess = "ConvertFailure failed because the Result is in a success state.";

            public static string ValueIsInaccessibleForFailure(string error)
            {
                return "You attempted to access the Value property for a failed result. A failed result has no Value. The error was: " + error;
            }
        }

        private readonly E? _error;

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        public E Error
        {
            get
            {
                if (!IsFailure)
                {
                    throw new InvalidOperationException();
                }

                return _error!;
            }
        }

        public ResultCommonLogic(bool isFailure, E? error)
        {
            if (isFailure)
            {
                if (error == null || (error is string && error.Equals(string.Empty)))
                {
                    throw new ArgumentNullException("error", Messages.ErrorObjectIsNotProvidedForFailure);
                }
            }
            else if (!EqualityComparer<E>.Default.Equals(error, default))
            {
                throw new ArgumentException(Messages.ErrorObjectIsProvidedForSuccess, "error");
            }

            IsFailure = isFailure;
            _error = error;
        }

        public void GetObjectData(SerializationInfo info)
        {
            info.AddValue("IsFailure", IsFailure);
            info.AddValue("IsSuccess", IsSuccess);
            if (IsFailure)
            {
                info.AddValue("Error", Error);
            }
        }

        public void GetObjectData<T>(SerializationInfo info, IValue<T> valueResult)
        {
            GetObjectData(info);
            if (IsSuccess)
            {
                info.AddValue("Value", valueResult.Value);
            }
        }

        public static ResultCommonLogic<E> Deserialize(SerializationInfo info)
        {
            bool isFailure = info.GetBoolean("IsFailure");

            E? error = default;
            var value = info.GetValue("Error", typeof(E));

            if (isFailure && value is not null)
            {
                error = (E)value;
            }

            return new ResultCommonLogic<E>(isFailure, error);
        }
    }
}
