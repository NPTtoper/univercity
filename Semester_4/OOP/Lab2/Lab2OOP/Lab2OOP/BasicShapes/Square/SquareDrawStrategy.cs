using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace BasicShapes.Square;

public class SquareDrawStrategy : IDrawStrategy
{
    public Shape Draw(AbstractShape shape)
    {
        var square = (SquareShape)shape;

        return new Path
        {
            Data = new RectangleGeometry(new Rect(square.TopLeft.X, square.TopLeft.Y, square.GetWidth(), square.GetHeight())),
            Fill = square.BackgroundColor,
            Stroke = square.PenColor,
            StrokeThickness = square.StrokeThickness
        };
    }
}