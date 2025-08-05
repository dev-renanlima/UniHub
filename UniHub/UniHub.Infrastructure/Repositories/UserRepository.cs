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

            command.CommandText = "SELECT public.\"InsertUser\"(@p_InternalIdentifier, @p_ExternalIdentifier, @p_Name, @p_Email, @p_Role, @p_Status, @p_ProfileUrl, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text;

            _dbContext.CreateParameter(command, "p_InternalIdentifier", user.InternalIdentifier);
            _dbContext.CreateParameter(command, "p_ExternalIdentifier", user.ExternalIdentifier);
            _dbContext.CreateParameter(command, "p_Name", user.Name);
            _dbContext.CreateParameter(command, "p_Email", user.Email);
            _dbContext.CreateParameter(command, "p_Role", user.Role.ToString());
            _dbContext.CreateParameter(command, "p_Status", user.Status.ToString());
            _dbContext.CreateParameter(command, "p_ProfileUrl", user.ProfileUrl);
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

        public async Task<User?> GetUserByIdentifierAsync(string identifier)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT * FROM public.\"GetUserByIdentifier\"(@p_Identifier)";
            command.CommandType = CommandType.Text;
            command.Transaction = _dbContext.CurrentTransaction;

            _dbContext.CreateParameter(command, "p_Identifier", identifier);

            using var reader = await command.ExecuteReaderAsync();

            User? user = null;

            if (await reader.ReadAsync()) 
            {
                user = new User
                {
                    Id = reader["Id"] is DBNull ? null : (long?)reader["Id"],
                    InternalIdentifier = reader["InternalIdentifier"] is DBNull ? null : (string)reader["InternalIdentifier"],
                    ExternalIdentifier = reader["ExternalIdentifier"] is DBNull ? null : (string)reader["ExternalIdentifier"],
                    Name = reader["Name"] is DBNull ? null : (string)reader["Name"],
                    Email = reader["Email"] is DBNull ? null : (string)reader["Email"],
                    Role = Enum.Parse<UserRole>((string)reader["Role"]),
                    Status = Enum.Parse<UserStatus>((string)reader["Status"]),
                    ProfileUrl = reader["ProfileUrl"] is DBNull ? null : (string)reader["ProfileUrl"],
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
