namespace Domain.Common
{
    public sealed class Error : IError
    {
        private const string Separator = "||";
        private const string DefaultCode = "Default code";

        public string Code { get; }

        public string Message { get; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString() => $"{Code}{Separator}{Message}";

        public static Error Deserialize(string? serialized)
        {
            if (string.IsNullOrEmpty(serialized))
                return Errors.General.ValueIsRequired();

            var data = serialized.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 2)
                return new Error(DefaultCode, serialized);

            return new Error(data[0], data[1]);
        }
    }
}
