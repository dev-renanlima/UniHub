using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"UniHub.API",
                Version = "v1",
                Description = "Documentação da API UniHub",
                Contact = new OpenApiContact
                {
                     Name = "Renan Lima",
                     Email = "tech.renanlima@gmail.com",
                     Url = new Uri("https://www.linkedin.com/in/dev-renanlima/")
                }
            });

            var xmlFile = "UniHub.API.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}