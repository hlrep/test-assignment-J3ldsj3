using Web.Constants;
using Web.Models.Validators;

namespace Web.Models;

public class User
{
    [XDeviceValidation(HttpHeaderXDevice.Web)]
    public string? Surname { get; set; }

    [XDeviceValidation(HttpHeaderXDevice.Mail, HttpHeaderXDevice.Web)]
    public string? Name { get; set; }

    [XDeviceValidation(HttpHeaderXDevice.Web)]
    public string? Patronymic { get; set; }

    [XDeviceValidation(HttpHeaderXDevice.Web)]
    public DateOnly? DateOfBirth { get; set; }

    [XDeviceValidation(HttpHeaderXDevice.Web)]
    public string? PassportNumber { get; set; }

    [XDeviceValidation(HttpHeaderXDevice.Web)]
    public string? PlaceOfBirth { get; set; }

    [XDeviceValidation(HttpHeaderXDevice.Mobile, HttpHeaderXDevice.Web)]
    [PhoneNumberValidation]
    public string? PhoneNumber { get; set; }

    [XDeviceValidation(HttpHeaderXDevice.Mail)]
    [EmailValidation]
    public string? Email { get; set; }

    [XDeviceValidation(HttpHeaderXDevice.Web)]
    public string? RegistrationAddress { get; set; }

    public string? ResidentialAddress { get; set; }
}
