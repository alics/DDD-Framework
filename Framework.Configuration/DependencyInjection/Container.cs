using System;
using Castle.Windsor;
using System.Collections.Generic;
using System.Linq;
using IContainer = Framework.Core.DependencyInjection.IContainer;

namespace Framework.Configuration.DependencyInjection
{
    public class Container : IContainer
    {
        private readonly IWindsorContainer windsorContainer;

        public Container(IWindsorContainer windsorContainer)
        {
            this.windsorContainer = windsorContainer;
        }

        public T Resolve<T>()
        {
            return windsorContainer.Resolve<T>();
        }

        public T Resolve<T>(Func<T, bool> selector)
        {
            var allInstances = windsorContainer.ResolveAll<T>();
            return allInstances.First(selector);
        }

        public T Resolve<T>(string objectName)
        {
            return windsorContainer.Resolve<T>(objectName);
        }

        public T TryResolve<T>()
        {
            var instances = windsorContainer.ResolveAll<T>();
            return instances.Any() ? instances.First() : default(T);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return windsorContainer.ResolveAll<T>();
        }
}
    }
