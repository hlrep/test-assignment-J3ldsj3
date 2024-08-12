using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Constants;

namespace Web.Filters;

public class RequiredXDeviceHeaderFilter : ActionFilterAttribute
{
    private readonly HashSet<string> _xDeviceHeaderValues;
    public RequiredXDeviceHeaderFilter(params string[] xDeviceHeaderValues)
    {
        _xDeviceHeaderValues = new HashSet<string>(xDeviceHeaderValues);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey(HttpHeaders.X_DEVICE))
        {
            context.Result = new BadRequestObjectResult($"'{HttpHeaders.X_DEVICE}' header is required.");
            return;
        }

        if (!_xDeviceHeaderValues.Contains(context.HttpContext.Request.Headers[HttpHeaders.X_DEVICE].ToString()))
        {
            context.Result = new BadRequestObjectResult($"'{HttpHeaders.X_DEVICE}' contains unexpected value.");
            return;
        }
    }
}
