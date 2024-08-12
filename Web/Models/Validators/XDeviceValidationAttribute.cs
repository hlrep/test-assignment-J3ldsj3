using System.ComponentModel.DataAnnotations;
using Web.Constants;

namespace Web.Models.Validators;

public class XDeviceValidationAttribute : ValidationAttribute
{
    private readonly HashSet<string> _xDeviceHeaderValues;
    public XDeviceValidationAttribute(params string[] xDeviceHeaderValues)
    {
        _xDeviceHeaderValues = new HashSet<string>(xDeviceHeaderValues);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var httpContextAccessor = validationContext.GetService<HttpContextAccessor>();
        var httpHeaderTextValue = httpContextAccessor?.HttpContext?.Request.Headers[HttpHeaders.X_DEVICE].ToString() ?? "";

        if (_xDeviceHeaderValues.Contains(httpHeaderTextValue))
        {
            if (value == null)
            {
                return new ValidationResult($"'{validationContext.MemberName}' field is required.");
            }
        }

        return ValidationResult.Success;
    }
}
