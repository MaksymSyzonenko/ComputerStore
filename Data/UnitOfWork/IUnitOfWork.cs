using ComputerStore_MSSQL.Data.Repositories;

namespace ComputerStore_MSSQL.Data.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task Commit();
        IRepository Repository<TEntity>();
    }
}

