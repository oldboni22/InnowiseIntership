using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InnowiseIntership.ActionFilters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        var dto = context.ActionArguments.SingleOrDefault(
            x => x.ToString().Contains("Dto")).Value;
        if (dto == null)
        {
            context.Result = new BadRequestObjectResult($"dto object is null. Action - {action}. Controller - {controller}");
            return;
        }
        
        if(context.ModelState.IsValid == false)
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}