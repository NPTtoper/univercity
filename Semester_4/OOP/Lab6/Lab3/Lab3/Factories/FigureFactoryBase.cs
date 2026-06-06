using System.Collections.Generic;
using System.Linq;

namespace Lab3.Factories
{
    public static class FigureFactoryBase
    {
        private static List<IFigureFactory> _factories = new List<IFigureFactory>();

        public static void RegisterFactory(IFigureFactory factory)
        {
            _factories.Add(factory);
        }

        public static IFigureFactory GetByName(string name)
        {
            return _factories.FirstOrDefault(f => f.Name == name);
        }

        public static IFigureFactory GetByFigureType(string figureType)
        {
            return _factories.FirstOrDefault(f => f.FigureType == figureType);
        }

        public static IEnumerable<string> GetNames()
        {
            return _factories.Select(f => f.Name);
        }
    }
}