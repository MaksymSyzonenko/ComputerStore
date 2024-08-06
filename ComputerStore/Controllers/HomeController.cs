using ComputerStore.Models;
using ComputerStore.Services.Cache.User;
using ComputerStore.Services.Filter.FilterServices;
using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Product;
using ComputerStore_MSSQL.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ComputerStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserCacheService _userCacheService;
        private readonly string[] _categories;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(UserCacheService userCacheService, IUnitOfWork unitOfWork)
        {
            _userCacheService = userCacheService;
            _unitOfWork = unitOfWork;
            _categories = ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).GetUniqueCategories();
        }
        [HttpGet]
        public async Task<IActionResult> Index
            (string searchQuery, 
            decimal? minPrice, 
            decimal? maxPrice, 
            double? minRating, 
            double? maxRating, 
            string category, 
            bool inStock, 
            int page = 1, int 
            pageSize = 8)
        {


            ViewBag.NoResults = false;

            var products = await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).GetAllProductsAsync();
            var filteredProducts = FilterService.ProductFilter(products!, searchQuery, minPrice, maxPrice, minRating, maxRating, category, inStock);

            #region PageProductsInteraction

            var productsForPage = filteredProducts.Skip((page - 1) * pageSize).Take(pageSize);
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in productsForPage)
            {
                var averageRating = product.Reviews != null && product.Reviews.Any() ? product.Reviews.Average(r => r.Rating) : 0;
                var productViewModel = new ProductViewModel
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Image = product.Image,
                    ShortDescription = product.ShortDescription,
                    Category = product.Category,
                    Price = product.Price,
                    InStock = product.InStock,
                    AverageRating = averageRating
                };
                productViewModels.Add(productViewModel);
            }
            var pageCount = (int)Math.Ceiling((double)filteredProducts.Count() / pageSize);

            #endregion

            #region ViewBagInteraction

            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = page;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.MinRating = minRating;
            ViewBag.MaxRating = maxRating;
            ViewBag.Category = category;
            ViewBag.InStock = inStock;
            ViewBag.Categories = _categories;

            #endregion

            if (!productViewModels.Any())
                ViewBag.NoResults = true;

            return View(productViewModels);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}