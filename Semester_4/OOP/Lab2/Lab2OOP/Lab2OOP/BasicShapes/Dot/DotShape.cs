using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Dot;

public class DotShape : AbstractShape
{
    public DotShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new DotDrawStrategy();
    }

    public override string GetShapeName() => "Dot";
    public override string ToString() => $"Dot at ({GetCenterX()}, {GetCenterY()})";
}