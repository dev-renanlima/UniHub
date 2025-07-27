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

        public async Task<Course?> CreateAsync(Course course)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT public.\"InsertCourse\"(@p_UserId, @p_Name, @p_Code, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text; 

            _dbContext.CreateParameter(command, "p_UserId", course.UserId);
            _dbContext.CreateParameter(command, "p_Name", course.Name);
            _dbContext.CreateParameter(command, "p_Code", course.Code);
            _dbContext.CreateParameter(command, "p_CreationDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_UpdateDate", DateTime.UtcNow);

            var result = await command.ExecuteScalarAsync();

            course.SetIdentity((long)result!);

            return course;
        }

        public async Task<CourseMember?> CreateCourseMemberAsync(CourseMember courseMember)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT public.\"InsertCourseMember\"(@p_CourseId, @p_UserId, @p_EnrollmentDate, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text;

            _dbContext.CreateParameter(command, "p_CourseId", courseMember.CourseId);
            _dbContext.CreateParameter(command, "p_UserId", courseMember.UserId);
            _dbContext.CreateParameter(command, "p_EnrollmentDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_CreationDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_UpdateDate", DateTime.UtcNow);

            var result = await command.ExecuteScalarAsync();

            courseMember.SetIdentity((long)result!);

            return courseMember;
        }

        public Task<List<Course>> GetAllCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetCourseByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Course?> GetCourseByCodeAsync(string code)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT * FROM public.\"GetCourseByCode\"(@p_Code)";
            command.CommandType = CommandType.Text;
            command.Transaction = _dbContext.CurrentTransaction;

            _dbContext.CreateParameter(command, "p_Code", code);

            using var reader = await command.ExecuteReaderAsync();

            Course? course = null;

            if (await reader.ReadAsync())
            {
                course = new Course
                {
                    Id = reader["Id"] is DBNull ? null : (long?)reader["Id"],
                    UserId = reader["UserId"] is DBNull ? null : (long?)reader["UserId"],
                    Name = reader["Name"] is DBNull ? null : (string)reader["Name"],
                    Code = reader["Code"] is DBNull ? null : (string)reader["Code"],
                    CreationDate = reader["CreationDate"] is DBNull ? null : (DateTime?)reader["CreationDate"],
                    UpdateDate = reader["UpdateDate"] is DBNull ? null : (DateTime?)reader["UpdateDate"]
                };
            }

            return course;
        }
    }
}
