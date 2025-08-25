using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UniHub.CrossCutting.OptionsSetup;

public class SwaggerGenOptionsSetup : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerGenOptionsSetup(IApiVersionDescriptionProvider provider)
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

        // 🔹 Definição da API Key
        options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            Description = "Chave de acesso via cabeçalho: X-API-KEY",
            Type = SecuritySchemeType.ApiKey,
            Name = "X-API-KEY",
            In = ParameterLocation.Header,
            Scheme = "ApiKeyScheme"
        });

        // 🔹 Definição do Bearer (JWT)
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Autenticação JWT via Bearer",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

        // 🔹 Requisitos de Segurança globais 
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    new List<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            }
        );
    }
}