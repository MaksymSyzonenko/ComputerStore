using ComputerStore.Models.DTO;
using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Services.Register
{
    public sealed class RegisterService : IRegisterService<RegisterViewModel, RegisterResultDto>
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        public RegisterService(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<RegisterResultDto> Register(RegisterViewModel dto)
        {
            var user = new UserEntity
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                await _userManager.AddToRoleAsync(user, "User");
                return BuildResult(true, $"Реєстрація акаунту {user.UserName} успішна.");
            }
            return BuildResult(false, "Користувач з таким логіном вже існує.");
        }
        private static RegisterResultDto BuildResult(bool succeeded, string message)
            => new(succeeded, message);
    }
}
