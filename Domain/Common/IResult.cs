﻿namespace Domain.Common
{
    public interface IResult
    {
        bool IsFailure { get; }

        bool HasWarning { get; }

        bool IsSuccess { get; }
    }

    public interface IError
    {
        string Code { get; }

        string Message { get; }
    }

    public interface IValue<out T>
    {
        T Value { get; }

        string? Warning { get; }
    }

    public interface IError<out E>
    {
        E Error { get; }
    }

    public interface IResult<out T, out E> : IResult, IValue<T>, IError<E>
    {

    }
}
