using ComputerStore.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.ViewModels
{
    public class ChangePasswordViewModel
    {
        [MinLength(8, ErrorMessage = "Старий пароль повинен містити щонайменше 8 символів.")]
        public string OldPassword { get; set; } = default!;
        [DifferentPasswords(nameof(OldPassword))]
        [StrongPassword]
        public string NewPassword { get; set; } = default!;
        [Compare(nameof(NewPassword), ErrorMessage = "Новий пароль та пароль для підтвердження повинні збігатися.")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
