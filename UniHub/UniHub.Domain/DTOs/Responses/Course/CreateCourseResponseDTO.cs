namespace UniHub.Domain.DTOs.Responses.Course;

public record CreateCourseResponseDTO
(
    long? Id,
    string? UserIdentifier,
    string? Name,
    string? Code
);
