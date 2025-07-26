using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.User;

namespace UniHub.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> Create(UserDTO userDTO);
        Task<GetUserResponseDTO> GetUserByClerkId(string clerkId);
    }
}
