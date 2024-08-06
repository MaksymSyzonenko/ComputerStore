using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Validation.Attributes
{
    public class StartsWithLetterAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            string input = value.ToString()!;
            if (char.IsLetter(input[0]))
                return true;
            ErrorMessage = "Логін повинен починатися з літери.";
            return false;
        }
    }
}
