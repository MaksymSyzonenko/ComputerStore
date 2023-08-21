using ComputerStore.DataRepositories;
using ComputerStore.Interfaces;
using ComputerStore.Models;
using ComputerStore.Services;
using ComputerStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    public class CartController : Controller
    {
        private readonly UserCacheService _userCacheService;
        private readonly CartRepository _cartRepository;
        private readonly IRepository<Transaction, Guid> _transactionRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private IEnumerable<Product> _orderProducts;
        public CartController(UserCacheService userCacheService, CartRepository cartRepository, IRepository<Transaction, Guid> transactionRepository, IRepository<Product, Guid> productRepository)
        {
            _userCacheService = userCacheService;
            _cartRepository = cartRepository;
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
            _orderProducts = new List<Product>();
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                Response.Cookies.Append("ErrorIndexMessage", "Щоб відкрити кошик, потрібно увійти в свій акаунт.");
                return Redirect("~/Home/Index");
            }
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            _orderProducts = (await _cartRepository.GetCartForUserAsync(userID))!;
            return View(_orderProducts);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productID, bool inStock)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                Response.Cookies.Append("ErrorIndexMessage", "Щоб додати товар до кошику, потрібно увійти в свій акаунт.");
                return Redirect("~/Home/Index");
            }
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            if (inStock)
            {
                if (await _cartRepository.AddItemAsync(userID, productID))
                    Response.Cookies.Append("SuccessMessage", "Товар успішно додано до вашого кошику.");
                else
                    Response.Cookies.Append("ErrorIndexMessage", "Товар вже наявний у вашому кошику.");
            }
            else
                Response.Cookies.Append("ErrorIndexMessage", "Товару нажаль зараз не має у наявності.");
            return Redirect("~/Home/Index");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid productID)
        {
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            await _cartRepository.RemoveItemAsync(userID, productID);
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public async Task<IActionResult> BuyProducts() 
        {
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            _orderProducts = (await _cartRepository.GetCartForUserAsync(userID))!;
            if(!_orderProducts.All(p => p.InStock))
            {
                Response.Cookies.Append("ErrorIndexMessage", "Одного або декількох товарів з вашого кошику немає в наявності, будь ласка видаліть їх з кошику.");
                return Redirect("~/Home/Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BuyProducts(OrderViewModel model)
        {
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            Guid orderID = Guid.NewGuid();
            _orderProducts = (await _cartRepository.GetCartForUserAsync(userID))!;
            foreach (var p in _orderProducts)
            {
                await _transactionRepository.AddItemAsync(new Transaction
                {
                    UserID = userID.ToString(),
                    ProductID = p.ProductID,
                    ShippingAddress = model.ShippingAddress,
                    ShippingCity = model.ShippingCity,
                    ShippingPostalCode = model.ShippingPostalCode,
                    TransactionDate = DateTime.Now,
                    OrderID = orderID
                });
                p.InStock = false;
                await _productRepository.UpdateItemAsync(p.ProductID, p);
            }
            await _cartRepository.ClearItemsAsync(userID);
            Response.Cookies.Append("SuccessMessage", "Замовлення успішно відправлене, очікуйте дзвінка менеджера.");
            return Redirect("~/Home/Index");
        }
    }
}
