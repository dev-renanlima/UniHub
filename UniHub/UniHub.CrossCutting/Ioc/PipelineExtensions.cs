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
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("UniHubConnection")));

        return services;
    }

    public static IServiceCollection AddUniHubRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();

        return services;
    }

    public static IServiceCollection AddUniHubServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IAssignmentService, AssignmentService>();

        return services;
    }
}
