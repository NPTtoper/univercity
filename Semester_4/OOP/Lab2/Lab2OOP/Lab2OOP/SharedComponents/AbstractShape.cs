using System.Windows.Media;
using System.Windows.Shapes;

namespace SharedComponents.AbstractClasses;

public abstract class AbstractShape
{
    public MyPoint TopLeft { get; set; }
    public MyPoint DownRight { get; set; }
    public Brush BackgroundColor { get; set; }
    public Brush PenColor { get; set; }
    public int Angle { get; set; }
    public double StrokeThickness { get; set; } = 2;

    public IDrawStrategy DrawStrategy { get; set; }

    protected AbstractShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        TopLeft = topLeft;
        DownRight = downRight;
        BackgroundColor = bgColor;
        PenColor = penColor;
        Angle = angle;
    }

    public Shape Draw() => DrawStrategy?.Draw(this);
    public abstract string GetShapeName();

    public double GetCenterX() => (TopLeft.X + DownRight.X) / 2;
    public double GetCenterY() => (TopLeft.Y + DownRight.Y) / 2;
    public double GetWidth() => Math.Abs(DownRight.X - TopLeft.X);
    public double GetHeight() => Math.Abs(DownRight.Y - TopLeft.Y);
}