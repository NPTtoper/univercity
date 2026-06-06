using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace Snowman;

public class SnowmanShape : AbstractShape
{
    public SnowmanShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new SnowmanDrawStrategy();
    }

    public override string GetShapeName() => "Snowman";
    public override string ToString() => $"Snowman at ({GetCenterX()}, {GetCenterY()})";
}