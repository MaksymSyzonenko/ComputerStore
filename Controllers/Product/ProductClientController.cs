using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Product;
using ComputerStore_MSSQL.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.Product
{
    public class ProductClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductClientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> ShowProduct(ProductViewModel productViewModel)
        {
            var product = await ((IProductRepository)_unitOfWork.Repository<ProductEntity>()).GetProductAsync(productViewModel.ProductId);
            return View(product);
        }
    }
}
