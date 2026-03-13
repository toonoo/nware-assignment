using Microsoft.AspNetCore.Mvc.Filters;
using NWARE.Common.Logging;

namespace NWARE.API.Filters
{
    public class ValidateActionFilterAttribute : IActionFilter
    {
        private readonly ILoggingService _loggingService;

        public ValidateActionFilterAttribute(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                return;
            }

            // Validate model state
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values;
                // Log validation errors if needed
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context?.Exception != null)
            {
                // Exception is already logged by middleware, but can add additional handling here
            }
        }
    }
}
