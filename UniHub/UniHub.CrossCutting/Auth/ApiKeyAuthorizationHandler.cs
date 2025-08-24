using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using UniHub.Application.Resources;
using UniHub.Domain.Options;

namespace UniHub.CrossCutting.Auth;

public sealed class ApiKeyAuthorizationHandler : AuthorizationHandler<ApiKeyRequirement>
{
    private readonly SecurityOptions _security;

    public ApiKeyAuthorizationHandler(IOptions<SecurityOptions> security)
        => _security = security.Value;

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
    {
        if (context.Resource is not HttpContext http)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        const string headerName = "X-API-KEY";

        if (!http.Request.Headers.TryGetValue(headerName, out var provided) || string.IsNullOrWhiteSpace(provided))
        {
            http.Items["AuthErrorCode"] = nameof(AuthMsg.MissingApiKey);
            http.Items["AuthErrorDetail"] = AuthMsg.MissingApiKey;
            context.Fail();
            return Task.CompletedTask;
        }

        if (!string.Equals(_security.ApiKey, provided.ToString(), StringComparison.Ordinal))
        {
            http.Items["AuthErrorCode"] = nameof(AuthMsg.InvalidApiKey);
            http.Items["AuthErrorDetail"] = AuthMsg.InvalidApiKey;
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
