namespace UniHub.Domain.Entities;

public class CourseMember
{
    public long CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public string? MemberId { get; set; }
    public User Member { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
}
