using StoreManager.Application.Common;

namespace StoreManager.Application.Auth.Errors;

public static class RefreshTokenErrors
{
    public static Error InvalidTokenError => new Error("InvalidToken",400,"Invalid token");
    public static Error TokenExpiredError => new Error("TokenExpired",400,"Token has expired");
}