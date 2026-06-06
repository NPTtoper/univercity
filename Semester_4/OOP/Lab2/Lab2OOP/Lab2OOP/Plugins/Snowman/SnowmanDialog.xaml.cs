using System.Windows;
using System.Windows.Media;

namespace Snowman;

public partial class SnowmanDialog : Window
{
    public double CenterX { get; private set; }
    public double CenterY { get; private set; }
    public double Size { get; private set; }
    public Brush SelectedColor { get; private set; }

    public SnowmanDialog()
    {
        InitializeComponent();
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        CenterX = double.TryParse(txtCenterX.Text, out var x) ? x : 200;
        CenterY = double.TryParse(txtCenterY.Text, out var y) ? y : 200;

        Size = cmbSize.SelectedIndex switch
        {
            0 => 100,
            1 => 150,
            2 => 200,
            _ => 150
        };

        SelectedColor = cmbColor.SelectedIndex switch
        {
            0 => Brushes.White,
            1 => Brushes.LightGray,
            2 => Brushes.LightBlue,
            _ => Brushes.White
        };

        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}