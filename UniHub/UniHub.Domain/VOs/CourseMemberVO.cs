using UniHub.Domain.Entities;

namespace UniHub.Domain.VOs;

public sealed class CourseMemberVO
{
    public long? CourseId { get; set; }
    public string? CourseName { get; set; }
    public long? UserId { get; set; }
    public string? UserIdentifier { get; set; }
    public string? UserName { get; set; }
    public DateTime? EnrollmentDate { get; set; }
}
