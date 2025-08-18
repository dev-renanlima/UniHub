using UniHub.Domain.DTOs;

namespace UniHub.Domain.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(UserDTO userDTO);
}
