using UniHub.Domain.Entities;

namespace UniHub.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> CreateAsync(User user);
        Task<User?> GetUserByIdAsync(Guid? userId);
        Task<User?> GetUserByIdentifierAsync(string identifier);
    }
}
