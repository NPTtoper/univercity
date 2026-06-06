public class CircleFactory : FigureFactory
{
    public override Figure CreateFigure() => new CircleFigure();
    public override string Name => "Круг";
    public override string FigureType => "Circle";
}