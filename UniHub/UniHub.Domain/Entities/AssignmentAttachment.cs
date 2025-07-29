using UniHub.Domain.Enums;

namespace UniHub.Domain.Entities;

public class AssignmentAttachment : BaseEntity
{
    public long? AssignmentId { get; set; }
    public Assignment Assignment { get; set; } = null!;

    public string? Url { get; set; }

    public AssignmentAttachmentType? Type { get; set; }
}
