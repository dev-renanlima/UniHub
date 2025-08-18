using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using UniHub.CrossCutting.Options;

namespace UniHub.CrossCutting.Ioc;

public static class SwaggerExtensions
{
    /// <summary>
    /// Configura o Swagger da API.
    /// </summary>
    public static IServiceCollection AddUniHubSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SchemaFilter<EnumSchemaFilterOptions>();

            var xmlFile = "UniHub.API.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.ConfigureOptions<SwaggerGenSetupOptions>();

        return services;
    }
}
