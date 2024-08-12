using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Models.Validators;

public class PhoneNumberValidationAttribute : ValidationAttribute
{
    private const string regExPattern = @"^7\d{10}$";
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            if (value is string phoneNumber)
            {
                Match match = Regex.Match(phoneNumber, regExPattern);
                if (!(match.Success && match.Value == phoneNumber))
                {
                    return new ValidationResult($"'{validationContext.MemberName}' expected to be phone number, but pattern doesn't match.");
                }
            }
        }

        return ValidationResult.Success;
    }
}
