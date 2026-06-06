using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Ellipse;

public class EllipseDrawStrategy : IDrawStrategy
{
    public Shape Draw(AbstractShape shape)
    {
        var ellipse = (EllipseShape)shape;

        return new Path
        {
            Data = new EllipseGeometry(new Point(ellipse.GetCenterX(), ellipse.GetCenterY()), ellipse.GetWidth() / 2, ellipse.GetHeight() / 2),
            Fill = ellipse.BackgroundColor,
            Stroke = ellipse.PenColor,
            StrokeThickness = ellipse.StrokeThickness
        };
    }
}