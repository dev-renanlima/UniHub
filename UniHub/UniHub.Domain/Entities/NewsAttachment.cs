namespace UniHub.Domain.Entities;

public class NewsAttachment : BaseEntity
{
    public Guid? NewsId { get; set; }
    public News News { get; set; } = null!;

    public string? Url { get; set; }
}
