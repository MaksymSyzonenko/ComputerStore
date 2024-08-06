using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Cart;
using ComputerStore_MSSQL.Data.Repositories.Product;
using ComputerStore_MSSQL.Data.Repositories.Review;
using ComputerStore_MSSQL.Data.Repositories.Transaction;
using ComputerStore_MSSQL.Data.Repositories.User;

namespace ComputerStore_MSSQL.Data.Repositories.Factory
{
    public sealed class RepositoryFactory : IRepositoryFactory
    {
        public IRepository Instantiate<TEntity>(ComputerStoreDbContext context)
        {
            return typeof(TEntity).Name switch
            {
                nameof(UserEntity) => new UserRepository(context),
                nameof(ProductEntity) => new ProductRepository(context),
                nameof(CartEntity) => new CartRepository(context),
                nameof(TransactionEntity) => new TransactionRepository(context),
                nameof(ReviewEntity) => new ReviewRepository(context),
                _ => throw new UnsupportedRepositoryTypeException(typeof(TEntity).Name)
            };
        }
    }
}

