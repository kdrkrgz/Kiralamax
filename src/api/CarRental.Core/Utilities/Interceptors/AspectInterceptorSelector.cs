using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CarRental.Core.Aspects.Autofac.Exception;
using CarRental.Core.Aspects.Autofac.Performance;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Castle.Core.Internal;
using Castle.DynamicProxy;

namespace CarRental.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector: IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            classAttributes.AddRange(methodAttributes);
            // tüm metodlar için Fileloggera loglama yap.
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
            // metodların çalışması x saniyeden fazla sürerse mail gönder.
            //classAttributes.Add(new PerformanceAspect(10));
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
