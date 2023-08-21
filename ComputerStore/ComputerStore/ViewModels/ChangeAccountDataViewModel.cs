using ComputerStore.Validation;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.ViewModels
{
    public class ChangeAccountDataViewModel
    {
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
        [MinLength(8, ErrorMessage = "Пароль повинен містити щонайменше 8 символів.")]
        public string PasswordForConfirm { get; set; }
    }
}
