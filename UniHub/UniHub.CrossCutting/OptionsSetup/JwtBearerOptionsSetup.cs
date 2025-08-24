using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UniHub.Application.Resources;
using UniHub.Domain.Options;

namespace UniHub.CrossCutting.OptionsSetup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly SecurityOptions _security;

    public JwtBearerOptionsSetup(IOptions<SecurityOptions> security)
        => _security = security.Value;

    public void Configure(JwtBearerOptions options)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_security.SecretKey!));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = _security.Issuer,
            ValidateAudience = true,
            ValidAudience = _security.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception is SecurityTokenExpiredException)
                {
                    context.HttpContext.Items["AuthErrorCode"] = nameof(AuthMsg.ExpiredJwtToken);
                    context.HttpContext.Items["AuthErrorDetail"] = AuthMsg.ExpiredJwtToken;
                }
                else
                {
                    context.HttpContext.Items["AuthErrorCode"] = nameof(AuthMsg.InvalidJwtToken);
                    context.HttpContext.Items["AuthErrorDetail"] = AuthMsg.InvalidJwtToken;
                }
                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                context.HandleResponse(); 
                return Task.CompletedTask;
            },

            OnForbidden = context =>
            {
                context.HttpContext.Items["AuthErrorCode"] = nameof(AuthMsg.BlockedResource);
                context.HttpContext.Items["AuthErrorDetail"] = AuthMsg.BlockedResource;
                return Task.CompletedTask;
            }
        };
    }
}
