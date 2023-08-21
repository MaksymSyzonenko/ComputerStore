using ComputerStore.DataRepositories;
using ComputerStore.Interfaces;
using ComputerStore.Models;
using ComputerStore.Services;
using ComputerStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace ComputerStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserCacheService _userCacheService;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Review, Guid> _reviewRepository;
        private readonly string[] _categories;
        public HomeController(UserCacheService userCacheService, IRepository<Product, Guid> productRepository, IRepository<Review, Guid> reviewRepository)
        {
            _userCacheService = userCacheService;
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _categories = ((ProductRepository)_productRepository).GetUniqueCategories();
        }
        public async Task<IActionResult> Index(string searchQuery, decimal? minPrice, decimal? maxPrice, double? minRating, double? maxRating, string category, bool inStock, int page = 1, int pageSize = 8)
        {
            ViewBag.NoResults = false;
            var filteredProducts = await FilterService.ProductFilter(searchQuery, minPrice, maxPrice, minRating, maxRating, category, inStock, (ProductRepository)_productRepository);

            #region PageProductsInteraction

            var productsForPage = filteredProducts.Skip((page - 1) * pageSize).Take(pageSize);
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in productsForPage)
            {
                var averageRating = product.Reviews != null && product.Reviews.Any() ? product.Reviews.Average(r => r.Rating) : 0;
                var productViewModel = new ProductViewModel
                {
                    ProductID = product.ProductID,
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
        [HttpGet]
        public async Task<IActionResult> ShowProduct(ProductViewModel productViewModel)
        {
            var product = await _productRepository.GetItemAsync(productViewModel.ProductID);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(string reviewComment, string reviewRating, Guid productID)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                Response.Cookies.Append("ErrorShowProductMessage", "Щоб залишати відгуки потрібно увійти в свій акаунт.");
                return RedirectToAction("ShowProduct", new ProductViewModel { ProductID = productID });
            }
            string userID = _userCacheService.GetUserId(User.Identity!.Name!);
            if (string.IsNullOrEmpty(reviewRating)) 
            {
                Response.Cookies.Append("ErrorShowProductMessage", "Рейтинг не може бути пустим.");
                return RedirectToAction("ShowProduct", new ProductViewModel { ProductID = productID });
            }
            double.TryParse(reviewRating, NumberStyles.Float, CultureInfo.InvariantCulture, out double rating);
            reviewComment ??= string.Empty;
            await _reviewRepository.AddItemAsync(new Review { UserID = userID, Comment = reviewComment, Rating = rating, ProductID = productID, ReviewDate = DateTime.Now });
            Response.Cookies.Append("SuccessMessage", "Відгук додано успішно.");
            return Redirect("~/Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}