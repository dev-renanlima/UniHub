using Mapster;
using UniHub.API.Responses;

namespace UniHub.API.Mapper;

public static class LoginMappingConfiguration
{
    public static void RegisterLoginMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<(GetUserResponse User, string Token), LoginResponse>
                .NewConfig()
                .Map(dest => dest.User, src => src.User)
                .Map(dest => dest.Token, src => src.Token);
    }
}
