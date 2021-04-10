using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace CarRental.Core.Utilities.Interceptors
{
    // method,class vs. üstündeki attribute => method çalıştığında üstündeki attribute'e bak ve uygun attribute varsa çlıştır.
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute: Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
