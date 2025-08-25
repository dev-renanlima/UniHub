using UniHub.Domain.DTOs;

namespace UniHub.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(UserDTO userDTO);
        Task<UserDTO> GetUserByIdAsync(Guid? userId);
        Task<UserDTO> GetUserByIdentifierAsync(string identifier);
    }
}
