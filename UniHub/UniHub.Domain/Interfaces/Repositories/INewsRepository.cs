using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories;

public interface INewsRepository
{
    Task<News?> CreateAsync(News news);
    Task<NewsAttachment?> CreateNewsAttachmentAsync(NewsAttachment newsAttachment);
}
