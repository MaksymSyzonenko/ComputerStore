using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ComputerStore.Validation
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string password = value.ToString()!;
            if (password.Length < 8)
            {
                ErrorMessage = "Пароль повинен бути не менше 8 символів.";
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                ErrorMessage = "Пароль повинен містити щонайменше одну велику літеру.";
                return false;
            }
            if (!password.Any(char.IsLower))
            {
                ErrorMessage = "Пароль повинен містити щонайменше одну малу літеру.";
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                ErrorMessage = "Пароль повинен містити щонайменше одну цифру.";
                return false;
            }
            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+[\]{};':""\\|,.<>/?]"))
            {
                ErrorMessage = "Пароль повинен містити щонайменше один спеціальний символ.";
                return false;
            }
            return true;
        }
    }

}
