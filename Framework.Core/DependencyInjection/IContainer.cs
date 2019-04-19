using System;
using System.Collections.Generic;

namespace Framework.Core.DependencyInjection
{
    public interface IContainer
    {
        T Resolve<T>();

        T Resolve<T>(Func<T, bool> selector);

        IEnumerable<T> ResolveAll<T>();
    }
}
