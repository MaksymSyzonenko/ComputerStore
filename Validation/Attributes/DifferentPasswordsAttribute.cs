using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Validation.Attributes
{
    public class DifferentPasswordsAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;

        public DifferentPasswordsAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyValue = validationContext.ObjectType.GetProperty(_otherProperty)?.GetValue(validationContext.ObjectInstance);

            if (value != null && value.Equals(otherPropertyValue))
                return new ValidationResult("Старий та новий паролі не повинні співпадати.");

            return ValidationResult.Success!;
        }
    }
}
