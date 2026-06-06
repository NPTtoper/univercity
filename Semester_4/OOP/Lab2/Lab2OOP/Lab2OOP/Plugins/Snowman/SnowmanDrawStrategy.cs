using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace Snowman;

public class SnowmanDrawStrategy : IDrawStrategy
{
    public Shape Draw(AbstractShape shape)
    {
        var snowman = (SnowmanShape)shape;

        double width = snowman.GetWidth();
        double height = snowman.GetHeight();
        double centerX = snowman.GetCenterX();
        double centerY = snowman.GetCenterY();
        double scale = Math.Min(width, height) / 150.0;

        var geometry = new GeometryGroup();

        geometry.Children.Add(new EllipseGeometry(new Point(centerX, centerY + 30 * scale), 45 * scale, 45 * scale));
        geometry.Children.Add(new EllipseGeometry(new Point(centerX, centerY - 20 * scale), 35 * scale, 35 * scale));
        geometry.Children.Add(new EllipseGeometry(new Point(centerX, centerY - 65 * scale), 25 * scale, 25 * scale));
        geometry.Children.Add(new EllipseGeometry(new Point(centerX - 10 * scale, centerY - 72 * scale), 4 * scale, 4 * scale));
        geometry.Children.Add(new EllipseGeometry(new Point(centerX + 10 * scale, centerY - 72 * scale), 4 * scale, 4 * scale));
        var nose = new StreamGeometry();
        using (var ctx = nose.Open())
        {
            ctx.BeginFigure(new Point(centerX, centerY - 65 * scale), true, true);
            ctx.LineTo(new Point(centerX + 12 * scale, centerY - 60 * scale), true, true);
            ctx.LineTo(new Point(centerX, centerY - 55 * scale), true, true);
        }
        geometry.Children.Add(nose);
        geometry.Children.Add(new EllipseGeometry(new Point(centerX, centerY - 15 * scale), 3 * scale, 3 * scale));
        geometry.Children.Add(new EllipseGeometry(new Point(centerX, centerY + 5 * scale), 3 * scale, 3 * scale));

        return new Path
        {
            Data = geometry,
            Fill = snowman.BackgroundColor,
            Stroke = snowman.PenColor,
            StrokeThickness = snowman.StrokeThickness
        };
    }
}