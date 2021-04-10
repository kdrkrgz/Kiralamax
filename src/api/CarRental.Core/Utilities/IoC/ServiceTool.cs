using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Core.Utilities.IoC
{
    // herhangi bir interfacein autofactaki karşılığını almak için service tooldan yararlanıyoruz.
    public class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
            //Default injectionları Core üzerinden yönetmek için
        }

    }
}
