using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers.AccountData.DataUpdate
{
    public class UpdateAccountDataController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        public UpdateAccountDataController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult ChangePassword() => View();
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _userManager.ChangePasswordAsync(user!, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    Response.Cookies.Append("SuccessMessage", "Пароль успішно змінено.");
                    return Redirect("~/Home/Index");
                }
                else
                {
                    TempData["ErrorChangePasswordMessage"] = "Не правильний старий пароль.";
                    return View();
                }
            }
            else
                TempData["ErrorChangePasswordMessage"] = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
            return View();
        }
        [HttpGet]
        public IActionResult ChangeAccountData() => View();
        [HttpPost]
        public async Task<IActionResult> ChangeAccountData(ChangeAccountDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (!(await _userManager.CheckPasswordAsync(user!, model.PasswordForConfirm)))
                {
                    TempData["ErrorChangeAccountDataMessage"] = "Не правильний пароль.";
                    return View();
                }
                user!.FirstName = model.FirstName;
                user!.LastName = model.LastName;
                user!.Email = model.Email;
                await _userManager.UpdateAsync(user);
                Response.Cookies.Append("SuccessMessage", "Дані успішно змінено.");
                return Redirect("~/Home/Index");
            }
            else
                TempData["ErrorChangeAccountDataMessage"] = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
            return View();
        }
    }
}
