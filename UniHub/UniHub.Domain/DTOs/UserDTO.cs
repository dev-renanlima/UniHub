using UniHub.Domain.Enums;

namespace UniHub.Domain.DTOs
{
    public class UserDTO
    {
        public required string InternalIdentifier { get; set; }

        public required string ExternalIdentifier { get; set; }

        public required string Name { get; set; } 
        
        public required string Email { get; set; }

        public UserRole Role { get; set; }

        public UserStatus Status { get; set; }

        public string? ProfileUrl { get; set; }
    }
}
