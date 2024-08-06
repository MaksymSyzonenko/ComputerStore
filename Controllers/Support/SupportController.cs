using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.Support
{
    public class SupportController : Controller
    {
        [HttpGet]
        public IActionResult Support() => View();
    }
}
