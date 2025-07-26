using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using UniHub.API.Resources;

namespace UniHub.API.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {

            _logger.LogError(e, e.Message);

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = nameof(ApiMsg.API0001),
                Detail = ApiMsg.API0001
            };

            var jsonResponse = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(jsonResponse);

            context.Response.ContentType = "application/json";
        }
    }
}
