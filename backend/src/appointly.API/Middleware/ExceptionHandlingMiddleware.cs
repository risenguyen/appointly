using System.Text.Json;
using appointly.BLL.Exceptions;

namespace appointly.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";

        var statusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        context.Response.StatusCode = statusCode;

        var problemDetails = new
        {
            title = exception switch
            {
                NotFoundException => "Resource Not Found",
                _ => "An error occurred",
            },
            status = statusCode,
            detail = exception.Message,
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}
