using UniHub.Domain.Enums;

namespace UniHub.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid? CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public Guid? UserId { get; set; }
    public User User { get; set; } = null!;

    public string? Content { get; set; }

    public string? RedirectUrl { get; set; }

    public bool Read { get; set; }

    public NotificationType Type { get; set; }
}
