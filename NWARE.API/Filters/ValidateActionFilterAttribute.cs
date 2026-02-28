using Microsoft.AspNetCore.Mvc.Filters;

namespace NWARE.API.Filters
{
    public class ValidateActionFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception != null)
            {

            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(context == null)
            {

            }
        }
    }
}
