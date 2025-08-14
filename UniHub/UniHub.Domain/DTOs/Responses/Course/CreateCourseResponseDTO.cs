namespace UniHub.Domain.DTOs.Responses.Course;

public record CreateCourseResponseDTO
(
    string? InternalIdentifier,
    string? UserIdentifier,
    string? Name,
    string? Code,
    DateTime? CreationDate
);
