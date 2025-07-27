using UniHub.Domain.Enums;

namespace UniHub.Domain.DTOs
{
    public class UserDTO
    {
        public string? ExternalIdentifier { get; set; }

        public string? Name { get; set; }

        public int? Role { get; set; }
    }
}
