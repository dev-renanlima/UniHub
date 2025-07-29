namespace UniHub.Domain.Entities;

public class News : BaseEntity
{
    public long? CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public long? UserId { get; set; }
    public User User { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public List<NewsAttachment> NewsAnnexes { get; set; } = new();
}
 