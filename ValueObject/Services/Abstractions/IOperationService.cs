namespace Api.Services.Abstractions
{
    public interface IOperationService
    {
        Task<OperationResult> ExecuteAsync(IEnumerable<IOperationContract> requirements);
    }
}
