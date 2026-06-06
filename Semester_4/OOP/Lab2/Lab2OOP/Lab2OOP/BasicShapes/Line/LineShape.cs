using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Line;

public class LineShape : AbstractShape
{
    public LineShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new LineDrawStrategy();
    }

    public override string GetShapeName() => "Line";
    public override string ToString() => $"Line ({TopLeft.X},{TopLeft.Y}) to ({DownRight.X},{DownRight.Y})";
}