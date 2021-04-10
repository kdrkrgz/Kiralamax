using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace CarRental.Core.Utilities.Interceptors
{
    // metod üzerindeki attribute ilgili sıralardaki before, after ,exception durumlarında ilgili methodları çalıştır.
    public class MethodInterception:MethodInterceptionBaseAttribute
    {
        // invocation: business metodları add, update, delete gibi...
        protected virtual void OnBefore(IInvocation invocation) {}
        protected virtual void OnAfter(IInvocation invocation) {}
        protected virtual void OnException(IInvocation invocation, System.Exception e) {}
        protected virtual void OnSuccess(IInvocation invocation) {}

        public override void Intercept(IInvocation invocation)
        {
            var isSucesss = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSucesss = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSucesss)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
