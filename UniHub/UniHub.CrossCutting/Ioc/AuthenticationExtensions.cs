using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace UniHub.CrossCutting.Ioc;

public static class AuthenticationExtensions
{
    /// <summary>
    /// Configura a autenticação da API.
    /// </summary>
    public static IServiceCollection AddUniHubAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureClerkAuth(services, configuration);

        return services;
    }

    private static IServiceCollection ConfigureClerkAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Clerk:Authority"];
                options.Audience = configuration["Clerk:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }
}
