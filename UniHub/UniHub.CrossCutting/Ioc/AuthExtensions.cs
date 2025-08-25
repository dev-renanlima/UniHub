using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using UniHub.CrossCutting.Auth;

namespace UniHub.CrossCutting.Ioc;

public static class AuthExtensions
{
    /// <summary>
    /// Configura a autenticação e autorização da API.
    /// </summary>
    public static IServiceCollection AddUniHubAuth(this IServiceCollection services)
    {
        // JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(); // JwtBearerOptionsSetup

        // Authorization
        services.AddAuthorization(options =>
        {
            //options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //    .AddRequirements(new ApiKeyRequirement())
            //    .Build();

            options.AddPolicy(AuthPolicies.ApiKey, policy =>
            {
                policy.AddRequirements(new ApiKeyRequirement());
            });

            options.AddPolicy(AuthPolicies.ApiKeyAndJwt, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.AddRequirements(new ApiKeyRequirement());
            });
        });

        // Handlers
        services.AddSingleton<IAuthorizationHandler, ApiKeyAuthorizationHandler>();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();

        return services;
    }
}
