using System;
using System.Collections.Generic;

namespace Lab3
{
    public class PluginManager
    {
        private static PluginManager _instance;
        private static readonly object _lock = new object();

        public static PluginManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PluginManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public event Action<string> OnPluginLoaded;
        public List<string> LoadedPlugins { get; } = new List<string>();

        private PluginManager() { }

        public void LoadPlugins(string pluginsPath)
        {
            PluginLoader.LoadPlugins(pluginsPath);
        }

        public void NotifyPluginLoaded(string message)
        {
            OnPluginLoaded?.Invoke(message);
        }
    }
}