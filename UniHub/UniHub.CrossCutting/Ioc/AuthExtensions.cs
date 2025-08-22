using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace UniHub.CrossCutting.Ioc;

public static class AuthExtensions
{
    /// <summary>
    /// Configura a autenticação e autorização da API.
    /// </summary>
    public static IServiceCollection AddUniHubAuth(this IServiceCollection services)
    {
        // Configura o sistema de autenticação
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        // Configura as políticas de autorização
        services.AddAuthorization();

        return services;
    }
}
