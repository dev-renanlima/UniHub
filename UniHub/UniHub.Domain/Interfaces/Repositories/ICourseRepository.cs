using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        void Create(Course course);
        Task<Course?> GetByIdAsync(int id);
        Task<Course?> GetByClerkIdAsync(string clerkId);
        Task<List<Course>> GetAllAsync();
    }
}
