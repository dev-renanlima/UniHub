using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Net;
using UniHub.Application.Resources;
using UniHub.Domain.Exceptions;

namespace UniHub.CrossCutting.Auth;

public sealed class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public Task HandleAsync(RequestDelegate next, HttpContext httpContext, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Succeeded)
        {
            return next(httpContext);
        }

        var statusCode = authorizeResult.Forbidden
            ? StatusCodes.Status403Forbidden
            : StatusCodes.Status401Unauthorized;

        var errorCode = httpContext.Items["AuthErrorCode"] as string;
        var detail = httpContext.Items["AuthErrorDetail"] as string;

        if (string.IsNullOrEmpty(errorCode) || string.IsNullOrEmpty(detail))
        {
            if (statusCode == StatusCodes.Status401Unauthorized)
            {
                if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    errorCode = nameof(AuthMsg.MissingJwtToken);
                    detail = AuthMsg.MissingJwtToken;
                }
                else
                {
                    errorCode = nameof(AuthMsg.InvalidJwtToken);
                    detail = AuthMsg.InvalidJwtToken;
                }
            }
            else
            {
                errorCode = nameof(AuthMsg.BlockedResource);
                detail = AuthMsg.BlockedResource;
            }
        }

        throw new AuthorizationException(errorCode, detail, (HttpStatusCode)statusCode);
    }
}
