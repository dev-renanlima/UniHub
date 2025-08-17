using Mapster;
using UniHub.API.Requests.User;
using UniHub.API.Responses.User;
using UniHub.Domain.DTOs;
using UniHub.Domain.Entities;
using UniHub.Domain.Enums;

namespace UniHub.API.Mapper;

public static class UserMappingConfigurations
{
    public static void RegisterUserMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateUserRequest, UserDTO>
                .NewConfig()
                .Map(dest => dest.Status, src => UserStatus.ACTIVE);

        TypeAdapterConfig<UserDTO, User>
                .NewConfig()
                .Ignore(dest => dest.Id) 
                .Ignore(dest => dest.InternalIdentifier); 

        TypeAdapterConfig<User, UserDTO>
                .NewConfig();

        TypeAdapterConfig<UserDTO, CreateUserResponse>
                .NewConfig();

        TypeAdapterConfig<UserDTO, GetUserResponse>
                .NewConfig();
    }
}
