using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Lab3.Factories;
using Lab3.Figures;
using Lab3.Serialization;

namespace Lab3
{
    public partial class MainWindow : Window
    {
        private List<Figure> figures = new List<Figure>();
        private IFigureFactory currentFactory;
        private Point? firstPoint = null;
        private int selectedIndex = -1;

        public MainWindow()
        {
            InitializeComponent();

            FigureFactoryBase.RegisterFactory(new LineFactory());
            FigureFactoryBase.RegisterFactory(new CircleFactory());
            FigureFactoryBase.RegisterFactory(new RectangleFactory());

            string pluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            PluginLoader.LoadPlugins(pluginsPath);

            FigureComboBox.ItemsSource = FigureFactoryBase.GetNames();
            FigureComboBox.SelectedIndex = 0;
        }

        private void DrawCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string selectedName = FigureComboBox.SelectedItem as string;
            if (selectedName == null) return;

            currentFactory = FigureFactoryBase.GetByName(selectedName);
            Point clickPoint = e.GetPosition(DrawCanvas);

            if (firstPoint == null)
            {
                firstPoint = clickPoint;
            }
            else
            {
                Figure figure = currentFactory.CreateFigure();
                figure.StartPoint = firstPoint.Value;
                figure.EndPoint = clickPoint;

                figures.Add(figure);
                RedrawCanvas();
                RefreshFiguresList();
                firstPoint = null;
            }
        }

        private void RedrawCanvas()
        {
            DrawCanvas.Children.Clear();
            foreach (var figure in figures)
            {
                DrawCanvas.Children.Add(figure.GetShape());
            }
        }

        private void RefreshFiguresList()
        {
            FiguresListBox.Items.Clear();
            for (int i = 0; i < figures.Count; i++)
            {
                FiguresListBox.Items.Add($"{figures[i].FigureType} #{i + 1}");
            }
        }

        private void FiguresListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedIndex = FiguresListBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < figures.Count)
            {
                Figure figure = figures[selectedIndex];
                StartXBox.Text = figure.StartX.ToString("F0");
                StartYBox.Text = figure.StartY.ToString("F0");
                EndXBox.Text = figure.EndX.ToString("F0");
                EndYBox.Text = figure.EndY.ToString("F0");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < figures.Count)
            {
                Figure figure = figures[selectedIndex];
                figure.StartX = double.Parse(StartXBox.Text);
                figure.StartY = double.Parse(StartYBox.Text);
                figure.EndX = double.Parse(EndXBox.Text);
                figure.EndY = double.Parse(EndYBox.Text);
                RedrawCanvas();
                RefreshFiguresList();
                FiguresListBox.SelectedIndex = selectedIndex;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < figures.Count)
            {
                figures.RemoveAt(selectedIndex);
                RedrawCanvas();
                RefreshFiguresList();
                selectedIndex = -1;
                StartXBox.Clear();
                StartYBox.Clear();
                EndXBox.Clear();
                EndYBox.Clear();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                DefaultExt = "json"
            };

            if (dialog.ShowDialog() == true)
            {
                FigureSerializer.Serialize(dialog.FileName, figures);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (dialog.ShowDialog() == true)
            {
                figures = FigureSerializer.Deserialize(dialog.FileName);
                RedrawCanvas();
                RefreshFiguresList();
            }
        }
    }
}