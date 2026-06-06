using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Ellipse;

public class EllipseShape : AbstractShape
{
    public EllipseShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new EllipseDrawStrategy();
    }

    public override string GetShapeName() => "Ellipse";
    public override string ToString() => $"Ellipse ({TopLeft.X},{TopLeft.Y}) to ({DownRight.X},{DownRight.Y})";
}