using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

public abstract class Figure
{
    public double StartX { get; set; }
    public double StartY { get; set; }
    public double EndX { get; set; }
    public double EndY { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Point StartPoint
    {
        get => new Point(StartX, StartY);
        set { StartX = value.X; StartY = value.Y; }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public Point EndPoint
    {
        get => new Point(EndX, EndY);
        set { EndX = value.X; EndY = value.Y; }
    }

    public abstract Shape GetShape();
    public abstract string FigureType { get; }
}