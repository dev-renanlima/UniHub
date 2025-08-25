using Mapster;
using System.Reflection;

namespace UniHub.API.Mapper;

public static class MappingConfigurations
{
    public static IServiceCollection RegisterMaps(this IServiceCollection services)
    { 
        services.RegisterLoginMaps();
        services.RegisterUserMaps();
        services.RegisterCourseMaps();
        services.RegisterCourseMemberMaps();
        services.RegisterAssignmentMaps();

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        return services;
    }
}
