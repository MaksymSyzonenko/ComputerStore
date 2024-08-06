using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore_MSSQL.Data.Repositories.User
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ComputerStoreDbContext _context;
        public UserRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(UserEntity user)
        {
            user.Id = Guid.NewGuid().ToString();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserAsync(string userId, UserEntity updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(updatedUser);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<UserEntity?> GetUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
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
        public async Task<IEnumerable<UserEntity?>> GetAllUsersAsync()
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
