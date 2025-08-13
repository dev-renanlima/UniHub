using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.User;

namespace UniHub.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(UserDTO userDTO);
        Task<GetUserResponseDTO> GetUserByIdAsync(long? userId);
        Task<GetUserResponseDTO> GetUserByIdentifierAsync(string identifier);
    }
}
