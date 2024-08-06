using ComputerStore.Models.DTO;
using ComputerStore.Services.Cache.User;
using ComputerStore.Services.Review;
using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Review;
using ComputerStore_MSSQL.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.Review
{
    public class ReviewClientController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserCacheService _userCacheService;
        public ReviewClientController(IReviewService reviewService, IUnitOfWork unitOfWork, UserCacheService userCacheService)
        {
            _reviewService = reviewService;
            _unitOfWork = unitOfWork;
            _userCacheService = userCacheService;

        }
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewAddDto dto)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                Response.Cookies.Append("ErrorShowProductMessage", "Щоб залишати відгуки потрібно увійти в свій акаунт.");
                return ReturnToProduct(dto.ProductId);
            }
            dto.UserName = User.Identity!.Name!;
            ReviewAddResultDto result = await _reviewService.AddReview(dto);
            if (result.Succeeded)
            {
                Response.Cookies.Append("SuccessMessage", result.Message);
                return Redirect("~/Home/Index");
            }
            else
            {
                Response.Cookies.Append("ErrorShowProductMessage", result.Message);
                return ReturnToProduct(dto.ProductId);
            }
        }
        private IActionResult ReturnToProduct(Guid productId)
            => RedirectToAction("ShowProduct", "ProductClient", new ProductViewModel { ProductId = productId });
        [HttpPost]
        public async Task<IActionResult> RemoveReview(Guid reviewId)
        {
            string userId = _userCacheService.GetUserId(User.Identity!.Name!);
            var reviewRepository = (IReviewRepository)_unitOfWork.Repository<ReviewEntity>();
            await reviewRepository.RemoveReviewAsync((await reviewRepository.GetReviewAsync(reviewId))!);
            var userReviews = reviewRepository.GetAllReviewsForUserAsync(userId);
            return RedirectToAction("ReviewsData", "GetAccountData", userReviews);
        }
    }
}
