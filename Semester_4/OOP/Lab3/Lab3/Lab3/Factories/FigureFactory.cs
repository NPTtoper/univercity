using System.Collections.Generic;
using System.Linq;

public abstract class FigureFactory
{
    public abstract Figure CreateFigure();
    public abstract string Name { get; }
    public abstract string FigureType { get; }

    private static List<FigureFactory> factories = new List<FigureFactory>();

    public static void RegisterFactory(FigureFactory factory)
    {
        factories.Add(factory);
    }

    public static FigureFactory GetByName(string name)
    {
        return factories.FirstOrDefault(f => f.Name == name);
    }

    public static FigureFactory GetByFigureType(string figureType)
    {
        return factories.FirstOrDefault(f => f.FigureType == figureType);
    }

    public static IEnumerable<string> GetNames()
    {
        return factories.Select(f => f.Name);
    }
}