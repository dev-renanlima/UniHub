using System.Data;
using UniHub.Domain.Entities;
using UniHub.Domain.Enums;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Infrastructure.Context;

namespace UniHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> CreateAsync(User user)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT public.\"InsertUser\"(@p_ExternalIdentifier, @p_Role, @p_Name, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text;

            _dbContext.CreateParameter(command, "p_ExternalIdentifier", user.ExternalIdentifier);
            _dbContext.CreateParameter(command, "p_Role", user.Role.ToString());
            _dbContext.CreateParameter(command, "p_Name", user.Name);
            _dbContext.CreateParameter(command, "p_CreationDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_UpdateDate", DateTime.UtcNow);

            var result = await command.ExecuteScalarAsync();

            user.SetIdentity((long)result!);

            return user;
        } 

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByExternalIdentifierAsync(string externalIdentifier)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT * FROM public.\"GetUserByExternalIdentifier\"(@p_ExternalIdentifier)";
            command.CommandType = CommandType.Text;
            command.Transaction = _dbContext.CurrentTransaction;

            _dbContext.CreateParameter(command, "p_ExternalIdentifier", externalIdentifier);

            using var reader = await command.ExecuteReaderAsync();

            User? user = null;

            if (await reader.ReadAsync()) 
            {
                user = new User
                {
                    Id = reader["Id"] is DBNull ? null : (long?)reader["Id"],
                    ExternalIdentifier = reader["ExternalIdentifier"] is DBNull ? null : (string)reader["ExternalIdentifier"],
                    Name = reader["Name"] is DBNull ? null : (string)reader["Name"],
                    Role = Enum.Parse<UserRole>((string)reader["Role"]),
                    CreationDate = reader["CreationDate"] is DBNull ? null : (DateTime?)reader["CreationDate"],
                    UpdateDate = reader["UpdateDate"] is DBNull ? null : (DateTime?)reader["UpdateDate"]
                };
            }

            return user;
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
