using System.Windows.Media;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace Snowman;

public class SnowmanFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new SnowmanShape(topLeft, downRight, bgColor, penColor, angle);
    }

    public override AbstractShape? CreateShapeFromDialog()
    {
        var dialog = new SnowmanDialog();
        if (dialog.ShowDialog() == true)
        {
            double size = dialog.Size;
            MyPoint topLeft = new MyPoint(dialog.CenterX - size / 2, dialog.CenterY - size / 2);
            MyPoint downRight = new MyPoint(dialog.CenterX + size / 2, dialog.CenterY + size / 2);
            return new SnowmanShape(topLeft, downRight, dialog.SelectedColor, Brushes.Black, 0);
        }
        return null;
    }

    public override bool UsesMouseInput() => false;
}