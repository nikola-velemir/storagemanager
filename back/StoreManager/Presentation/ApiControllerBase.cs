using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Common;

namespace StoreManager.Presentation;

public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult FromResult(Result result, Func<IActionResult>? onSuccess = null)
    {
        if (result.IsSuccess)
            return onSuccess?.Invoke() ?? Ok();
        return StatusCode(result.Error.StatusCode, new { result.Error.Name, result.Error.Description });
    }

    protected IActionResult FromResult<T>(Result<T> result, Func<T, IActionResult>? onSuccess = null)
    {
        if (result.IsSuccess)
            return onSuccess?.Invoke(result.Value) ?? Ok(result.Value);
        return StatusCode(result.Error.StatusCode, new { result.Error.Name, result.Error.Description });
    }
}