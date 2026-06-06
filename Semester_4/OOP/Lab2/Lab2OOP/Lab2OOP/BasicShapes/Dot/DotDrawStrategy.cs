using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Dot;

public class DotDrawStrategy : IDrawStrategy
{
    public Shape Draw(AbstractShape shape)
    {
        var dot = (DotShape)shape;
        double radius = 3;

        return new Path
        {
            Data = new EllipseGeometry(new Point(dot.GetCenterX(), dot.GetCenterY()), radius, radius),
            Fill = dot.PenColor,
            Stroke = dot.PenColor,
            StrokeThickness = 1
        };
    }
}