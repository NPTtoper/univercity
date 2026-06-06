using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Lab3.Factories;
using Lab3.Serialization;

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

                    var processorTypes = assembly.GetTypes()
                        .Where(t => typeof(IDataProcessor).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var type in processorTypes)
                    {
                        IDataProcessor processor = (IDataProcessor)Activator.CreateInstance(type);
                        DataProcessorManager.RegisterProcessor(processor);
                    }

                    // Адаптер для плагинов сжатия
                    var compressorTypes = assembly.GetTypes()
                        .Where(t => typeof(ICompressor).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var type in compressorTypes)
                    {
                        ICompressor compressor = (ICompressor)Activator.CreateInstance(type);
                        CompressorAdapter adapter = new CompressorAdapter(compressor);
                        DataProcessorManager.RegisterProcessor(adapter);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}