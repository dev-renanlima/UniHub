using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.User;

namespace UniHub.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(UserDTO userDTO);
        Task<GetUserResponseDTO> GetUserByClerkIdAsync(string clerkId);
    }
}
