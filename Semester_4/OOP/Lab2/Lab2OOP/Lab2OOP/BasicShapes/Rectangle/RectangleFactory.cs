using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Rectangle;

public class RectangleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new RectangleShape(topLeft, downRight, bgColor, penColor, angle);
    }

    public override AbstractShape? CreateShapeFromDialog() => null;
}