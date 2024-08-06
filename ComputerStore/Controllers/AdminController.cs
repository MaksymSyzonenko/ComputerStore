using ComputerStore.Services.Filter.FilterServices;
using ComputerStore.Services.Image;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Product;
using ComputerStore_MSSQL.Data.Repositories.Review;
using ComputerStore_MSSQL.Data.Repositories.Transaction;
using ComputerStore_MSSQL.Data.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Controllers
{
    [Authorize(Policy = "ManagerPolicy")]
    public class AdminController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(UserManager<UserEntity> userManager, IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult AdminIndex() => View();
        [HttpGet]
        public IActionResult GetOut() =>  Redirect("~/Home/Index");
        [HttpGet]
        public async Task<IActionResult> ProductsManagement(string searchQuery)
        {
            var products = await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).GetAllProductsAsync();
            products = FilterService.ProductFilter(products!, searchQuery);
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ReviewsManagement(string searchQuery)
        {
            var reviews = await ((IReviewRepository)_unitOfWork.Repository<ReviewEntity>()).GetAllReviewsAsync();
            reviews = FilterService.ReviewFilter(reviews!, searchQuery).OrderByDescending(r => r.ReviewDate).ToList();
            return View(reviews);
        }
        [HttpGet]
        public async Task<IActionResult> TransactionsManagement(string searchQuery)
        {
            var transactions = await ((ITransactionRepository)_unitOfWork.Repository<TransactionEntity>()).GetAllTransactionsAsync();
            transactions = FilterService.TransactionsFilter(transactions!, searchQuery).OrderByDescending(t => t.TransactionDate).ToList();
            return View(transactions);
        }
        [HttpGet]
        public async Task<IActionResult> UsersManagement(string searchQuery) 
        {
            var users = FilterService.UserFilter((await _userManager.Users.ToListAsync())!, searchQuery);
            return View(users);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManagersManagement(string searchQuery)
        {
            var managers = (IEnumerable<UserEntity>)await _userManager.GetUsersInRoleAsync("Manager");
            managers = FilterService.UserFilter(managers!, searchQuery);
            return View(managers);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddManagerRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user!, "Manager");
            return Redirect("~/Admin/UsersManagement");     
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveManager(string managerId)
        {
            var user = await _userManager.FindByIdAsync(managerId);
            await _userManager.RemoveFromRoleAsync(user!, "Manager");
            return Redirect("~/Admin/ManagersManagement");
        }
        [HttpGet]
        public IActionResult AddNewProduct() => View();
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductEntity product, IFormFile imageUpload)
        {
            if (imageUpload != null && imageUpload.Length > 0)
                product.Image = "/Images/" + ImageService.DownloadImage(imageUpload);
            await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).AddProductAsync(product);
            return Redirect("~/Admin/ProductsManagement");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(Guid productId)
        {
            var product = await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).GetProductAsync(productId);
            return View(product);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductEntity product, IFormFile imageUpload)
        {
            var existingProduct = await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).GetProductAsync(product.ProductId);

            if (existingProduct == null)
                return NotFound();
            if (imageUpload != null && imageUpload.Length > 0)
                product.Image = "/Images/" + ImageService.UpdateImage(existingProduct.Image, imageUpload);
            else
                product.Image = existingProduct.Image;

            await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).UpdateProductAsync(product.ProductId, product);
            return Redirect("~/Admin/ProductsManagement");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(Guid productId)
        {
            var product = await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).GetProductAsync(productId);
            if (product != null)
            {
                ImageService.DeleteImage(product.Image, _webHostEnvironment);
                await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).RemoveProductAsync(product);
            }
            return Redirect("~/Admin/ProductsManagement");
        }
    }
}
