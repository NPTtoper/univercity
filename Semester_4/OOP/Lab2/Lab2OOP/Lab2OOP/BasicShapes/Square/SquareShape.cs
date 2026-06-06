using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Square;

public class SquareShape : AbstractShape
{
    public SquareShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        double side = Math.Min(GetWidth(), GetHeight());
        double centerX = GetCenterX();
        double centerY = GetCenterY();
        TopLeft = new MyPoint(centerX - side / 2, centerY - side / 2);
        DownRight = new MyPoint(centerX + side / 2, centerY + side / 2);
        DrawStrategy = new SquareDrawStrategy();
    }

    public override string GetShapeName() => "Square";
    public override string ToString() => $"Square at ({GetCenterX()}, {GetCenterY()}) size={GetWidth():F0}";
}