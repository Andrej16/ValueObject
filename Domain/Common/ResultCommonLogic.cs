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
        
        public ResultCommonLogic(string warning)
        {
            if (string.IsNullOrWhiteSpace(warning))
            {
                throw new ArgumentException(
                    Result.Messages.WarningIsNotProvidedForSuccess, 
                    nameof(warning));
            }

            IsFailure = false;
            _error = default(E);
            _warning = warning;
        }

        public ResultCommonLogic(bool isFailure, E? error)
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
        }

        public void GetObjectData(SerializationInfo info)
        {
            info.AddValue("IsFailure", IsFailure);
            info.AddValue("IsSuccess", IsSuccess);
            if (IsFailure)
            {
                info.AddValue("Error", Error);
            }
        }

        public void GetObjectData<T>(SerializationInfo info, IValue<T> valueResult)
        {
            GetObjectData(info);
            if (IsSuccess)
            {
                info.AddValue("Value", valueResult.Value);
            }
        }

        public static ResultCommonLogic<E> Deserialize(SerializationInfo info)
        {
            bool isFailure = info.GetBoolean("IsFailure");

            E? error = default;
            var value = info.GetValue("Error", typeof(E));

            if (isFailure && value is not null)
            {
                error = (E)value;
            }

            return new ResultCommonLogic<E>(isFailure, error);
        }
    }
}
