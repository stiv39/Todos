using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Todos.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public GlobalExceptionHandlingMiddleware(
    ILogger<GlobalExceptionHandlingMiddleware> logger) =>
    _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            var problem = ProblemDetails(e);

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }

    private ProblemDetails ProblemDetails(Exception exception) =>
    exception switch
    {
        ValidationException valEx =>
            new ValidationProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Validation error",
                Title = "Validation error",
                Detail = "An validation error has occured",
                Extensions = { ["errorCodes"] = valEx.Errors.Select(err => err.ErrorMessage) }
            },
        _ => new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = "Server error",
            Title = "Server error",
            Detail = "An internal server error has occurred"
        }
    };
}
