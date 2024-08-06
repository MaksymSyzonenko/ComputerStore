using ComputerStore.Services.Cache.User;
using ComputerStore.Services.Cart;
using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Cart;
using ComputerStore_MSSQL.Data.Repositories.Product;
using ComputerStore_MSSQL.Data.Repositories.Transaction;
using ComputerStore_MSSQL.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.Cart
{
    public class CartController : Controller
    {
        private readonly UserCacheService _userCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private IEnumerable<ProductEntity> _orderProducts;
        private readonly ICartService _cartService;
        public CartController(UserCacheService userCacheService, IUnitOfWork unitOfWork, ICartService cartService)
        {
            _userCacheService = userCacheService;
            _unitOfWork = unitOfWork;
            _cartService = cartService;
            _orderProducts = new List<ProductEntity>();
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                Response.Cookies.Append("ErrorIndexMessage", "Щоб відкрити кошик, потрібно увійти в свій акаунт.");
                return Redirect("~/Home/Index");
            }
            string userId = _userCacheService.GetUserId(User.Identity!.Name!);
            _orderProducts = (await ((ICartRepository)_unitOfWork.Repository<CartEntity>()).GetCartForUserAsync(userId))!;
            return View(_orderProducts);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, bool inStock)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                Response.Cookies.Append("ErrorIndexMessage", "Щоб додати товар до кошику, потрібно увійти в свій акаунт.");
                return Redirect("~/Home/Index");
            }
            string userId = _userCacheService.GetUserId(User.Identity!.Name!);
            if (inStock)
            {
                if (await ((ICartRepository)_unitOfWork.Repository<CartEntity>()).AddProductToCartAsync(userId, productId))
                    Response.Cookies.Append("SuccessMessage", "Товар успішно додано до вашого кошику.");
                else
                    Response.Cookies.Append("ErrorIndexMessage", "Товар вже наявний у вашому кошику.");
            }
            else
                Response.Cookies.Append("ErrorIndexMessage", "Товару нажаль зараз не має у наявності.");
            return Redirect("~/Home/Index");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            string userId = _userCacheService.GetUserId(User.Identity!.Name!);
            await ((ICartRepository)_unitOfWork.Repository<CartEntity>()).RemoveProductFromCartAsync(userId, productId);
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public async Task<IActionResult> BuyProducts()
        {
            string userId = _userCacheService.GetUserId(User.Identity!.Name!);
            _orderProducts = (await ((ICartRepository)_unitOfWork.Repository<CartEntity>()).GetCartForUserAsync(userId))!;
            if (!_orderProducts.All(p => p.InStock))
            {
                Response.Cookies.Append("ErrorIndexMessage", "Одного або декількох товарів з вашого кошику немає в наявності, будь ласка видаліть їх з кошику.");
                return Redirect("~/Home/Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BuyProducts(OrderViewModel model)
        {
            string userId = _userCacheService.GetUserId(User.Identity!.Name!);
            await _cartService.CreateOrder(userId, model);
            Response.Cookies.Append("SuccessMessage", "Замовлення успішно відправлене, очікуйте дзвінка менеджера.");
            return Redirect("~/Home/Index");
        }
    }
}
