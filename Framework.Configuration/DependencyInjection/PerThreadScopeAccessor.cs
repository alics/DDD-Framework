using System;
using System.Collections.Concurrent;
using System.Threading;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace Framework.Configuration.DependencyInjection
{
    public class PerThreadScopeAccessor : IScopeAccessor
    {
        private static readonly ConcurrentDictionary<int, ILifetimeScope> collection = new ConcurrentDictionary<int, ILifetimeScope>();

        public ILifetimeScope GetScope(CreationContext context)
        {
            return collection.GetOrAdd(Thread.CurrentThread.ManagedThreadId, id => new DefaultLifetimeScope());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}