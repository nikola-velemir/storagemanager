using StoreManager.Application.Common;

namespace StoreManager.Application.Auth.Errors;

public static class LoginErrors
{
    public static Error IncorrectCredentialsError => new Error("IncorrectCredentials",400,"Incorrect credentials");
}