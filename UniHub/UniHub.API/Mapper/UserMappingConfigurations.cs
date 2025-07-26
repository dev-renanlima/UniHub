using Mapster;
using UniHub.API.Model.User.CreateUser;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.User;
using UniHub.Domain.Entities;

namespace UniHub.API.Mapper
{
    public static class UserMappingConfigurations
    {
        public static void RegisterUserMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<CreateUserModel, UserDTO>
                    .NewConfig()
                    .Map(dest => dest.ClerkId, src => src.Body!.ClerkId)
                    .Map(dest => dest.Name, src => src.Body!.Name)
                    .Map(dest => dest.Role, src => src.Body!.Role);

            TypeAdapterConfig<UserDTO, User>
                    .NewConfig()
                    .Map(dest => dest.ClerkId, src => src.ClerkId)
                    .Map(dest => dest.Name, src => src.Name) 
                    .Map(dest => dest.Role, src => src.Role);

            TypeAdapterConfig<User, CreateUserResponseDTO>
                    .NewConfig()
                    .Map(dest => dest.ClerkId, src => src.ClerkId)
                    .Map(dest => dest.Name, src => src.Name)
                    .Map(dest => dest.Role, src => src.Role);

            TypeAdapterConfig<User, GetUserResponseDTO>
                    .NewConfig()
                    .Map(dest => dest.ClerkId, src => src.ClerkId)
                    .Map(dest => dest.Name, src => src.Name)
                    .Map(dest => dest.Role, src => src.Role);
        }
    }
}
