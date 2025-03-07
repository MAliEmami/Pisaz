// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc.Filters;
// using Pisaz.Backend.API.Services;

// namespace Pisaz.Backend.API.Filters
// {
//     public class VIPAttribute
//     {
//         public void OnAuthorization(AuthorizationFilterContext context)
//         {
//             var jwtService = context.HttpContext.RequestServices.GetRequiredService<AuthService>();
//             var isUserVIP  = jwtService.GetUserVIPStatus(context.HttpContext);

//             if (isUserVIP is false)
//             {
//                 throw new UnauthorizedAccessException("Access Denied: User must be VIP.");
//             }
//         }
//     }
// }