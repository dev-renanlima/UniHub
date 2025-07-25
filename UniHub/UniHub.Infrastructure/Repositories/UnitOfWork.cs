using UniHub.Domain.Interfaces.Repositories;
using UniHub.Infrastructure.Context;

namespace UniHub.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IUserRepository? _userRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);

        public void BeginTransaction(string connectionString)
        {
            _dbContext.InitConnection(connectionString);
        }

        public void Commit()
        {
            _dbContext.CommitTransaction();
            ClearRepositories();
        }

        public void Rollback()
        {
            _dbContext.RollbackTransaction();
            ClearRepositories();
        }

        private void ClearRepositories()
        {
            _userRepository = null;
        }
    }
}
