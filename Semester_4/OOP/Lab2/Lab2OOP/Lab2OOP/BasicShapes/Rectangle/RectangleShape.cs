using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Rectangle;

public class RectangleShape : AbstractShape
{
    public RectangleShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new RectangleDrawStrategy();
    }

    public override string GetShapeName() => "Rectangle";
    public override string ToString() => $"Rectangle ({TopLeft.X},{TopLeft.Y}) to ({DownRight.X},{DownRight.Y})";
}