using ComputerStore_MSSQL.Data.Entities;

namespace ComputerStore_MSSQL.Data.Repositories.Cart
{
    public  interface ICartRepository : IRepository
    {
        Task<IEnumerable<ProductEntity?>> GetCartForUserAsync(string userId);
        Task<bool> AddProductToCartAsync(string userId, Guid productId);
        Task RemoveProductFromCartAsync(string userId, Guid productId);
        Task ClearCartAsync(string userId);
    }
}
