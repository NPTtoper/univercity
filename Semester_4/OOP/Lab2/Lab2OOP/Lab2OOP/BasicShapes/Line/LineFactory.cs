using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Line;

public class LineFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new LineShape(topLeft, downRight, bgColor, penColor, angle);
    }

    public override AbstractShape? CreateShapeFromDialog() => null;
}