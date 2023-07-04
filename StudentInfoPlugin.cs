using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core.Domain.Cms;
using Nop.Plugin.Widgets.StudentInfo.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.StudentInfo
{
    public class StudentInfoPlugin : BasePlugin, IWidgetPlugin,IAdminMenuPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly WidgetSettings _widgetSettings;
        public bool HideInWidgetList => true;

        public StudentInfoPlugin(
            ILocalizationService localizationService,
            ISettingService settingService,
            WidgetSettings widgetSettings)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _widgetSettings = widgetSettings;
        }

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
                ControllerName = "StudentRecord",
                ActionName = "Index",
                IconClass = "far fa-dot-circle",
                RouteValues = new RouteValueDictionary { { "area", AreaNames.Admin } }
            });
        }

        public override async Task InstallAsync()
        {
            //locales
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Student.Search.Name"] = "Name",
                ["Student.Search.Name.Hint"] = "Please provide your name.",
                ["Student.Search.Age"] = "Age",
                ["Student.Search.Age.Hint"] = "Please provide your age.",
                ["Student.Search.DateOfBirth"] = "Date of Birth",
                ["Student.Search.DateOfBirth.Hint"] = "Please provide your date of birth.",
                ["Student.Search.MaritalStatus"] = "Marital Status",
                ["Student.Search.MaritalStatus.Hint"] = "Marital Status",
                ["Admin.System.StudentRecord.List.CreatedOnTo"]="Starting date",
                ["Admin.System.StudentRecord.List.CreatedOnFrom"] = "Ending date",
                ["Student.Create.Name"] = "Name",
                ["Student.Create.Name.Hint"] = "Please provide your name.",
                ["Student.Create.Age"] = "Age",
                ["Student.Create.Age.Hint"] = "Please provide your age.",
                ["Student.Create.DateOfBirth"] = "Date of Birth",
                ["Student.Create.DateOfBirth.Hint"] = "Please provide your date of birth.",
                ["Student.Create.MaritalStatus"] = "Marital Status",
                ["Student.Create.MaritalStatus.Hint"] = "Marital Status",

            });

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            _widgetSettings.ActiveWidgetSystemNames.Remove(StudentInfoDefaults.SystemName);
            await _settingService.SaveSettingAsync(_widgetSettings);
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.StudentInfo");
            await base.UninstallAsync();
        }
    }
}
