namespace UniHub.Domain.DTOs;

public class CourseMemberDTO
{
    public long? CourseId { get; set; }

    public string? ExternalIdentifier { get; set; }

    public string? Code { get; set; }

    public DateTime? EnrollmentDate { get; set; }
}
