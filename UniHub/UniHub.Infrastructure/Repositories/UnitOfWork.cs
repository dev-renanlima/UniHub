using Microsoft.Extensions.Configuration;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Infrastructure.Context;

namespace UniHub.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        private IUserRepository? _userRepository;
        private ICourseRepository? _courseRepository;
        private IAssignmentRepository? _assignmentRepository;
        private INewsRepository? _newsRepository;
        private string? _connectionString;

        public UnitOfWork(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("UniHubConnection");
        }

        private void EnsureConnectionInitialized()
        {
            if (_dbContext.CurrentConnection == null)
            {
                if (string.IsNullOrEmpty(_connectionString))
                    throw new InvalidOperationException("A connection string não foi definida");

                _dbContext.InitConnection(_connectionString);
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                EnsureConnectionInitialized();

                _userRepository ??= new UserRepository(_dbContext);
                return _userRepository;
            }
        }

        public ICourseRepository CourseRepository 
        {
            get
            {
                EnsureConnectionInitialized();

                _courseRepository ??= new CourseRepository(_dbContext);
                return _courseRepository;
            }
        }

        public IAssignmentRepository AssignmentRepository
        {
            get
            {
                EnsureConnectionInitialized();

                _assignmentRepository ??= new AssignmentRepository(_dbContext);
                return _assignmentRepository;
            }
        }

        public INewsRepository NewsRepository
        {
            get
            {
                EnsureConnectionInitialized();

                _newsRepository ??= new NewsRepository(_dbContext);
                return _newsRepository;
            }
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
            _courseRepository = null;
        }
    }
}
