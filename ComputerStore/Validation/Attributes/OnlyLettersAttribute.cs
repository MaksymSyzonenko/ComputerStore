using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ComputerStore.Validation.Attributes
{
    public class OnlyLettersAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            string input = value.ToString()!;
            string pattern = "^[a-zA-Zа-яА-Я]+$";
            return Regex.IsMatch(input, pattern);
        }
        public override string FormatErrorMessage(string name) => $"Поле {name} може містити тільки літери.";
    }
}
