using System.Runtime.Serialization;

namespace Domain.Common
{
    [Serializable]
    public readonly struct Result : IResult, ISerializable
    {
        //ToDo: Move raw strings to resources
        internal static class Messages
        {
            public static readonly string ValueIsInaccessibleForFail = "You attempted to access the Value property for a unsuccessful result. A unsuccessful result has no Value.";

            public static readonly string ErrorObjectIsNotProvidedForFailure = "You attempted to create a failure result, which must have an error, but a null error object (or empty string) was passed to the constructor.";

            public static readonly string WarningIsNotProvidedForSuccess = "You attempted to create a success result, which must have a warning, but a null or empty string warning was passed to the constructor."; 

            public static readonly string ErrorObjectIsProvidedForSuccess = "You attempted to create a success result, which cannot have an error, but a non-null error object was passed to the constructor.";

            public static string ValueIsInaccessibleForFailure(string error)
            {
                return "You attempted to access the Value property for a failed result. A failed result has no Value. The error was: " + error;
            }
        }

        private readonly ResultCommonLogic<string> _logic;

        public readonly bool IsFailure => _logic.IsFailure;

        public readonly bool IsSuccess => _logic.IsSuccess;

        public readonly string Error => _logic.Error;

        private Result(SerializationInfo info, StreamingContext context)
        {
            _logic = ResultCommonLogic<string>.Deserialize(info);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _logic.GetObjectData(info);
        }

        public static Result<T, E> Success<T, E>(T value)
        {
            return new Result<T, E>(isFailure: false, default(E), value);
        }

        public static Result<T, E> Warning<T, E>(T value, string warning)
        {
            return new Result<T, E>(value, warning);
        }

        public static Result<T, E> Failure<T, E>(E error)
        {
            return new Result<T, E>(isFailure: true, error, default(T));
        }
    }
}
