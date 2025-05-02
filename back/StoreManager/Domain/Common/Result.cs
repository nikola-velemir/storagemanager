namespace StoreManager.Domain.Common;

public class Result<T> : Result
{
    public T Value { get; init; }

    public Result(T value, bool isSuccess, DomainError domainError) : base(isSuccess, domainError)
    {
        Value = value;
    }

    public static implicit operator Result<T>(DomainError error) => Failure<T>(error);
}

public class Result
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public DomainError DomainError { get; init; }

    protected Result(bool isSuccess, DomainError domainError)
    {
        if (isSuccess && domainError != DomainError.None || !isSuccess && domainError == DomainError.None)
            throw new ArgumentException($"{domainError} is not a valid error");
        IsSuccess = isSuccess;
        DomainError = domainError;
    }
    public static implicit operator Result(DomainError error) => Failure(error);

    public static Result Success() => new Result(true, DomainError.None);
    public static Result Failure(DomainError domainError) => new Result(false, domainError);
    public static Result<T> Success<T>(T value) => new Result<T>(value, true, DomainError.None);
    public static Result<T> Failure<T>(DomainError error) => new Result<T>(default!, false, error);
}