using ComputerStore_MSSQL.Data.Entities;

namespace ComputerStore_MSSQL.Data.Repositories.Product
{
    public interface IProductRepository : IRepository
    {
        Task AddProductAsync(ProductEntity product);
        Task RemoveProductAsync(ProductEntity product);
        Task UpdateProductAsync(Guid productId, ProductEntity updatedProduct);
        Task<ProductEntity?> GetProductAsync(Guid productId);
        Task<IEnumerable<ProductEntity?>> GetAllProductsAsync();
        string[] GetUniqueCategories();
    }
}
