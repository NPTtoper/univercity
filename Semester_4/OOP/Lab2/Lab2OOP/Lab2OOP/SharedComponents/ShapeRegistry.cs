namespace SharedComponents;

public static class ShapeRegistry
{
    private static readonly Dictionary<string, AbstractFactory> _factories = new();
    private static readonly List<string> _shapeNames = new();

    public static void RegisterFactory(string shapeName, AbstractFactory factory)
    {
        if (!_factories.ContainsKey(shapeName))
        {
            _factories[shapeName] = factory;
            _shapeNames.Add(shapeName);
        }
    }

    public static AbstractFactory? GetFactory(string shapeName)
    {
        return _factories.GetValueOrDefault(shapeName);
    }

    public static List<string> GetShapeNames()
    {
        return new List<string>(_shapeNames);
    }

    public static bool IsRegistered(string shapeName)
    {
        return _factories.ContainsKey(shapeName);
    }
}