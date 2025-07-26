using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByClerkIdAsync(string clerkId);
        Task<List<User>> GetAllAsync();
    }
}
