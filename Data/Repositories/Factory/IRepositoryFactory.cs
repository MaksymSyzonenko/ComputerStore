
namespace ComputerStore_MSSQL.Data.Repositories.Factory
{
    public interface IRepositoryFactory
    {
        IRepository Instantiate<TEntity>(ComputerStoreDbContext context);
    }
}

