using Lab3.Figures;

namespace Lab3.Factories
{
    public interface IFigureFactory
    {
        Figure CreateFigure();
        string Name { get; }
        string FigureType { get; }
    }
}