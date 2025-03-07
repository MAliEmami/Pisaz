using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pisaz.Backend.API.Services;

namespace Pisaz.Backend.API.Filters
{
    public class VIPAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var jwtService = context.HttpContext.RequestServices.GetRequiredService<AuthService>();

            var IsVIP  = jwtService.GetUserVIPStatus(context.HttpContext);


            if (!IsVIP)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}