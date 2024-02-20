namespace Domain.Common
{
    public class ResultFailureException : Exception
    {
        public string Error { get; } = default!;

        protected ResultFailureException(string error)
            : base(Result.Messages.ValueIsInaccessibleForFailure(error))
        {
            Error = error;
        }
    }
}