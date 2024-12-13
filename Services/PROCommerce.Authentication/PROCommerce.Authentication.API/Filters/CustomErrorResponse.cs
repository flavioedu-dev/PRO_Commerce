using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PROCommerce.Authentication.API.Filters;

public class CustomErrorResponse : Attribute, IActionFilter
{
    private List<string> ListModelErros(ActionContext context) =>
        context.ModelState
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage)
        .ToList();

    public void OnActionExecuted(ActionExecutedContext context) { }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            List<string> errors = ListModelErros(context);

            var result = new
            {
                success = false,
                errors
            };

            context.Result = new BadRequestObjectResult(result);

            return;
        }

        return;
    }
}