using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Models.Validators;

public class EmailValidationAttribute : ValidationAttribute
{
    private const string regExPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            if (value is string email)
            {
                Match match = Regex.Match(email, regExPattern);
                if (!(match.Success && match.Value == email))
                {
                    return new ValidationResult($"'{validationContext.MemberName}' expected to be email, but pattern doesn't match.");
                }
            }
        }

        return ValidationResult.Success;
    }
}
