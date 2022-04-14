using System;
using System.Reflection;

namespace Framework.Core
{
    public interface IEventTypeResolver
    {
        Type GetType(string messageName);
        void RegisterEventTypes(Assembly assembly);
    }
}