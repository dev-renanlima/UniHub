using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniHub.Application.Services;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Domain.Interfaces.Services;
using UniHub.Infrastructure.Context;
using UniHub.Infrastructure.Repositories;

namespace UniHub.CrossCutting.Ioc;

public static class PipelineExtensions
{
    public static IServiceCollection AddUniHubContext(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("UniHubConnection")));

        return services;
    }

    public static IServiceCollection AddUniHubRepositories(this IServiceCollection services)
    {
        // Unit of Work e Repositórios
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        return services;
    }

    public static IServiceCollection AddUniHubServices(this IServiceCollection services)
    {        
        // Serviços de domínio
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICourseService, CourseService>();

        return services;
    }
}
