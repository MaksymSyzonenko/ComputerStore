using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.Review
{
    public class ReviewAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
