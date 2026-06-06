using Lab3.Figures;

namespace Lab3.Factories
{
    public class LineFactory : IFigureFactory
    {
        public Figure CreateFigure() => new LineFigure();
        public string Name => "Линия";
        public string FigureType => "Line";
    }
}