using UniHub.Domain.DTOs.Responses;
using UniHub.Domain.Entities;

namespace UniHub.Domain.DTOs;

public class AssignmentDTO
{
    public string? CourseCode { get; set; }

    public long? CourseId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public List<AssignmentAttachmentDTO>? AssignmentAttachments { get; set; }
}
