using ComputerStore.DataBaseInteraction;
using ComputerStore.Interfaces;
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataRepositories
{
    public class ProductRepository : IRepository<Product, Guid>
    {
        private readonly ComputerStoreDbContext _context;
        public ProductRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddItemAsync(Product product)
        {
            product.ProductID = Guid.NewGuid();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task AddItemsAsync(params Product[] products)
        {
            products.ToList().ForEach(p => p.ProductID = Guid.NewGuid());
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveItemAsync(Product product)
        {
            var reviews = await _context.Reviews.Where(r => r.ProductID == product.ProductID).ToListAsync();
            _context.Reviews.RemoveRange(reviews);
            var cartItems = await _context.Carts.Where(c => c.ProductID == product.ProductID).ToListAsync();
            _context.Carts.RemoveRange(cartItems);
            var transactions = await _context.Transactions.Where(t => t.ProductID == product.ProductID).ToListAsync();
            _context.Transactions.RemoveRange(transactions);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateItemAsync(Guid productID, Product updatedProduct)
        {
            var existingProduct = await _context.Products.FindAsync(productID);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Product?> GetItemAsync(Guid productID)
        {
            return await _context.Products
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.ProductID == productID);
        }
        public async Task<IEnumerable<Product?>> GetAllItemsAsync()
        {
            return await _context.Products
                .Include(p => p.Reviews)
                .ToListAsync();
        }
        public string[] GetUniqueCategories() => _context.Products.Select(p => p.Category).Distinct().ToArray();
    }
}
