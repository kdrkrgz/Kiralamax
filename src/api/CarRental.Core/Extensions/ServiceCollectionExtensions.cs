using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,
            ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services); 
            }

            return ServiceTool.Create(services);
        }
    }
}
