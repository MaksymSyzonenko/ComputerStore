using ComputerStore.DataBaseInteraction;
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataRepositories
{
    public class CartRepository
    {
        private readonly ComputerStoreDbContext _context;
        public CartRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product?>> GetCartForUserAsync(string userID)
        {
            var productsID = await _context.Carts
                .Where(cart => cart.UserID == userID)
                .Select(cart => cart.ProductID).ToListAsync();
            var products =  await _context.Products
                .Where(product => productsID.Contains(product.ProductID)).ToListAsync();
            return products;
        }
        public async Task<bool> AddItemAsync(string userID, Guid productID)
        {
            bool itemExists = await _context.Carts.AnyAsync(cart => cart.UserID == userID && cart.ProductID == productID);
            if (!itemExists)
            {
                _context.Carts.Add(new Cart { CartID = Guid.NewGuid(), UserID = userID, ProductID = productID });
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task RemoveItemAsync(string userID, Guid productID)
        {
            var cartItem = await _context.Carts.FirstOrDefaultAsync(cart => cart.UserID == userID && cart.ProductID == productID);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task ClearItemsAsync(string userID)
        {
            var cartItems = await _context.Carts.Where(c => c.UserID == userID).ToListAsync();
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
