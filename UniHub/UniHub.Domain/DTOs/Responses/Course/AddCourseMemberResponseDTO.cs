namespace UniHub.Domain.DTOs.Responses.Course;

public record AddCourseMemberResponseDTO
(
    string? UserIdentifier,
    long? CourseId,
    string? CourseName,
    DateTime? EnrollmentDate
);
