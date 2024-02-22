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

        public bool HasWarning => _logic.HasWarning;

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

        public static Result<T, E> Success<T, E>(T value) where E : IError
        {
            var result = new Result<T, E>(isFailure: false, default, value, default);

            return result;
        }

        public static Result<T, E> Warning<T, E>(T value, string warning) where E : IError
        {
            var result = new Result<T, E>(isFailure: false, default, value, warning);

            return result;
        }

        public static Result<T, E> Failure<T, E>(E error) where E : IError
        {
            var result = new Result<T, E>(isFailure: true, error, default, default);

            return result;
        }
    }
}
