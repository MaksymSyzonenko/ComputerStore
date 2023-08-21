using ComputerStore.DataBaseInteraction;
using ComputerStore.Interfaces;
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataRepositories
{
    public class ReviewRepository : IRepository<Review, Guid>
    {
        private readonly ComputerStoreDbContext _context;
        public ReviewRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddItemAsync(Review review)
        {
            review.ReviewID = Guid.NewGuid();
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveItemAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateItemAsync(Guid reviewID, Review updatedReview)
        {
            var existingReview = await _context.Reviews.FindAsync(reviewID);
            if (existingReview != null)
            {
                _context.Entry(existingReview).CurrentValues.SetValues(updatedReview!);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Review?> GetItemAsync(Guid reviewID) => await _context.Reviews
            .Include(r => r.Product)
            .Include(r => r.User)
            .SingleOrDefaultAsync(r => r.ReviewID == reviewID)!;

        public async Task<IEnumerable<Review?>> GetAllItemsAsync() => await _context.Reviews
            .Include(r => r.Product)
            .Include(r => r.User)
            .ToListAsync();
    }
}
