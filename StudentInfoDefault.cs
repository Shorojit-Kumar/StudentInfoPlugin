using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Widgets.StudentInfo.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.StudentInfo
{
    public class StudentInfoDefault : BasePlugin, IWidgetPlugin,IAdminMenuPlugin
    {
        public bool HideInWidgetList => true;

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(NavLinkViewComponent);
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HeaderLinksAfter });
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var configurationItem = rootNode.ChildNodes.FirstOrDefault(node => node.SystemName.Equals("Configuration"));
            if (configurationItem is null)
                return;

            var widgetsItem = configurationItem.ChildNodes.FirstOrDefault(node => node.SystemName.Equals("Widgets"));
            var index = configurationItem is not null ? configurationItem.ChildNodes.IndexOf(widgetsItem) : -1;

            configurationItem.ChildNodes.Insert(index + 1, new SiteMapNode
            {
                Visible = true,
                SystemName = PluginDescriptor.SystemName,
                Title = PluginDescriptor.FriendlyName,
                ControllerName = "Student",
                ActionName = "Index",
                IconClass = "far fa-dot-circle",
                RouteValues = new RouteValueDictionary { { "area", AreaNames.Admin } }
            });
        }
    }
}
