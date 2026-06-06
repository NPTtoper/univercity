using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Ellipse;

public class EllipseFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new EllipseShape(topLeft, downRight, bgColor, penColor, angle);
    }

    public override AbstractShape? CreateShapeFromDialog() => null;
}