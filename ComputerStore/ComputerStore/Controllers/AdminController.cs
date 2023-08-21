using ComputerStore.DataBaseInteraction;
using ComputerStore.DataRepositories;
using ComputerStore.Interfaces;
using ComputerStore.Models;
using ComputerStore.Services;
using ComputerStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ComputerStore.Controllers
{
    [Authorize(Policy = "ManagePolicy")]
    public class AdminController : Controller
    {
        // переделать методы под get, post, delete, update
        // система ролей админа
        // изменить суть контроллеров, всё структурировать по соответствующим контроллерам
        //
        //
        //
        //
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Review, Guid> _reviewRepository;
        private readonly IRepository<Transaction, Guid> _transactionRepository;
        public AdminController(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IRepository<Transaction, Guid> transactionRepository, IRepository<Product, Guid> productRepository, IRepository<Review, Guid> reviewRepository)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _transactionRepository = transactionRepository;
        }
        [HttpGet]
        public IActionResult AdminIndex() => View();
        [HttpGet]
        public IActionResult GetOut() =>  Redirect("~/Home/Index");
        [HttpGet]
        public async Task<IActionResult> ProductsManagement(string searchQuery)
        {
            var products = FilterService.ProductFilter((await _productRepository.GetAllItemsAsync())!, searchQuery);
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ReviewsManagement(string searchQuery)
        {
            var reviews = FilterService.ReviewFilter((await _reviewRepository.GetAllItemsAsync())!, searchQuery).OrderByDescending(r => r.ReviewDate).ToList();
            return View(reviews);
        }
        [HttpGet]
        public async Task<IActionResult> TransactionsManagement(string searchQuery)
        {
            var transactions = FilterService.TransactionsFilter((await _transactionRepository.GetAllItemsAsync())!, searchQuery).OrderByDescending(t => t.TransactionDate).ToList();
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
            var managers = (IEnumerable<User>)await _userManager.GetUsersInRoleAsync("Manager");
            managers = FilterService.UserFilter(managers!, searchQuery);
            return View(managers);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddManagerRole(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            await _userManager.AddToRoleAsync(user!, "Manager");
            return Redirect("~/Admin/UsersManagement");     
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveManager(string managerID)
        {
            var user = await _userManager.FindByIdAsync(managerID);
            await _userManager.RemoveFromRoleAsync(user!, "Manager");
            return Redirect("~/Admin/ManagersManagement");
        }
        [HttpGet]
        public IActionResult AddNewProduct() => View();
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile imageUpload)
        {
            if (imageUpload != null && imageUpload.Length > 0)
                product.Image = "/Images/" + ImageService.DownloadImage(imageUpload);
            await _productRepository.AddItemAsync(product);
            return Redirect("~/Admin/ProductsManagement");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(Guid productID)
        {
            var product = await _productRepository.GetItemAsync(productID);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product, IFormFile imageUpload)
        {
            var existingProduct = await _productRepository.GetItemAsync(product.ProductID);

            if (existingProduct == null)
                return NotFound();
            if (imageUpload != null && imageUpload.Length > 0)
                product.Image = "/Images/" + ImageService.UpdateImage(existingProduct.Image, imageUpload);
            else
                product.Image = existingProduct.Image;

            await _productRepository.UpdateItemAsync(product.ProductID, product);
            return Redirect("~/Admin/ProductsManagement");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveProduct(Guid productID)
        {
            var product = await _productRepository.GetItemAsync(productID);
            if (product != null)
            {
                ImageService.DeleteImage(product.Image, _webHostEnvironment);
                await _productRepository.RemoveItemAsync(product);
            }
            return Redirect("~/Admin/ProductsManagement");
        }
    }
}
