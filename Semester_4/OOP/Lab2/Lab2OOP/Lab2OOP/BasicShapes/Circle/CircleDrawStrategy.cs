using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Circle;

public class CircleDrawStrategy : IDrawStrategy
{
    public Shape Draw(AbstractShape shape)
    {
        var circle = (CircleShape)shape;

        return new Path
        {
            Data = new EllipseGeometry(new Point(circle.GetCenterX(), circle.GetCenterY()), circle.GetWidth() / 2, circle.GetHeight() / 2),
            Fill = circle.BackgroundColor,
            Stroke = circle.PenColor,
            StrokeThickness = circle.StrokeThickness
        };
    }
}