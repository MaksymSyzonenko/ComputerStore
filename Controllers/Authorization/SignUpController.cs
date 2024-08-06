using ComputerStore.Models.DTO;
using ComputerStore.Services.Register;
using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.Authorization
{
    public class SignUpController : Controller
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRegisterService<RegisterViewModel, RegisterResultDto> _registerService;
        public SignUpController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IRegisterService<RegisterViewModel, RegisterResultDto> registerService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _registerService = registerService;
        }
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                RegisterResultDto result = await _registerService.Register(model);
                if (result.Succeeded)
                {
                    Response.Cookies.Append("SuccessMessage", result.Message);
                    return Redirect("~/Home/Index");
                }
                else
                    TempData["ErrorRegisterMessage"] = result.Message;
            }
            else
                TempData["ErrorRegisterMessage"] = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
            return View(model);
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                Response.Cookies.Append("SuccessMessage", $"Вхід в акаунт {model.UserName} успішний.");
                return Redirect("~/Home/Index");
            }
            else
            {
                TempData["ErrorLoginMessage"] = "Не правильний логін або пароль.";
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/Home/Index");
        }
    }
}
