using System.Text.Json;
using appointly.BLL.Exceptions;
using FluentValidation;

namespace appointly.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";

        var statusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };

        context.Response.StatusCode = statusCode;

        var problemDetails = new
        {
            type = exception switch
            {
                NotFoundException => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ValidationException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            },
            title = exception switch
            {
                NotFoundException => "Resource Not Found",
                ValidationException => "Validation Failed",
                _ => "An error occurred",
            },
            status = statusCode,
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}
