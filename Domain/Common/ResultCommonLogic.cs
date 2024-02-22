using System.Runtime.Serialization;

namespace Domain.Common
{
    internal readonly struct ResultCommonLogic<E>
    {
        private readonly string? _warning;

        private readonly E? _error;

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        public bool HasWarning => IsSuccess && _warning != null;

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

        public string? Warning
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException();
                }

                return _warning;
            }
        }
        
        public ResultCommonLogic(bool isFailure, E? error, string? warning)
        {
            if (isFailure)
            {
                if (error == null || (error is string && error.Equals(string.Empty)))
                {
                    throw new ArgumentNullException(
                        nameof(error), 
                        Result.Messages.ErrorObjectIsNotProvidedForFailure);
                }
            }
            else if (!EqualityComparer<E>.Default.Equals(error, default))
            {
                throw new ArgumentException(
                    Result.Messages.ErrorObjectIsProvidedForSuccess, 
                    nameof(error));
            }

            IsFailure = isFailure;
            _error = error;
            _warning = warning;
        }

        public void GetObjectData(SerializationInfo info)
        {
            info.AddValue("IsFailure", IsFailure);
            info.AddValue("IsSuccess", IsSuccess);
            info.AddValue("HasWarning", HasWarning);

            if (IsFailure)
            {
                info.AddValue("Error", Error);
            }

            if (HasWarning)
            {
                info.AddValue("Warning", Warning);
            }
        }

        public void GetObjectData<T>(SerializationInfo info, IValue<T> valueResult)
        {
            GetObjectData(info);
            if (IsSuccess)
            {
                info.AddValue("Value", valueResult.Value);
                info.AddValue("Warning", valueResult.Warning);
            }
        }

        public static ResultCommonLogic<E> Deserialize(SerializationInfo info)
        {
            bool isFailure = info.GetBoolean("IsFailure");

            E? error = default;
            var value = info.GetValue("Error", typeof(E));
            var warning = info.GetString("Warning");

            if (isFailure && value is not null)
            {
                error = (E)value;
            }

            return new ResultCommonLogic<E>(isFailure, error, warning);
        }
    }
}
