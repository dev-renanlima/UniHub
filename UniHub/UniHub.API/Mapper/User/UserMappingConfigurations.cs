using Mapster;
using UniHub.API.Model.User.CreateUser;
using UniHub.Domain.DTOs;

namespace UniHub.API.Mapper.User
{
    public static class UserMappingConfigurations
    {
        public static void RegisterRecurrenceMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<CreateUserBody, UserDTO>
                    .NewConfig()
                    .Map(dest => dest.Name, src => src.Name)
                    .Map(dest => dest.Email, src => src.Email)
                    .Map(dest => dest.Role, src => src.Role);
        }
    }
}
