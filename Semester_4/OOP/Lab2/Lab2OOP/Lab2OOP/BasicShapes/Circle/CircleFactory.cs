using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Circle;

public class CircleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new CircleShape(topLeft, downRight, bgColor, penColor, angle);
    }

    public override AbstractShape? CreateShapeFromDialog() => null;
}