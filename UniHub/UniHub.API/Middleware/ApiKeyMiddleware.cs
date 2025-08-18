using System.Net;
using UniHub.Application.Exceptions;
using UniHub.Application.Resources;

namespace UniHub.API.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-API-KEY";
    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration.GetValue<string>("Authentication:ApiKey")!;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            throw new ApiKeyAuthException(nameof(AuthMsg.AUTH0001), AuthMsg.AUTH0001, HttpStatusCode.Unauthorized);
        }

        if (!_apiKey.Equals(extractedApiKey))
        {
            throw new ApiKeyAuthException(nameof(AuthMsg.AUTH0002), AuthMsg.AUTH0002, HttpStatusCode.Unauthorized);
        }

        await _next(context);
    }
}
