using ComputerStore.DataBaseInteraction;
using ComputerStore.Interfaces;
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataRepositories
{
    public class UserRepository : IRepository<User, string>
    {
        private readonly ComputerStoreDbContext _context;
        public UserRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddItemAsync(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveItemAsync(User user)
        {
            var reviews = await _context.Reviews.Where(r => r.UserID == user.Id).ToListAsync();
            _context.Reviews.RemoveRange(reviews);
            var cartItems = await _context.Carts.Where(c => c.UserID == user.Id).ToListAsync();
            _context.Carts.RemoveRange(cartItems);
            var transactions = await _context.Transactions.Where(t => t.UserID == user.Id).ToListAsync();
            _context.Transactions.RemoveRange(transactions);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateItemAsync(string userID, User updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(userID);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(updatedUser);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<User?> GetItemAsync(string userID)
        {
            var user = await _context.Users.FindAsync(userID);
            if (user != null)
            {
                await _context.Entry(user)
                    .Collection(u => u.Transactions)
                    .LoadAsync();

                await _context.Entry(user)
                    .Collection(u => u.Reviews)
                    .LoadAsync();
            }
            return user!;
        }
        public async Task<IEnumerable<User?>> GetAllItemsAsync()
        {
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                await _context.Entry(user)
                    .Collection(u => u.Transactions)
                    .LoadAsync();

                await _context.Entry(user)
                    .Collection(u => u.Reviews)
                    .LoadAsync();
            }
            return users;
        }
    }
}
