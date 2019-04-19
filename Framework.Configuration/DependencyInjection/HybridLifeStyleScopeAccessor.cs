using System;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace Framework.Configuration.DependencyInjection
{
    public class HybridLifeStyleScopeAccessor : IScopeAccessor
    {
        private IScopeAccessor perThreadScopeAccessor;

        public ILifetimeScope GetScope(CreationContext context)
        {
            //if (OperationContext.Current != null)
            //{
            //    wcfOperationScopeAccessor = new WcfOperationScopeAccessor();
            //    return wcfOperationScopeAccessor.GetScope(context);
            //}

            perThreadScopeAccessor = new PerThreadScopeAccessor();
            return perThreadScopeAccessor.GetScope(context);
        }

        public void Dispose()
        {
            perThreadScopeAccessor?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}