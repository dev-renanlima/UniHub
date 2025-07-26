using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByClerkIdAsync(string clerkId);
        Task<List<User>> GetAllAsync();
    }
}
