namespace UniHub.Domain.Entities;

public class Course : BaseEntity
{
    public Guid? UserId { get; set; }  
    public User User { get; set; } = null!;

    public string? Name { get; set; }

    public string? Code { get; set; }

    public List<CourseMember> CourseMembers { get; set; } = new();
}
