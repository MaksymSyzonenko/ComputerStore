using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore_MSSQL.Data.Repositories.Review
{
    public sealed class ReviewRepository : IReviewRepository
    {
        private readonly ComputerStoreDbContext _context;
        public ReviewRepository(ComputerStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddReviewAsync(ReviewEntity review)
        {
            review.ReviewId = Guid.NewGuid();
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveReviewAsync(ReviewEntity review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateReviewAsync(Guid reviewId, ReviewEntity updatedReview)
        {
            var existingReview = await _context.Reviews.FindAsync(reviewId);
            if (existingReview != null)
            {
                _context.Entry(existingReview).CurrentValues.SetValues(updatedReview!);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ReviewEntity?> GetReviewAsync(Guid reviewId)
            => await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .SingleOrDefaultAsync(r => r.ReviewId == reviewId)!;

        public async Task<IEnumerable<ReviewEntity?>> GetAllReviewsAsync() 
            => await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToListAsync();

        public async Task<IEnumerable<ReviewEntity?>> GetAllReviewsForUserAsync(string userId)
            => await _context.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToListAsync();
    }
}
