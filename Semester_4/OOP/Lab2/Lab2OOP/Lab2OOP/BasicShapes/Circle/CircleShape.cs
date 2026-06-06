using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Circle;

public class CircleShape : AbstractShape
{
    public CircleShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new CircleDrawStrategy();
    }

    public override string GetShapeName() => "Circle";
    public override string ToString() => $"Circle at ({GetCenterX()}, {GetCenterY()}) r={GetWidth() / 2:F0}";
}