using Api.Services.Abstractions;

namespace Api.Services.Implementations
{
    public class OperationAuthorizationContract : IOperationContract
    {
        public string Name { get; set; } = default!;

        public override string ToString()
        {
            return $"{nameof(OperationAuthorizationContract)}:Name={Name}";
        }
    }
}
