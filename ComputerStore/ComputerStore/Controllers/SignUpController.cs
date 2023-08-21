using ComputerStore.DataBaseInteraction;
using ComputerStore.Models;
using ComputerStore.Services;
using ComputerStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    public class SignUpController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public SignUpController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, "User");
                    Response.Cookies.Append("SuccessMessage", $"Реєстрація акаунту {user.UserName} успішна.");
                    return Redirect("~/Home/Index");
                }
                TempData["ErrorRegisterMessage"] = "Користувач з таким логіном вже існує.";
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/Home/Index");
        }
    }
}
