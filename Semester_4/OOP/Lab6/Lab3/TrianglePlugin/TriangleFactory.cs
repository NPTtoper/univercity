using Lab3.Factories;
using Lab3.Figures;

namespace TrianglePlugin
{
    public class TriangleFactory : IFigureFactory
    {
        public Figure CreateFigure() => new TriangleFigure();
        public string Name => "Треугольник";
        public string FigureType => "Triangle";
    }
}