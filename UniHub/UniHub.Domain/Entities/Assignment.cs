namespace UniHub.Domain.Entities;

public class Assignment : BaseEntity
{
    public Guid? CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public Guid? UserId { get; set; }
    public User User { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public List<AssignmentAttachment> AssignmentAttachments { get; set; } = new();
}
