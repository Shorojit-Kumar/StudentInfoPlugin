﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.StudentInfo.Factory;
using Nop.Plugin.Widgets.StudentInfo.Service;

namespace Nop.Plugin.Widgets.StudentInfo.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public int Order => 120;

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentFactory,StudentFactory>();

        }
    }
}
