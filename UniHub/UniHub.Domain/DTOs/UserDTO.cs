using UniHub.Domain.Enums;

namespace UniHub.Domain.DTOs
{
    public class UserDTO
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public int? Role { get; set; }
    }
}
