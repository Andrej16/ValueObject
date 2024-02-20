namespace Domain.Common
{
    public class ResultFailureGenericException<E> : ResultFailureException
    {
        public new E Error { get; } = default!;

        public ResultFailureGenericException(E error)
            : base(Result.Messages.ValueIsInaccessibleForFailure(error!.ToString()!))
        {
            Error = error;
        }
    }
}