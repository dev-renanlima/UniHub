using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UniHub.Domain.Entities;
using UniHub.Infrastructure.Context.Mappings;

namespace UniHub.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;

        public IDbConnection? CurrentConnection => _connection;
        public IDbTransaction? CurrentTransaction => _transaction;

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMappings).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseMappings).Assembly);

            base.OnModelCreating(modelBuilder);
        }


        public void InitConnection(string connectionString)
        {
            if (_connection != null)
                return;

            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            DisposeTransaction();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            DisposeTransaction();
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            _transaction = null;
            _connection = null;
        }

        public IDbCommand CreateCommand()
        {
            if (_connection == null || _transaction == null)
                throw new InvalidOperationException("Conexão ou transação não inicializada.");

            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public IDbDataParameter CreateParameter(IDbCommand command, string name, object? value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            return parameter;
        }
    }
}
