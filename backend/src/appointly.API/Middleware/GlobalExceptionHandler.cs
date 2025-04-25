using System.Diagnostics;
using appointly.BLL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace appointly.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        var (type, statusCode, title, detail) = exception switch
        {
            NotFoundException notFound => (
                "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                StatusCodes.Status404NotFound,
                "Not Found",
                notFound.Message
            ),
            _ => (
                "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                StatusCodes.Status500InternalServerError,
                "Server Error",
                "An unexpected internal server error has occurred."
            ),
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails()
        {
            Type = type,
            Status = statusCode,
            Title = title,
            Detail = detail,
        };
        problemDetails.Extensions["traceId"] = traceId;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
