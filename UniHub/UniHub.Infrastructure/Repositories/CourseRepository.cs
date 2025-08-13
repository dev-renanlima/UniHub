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
            course.SetDates();

            var parameters = new (string, object?)[]
            {
                ("p_Id", course.Id),
                ("p_InternalIdentifier", course.InternalIdentifier),
                ("p_UserId", course.UserId),
                ("p_Name", course.Name),
                ("p_Code", course.Code),
                ("p_CreationDate", course.CreationDate),
                ("p_UpdateDate", course.UpdateDate)
            };

            using var command = _dbContext.CreateFunctionCommand(functionName: "InsertCourse", parameters);

            var result = await command.ExecuteScalarAsync();

            course.SetIdentity((Guid)result!);

            return course;
        }

        public async Task<CourseMember?> CreateCourseMemberAsync(CourseMember courseMember)
        {
            courseMember.SetDates();

            var parameters = new (string, object?)[]
            {
                ("p_Id", courseMember.Id),
                ("p_InternalIdentifier", courseMember.InternalIdentifier),
                ("p_CourseId", courseMember.CourseId),
                ("p_UserId", courseMember.UserId),
                ("p_EnrollmentDate", courseMember.EnrollmentDate),
                ("p_CreationDate", courseMember.CreationDate),
                ("p_UpdateDate", courseMember.UpdateDate)
            };

            using var command = _dbContext.CreateFunctionCommand(functionName: "InsertCourseMember", parameters);

            var result = await command.ExecuteScalarAsync();

            courseMember.SetIdentity((Guid)result!);

            return courseMember;
        }

        public Task<List<Course>> GetAllCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Course?> GetCourseByCodeAsync(string? code)
        {
            var parameters = new (string, object?)[]
            {
                ("p_Code", code)
            };

            using var command = _dbContext.CreateFunctionCommand(functionName: "GetCourseByCode", parameters);

            using var reader = await command.ExecuteReaderAsync();

            Course? course = null;

            if (await reader.ReadAsync())
            {
                course = new Course
                {
                    Id = (Guid)reader["Id"],
                    UserId = reader["UserId"] is DBNull ? null : (Guid?)reader["UserId"],
                    Name = reader["Name"] is DBNull ? null : (string)reader["Name"],
                    Code = reader["Code"] is DBNull ? null : (string)reader["Code"],
                    CreationDate = reader["CreationDate"] is DBNull ? null : (DateTime?)reader["CreationDate"],
                    UpdateDate = reader["UpdateDate"] is DBNull ? null : (DateTime?)reader["UpdateDate"]
                };
            }

            return course;
        }

        public async Task<List<CourseVO>?> GetCoursesByUserIdAsync(Guid? userId)
        {
            var parameters = new (string, object?)[]
            {
                ("p_UserId", userId)
            };

            using var command = _dbContext.CreateFunctionCommand(functionName: "GetCoursesByUserId", parameters);

            using var reader = await command.ExecuteReaderAsync();

            List<CourseVO>? courses = [];

            while (await reader.ReadAsync())
            {
                courses.Add(
                    new CourseVO
                    {
                        CourseId = reader["CourseId"] is DBNull ? null : (Guid?)reader["CourseId"],
                        CourseIdentifier = reader["CourseIdentifier"] is DBNull ? null : (string)reader["CourseIdentifier"],
                        CourseName = reader["CourseName"] is DBNull ? null : (string)reader["CourseName"],
                        CourseCode = reader["CourseCode"] is DBNull ? null : (string)reader["CourseCode"],
                        UserId = reader["UserId"] is DBNull ? null : (Guid?)reader["UserId"],
                        UserName = reader["UserName"] is DBNull ? null : (string)reader["UserName"],
                        UserIdentifier = reader["UserIdentifier"] is DBNull ? null : (string)reader["UserIdentifier"],
                        NumberOfMembers = reader["NumberOfMembers"] is DBNull ? null : (long?)reader["NumberOfMembers"],
                        CreationDate = reader["CreationDate"] is DBNull ? null : (DateTime?)reader["CreationDate"],
                        UpdateDate = reader["UpdateDate"] is DBNull ? null : (DateTime?)reader["UpdateDate"]
                    }
                );
            }

            return courses;
        }

        public async Task<List<CourseMemberVO>?> GetCourseMembersByCourseIdAsync(Guid? courseId)
        {
            var parameters = new (string, object?)[]
            {
                ("p_CourseId", courseId)
            };

            using var command = _dbContext.CreateFunctionCommand(functionName: "GetCourseMembersByCourseId", parameters);

            using var reader = await command.ExecuteReaderAsync();

            List<CourseMemberVO>? courseMembers = [];

            while (await reader.ReadAsync())
            {
                courseMembers.Add(
                    new CourseMemberVO
                    {
                        CourseId = reader["CourseId"] is DBNull ? null : (Guid?)reader["CourseId"],
                        CourseName = reader["CourseName"] is DBNull ? null : (string?)reader["CourseName"],
                        UserId = reader["UserId"] is DBNull ? null : (Guid?)reader["UserId"],
                        UserIdentifier = reader["UserIdentifier"] is DBNull ? null : (string?)reader["UserIdentifier"],
                        UserName = reader["UserName"] is DBNull ? null : (string?)reader["UserName"],
                        EnrollmentDate = reader["EnrollmentDate"] is DBNull ? null : (DateTime?)reader["EnrollmentDate"]
                    }
                );
            }

            return courseMembers;
        }
    }
}
