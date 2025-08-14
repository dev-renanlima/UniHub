using UniHub.Domain.Entities;

namespace UniHub.Domain.VOs;

public sealed class CourseMemberVO
{
    public Guid? CourseId { get; set; }
    public string? CourseName { get; set; }
    public Guid? UserId { get; set; }
    public string? UserIdentifier { get; set; }
    public string? UserName { get; set; }
    public DateTime? EnrollmentDate { get; set; }
}
