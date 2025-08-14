using UniHub.Domain.Enums;

namespace UniHub.Domain.DTOs;

public class 
    AssignmentAttachmentDTO
{
    public Guid? AssignmentId { get; set; }

    public Guid? UserId { get; set; }

    public string? Url { get; set; }

    public AssignmentAttachmentType? Type { get; set; }
}
