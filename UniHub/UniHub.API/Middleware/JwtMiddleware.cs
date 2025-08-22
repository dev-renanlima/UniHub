using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using UniHub.Application.Exceptions;
using UniHub.Application.Resources;
using UniHub.Infrastructure.Authentication;

namespace UniHub.API.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SecurityOptions _securityOptions;

    public JwtMiddleware(RequestDelegate next, IOptions<SecurityOptions> securityOptions)
    {
        _next = next;
        _securityOptions = securityOptions.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            string? authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                await _next(context);
                return;
            }

            string token = authHeader["Bearer ".Length..].Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityOptions.SecretKey!));

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,

                ValidateIssuer = true,
                ValidIssuer = _securityOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = _securityOptions.Audience,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);

            context.User = principal;
        }
        catch (SecurityTokenExpiredException)
        {
            throw new JwtAuthException(nameof(AuthMsg.AUTH0004), AuthMsg.AUTH0004, HttpStatusCode.Unauthorized);
        }
        catch (Exception)
        {
            throw new JwtAuthException(nameof(AuthMsg.AUTH0003), AuthMsg.AUTH0003, HttpStatusCode.Unauthorized);
        }

        await _next(context);
    }
}
