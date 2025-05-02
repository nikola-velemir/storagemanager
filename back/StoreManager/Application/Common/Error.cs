namespace StoreManager.Application.Common;

public sealed record Error(string Name,int StatusCode, string? Description = null)
{
    public static readonly Error None = new Error(string.Empty,0);
    public static implicit operator Result(Error error) => Result.Failure(error);
}