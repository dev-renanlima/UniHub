using Mapster;
using System.Reflection;

namespace UniHub.API.Mapper
{
    public static class MappingConfigurations
    {
        public static IServiceCollection RegisterMaps(this IServiceCollection services)
        { 
            services.RegisterUserMaps();
            services.RegisterCourseMaps();

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
