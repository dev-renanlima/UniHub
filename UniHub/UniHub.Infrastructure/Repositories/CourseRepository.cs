using System.Data;
using UniHub.Domain.Entities;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Domain.VOs;
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

        public async Task<List<Course>?> GetCoursesByUserAsync(long? userId)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT * FROM public.\"GetCoursesByUserId\"(@p_UserId)";
            command.CommandType = CommandType.Text;
            command.Transaction = _dbContext.CurrentTransaction;

            _dbContext.CreateParameter(command, "p_UserId", userId);

            using var reader = await command.ExecuteReaderAsync();

            List<Course>? courses = [];

            while (await reader.ReadAsync())
            {
                courses.Add(
                    new Course
                    {
                        Id = reader["Id"] is DBNull ? null : (long?)reader["Id"],
                        Name = reader["Name"] is DBNull ? null : (string)reader["Name"],
                        Code = reader["Code"] is DBNull ? null : (string)reader["Code"]
                    }
                );
            }

            return courses;
        }

        public async Task<List<CourseMemberVO>?> GetCourseMembersByCourseAsync(long? courseId)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT * FROM public.\"GetCourseMembersByCourseId\"(@p_CourseId)";
            command.CommandType = CommandType.Text;
            command.Transaction = _dbContext.CurrentTransaction;

            _dbContext.CreateParameter(command, "p_CourseId", courseId);

            using var reader = await command.ExecuteReaderAsync();

            List<CourseMemberVO>? courseMembers = [];

            while (await reader.ReadAsync())
            {
                courseMembers.Add(
                    new CourseMemberVO
                    {
                        CourseId = reader["CourseId"] is DBNull ? null : (long?)reader["CourseId"],
                        CourseName = reader["CourseName"] is DBNull ? null : (string?)reader["CourseName"],
                        UserId = reader["UserId"] is DBNull ? null : (long?)reader["UserId"],
                        UserName = reader["UserName"] is DBNull ? null : (string?)reader["UserName"],
                        UserIdentifier = reader["UserIdentifier"] is DBNull ? null : (string?)reader["UserIdentifier"],
                        EnrollmentDate = reader["EnrollmentDate"] is DBNull ? null : (DateTime?)reader["EnrollmentDate"]
                    }
                );
            }

            return courseMembers;
        }
    }
}
