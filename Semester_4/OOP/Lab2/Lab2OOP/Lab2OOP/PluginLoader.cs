using SharedComponents;
using System.IO;
using System.Reflection;

namespace GraphicEditor;

public static class PluginLoader
{
    public static void LoadPluginsFromFolder(string pluginsPath)
    {
        if (!Directory.Exists(pluginsPath))
        {
            Directory.CreateDirectory(pluginsPath);
            return;
        }

        foreach (string dllPath in Directory.GetFiles(pluginsPath, "*.dll"))
        {
            try
            {
                var assembly = Assembly.LoadFrom(dllPath);

                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(AbstractFactory).IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        var factory = (AbstractFactory)Activator.CreateInstance(type);
                        string shapeName = type.Name.Replace("Factory", "");
                        ShapeRegistry.RegisterFactory(shapeName, factory);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load plugin: {dllPath} - {ex.Message}");
            }
        }
    }
}