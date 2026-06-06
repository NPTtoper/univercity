using System.Collections.Generic;
using System.Linq;

namespace Lab3.Serialization
{
    public static class DataProcessorManager
    {
        private static List<IDataProcessor> _processors = new List<IDataProcessor>();

        public static void RegisterProcessor(IDataProcessor processor)
        {
            _processors.Add(processor);
        }

        public static IEnumerable<string> GetProcessorNames()
        {
            return _processors.Select(p => p.Name);
        }

        public static IDataProcessor GetByName(string name)
        {
            return _processors.FirstOrDefault(p => p.Name == name);
        }

        public static void Clear()
        {
            _processors.Clear();
        }
    }
}