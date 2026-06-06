using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Lab3.Figures;

namespace TrianglePlugin
{
    public class TriangleFigure : Figure
    {
        public override string FigureType => "Triangle";

        public override Shape GetShape()
        {
            Point p1 = new Point(StartX, EndY);
            Point p2 = new Point((StartX + EndX) / 2, StartY);
            Point p3 = new Point(EndX, EndY);

            return new Polygon
            {
                Points = new PointCollection { p1, p2, p3 },
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
        }
    }
}