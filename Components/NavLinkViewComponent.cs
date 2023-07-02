using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.StudentInfo.Service;
using Nop.Services.Catalog;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.StudentInfo.Components
{
    public class NavLinkViewComponent:NopViewComponent
    {
        public NavLinkViewComponent(
            IProductService productService,
            IStudentService studentService,
            IWorkContext workContext)
        {
            
        }
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Widgets.StudentInfo/Views/PublicInfo.cshtml");
        }
    }
}
