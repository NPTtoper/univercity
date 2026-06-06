
namespace Lab3.Factories
{
    public class LineFactory : FigureFactory
    {
        public override Figure CreateFigure() => new LineFigure();
        public override string Name => "Линия";
        public override string FigureType => "Line";
    }
}