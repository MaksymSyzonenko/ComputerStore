using ComputerStore.Services.Cache.User;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Review;
using ComputerStore_MSSQL.Data.Repositories.Transaction;
using ComputerStore_MSSQL.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.AccountData.DataGet
{
    public class GetAccountDataController : Controller
    {
        private readonly UserCacheService _userCacheService;
        private readonly IUnitOfWork _unitOfWork;
        public GetAccountDataController(UserCacheService userCacheService, IUnitOfWork unitOfWork)
        {
            _userCacheService = userCacheService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> OrdersData()
        {
            string userId = _userCacheService.GetUserId(User.Identity!.Name!);
            var userTransactions = await ((ITransactionRepository)_unitOfWork.Repository<TransactionEntity>()).GetAllTransactionsForUserAsync(userId);
            return View(userTransactions);
        }
        [HttpGet]
        public async Task<IActionResult> ReviewsData()
        {
            string userId = _userCacheService.GetUserId(User.Identity!.Name!);
            var userReviews = await ((IReviewRepository)_unitOfWork.Repository<ReviewEntity>()).GetAllReviewsForUserAsync(userId);
            return View(userReviews);
        }
    }
}
