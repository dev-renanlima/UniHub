using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UniHub.CrossCutting.Options;

public class SwaggerGenSetupOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerGenSetupOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var desc in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, new OpenApiInfo
            {
                Title = "UniHub.API",
                Version = desc.ApiVersion.ToString(),
                Description = "Documentação da API UniHub",
                Contact = new OpenApiContact
                {
                    Name = "Renan Lima",
                    Email = "tech.renanlima@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/dev-renanlima/")
                }
            });
        }
    }
}