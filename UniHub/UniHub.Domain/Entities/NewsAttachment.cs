namespace UniHub.Domain.Entities;

public class NewsAttachment : BaseEntity
{
    public long? NewsId { get; set; }
    public News News { get; set; } = null!;

    public string? Url { get; set; }
}
