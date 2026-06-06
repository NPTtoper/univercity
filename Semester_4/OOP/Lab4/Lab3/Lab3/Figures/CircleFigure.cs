using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab3.Figures
{
    public class CircleFigure : Figure
    {
        public override string FigureType => "Circle";

        public override Shape GetShape()
        {
            double radius = Math.Sqrt(Math.Pow(EndX - StartX, 2) + Math.Pow(EndY - StartY, 2));
            return new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Margin = new Thickness(StartX - radius, StartY - radius, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
        }
    }
}