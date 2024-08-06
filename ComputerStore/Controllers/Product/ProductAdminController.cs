using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.Product
{
    public class ProductAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
