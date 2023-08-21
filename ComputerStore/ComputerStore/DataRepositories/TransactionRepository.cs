using ComputerStore.DataBaseInteraction;
using ComputerStore.Interfaces;
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataRepositories
{
    public class TransactionRepository : IRepository<Transaction, Guid>
    {
        private readonly ComputerStoreDbContext _context;
        public TransactionRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddItemAsync(Transaction transaction)
        {
            transaction.TransactionID = Guid.NewGuid();
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveItemAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateItemAsync(Guid transactionID, Transaction updatedTransaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(transactionID);
            if (existingTransaction != null)
            {
                _context.Entry(existingTransaction).CurrentValues.SetValues(updatedTransaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Transaction?> GetItemAsync(Guid transactionID) => await _context.Transactions
             .Include(t => t.User)
             .Include(t => t.Product)
             .SingleOrDefaultAsync(t => t.TransactionID == transactionID)!;

        public async Task<IEnumerable<Transaction?>> GetAllItemsAsync() => await _context.Transactions
            .Include(t => t.User)
            .Include(t => t.Product)
            .ToListAsync();

    }
}
