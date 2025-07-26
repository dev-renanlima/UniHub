using System.Data;
using UniHub.Domain.Entities;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Infrastructure.Context;

namespace UniHub.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> CreateAsync(Course course)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT public.\"InsertCourse\"(@p_AdminId, @p_Name, @p_Code, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text; 

            _dbContext.CreateParameter(command, "p_AdminId", course.AdminId);
            _dbContext.CreateParameter(command, "p_Name", course.Name);
            _dbContext.CreateParameter(command, "p_Code", course.Code);
            _dbContext.CreateParameter(command, "p_CreationDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_UpdateDate", DateTime.UtcNow);

            var result = await command.ExecuteScalarAsync();
            return (long)result!;
        } 

        public Task<List<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetByClerkIdAsync(string clerkId)
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
