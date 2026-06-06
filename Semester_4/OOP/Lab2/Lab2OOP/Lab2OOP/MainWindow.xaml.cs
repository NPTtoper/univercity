using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Reflection;
using SharedComponents;
using SharedComponents.AbstractClasses;

namespace GraphicEditor;

public partial class MainWindow : Window
{
    private List<AbstractShape> _shapes = new();
    private List<UIElement> _shapeElements = new();
    private AbstractFactory? _currentFactory;
    private MyPoint? _startPoint;
    private Rectangle? _rubberBand;
    private bool _isDrawing;

    private readonly Random _random = new();
    private readonly Brush[] _colors = { Brushes.LightBlue, Brushes.LightGreen, Brushes.LightPink, Brushes.LightYellow, Brushes.Lavender, Brushes.LightCoral };

    public MainWindow()
    {
        InitializeComponent();
        LoadAllShapesAndPlugins();
        CreateAllButtons();
    }

    private void LoadAllShapesAndPlugins()
    {
        string pluginsPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");

        if (!System.IO.Directory.Exists(pluginsPath))
        {
            System.IO.Directory.CreateDirectory(pluginsPath);
        }

        foreach (string dllPath in System.IO.Directory.GetFiles(pluginsPath, "*.dll"))
        {
            try
            {
                var assembly = Assembly.LoadFrom(dllPath);
                RegisterFactoriesFromAssembly(assembly);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed: {dllPath} - {ex.Message}");
            }
        }

        var executingAssembly = Assembly.GetExecutingAssembly();
        RegisterFactoriesFromAssembly(executingAssembly);
    }

