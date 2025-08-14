namespace UniHub.Domain.DTOs.Responses.Course;

public record AddCourseMemberResponseDTO
(
    string? UserIdentifier,
    string? UserName,
    string? CourseIdentifier,
    string? CourseName,
    string? CourseCode,
    DateTime? EnrollmentDate
);
