using UniHub.Domain.Enums;

namespace UniHub.Domain.Entities;

public class AssignmentAttachment : BaseEntity
{
    public Guid? AssignmentId { get; set; }
    public Assignment Assignment { get; set; } = null!;

    public Guid? UserId { get; set; }
    public User User { get; set; } = null!;

    public string? Url { get; set; }

    public AssignmentAttachmentType? Type { get; set; }

    public string? Description { get; set; }
}
