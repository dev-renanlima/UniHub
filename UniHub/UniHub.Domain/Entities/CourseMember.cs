namespace UniHub.Domain.Entities;

public class CourseMember : BaseEntity
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }
}
