using System.Data;
using UniHub.Domain.Entities;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Infrastructure.Context;

namespace UniHub.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NewsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<News?> CreateAsync(News news)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT public.\"InsertNews\"(@p_CourseId, @p_UserId, @p_Title, @p_Description, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text;

            _dbContext.CreateParameter(command, "p_CourseId", news.CourseId);
            _dbContext.CreateParameter(command, "p_UserId", news.UserId);
            _dbContext.CreateParameter(command, "p_Title", news.Title);
            _dbContext.CreateParameter(command, "p_Description", news.Description);
            _dbContext.CreateParameter(command, "p_CreationDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_UpdateDate", DateTime.UtcNow);

            var result = await command.ExecuteScalarAsync();
            
            news.SetIdentity((Guid)result!);

            return news;
        }

        public async Task<NewsAttachment?> CreateNewsAttachmentAsync(NewsAttachment newsAttachment)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT public.\"InsertNewsAttachment\"(@p_NewsId, @p_Url, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text;

            _dbContext.CreateParameter(command, "p_NewsId", newsAttachment.NewsId);
            _dbContext.CreateParameter(command, "p_Url", newsAttachment.Url);
            _dbContext.CreateParameter(command, "p_CreationDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_UpdateDate", DateTime.UtcNow);

            var result = await command.ExecuteScalarAsync();

            newsAttachment.SetIdentity((Guid)result!);

            return newsAttachment;
        }
    }
}
