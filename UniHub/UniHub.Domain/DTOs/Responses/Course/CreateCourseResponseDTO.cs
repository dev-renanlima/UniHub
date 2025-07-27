namespace UniHub.Domain.DTOs.Responses.Course;

public record CreateCourseResponseDTO
(
    long? Id,
    string? AdminId,
    string? Name,
    string? Code
);
