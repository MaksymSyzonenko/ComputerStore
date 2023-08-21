using ComputerStore.Interfaces;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ComputerStore.ViewModels;
using ComputerStore.Services;
using ComputerStore.DataRepositories;

namespace ComputerStore.Controllers
{
    public class HamburgerMenuController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly UserCacheService _userCacheService;
        private readonly IRepository<Transaction, Guid> _transactionRepository;
        private readonly IRepository<Review, Guid> _reviewRepository;
        public HamburgerMenuController(UserManager<User> userManager, UserCacheService userCacheService, IRepository<Transaction, Guid> transactionRepository, IRepository<Review, Guid> reviewRepository) 
        {
            _userManager = userManager;
            _userCacheService = userCacheService;
            _transactionRepository = transactionRepository;
            _reviewRepository = reviewRepository;
        }
        [HttpGet]
        public IActionResult ChangePassword() => View();
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _userManager.ChangePasswordAsync(user!, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    Response.Cookies.Append("SuccessMessage", "Пароль успішно змінено.");
                    return Redirect("~/Home/Index");
                }
                else
                {
                    TempData["ErrorChangePasswordMessage"] = "Не правильний старий пароль.";
                    return View();
                }
            }
            else
                TempData["ErrorChangePasswordMessage"] = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
            return View();
        }
        [HttpGet]
        public IActionResult ChangeAccountData() => View();
        [HttpPost]
        public async Task<IActionResult> ChangeAccountData(ChangeAccountDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (!(await _userManager.CheckPasswordAsync(user!, model.PasswordForConfirm)))
                {
                    TempData["ErrorChangeAccountDataMessage"] = "Не правильний пароль.";
                    return View();
                }
                user!.FirstName = model.FirstName;
                user!.LastName = model.LastName;
                user!.Email = model.Email;
                await _userManager.UpdateAsync(user);
                Response.Cookies.Append("SuccessMessage", "Дані успішно змінено.");
                return Redirect("~/Home/Index");
            }
            else
                TempData["ErrorChangeAccountDataMessage"] = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> OrdersData()
        {
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            var transactions = await _transactionRepository.GetAllItemsAsync();
            var userTransactions = transactions.Where(t => t?.UserID == userID).ToList();
            return View(userTransactions);
        }
        [HttpGet]
        public async Task<IActionResult> ReviewsData()
        {
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            var reviews = await _reviewRepository.GetAllItemsAsync();
            var userReviews = reviews.Where(r => r?.UserID == userID).ToList();
            return View(userReviews);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveReview(Guid reviewID)
        {
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            await _reviewRepository.RemoveItemAsync((await _reviewRepository.GetItemAsync(reviewID))!);
            var reviews = await _reviewRepository.GetAllItemsAsync();
            var userReviews = reviews.Where(r => r?.UserID == userID).ToList();
            return View("ReviewsData", userReviews);
        }
        [HttpGet]
        public IActionResult Support() => View();
    }
}
