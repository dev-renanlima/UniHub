using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniHub.Domain.DTOs;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.Application.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
