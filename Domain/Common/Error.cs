namespace Domain.Common
{
    public sealed class Error : IError
    {
        private const string _separator = "||";
        private const string _defaultCode = "Default code";

        public string Code { get; }

        public string Message { get; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString() => $"{Code}{_separator}{Message}";

        public static Error Deserialize(string? serialized)
        {
            if (string.IsNullOrEmpty(serialized))
                return Errors.General.ValueIsRequired();

            var data = serialized.Split(new[] { _separator }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 2)
                return new Error(_defaultCode, serialized);

            return new Error(data[0], data[1]);
        }
    }
}
