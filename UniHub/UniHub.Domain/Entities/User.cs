using UniHub.Domain.Enums;

namespace UniHub.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public UserRole Role { get; set; }
    }
}
