using ComputerStore.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ComputerStore.ViewModels
{
    public class ChangePasswordViewModel
    {
        [MinLength(8, ErrorMessage = "Старий пароль повинен містити щонайменше 8 символів.")]
        public string OldPassword { get; set; }
        [DifferentPasswords(nameof(OldPassword))]
        [StrongPassword]
        public string NewPassword { get; set; }
        [Compare(nameof(NewPassword), ErrorMessage = "Новий пароль та пароль для підтвердження повинні збігатися.")]
        public string ConfirmPassword { get; set;}
    }
}
