using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniHub.Domain.Interfaces.Repositories;
using static System.Formats.Asn1.AsnWriter;

namespace UniHub.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext _dbContext;
        private IUserRepository? _userRepository;


        public void Commit()
        {
            foreach (KeyValuePair<string, DbContext> dbContext in _dbContext.dbContexts)
            {
                _dbContext.connections.TryGetValue(dbContext.Key, out var connection);
                _dbContext.transactions.TryGetValue(dbContext.Key, out var transaction);

                transaction?.Commit();
                Closed(connection!, transaction);
            }

            Clear();
        }

        public void Rollback()
        {
            foreach (KeyValuePair<string, DbContext> dbContext in _dbContext.dbContexts)
            {
                _dbContext.connections.TryGetValue(dbContext.Key, out var connection);
                _dbContext.transactions.TryGetValue(dbContext.Key, out var transaction);

                transaction?.Rollback();
                Closed(connection!, transaction);
            }

            Clear();
        }

        private void Closed(IDbConnection connection, IDbTransaction transaction)
        {
            connection?.Dispose();
            transaction?.Dispose();
        }

        private DbRepositoryDTO InitConnection(string scope, string connectionName)
        {
            string? connection = _tenantService.GetConnectionString(_tenantTransaction.TenantId!.Value, scope, connectionName);

            DbRepositoryDTO dbRepositoryDTO = new DbRepositoryDTO();
            dbRepositoryDTO.DbContext = _dbContext.InitConnection(connection!, scope);
            dbRepositoryDTO.Transaction = dbRepositoryDTO.DbContext.BeginTransaction(scope);
            dbRepositoryDTO.ConnectionScope = scope;

            return dbRepositoryDTO;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _balanceRepository ??= new BalanceRepository(_dbContext, _tenantTransaction, _tenantService);
            }
        }

        public IPixOutReasonCodeRepository PixOutReasonCodeRepository
        {
            get
            {
                DbRepositoryDTO dbRepositoryDTO = InitConnection(Scopes.Pix, ConnectionType.Sql);

                _pixOutReasonCodeRepository ??= new PixOutReasonCodeRepository(dbRepositoryDTO, _caching, _hashingKey);

                return _pixOutReasonCodeRepository;
            }
        }

        private void Clear()
        {
            _dbContext.Clear();
            _pixOutRepository = default;
            _pixInRepository = default;
            _pixRefundRepository = default;
            _refundReasonCodeRepository = default;
            _pacs008InRepository = default;
            _pacs002Repository = default;
            _pacs004Repository = default;
            _paymentLaunchRepository = default;
            _paymentLaunchRepository = default;
            _pacs008OutRepository = default;
            _providerRepository = default;
            _participantsRepository = default;
            _pixSchedulerRepository = default;
        }
    }
}
