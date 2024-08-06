using ComputerStore.Models.DTO;
using ComputerStore.Services.Cache.User;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Review;
using ComputerStore_MSSQL.Data.UnitOfWork;
using System.Globalization;

namespace ComputerStore.Services.Review
{
    public sealed class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserCacheService _userCacheService;
        public ReviewService(IUnitOfWork unitOfWork, UserCacheService userCacheService)
        {
            _unitOfWork = unitOfWork;
            _userCacheService = userCacheService;
        }
        public async Task<ReviewAddResultDto> AddReview(ReviewAddDto dto)
        {
            if (string.IsNullOrEmpty(dto.ReviewRating))
                return BuildResult(false, "Рейтинг не може бути пустим.", dto.ProductId);
            string userId = _userCacheService.GetUserId(dto.UserName);
            double.TryParse(dto.ReviewRating, NumberStyles.Float, CultureInfo.InvariantCulture, out double rating);
            dto.ReviewComment ??= string.Empty;
            await ((IReviewRepository)_unitOfWork.Repository<ReviewEntity>())
                .AddReviewAsync(
                new ReviewEntity
                {
                    UserId = userId,
                    Comment = dto.ReviewComment,
                    Rating = rating,
                    ProductId = dto.ProductId,
                    ReviewDate = DateTime.Now
                });
            return BuildResult(true, "Відгук додано успішно.", Guid.Empty);
        }
        private static ReviewAddResultDto BuildResult(bool succeeded, string message, Guid productId)
            => new(succeeded, message, productId);

    }
}
