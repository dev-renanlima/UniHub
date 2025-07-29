namespace UniHub.Domain.DTOs.Responses.Assignment;

public record CreateAssignmentResponseDTO
(
    string? CourseName,
    string? Title,
    string? Description,
    DateTime? ExpirationDate,
    List<AttachmentDTO>? AssignmentAttachments
);
