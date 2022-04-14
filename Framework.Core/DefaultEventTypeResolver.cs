using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Core
{
    public class DefaultEventTypeResolver : IEventTypeResolver
    {
        private readonly Dictionary<string, Type> _dic;

        public DefaultEventTypeResolver()
        {
            _dic = new Dictionary<string, Type>();
        }

        public void RegisterEventTypes(Assembly assembly)
        {
            var eventTypes = assembly.GetTypes()
                .Where(n=> n.GetInterfaces().Contains(typeof(IEvent)))
                .ToList();

            foreach (var eventType in eventTypes)
            {
                _dic.Add(eventType.Name, eventType);
            }
        }

        public Type GetType(string messageName)
        {
            return _dic[messageName];
        }
    }
}