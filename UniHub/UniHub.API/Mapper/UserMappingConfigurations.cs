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
                    .Map(dest => dest.ExternalIdentifier, src => src.Body!.ExternalIdentifier)
                    .Map(dest => dest.Name, src => src.Body!.Name)
                    .Map(dest => dest.Role, src => src.Body!.Role);

            TypeAdapterConfig<UserDTO, User>
                    .NewConfig();

            TypeAdapterConfig<User, CreateUserResponseDTO>
                    .NewConfig();

            TypeAdapterConfig<User, GetUserResponseDTO>
                    .NewConfig();
        }
    }
}
