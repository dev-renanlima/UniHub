using UniHub.Domain.DTOs;

namespace UniHub.Domain.Interfaces.Services;

public interface IJwtProvider
{
    string GenerateToken(UserDTO userDTO);
}
