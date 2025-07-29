using UniHub.Domain.Enums;

namespace UniHub.Domain.DTOs;

public class AssignmentAttachmentDTO
{
    public long? AssignmentId { get; set; }

    public string? Url { get; set; }

    public AssignmentAttachmentType? Type { get; set; }
}
