using System.Windows.Shapes;
using SharedComponents.AbstractClasses;

namespace SharedComponents;

public interface IDrawStrategy
{
    Shape Draw(AbstractShape shape);
}