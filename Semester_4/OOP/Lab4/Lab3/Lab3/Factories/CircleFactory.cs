using Lab3.Figures;

namespace Lab3.Factories
{
    public class CircleFactory : IFigureFactory
    {
        public Figure CreateFigure() => new CircleFigure();
        public string Name => "Круг";
        public string FigureType => "Circle";
    }
}