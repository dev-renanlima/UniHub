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

        public void Create(Course course)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "InsertCourse";
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _dbContext.CurrentTransaction;

            command.Parameters.Add(_dbContext.CreateParameter(command, "@adminId", course.AdminId));
            command.Parameters.Add(_dbContext.CreateParameter(command, "@name", course.Name));
            command.Parameters.Add(_dbContext.CreateParameter(command, "@code", course.Code));
            command.Parameters.Add(_dbContext.CreateParameter(command, "@creationDate", DateTime.UtcNow));
            command.Parameters.Add(_dbContext.CreateParameter(command, "@updateDate", DateTime.UtcNow));

            command.ExecuteNonQuery();
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
