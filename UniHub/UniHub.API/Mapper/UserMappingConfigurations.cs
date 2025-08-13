using Mapster;
using UniHub.API.Model.User.CreateUser;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.User;
using UniHub.Domain.Entities;
using UniHub.Domain.Enums;
using UniHub.Domain.Extensions;

namespace UniHub.API.Mapper
{
    public static class UserMappingConfigurations
    {
        public static void RegisterUserMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<CreateUserModel, UserDTO>
                    .NewConfig()
                    .Map(dest => dest.InternalIdentifier, src => UUIDExtensions.GenerateNanoId())
                    .Map(dest => dest.ExternalIdentifier, src => src.Body!.ExternalIdentifier)
                    .Map(dest => dest.Name, src => src.Body!.Name)
                    .Map(dest => dest.Email, src => src.Body!.Email)
                    .Map(dest => dest.Role, src => src.Body!.Role)
                    .Map(dest => dest.Status, src => UserStatus.ACTIVE)
                    .Map(dest => dest.ProfileUrl, src => src.Body!.ProfileUrl);

            TypeAdapterConfig<UserDTO, User>
                    .NewConfig();

            TypeAdapterConfig<User, CreateUserResponseDTO>
                    .NewConfig();

            TypeAdapterConfig<User, GetUserResponseDTO>
                    .NewConfig();
        }
    }
}
