namespace UniHub.Domain.DTOs.Responses.Course;

public record GetCoursesByUserResponseDTO
(
    string? UserIdentifier,
    string? UserName,
    long? NumberOfCourses,
    List<CourseByUserDTO> Courses    
);
