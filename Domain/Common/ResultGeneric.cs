using System.Runtime.Serialization;

namespace Domain.Common
{
    [Serializable]
    public readonly struct Result<T, E> : IResult<T, E>, IResult, IValue<T>, IError<E>, ISerializable
        where E : IError
    {
        private readonly ResultCommonLogic<E> _logic;

        private readonly T? _value;

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        public bool HasWarning => _logic.HasWarning;

        public E Error => _logic.Error;

        public string? Warning => _logic.Warning;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                {
                    throw new ResultFailureGenericException<E>(Error);
                }

                return _value!;
            }
        }

        public Result(bool isFailure, E? error, T? value, string? warning)
        {
            _logic = new ResultCommonLogic<E>(isFailure, error, warning);
            _value = value;
        }

        public override string ToString()
        {
            if (!IsSuccess)
            {
                return $"Failure({Error})";
            }

            return $"Success({Value})";
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _logic.GetObjectData(info, this);
        }

        public static implicit operator Result<T, E>(T value)
        {
            if (value is IResult<T, E> result)
            {
                E? error = result.IsFailure ? result.Error : default;
                T? value2 = result.IsSuccess ? result.Value : default;
                var warning = result.HasWarning ? result.Warning : default;

                return new Result<T, E>(result.IsFailure, error, value2, warning);
            }

            return Result.Success<T, E>(value);
        }

        public static implicit operator Result<T, E>((T Value, string Warning) valueWithWarning)
        {
            return Result.Warning<T, E>(valueWithWarning.Value, valueWithWarning.Warning);
        }

        public static implicit operator Result<T, E>(E error)
        {
            if (error is IResult<T, E> result)
            {
                E? error2 = result.IsFailure ? result.Error : default;
                T? value = result.IsSuccess ? result.Value : default;

                return new Result<T, E>(result.IsFailure, error2, value, default);
            }

            return Result.Failure<T, E>(error);
        }
    }
}