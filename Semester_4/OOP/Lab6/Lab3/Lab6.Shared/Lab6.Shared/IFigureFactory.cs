using Lab6.Figures;

namespace Lab6.Factories
{
    public interface IFigureFactory
    {
        Figure CreateFigure();
        string Name { get; }
        string FigureType { get; }
    }
}