    private void RegisterFactoriesFromAssembly(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (typeof(AbstractFactory).IsAssignableFrom(type) && !type.IsAbstract)
            {
                var factory = (AbstractFactory)Activator.CreateInstance(type);
                string shapeName = type.Name.Replace("Factory", "");
                if (!ShapeRegistry.IsRegistered(shapeName))
                {
                    ShapeRegistry.RegisterFactory(shapeName, factory);
                }
            }
        }
    }

    private void CreateAllButtons()
    {
        buttonsPanel.Children.Clear();

        int pluginSeparatorIndex = 0;

        foreach (string shapeName in ShapeRegistry.GetShapeNames())
        {
            var factory = ShapeRegistry.GetFactory(shapeName);
            if (factory == null) continue;

            var btn = new Button
            {
                Content = GetButtonDisplay(shapeName, factory),
                Margin = new Thickness(0, 2, 0, 2),
                Height = 40,
                Tag = shapeName,
                FontSize = 14
            };

            btn.Click += ShapeButton_Click;
            buttonsPanel.Children.Add(btn);

            var menuItem = new MenuItem
            {
                Header = GetMenuDisplay(shapeName, factory),
                Tag = shapeName
            };
            menuItem.Click += ShapeButton_Click;

            if (factory.UsesMouseInput())
            {
                mnuBasic.Items.Add(menuItem);
            }
            else
            {
                mnuPlugins.Items.Add(menuItem);
            }
        }

        buttonsPanel.Children.Add(new Separator { Margin = new Thickness(0, 10, 0, 10) });

        var clearBtn = new Button { Content = "Clear All", Margin = new Thickness(0, 2, 0, 2), Height = 35 };
        clearBtn.Click += ClearAll;
        buttonsPanel.Children.Add(clearBtn);
    }

    private string GetButtonDisplay(string shapeName, AbstractFactory factory)
    {
        return $"{shapeName}";
    }

    private string GetMenuDisplay(string shapeName, AbstractFactory factory)
    {
        return factory.UsesMouseInput() ? shapeName : $"{shapeName} (plugin)";
    }

    private void ShapeButton_Click(object sender, RoutedEventArgs e)
    {
        string? shapeName = null;

        if (sender is Button btn)
            shapeName = btn.Tag as string;
        else if (sender is MenuItem mi)
            shapeName = mi.Tag as string;

        if (shapeName == null) return;

        _currentFactory = ShapeRegistry.GetFactory(shapeName);

        if (_currentFactory != null)
        {
            if (_currentFactory.UsesMouseInput())
            {
                lblStatus.Text = $"Selected: {shapeName} - Click and drag on canvas";
                _currentShapeType = shapeName;
            }
            else
            {
                var shape = _currentFactory.CreateShapeFromDialog();
                if (shape != null)
                {
                    AddShape(shape);
                    lblStatus.Text = $"Created: {shape.GetShapeName()} from plugin";
                }
                _currentShapeType = null;
            }
        }
    }

    private string? _currentShapeType;

    private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (_currentShapeType == null || _currentFactory == null || !_currentFactory.UsesMouseInput())
        {
            lblStatus.Text = "Select a basic shape first!";
            return;
        }

        var point = e.GetPosition(canvas);
        _startPoint = new MyPoint(point.X, point.Y);
        _isDrawing = true;

        _rubberBand = new Rectangle
        {
            Stroke = Brushes.Black,
            StrokeDashArray = new DoubleCollection { 4, 2 },
            Fill = Brushes.Transparent
        };
        Canvas.SetLeft(_rubberBand, _startPoint.X);
        Canvas.SetTop(_rubberBand, _startPoint.Y);
        canvas.Children.Add(_rubberBand);
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDrawing || _startPoint == null || _rubberBand == null) return;

        var current = e.GetPosition(canvas);
        double x = Math.Min(_startPoint.X, current.X);
        double y = Math.Min(_startPoint.Y, current.Y);
        double w = Math.Abs(current.X - _startPoint.X);
        double h = Math.Abs(current.Y - _startPoint.Y);

        _rubberBand.Width = w;
        _rubberBand.Height = h;
        Canvas.SetLeft(_rubberBand, x);
        Canvas.SetTop(_rubberBand, y);
    }

    private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (!_isDrawing || _startPoint == null || _currentFactory == null || _currentShapeType == null) return;

        var endPoint = e.GetPosition(canvas);

        if (_rubberBand != null)
        {
            canvas.Children.Remove(_rubberBand);
            _rubberBand = null;
        }

        MyPoint topLeft = new MyPoint(Math.Min(_startPoint.X, endPoint.X), Math.Min(_startPoint.Y, endPoint.Y));
        MyPoint downRight = new MyPoint(Math.Max(_startPoint.X, endPoint.X), Math.Max(_startPoint.Y, endPoint.Y));

        if (Math.Abs(downRight.X - topLeft.X) > 3 && Math.Abs(downRight.Y - topLeft.Y) > 3)
        {
            Brush bgColor = _currentShapeType == "Dot" ? Brushes.Transparent : _colors[_random.Next(_colors.Length)];
            var shape = _currentFactory.CreateShape(topLeft, downRight, bgColor, Brushes.Black, 0);
            AddShape(shape);
            lblStatus.Text = $"Created: {shape.GetShapeName()}";
        }
        else
        {
            lblStatus.Text = "Shape too small - try again";
        }

        _isDrawing = false;
        _startPoint = null;
        _currentShapeType = null;
        _currentFactory = null;
    }

    private void AddShape(AbstractShape shape)
    {
        _shapes.Add(shape);
        var wpfShape = shape.Draw();
        wpfShape.Tag = _shapes.Count - 1;
        wpfShape.MouseLeftButtonDown += (s, e) =>
        {
            if (s is Shape sh && sh.Tag is int i)
                lstShapes.SelectedIndex = i;
        };
        canvas.Children.Add(wpfShape);
        _shapeElements.Add(wpfShape);
        UpdateList();
    }

    private void UpdateList()
    {
        lstShapes.Items.Clear();
        foreach (var s in _shapes)
            lstShapes.Items.Add(s.ToString());
        lblStatus.Text = $"Shapes: {_shapes.Count}";
    }

    private void ClearAll(object sender, RoutedEventArgs e)
    {
        _shapes.Clear();
        _shapeElements.Clear();
        canvas.Children.Clear();
        UpdateList();
    }

    private void LstShapes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (lstShapes.SelectedIndex >= 0)
            lblStatus.Text = $"Selected: {_shapes[lstShapes.SelectedIndex]}";
    }
}