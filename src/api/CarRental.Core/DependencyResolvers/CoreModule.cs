using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CarRental.Core.CrossCuttingConcerns.Caching;
using CarRental.Core.CrossCuttingConcerns.Caching.Microsoft;
using CarRental.Core.Utilities.Email;
using CarRental.Core.Utilities.Email.MailKit;
using CarRental.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Core.DependencyResolvers
{
    public class CoreModule: ICoreModule
    {


        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IEMailService, EMailService>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
