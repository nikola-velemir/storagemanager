using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Infrastructure.MiddleWare;

public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandler> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;
        object result;
        switch (exception)
        {
            case NotFoundException:
                response.StatusCode = StatusCodes.Status404NotFound;
                result = new { message = exception.Message };
                break;
            default:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                result =  new { message = "An unexpected error occurred." };
                break;
        }

        return context.Response.WriteAsJsonAsync(result);
    }
}