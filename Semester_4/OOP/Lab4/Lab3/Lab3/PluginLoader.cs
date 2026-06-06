using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Lab3.Factories;

namespace Lab3
{
    public static class PluginLoader
    {
        public static void LoadPlugins(string pluginsPath)
        {
            if (!Directory.Exists(pluginsPath))
                return;

            var dllFiles = Directory.GetFiles(pluginsPath, "*.dll");

            foreach (var dll in dllFiles)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dll);

                    var factoryTypes = assembly.GetTypes()
                        .Where(t => typeof(IFigureFactory).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var type in factoryTypes)
                    {
                        IFigureFactory factory = (IFigureFactory)Activator.CreateInstance(type);
                        FigureFactoryBase.RegisterFactory(factory);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading plugin {dll}: {ex.Message}");
                }
            }
        }
    }
}