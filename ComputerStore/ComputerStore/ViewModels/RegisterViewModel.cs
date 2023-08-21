using ComputerStore.Validation;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.ViewModels
{
    public class RegisterViewModel
    {
        [MinLength(4, ErrorMessage = "Логін повинен бути не менше 4 символів.")]
        [StartsWithLetter]
        public string UserName { get; set; }
        [StrongPassword]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Паролі повинні збігатися.")]
        public string ConfirmPassword { get; set; }
        [OnlyLetters]
        [MinLength(3, ErrorMessage = "Ім'я повинно містити щонайменше 3 літери.")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [OnlyLetters]
        [MinLength(3, ErrorMessage = "Прізвище повинно містити щонайменше 3 літери.")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Пошта повинна бути у правильному форматі.")]
        public string Email { get; set; }
    }
}
