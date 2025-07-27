using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Task<Course?> CreateAsync(Course course);
        Task<CourseMember?> CreateCourseMemberAsync(CourseMember courseMember);
        Task<Course?> GetCourseByIdAsync(int id);
        Task<Course?> GetCourseByCodeAsync(string code);
        Task<List<Course>> GetAllCoursesAsync();
    }
}
