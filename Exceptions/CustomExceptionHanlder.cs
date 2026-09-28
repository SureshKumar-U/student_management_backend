using Microsoft.AspNetCore.Diagnostics; // 👈 CRITICAL: Must include this
namespace CrudAPi.Exceptions;

// 1. Ensure it implements the NATIVE IExceptionHandler interface
public class CustomExceptionHandler : IExceptionHandler
{
    // 2. This method name and signature must match exactly
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        
        if (exception is NotFoundException notFoundEx)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            var response = new
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Resource Not Found",
                message = notFoundEx.Message,
                Instance = httpContext.Request.Path,
                Timestamp = DateTime.UtcNow
            };

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
        if (exception is UnauthorizedException unauthorizedEx)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            var response = new
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized User",
                message = unauthorizedEx.Message,
                Instance = httpContext.Request.Path,
                Timestamp = DateTime.UtcNow
            };

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
        if (exception is UserAlreadyExistsException userAlreadyExistsEx)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            var response = new
            {
                Status = StatusCodes.Status409Conflict,
                Title = "User Already had an account",
                message = userAlreadyExistsEx.Message,
                Instance = httpContext.Request.Path,
                Timestamp = DateTime.UtcNow
            };

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }

        return false;
    }
}
