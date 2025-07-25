using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniHub.Domain.DTOs.Responses.User
{
    public record CreateUserResponseDTO(
        string? Name,
        string? Email,
        string? Role
    );
}
