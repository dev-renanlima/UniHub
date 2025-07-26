namespace UniHub.Domain.Entities;

public class Course : BaseEntity
{
    public string? AdminId { get; set; }  
    public User Admin { get; set; } = null!;

    public string? Name { get; set; }

    public string? Code { get; set; }

    public List<CourseMember> Members { get; set; } = new();
}
