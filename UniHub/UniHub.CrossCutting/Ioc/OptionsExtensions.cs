using Microsoft.Extensions.DependencyInjection;
using UniHub.CrossCutting.OptionsSetup;

namespace UniHub.CrossCutting.Ioc;

public static class OptionsExtensions
{
    public static IServiceCollection AddUniHubOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<SecurityOptionSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<SwaggerGenOptionsSetup>();

        return services;
    }
}
