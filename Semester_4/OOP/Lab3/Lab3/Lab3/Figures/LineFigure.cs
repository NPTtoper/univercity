using System.Windows.Media;
using System.Windows.Shapes;

public class LineFigure : Figure
{
    public override string FigureType => "Line";

    public override Shape GetShape()
    {
        return new Line
        {
            X1 = StartX,
            Y1 = StartY,
            X2 = EndX,
            Y2 = EndY,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };
    }
}