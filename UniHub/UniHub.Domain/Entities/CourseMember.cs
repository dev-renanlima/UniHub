namespace UniHub.Domain.Entities;

public class CourseMember : BaseEntity
{
    public long? CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public long? UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }
}
