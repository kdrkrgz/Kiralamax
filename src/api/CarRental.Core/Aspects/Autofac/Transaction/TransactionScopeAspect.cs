using CarRental.Core.Utilities.Interceptors;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CarRental.Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect: MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    scope.Complete();
                }
                catch (System.Exception exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }
    }
}
