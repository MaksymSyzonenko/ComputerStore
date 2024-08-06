using System.Collections.Concurrent;
using ComputerStore_MSSQL.Data.Repositories;
using ComputerStore_MSSQL.Data.Repositories.Factory;
using Microsoft.EntityFrameworkCore.Storage;

namespace ComputerStore_MSSQL.Data.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ComputerStoreDbContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ConcurrentDictionary<Type, object> _repositories;

        public UnitOfWork(ComputerStoreDbContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
            _repositories = new ConcurrentDictionary<Type, object>();
        }

        public async Task Commit()
        {
            await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public IRepository Repository<TEntity>()
        {
            if (!_repositories.TryGetValue(typeof(TEntity), out object? repository))
            {
                repository = _repositoryFactory.Instantiate<TEntity>(_context);
                _repositories.TryAdd(typeof(TEntity), repository);
            }

            return (IRepository)repository;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}

