using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Line;

public class LineDrawStrategy : IDrawStrategy
{
    public Shape Draw(AbstractShape shape)
    {
        var line = (LineShape)shape;

        return new Path
        {
            Data = new LineGeometry(new Point(line.TopLeft.X, line.TopLeft.Y), new Point(line.DownRight.X, line.DownRight.Y)),
            Stroke = line.PenColor,
            StrokeThickness = line.StrokeThickness
        };
    }
}