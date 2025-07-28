namespace UniHub.Domain.DTOs.Responses.Course;

public record GetCoursesByUserResponseDTO
(
    string? UserIdentifier,
    List<CourseByUserDTO> Courses    
);
