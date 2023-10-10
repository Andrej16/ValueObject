using Domain.Common;

namespace Api.Common
{
    public class Envelope
    {
        public bool IsSuccess { get; }

        public object? Result { get; }

        public string? ErrorCode { get; }

        public string? ErrorMessage { get; }

        private Envelope(object? result, Error? error)
        {
            Result = result;
            ErrorCode = error?.Code;
            ErrorMessage = error?.Message;
            IsSuccess = string.IsNullOrEmpty(ErrorCode);
        }

        public static Envelope Ok(object? result = null) => new(result, null);

        public static Envelope Error(Error error) => new(null, error);
    }

    public interface IEnvelope
    {
        bool IsSuccess { get; }

        string? ErrorCode { get; }

        string? ErrorMessage { get; }
    }

    public class Envelope<T> : IEnvelope where T : class
    {
        public bool IsSuccess { get; }

        public T? Result { get; }

        public string? ErrorCode { get; }

        public string? ErrorMessage { get; }

        private Envelope(T? result, Error? error)
        {
            Result = result;
            ErrorCode = error?.Code;
            ErrorMessage = error?.Message;
            IsSuccess = string.IsNullOrEmpty(ErrorCode);
        }

        public static Envelope<T> Ok(T? result = null) => new(result, null);

        public static Envelope<T> Error(Error error) => new(null, error);

        public static Envelope<T> FromResult(Result<T, Error> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            return Error(result.Error);
        }
    }
}