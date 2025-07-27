using UniHub.Domain.Enums;

namespace UniHub.Domain.Entities;

public class User : BaseEntity
{
    public string? ExternalIdentifier { get; set; }

    public string? Name { get; set; }

    public UserRole Role { get; set; }

    public List<CourseMember> Courses { get; set; } = new();
}
