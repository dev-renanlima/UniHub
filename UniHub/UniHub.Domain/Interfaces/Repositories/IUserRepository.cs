using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<long> CreateAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByClerkIdAsync(string clerkId);
        Task<List<User>> GetAllUsersAsync();
    }
}
