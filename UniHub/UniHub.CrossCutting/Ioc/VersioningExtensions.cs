using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniHub.CrossCutting.Ioc;

public static class VersioningExtensions
{
    /// <summary>
    /// Configura API Versioning para o projeto.
    /// </summary>
    public static IServiceCollection AddUniHubApiVersioning(this IServiceCollection services)
    {
        // Configura versionamento da API
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;

            options.AssumeDefaultVersionWhenUnspecified = true;

            options.DefaultApiVersion = new ApiVersion(1, 0);

            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
        
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
