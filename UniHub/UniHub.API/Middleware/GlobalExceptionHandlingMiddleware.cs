using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using UniHub.API.Model;
using UniHub.API.Resources;
using UniHub.Application.Exceptions;

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
        catch (HttpRequestFailException ex)
        {
            _logger.LogWarning(ex, ex.Message);

            ProblemResponse problem = new()
            {
                StatusCode = (int)ex.StatusCode,
                ErrorCode = ex.ErrorCode,
                Detail = ex.Message,
                CorrelationId = ex.CorrelationId
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ex.StatusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, ex.Message);

            ProblemResponse problem = new()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorCode = nameof(ApiMsg.API0001),
                Detail = ApiMsg.API0001
            };

            var jsonResponse = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
