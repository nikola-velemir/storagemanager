namespace StoreManager.Application.Common;

public class Result<T> : Result
{
    public T Value { get; init; }

    internal Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public static implicit operator Result<T>(Error error) => Failure<T>(error);
}

public class Result
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; init; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            throw new ArgumentException("Invalid error");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new Result(false, error);
    public static Result<T> Success<T>(T value) => new Result<T>(value, true, Error.None);
    public static Result<T> Failure<T>(Error error) => new Result<T>(default!, false, error);
}