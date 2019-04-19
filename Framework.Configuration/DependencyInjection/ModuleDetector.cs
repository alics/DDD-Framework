using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Framework.Configuration.DependencyInjection
{
    public static class ModuleDetector
    {
        public static List<TInterface> DetectModules<TInterface>(params string[] assemblySearchPatterns) where TInterface : class
        {
            var assemblies = new List<Assembly>();
            foreach (var assemblySearchPattern in assemblySearchPatterns)
            {
                assemblies.AddRange(DetectAssemblies(assemblySearchPattern));
            }

            var interfaceName = typeof(TInterface).Name;
            var modules = new List<TInterface>();

            foreach (var assembly in assemblies)
            {
                var instances = from type in assembly.GetTypes()
                                where type.GetInterface(interfaceName) != null
                                where !type.IsInterface && !type.IsAbstract
                                select Activator.CreateInstance(type) as TInterface;

                modules.AddRange(instances);
            }

            return modules;
        }

        public static List<Assembly> DetectAssemblies(string assemblySearchPattern)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)?.Replace(@"file:\", string.Empty);
            var assemblies = Directory.GetFiles(directory, assemblySearchPattern, SearchOption.TopDirectoryOnly);

            return assemblies
                .Where(a => a.EndsWith(".dll") || a.EndsWith(".exe"))
                .Select(Assembly.LoadFrom)
                .ToList();
        }
    }
}