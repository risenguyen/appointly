using System.Diagnostics;
using appointly.BLL.Exceptions;
using FluentValidation;
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
        var (statusCode, title, detail, errors) = exception switch
        {
            NotFoundException notFound => (
                StatusCodes.Status404NotFound,
                "Not Found",
                notFound.Message,
                null
            ),
            ValidationException validation => (
                StatusCodes.Status400BadRequest,
                "Validation Error",
                "One or more validation errors occurred.",
                validation
                    .Errors.GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray())
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Server Error",
                "An unexpected internal server error has occurred.",
                null
            ),
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails()
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path,
        };
        problemDetails.Extensions["traceId"] = traceId;
        problemDetails.Extensions["errors"] = errors;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
