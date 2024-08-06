using ComputerStore_MSSQL.Data.Entities;

namespace ComputerStore_MSSQL.Data.Repositories.Transaction
{
    public interface ITransactionRepository : IRepository
    {
        Task AddTransactionAsync(TransactionEntity transaction);
        Task RemoveTransactionAsync(TransactionEntity transaction);
        Task UpdateTransactionAsync(Guid transactionId, TransactionEntity updatedTransaction);
        Task<TransactionEntity?> GetTransactionAsync(Guid transactionId);
        Task<IEnumerable<TransactionEntity?>> GetAllTransactionsAsync();
        Task<IEnumerable<TransactionEntity?>> GetAllTransactionsForUserAsync(string userId);
    }
}
