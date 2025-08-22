using UniHub.Domain.DTOs;

namespace UniHub.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(UserDTO userDTO);
}
