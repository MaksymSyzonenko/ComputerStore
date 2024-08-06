using ComputerStore_MSSQL.Data.Entities;

namespace ComputerStore_MSSQL.Data.Repositories.Review
{
    public interface IReviewRepository : IRepository
    {
        Task AddReviewAsync(ReviewEntity review);
        Task RemoveReviewAsync(ReviewEntity review);
        Task UpdateReviewAsync(Guid reviewId, ReviewEntity updatedReview);
        Task<ReviewEntity?> GetReviewAsync(Guid reviewId);
        Task<IEnumerable<ReviewEntity?>> GetAllReviewsAsync();
        Task<IEnumerable<ReviewEntity?>> GetAllReviewsForUserAsync(string userId);
    }
}
