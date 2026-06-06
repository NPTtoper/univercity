using System.Windows.Media;
using SharedComponents.AbstractClasses;

namespace SharedComponents;

public abstract class AbstractFactory
{
    public abstract AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle);
    public abstract AbstractShape? CreateShapeFromDialog();
    public virtual bool UsesMouseInput() => true;
}