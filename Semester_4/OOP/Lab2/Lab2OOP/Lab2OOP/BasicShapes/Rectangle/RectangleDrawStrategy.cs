using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Rectangle;

public class RectangleDrawStrategy : IDrawStrategy
{
    public Shape Draw(AbstractShape shape)
    {
        var rect = (RectangleShape)shape;

        return new Path
        {
            Data = new RectangleGeometry(new Rect(rect.TopLeft.X, rect.TopLeft.Y, rect.GetWidth(), rect.GetHeight())),
            Fill = rect.BackgroundColor,
            Stroke = rect.PenColor,
            StrokeThickness = rect.StrokeThickness
        };
    }
}