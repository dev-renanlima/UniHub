using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Task<Course?> CreateAsync(Course course);
        Task<Course?> GetByIdAsync(int id);
        Task<Course?> GetByClerkIdAsync(string clerkId);
        Task<List<Course>> GetAllAsync();
    }
}
