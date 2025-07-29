namespace UniHub.Domain.DTOs;

public class AssignmentDTO
{
    public string? Assignment { get; set; }

    public long? AssignmentId { get; set; }

    public string? UserIdentification { get; set; }

    public long? UserId { get; set; }

    public string? CourseCode { get; set; }

    public long? CourseId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public List<AssignmentAttachmentDTO>? AssignmentAttachments { get; set; } = new();
}
