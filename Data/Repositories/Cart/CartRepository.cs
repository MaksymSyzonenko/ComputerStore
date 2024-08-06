using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore_MSSQL.Data.Repositories.Cart
{
    public sealed class CartRepository : ICartRepository
    {
        private readonly ComputerStoreDbContext _context;
        public CartRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductEntity?>> GetCartForUserAsync(string userId)
        {
            var productsId = await _context.Carts
                .Where(cart => cart.UserId == userId)
                .Select(cart => cart.ProductId).ToListAsync();
            var products =  await _context.Products
                .Where(product => productsId.Contains(product.ProductId)).ToListAsync();
            return products;
        }
        public async Task<bool> AddProductToCartAsync(string userId, Guid productId)
        {
            bool productExists = await _context.Carts.AnyAsync(cart => cart.UserId == userId && cart.ProductId == productId);
            if (!productExists)
            {
                _context.Carts.Add(new CartEntity { CartId = Guid.NewGuid(), UserId = userId, ProductId = productId });
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task RemoveProductFromCartAsync(string userId, Guid productId)
        {
            var cartItem = await _context.Carts.FirstOrDefaultAsync(cart => cart.UserId == userId && cart.ProductId == productId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
