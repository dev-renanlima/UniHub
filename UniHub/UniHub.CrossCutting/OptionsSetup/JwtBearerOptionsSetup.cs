using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UniHub.Application.Resources;
using UniHub.Domain.Options;

namespace UniHub.CrossCutting.OptionsSetup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{ 
    private readonly SecurityOptions _securityOptions;

    public JwtBearerOptionsSetup(IOptions<SecurityOptions> securityOptions)
    {
        _securityOptions = securityOptions.Value;
    }

    public void Configure(JwtBearerOptions options) 
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityOptions.SecretKey!));

        options.TokenValidationParameters = new TokenValidationParameters
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

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Falha na autenticação",
                    Detail = AuthMsg.InvalidJwtToken
                };

                return context.Response.WriteAsJsonAsync(problem);
            },
            OnChallenge = context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Não autorizado",
                    Detail = AuthMsg.ExpiredJwtToken
                };

                return context.Response.WriteAsJsonAsync(problem);
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Acesso negado",
                    Detail = AuthMsg.BlockedResource
                };

                return context.Response.WriteAsJsonAsync(problem);
            }
        };
    }
}
