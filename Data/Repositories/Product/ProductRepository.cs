using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore_MSSQL.Data.Repositories.Product
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly ComputerStoreDbContext _context;
        public ProductRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(ProductEntity product)
        {
            product.ProductId = Guid.NewGuid();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveProductAsync(ProductEntity product)
        {
            var reviews = await _context.Reviews.Where(r => r.ProductId == product.ProductId).ToListAsync();
            _context.Reviews.RemoveRange(reviews);
            var cartProducts = await _context.Carts.Where(c => c.ProductId == product.ProductId).ToListAsync();
            _context.Carts.RemoveRange(cartProducts);
            var transactions = await _context.Transactions.Where(t => t.ProductId == product.ProductId).ToListAsync();
            _context.Transactions.RemoveRange(transactions);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Guid productId, ProductEntity updatedProduct)
        {
            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ProductEntity?> GetProductAsync(Guid productId)
            => await _context.Products
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        public async Task<IEnumerable<ProductEntity?>> GetAllProductsAsync()
            => await _context.Products
                .Include(p => p.Reviews)
                .ToListAsync();
        public string[] GetUniqueCategories() 
            => _context.Products.Select(p => p.Category).Distinct().ToArray();
    }
}
