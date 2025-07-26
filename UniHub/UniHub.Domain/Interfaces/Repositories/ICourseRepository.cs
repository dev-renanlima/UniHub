using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Task<long> CreateAsync(Course course);
        Task<Course?> GetByIdAsync(int id);
        Task<Course?> GetByClerkIdAsync(string clerkId);
        Task<List<Course>> GetAllAsync();
    }
}
