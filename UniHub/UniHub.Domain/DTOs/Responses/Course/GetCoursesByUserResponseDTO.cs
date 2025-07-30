namespace UniHub.Domain.DTOs.Responses.Course;

public record GetCoursesByUserResponseDTO
(
    string? UserIdentifier,
    string? UserName,
    int? NumberOfCourses,
    List<CourseByUserDTO> Courses    
);
