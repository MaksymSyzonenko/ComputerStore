using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore_MSSQL.Data.Repositories.Transaction
{
    public sealed class TransactionRepository : ITransactionRepository
    {
        private readonly ComputerStoreDbContext _context;
        public TransactionRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddTransactionAsync(TransactionEntity transaction)
        {
            transaction.TransactionId = Guid.NewGuid();
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveTransactionAsync(TransactionEntity transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTransactionAsync(Guid transactionId, TransactionEntity updatedTransaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(transactionId);
            if (existingTransaction != null)
            {
                _context.Entry(existingTransaction).CurrentValues.SetValues(updatedTransaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TransactionEntity?> GetTransactionAsync(Guid transactionId) 
            => await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.Product)
                .SingleOrDefaultAsync(t => t.TransactionId == transactionId)!;

        public async Task<IEnumerable<TransactionEntity?>> GetAllTransactionsAsync() 
            => await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.Product)
                .ToListAsync();

        public async Task<IEnumerable<TransactionEntity?>> GetAllTransactionsForUserAsync(string userId)
            => await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .Include(t => t.Product)
                .ToListAsync();
    }
}
