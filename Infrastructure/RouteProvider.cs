using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;

namespace Nop.Plugin.Widgets.StudentInfo.Infrastructure
{
    public class RouteProvider 
    {
        public int Priority => 0;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
           /* var lang = GetLanguageRoutePattern();*/
         /*   endpointRouteBuilder.MapControllerRoute("StudentInfo",
               "Student/Index",
               new { controller = "Student", action = "Index"});
*/

            //areas
            /* endpointRouteBuilder.MapControllerRoute(name: "areaRoute",
                 pattern: $"{{area:exists}}/{{controller=Student}}/{{action=Index}}/{{id?}}");*/

        }
    }
}
