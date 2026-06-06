using Lab3.Figures;

namespace Lab3.Factories
{
    public class RectangleFactory : IFigureFactory
    {
        public Figure CreateFigure() => new RectangleFigure();
        public string Name => "Прямоугольник";
        public string FigureType => "Rectangle";
    }
}