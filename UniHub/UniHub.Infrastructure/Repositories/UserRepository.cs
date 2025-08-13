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
            user.SetDates();

            var parameters = new (string, object?)[]
            {
                ("p_Id", user.Id),
                ("p_InternalIdentifier", user.InternalIdentifier),
                ("p_ExternalIdentifier", user.ExternalIdentifier),
                ("p_Name", user.Name),
                ("p_Email", user.Email),
                ("p_Role", user.Role.ToString()),
                ("p_Status", user.Status.ToString()),
                ("p_ProfileUrl", user.ProfileUrl),
                ("p_CreationDate", user.CreationDate),
                ("p_UpdateDate", user.UpdateDate)
            };

            using var command = _dbContext.CreateFunctionCommand(functionName: "InsertUser", parameters);

            var result = await command.ExecuteScalarAsync();

            user.SetIdentity((Guid)result!);

            return user;
        } 

        public async Task<User?> GetUserByIdentifierAsync(string identifier)
        {
            var parameters = new (string, object?)[]
            {
                ("p_Identifier", identifier),
            };

            using var command = _dbContext.CreateFunctionCommand(functionName: "GetUserByIdentifier", parameters);

            using var reader = await command.ExecuteReaderAsync();

            User? user = null;

            if (await reader.ReadAsync()) 
            {
                user = new User
                {
                    Id = (Guid)reader["Id"],
                    InternalIdentifier = (string)reader["InternalIdentifier"],
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

        public async Task<User?> GetUserByIdAsync(Guid? userId)
        {
            var parameters = new (string, object?)[]
            {
                ("p_Id", userId),
            };

            using var command = _dbContext.CreateFunctionCommand(functionName: "GetUserById", parameters);

            using var reader = await command.ExecuteReaderAsync();

            User? user = null;

            if (await reader.ReadAsync())
            {
                user = new User
                {
                    Id = (Guid)reader["Id"],
                    InternalIdentifier = (string)reader["InternalIdentifier"],
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
    }
}
