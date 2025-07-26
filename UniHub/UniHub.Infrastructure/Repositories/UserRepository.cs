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

        public void CreateAsync(User user)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "InsertUser";
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _dbContext.CurrentTransaction;

            command.Parameters.Add(_dbContext.CreateParameter(command, "@clerkId", user.ClerkId));
            command.Parameters.Add(_dbContext.CreateParameter(command, "@name", user.Name));
            command.Parameters.Add(_dbContext.CreateParameter(command, "@creationDate", DateTime.UtcNow));
            command.Parameters.Add(_dbContext.CreateParameter(command, "@updateDate", DateTime.UtcNow));

            command.ExecuteNonQuery();
        } 

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByClerkIdAsync(string clerkId)
        {
            using var command = _dbContext.CreateCommand();
            command.CommandText = "GetUserByClerkId";
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _dbContext.CurrentTransaction;

            command.Parameters.Add(_dbContext.CreateParameter(command, "@clerkId", clerkId));

            using var reader = command.ExecuteReader();

            User? user = null;

            if (reader.Read())
            {
                user = new User
                {
                    Id = reader["Id"] is DBNull ? null : (long?)reader["Id"],
                    ClerkId = reader["ClerkId"] is DBNull ? null : (string)reader["ClerkId"],
                    Name = reader["Name"] is DBNull ? null : (string)reader["Name"],
                    Role = Enum.Parse<UserRole>((string)reader["Role"]),
                    CreationDate = reader["CreationDate"] is DBNull ? null : (DateTime?)reader["CreationDate"],
                    UpdateDate = reader["UpdateDate"] is DBNull ? null : (DateTime?)reader["UpdateDate"],
                    DeletionDate = reader["DeletionDate"] is DBNull ? null : (DateTime?)reader["DeletionDate"]
                };
            }
            else
                return Task.FromResult<User?>(null);

            return Task.FromResult<User?>(user);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IUserRepository.CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        /*
        public void Update(Guid? id, PixOutStatus status)
        {
            var command = _dbRepositoryDTO.DbContext!.CreateCommand(_dbRepositoryDTO.ConnectionScope!);

            command.CommandText = "UpdatePixOutStatus";
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _dbRepositoryDTO.Transaction;

            command.Parameters.Add(_dbRepositoryDTO.DbContext!.CreateParameter(command, "@id", id!));
            command.Parameters.Add(_dbRepositoryDTO.DbContext!.CreateParameter(command, "@status", status.ToString()));
            command.Parameters.Add(_dbRepositoryDTO.DbContext!.CreateParameter(command, "@updateDate", DateTime.UtcNow));

            command.ExecuteNonQuery();
        }

        public PixOutInfosVO? GetPixOutInfosByIdempotencyKey(Guid idempotencyKey)
        {
            PixOutInfosVO? pixOutInfosVO = null;

            string key = _hashingKey.GetKeyType<PixOutInfosVO>()
                                    .WithParam(key: nameof(idempotencyKey), value: idempotencyKey)
                                    .GenerateHashKey();

            pixOutInfosVO = _caching.Get<PixOutInfosVO>(key);

            if (pixOutInfosVO is not null)
                return pixOutInfosVO;

            var command = _dbRepositoryDTO.DbContext!.CreateCommand(_dbRepositoryDTO.ConnectionScope!);

            command.CommandText = "GetPixOutInfosByIdempotencyKey";
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _dbRepositoryDTO.Transaction;

            command.Parameters.Add(_dbRepositoryDTO.DbContext!.CreateParameter(command, "@idempotencyKey", idempotencyKey));

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                pixOutInfosVO = new()
                {
                    IssueDate = (DateTime)reader["IssueDate"],
                    EndToEndId = (string)reader["EndToEndId"],
                    Status = Enum.Parse<PixOutStatus>((string)reader["Status"]),
                    ReqIdentifier = (string)reader["ReqIdentifier"]?.ToString()!,
                    ClientRequestId = reader["ClientRequestId"] is DBNull ? null : (Guid)reader["ClientRequestId"]
                };
            }
            else
                return null;

            if (pixOutInfosVO is not null)
                _caching.Set(key, pixOutInfosVO);

            return pixOutInfosVO;
        }

        public bool ExistsPixOutByEndToEndId(string endToEndId)
        {
            string key = _hashingKey.GetKeyType<bool>()
                                    .WithParam(key: nameof(PixOut.EndToEndId), value: endToEndId)
                                    .GenerateHashKey();

            bool existsPixOut = false;

            existsPixOut = _caching.Get<bool>(key);

            if (existsPixOut)
                return existsPixOut;

            var command = _dbRepositoryDTO.DbContext!.CreateCommand(_dbRepositoryDTO.ConnectionScope!);

            command.CommandText = "ExistsPixOutByEndToEndId";
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _dbRepositoryDTO.Transaction;

            command.Parameters.Add(_dbRepositoryDTO.DbContext.CreateParameter(command, "@endToEndId", endToEndId));

            using var reader = command.ExecuteReader();

            if (reader.Read())
                existsPixOut = true;

            return existsPixOut;
        }

        public PixOut? GetByEndToEndId(string endToEndId)
        {
            string Key = _hashingKey.GetKeyType<PixOut>()
                                .WithParam(key: nameof(endToEndId), value: endToEndId)
                                .GenerateHashKey();

            PixOut? pixOut = _caching.Get<PixOut>(Key);

            if (pixOut != null)
                return pixOut;

            var command = _dbRepositoryDTO.DbContext!.CreateCommand(_dbRepositoryDTO.ConnectionScope!);

            command.CommandText = "GetPixOutByEndToEndId";
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _dbRepositoryDTO.Transaction;

            command.Parameters.Add(_dbRepositoryDTO.DbContext.CreateParameter(command, "@endToEndId", endToEndId));

            using var reader = command.ExecuteReader();

            if (reader.Read())
                pixOut = new()
                {
                    IdempotencyKey = (Guid)reader["IdempotencyKey"],
                    PspRequestDate = (DateTime)reader["PspRequestDate"],
                    SettlementMethod = (string)reader["SettlementMethod"],
                    PriorityServiceLevel = Enum.Parse<ServiceLevel>((string)reader["PriorityServiceLevel"]),
                    PriorityInstructionLevel = Enum.Parse<InstructionLevel>((string)reader["PriorityInstructionLevel"]),
                    EndToEndId = (string)reader["EndToEndId"],
                    SettlementAmount = (decimal)reader["SettlementAmount"],
                    LocalInstrument = Enum.Parse<LocalInstrument>((string)reader["LocalInstrument"]),
                    DebtorFullName = reader["DebtorFullName"] is DBNull ? null : (string)reader["DebtorFullName"],
                    DebtorIdentification = reader["DebtorIdentification"] is DBNull ? null : (string)reader["DebtorIdentification"],
                    DebtorAccountIdentification = reader["DebtorAccountIdentification"] is DBNull ? null : (string)reader["DebtorAccountIdentification"],
                    DebtorAccountIssuer = reader["DebtorAccountIssuer"] is DBNull ? null : (string)reader["DebtorAccountIssuer"],
                    DebtorAccountType = reader["DebtorAccountType"] is DBNull ? default : Enum.Parse<AccountType>((string)reader["DebtorAccountType"]),
                    DebtorAgentIdentification = reader["DebtorAgentIdentification"] is DBNull ? null : (string)reader["DebtorAgentIdentification"],
                    CreditorIdentification = reader["CreditorIdentification"] is DBNull ? null : (string)reader["CreditorIdentification"],
                    CreditorAccountIdentification = reader["CreditorAccountIdentification"] is DBNull ? null : (string)reader["CreditorAccountIdentification"],
                    CreditorAccountIssuer = reader["CreditorAccountIssuer"] is DBNull ? null : (string)reader["CreditorAccountIssuer"],
                    CreditorAccountType = reader["CreditorAccountType"] is DBNull ? default : Enum.Parse<AccountType>((string)reader["CreditorAccountType"]),
                    CreditorAgentIdentification = reader["CreditorAgentIdentification"] is DBNull ? null : (string)reader["CreditorAgentIdentification"],
                    Purpose = Enum.Parse<TransactionPurpose>((string)reader["Purpose"]),
                    UserText = reader["UserText"] is DBNull ? null : (string)reader["UserText"],
                    Status = Enum.Parse<PixOutStatus>((string)reader["Status"]),
                    LocalIdentifier = reader["LocalIdentifier"] is DBNull ? null : (string)reader["LocalIdentifier"],
                    IssueDate = reader["IssueDate"] is DBNull ? DateTime.MinValue : (DateTime)reader["IssueDate"],
                    InterbankSettlementDate = reader["InterbankSettlementDate"] is DBNull ? DateTime.MinValue : (DateTime)reader["InterbankSettlementDate"],
                    EffectiveInterbankSettlementDate = reader["EffectiveInterbankSettlementDate"] is DBNull ? DateTime.MinValue : (DateTime)reader["EffectiveInterbankSettlementDate"],
                    AgentType = reader["AgentType"] is DBNull ? default : Enum.Parse<AgentType>((string)reader["AgentType"]),
                    AdjustmentsAmount = reader["AdjustmentsAmount"] is DBNull ? null : (string)reader["AdjustmentsAmount"],
                    AdjustmentsReason = reader["AdjustmentsReason"] is DBNull ? null : (string)reader["AdjustmentsReason"],
                    ReqIdentifier = reader["ReqIdentifier"] is DBNull ? null : reader["ReqIdentifier"].ToString(),
                    ClientRequestId = reader["ClientRequestId"] is DBNull ? null : (Guid?)reader["ClientRequestId"],
                };

            _caching.Set(Key, pixOut);

            return pixOut;
        }

        public Task<PixOut?> Get(string? identifier)
        {
            var command = _dbRepositoryDTO.DbContext!.CreateCommand(_dbRepositoryDTO.ConnectionScope!);

            command.CommandText = "GetPixOutByIdentifier";
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _dbRepositoryDTO.Transaction;
            command.Parameters.Add(_dbRepositoryDTO.DbContext!.CreateParameter(command, "@identifier", identifier));

            using var reader = command.ExecuteReader();

            PixOut? pixOut = null;

            if (reader.Read())
            {
                pixOut = new()
                {
                    Id = (long)reader["Id"],
                    IdempotencyKey = (Guid)reader["IdempotencyKey"],
                    PspRequestDate = (DateTime)reader["PspRequestDate"],
                    SettlementMethod = (string)reader["SettlementMethod"],
                    PriorityServiceLevel = Enum.Parse<ServiceLevel>((string)reader["PriorityServiceLevel"]),
                    PriorityInstructionLevel = Enum.Parse<InstructionLevel>((string)reader["PriorityInstructionLevel"]),
                    EndToEndId = (string)reader["EndToEndId"],
                    SettlementAmount = (decimal)reader["SettlementAmount"],
                    LocalInstrument = Enum.Parse<LocalInstrument>((string)reader["LocalInstrument"]),
                    DebtorFullName = (string)reader["DebtorFullName"],
                    DebtorIdentification = (string)reader["DebtorIdentification"],
                    DebtorAccountIdentification = (string)reader["DebtorAccountIdentification"],
                    DebtorAccountIssuer = (string)reader["DebtorAccountIssuer"],
                    DebtorAccountType = Enum.Parse<AccountType>((string)reader["DebtorAccountType"]),
                    DebtorAgentIdentification = (string)reader["DebtorAgentIdentification"],
                    CreditorIdentification = (string)reader["CreditorIdentification"],
                    CreditorAccountIdentification = (string)reader["CreditorAccountIdentification"],
                    CreditorAccountIssuer = reader["CreditorAccountIssuer"] is DBNull ? null : (string)reader["CreditorAccountIssuer"],
                    CreditorAccountType = Enum.Parse<AccountType>((string)reader["CreditorAccountType"]),
                    CreditorAgentIdentification = (string)reader["CreditorAgentIdentification"],
                    Purpose = Enum.Parse<TransactionPurpose>((string)reader["Purpose"]),
                    UserText = reader["UserText"] is DBNull ? null : (string)reader["UserText"],
                    Status = Enum.Parse<PixOutStatus>((string)reader["Status"]),
                    LocalIdentifier = (string)reader["LocalIdentifier"],
                    ReqIdentifier = ((Guid)reader["ReqIdentifier"]).ToString(),
                    IssueDate = reader.IsDBNull(reader.GetOrdinal("IssueDate")) ? null : (DateTime)reader["IssueDate"],
                    InterbankSettlementDate = reader.IsDBNull(reader.GetOrdinal("InterbankSettlementDate")) ? null : (DateTime)reader["InterbankSettlementDate"],
                    EffectiveInterbankSettlementDate = reader.IsDBNull(reader.GetOrdinal("EffectiveInterbankSettlementDate")) ? null : (DateTime)reader["EffectiveInterbankSettlementDate"],
                    AgentType = reader.IsDBNull(reader.GetOrdinal("AgentType")) ? null : Enum.Parse<AgentType>((string)reader["AgentType"]),
                    AdjustmentsAmount = reader.IsDBNull(reader.GetOrdinal("AdjustmentsAmount")) ? null : (string)reader["AdjustmentsAmount"],
                    AdjustmentsReason = reader.IsDBNull(reader.GetOrdinal("AdjustmentsReason")) ? null : (string)reader["AdjustmentsReason"],
                    ClientRequestId = reader["ClientRequestId"] is DBNull ? null : (Guid)reader["ClientRequestId"],
                    CreationDate = reader.IsDBNull(reader.GetOrdinal("CreationDate")) ? null : (DateTime)reader["CreationDate"],
                    UpdateDate = reader.IsDBNull(reader.GetOrdinal("UpdateDate")) ? null : (DateTime)reader["UpdateDate"]
                };
            }
            else
                return Task.FromResult<PixOut?>(null);

            return Task.FromResult(pixOut)!;
        }*/
    }
}
