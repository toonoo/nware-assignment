using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NWARE.Common;
using System;
using System.Collections.Generic;

namespace NWARE.API.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetService(typeof(IAuthorizationService)) as IAuthorizationService;

            if (authorizationService == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader))
            {
                context.Result = new UnauthorizedObjectResult(
                    ServiceResult<object>.Fail("Authorization header is missing", 401)
                );
                return;
            }

            var token = authHeader.ToString().Replace("Bearer ", "");

            if (!authorizationService.ValidateApiKey(token))
            {
                context.Result = new UnauthorizedObjectResult(
                    ServiceResult<object>.Fail("Invalid API Key", 401)
                );
                return;
            }
        }
    }
}
