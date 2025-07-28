using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using UniHub.Domain.Entities;
using UniHub.Infrastructure.Context.Mappings;

namespace UniHub.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        private NpgsqlConnection? _connection;
        private NpgsqlTransaction? _transaction;

        public NpgsqlConnection? CurrentConnection => _connection;
        public NpgsqlTransaction? CurrentTransaction => _transaction;

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMappings).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseMappings).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Inicializa a conexão com o banco de dados PostgreSQL.
        /// </summary>
        /// <param name="connectionString">A string de conexão para o PostgreSQL.</param>
        public void InitConnection(string connectionString)
        {
            if (_connection != null)
                return;

            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Confirma a transação atual.
        /// </summary>
        public void CommitTransaction()
        {
            _transaction?.Commit();
            DisposeTransaction();
        }

        /// <summary>
        /// Reverte a transação atual.
        /// </summary>
        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            DisposeTransaction();
        }

        /// <summary>
        /// Descarta a transação e a conexão.
        /// </summary>
        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            _transaction = null;
            _connection = null;
        }

        /// <summary>
        /// Cria um novo comando Npgsql.
        /// </summary>
        /// <returns>Um objeto NpgsqlCommand.</returns>
        /// <exception cref="InvalidOperationException">Lançada se a conexão ou transação não estiver inicializada.</exception>
        public NpgsqlCommand CreateCommand()
        {
            if (_connection == null || _transaction == null)
                throw new InvalidOperationException("Conexão ou transação não inicializada.");

            var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            return command;
        }

        /// <summary>
        /// Cria um novo parâmetro Npgsql.
        /// </summary>
        /// <param name="command">O comando ao qual o parâmetro será adicionado.</param>
        /// <param name="name">O nome do parâmetro (sem o prefixo '@' ou ':').</param>
        /// <param name="value">O valor do parâmetro.</param>
        /// <returns>Um objeto NpgsqlParameter.</returns>
        public NpgsqlParameter CreateParameter(NpgsqlCommand command, string name, object? value) 
        {
            NpgsqlParameter parameter;

            if(value is DateTime dateTimeValue)
            {
                if (dateTimeValue.Kind == DateTimeKind.Utc || dateTimeValue.Kind == DateTimeKind.Local)
                {
                    dateTimeValue = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Unspecified);
                }

                parameter = new NpgsqlParameter(name, NpgsqlDbType.Timestamp)
                {
                    Value = dateTimeValue
                };
            }
            else
            {
                parameter = new NpgsqlParameter(name, value ?? DBNull.Value);
            }

            command.Parameters.Add(parameter); 
            return parameter;
        }
    }
}
