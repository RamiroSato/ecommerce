using System.Net.Http;
using Amazon.CognitoIdentityProvider.Model;

namespace Ecommerce.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (NotAuthorizedException ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (UsernameExistsException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (Exceptions.ResourceNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }
}
