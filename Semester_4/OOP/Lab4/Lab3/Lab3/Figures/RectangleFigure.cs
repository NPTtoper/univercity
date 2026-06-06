using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab3.Figures
{
    public class RectangleFigure : Figure
    {
        public override string FigureType => "Rectangle";

        public override Shape GetShape()
        {
            double x = Math.Min(StartX, EndX);
            double y = Math.Min(StartY, EndY);
            double width = Math.Abs(EndX - StartX);
            double height = Math.Abs(EndY - StartY);

            return new Rectangle
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Margin = new Thickness(x, y, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
        }
    }
}