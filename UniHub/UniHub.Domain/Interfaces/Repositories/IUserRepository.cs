using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> CreateAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByClerkIdAsync(string clerkId);
        Task<List<User>> GetAllUsersAsync();
    }
}
