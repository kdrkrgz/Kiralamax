using CarRental.Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using CarRental.Core.Utilities.Interceptors;
using Castle.DynamicProxy;
using CarRental.Core.Utilities.Email;
using CarRental.Core.Utilities.Email.MailKit;

namespace CarRental.Core.Aspects.Autofac.Performance
{

    public class PerformanceAspect: MethodInterception
    {
        private readonly int _interval;
        private readonly Stopwatch _stopwatch;
        private readonly IEMailService _mailService;
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _mailService = ServiceTool.ServiceProvider.GetService<IEMailService>();
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} --------> {_stopwatch.Elapsed.TotalSeconds}");
                _mailService.Send("CarRentAl Performance Issue Dedected", 
                    $"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} --------> {_stopwatch.Elapsed.TotalSeconds}", null);
            }
            _stopwatch.Reset();
        }
    }
}
