public class RectangleFactory : FigureFactory
{
    public override Figure CreateFigure() => new RectangleFigure();
    public override string Name => "Прямоугольник";
    public override string FigureType => "Rectangle";
}