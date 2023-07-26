using System.Runtime.Serialization;

namespace Domain.Common
{
    [Serializable]
    public struct Result : IResult, ISerializable
    {
        private readonly ResultCommonLogic<string> _logic;

        public readonly bool IsFailure => _logic.IsFailure;

        public readonly bool IsSuccess => _logic.IsSuccess;

        public readonly string Error => _logic.Error;

        private Result(SerializationInfo info, StreamingContext context)
        {
            _logic = ResultCommonLogic<string>.Deserialize(info);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _logic.GetObjectData(info);
        }

        public static Result<T, E> Success<T, E>(T value)
        {
            return new Result<T, E>(isFailure: false, default(E), value);
        }

        public static Result<T, E> Failure<T, E>(E error)
        {
            return new Result<T, E>(isFailure: true, error, default(T));
        }
    }

    [Serializable]
    public struct Result<T, E> : IResult<T, E>, IResult, IValue<T>, IError<E>, ISerializable
    {
        private readonly ResultCommonLogic<E> _logic;

        private readonly T? _value;

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        public E Error => _logic.Error;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                {
                    throw new Exception(ResultCommonLogic<string>.Messages.ValueIsInaccessibleForFail);
                }

                return _value!;
            }
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

        internal Result(bool isFailure, E? error, T? value)
        {
            _logic = new ResultCommonLogic<E>(isFailure, error);
            _value = value;
        }

        public static implicit operator Result<T, E>(T value)
        {
            if (value is IResult<T, E> result)
            {
                E? error = result.IsFailure ? result.Error : default;
                T? value2 = result.IsSuccess ? result.Value : default;

                return new Result<T, E>(result.IsFailure, error, value2);
            }

            return Result.Success<T, E>(value);
        }

        public static implicit operator Result<T, E>(E error)
        {
            if (error is IResult<T, E> result)
            {
                E? error2 = result.IsFailure ? result.Error : default;
                T? value = result.IsSuccess ? result.Value : default;

                return new Result<T, E>(result.IsFailure, error2, value);
            }

            return Result.Failure<T, E>(error);
        }
    }
}
