using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> CreateAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByExternalIdentifierAsync(string externalIdentifier);
        Task<List<User>> GetAllUsersAsync();
    }
}
