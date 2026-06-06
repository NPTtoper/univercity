using System;
using System.Drawing;
using System.Windows.Forms;
using OOTPiSP_Lab1.Models;
using OOTPiSP_Lab1.Models.Shapes;

namespace Lab1OOP
{
    public partial class Form1 : Form
    {
        private ShapeList shapeList = new ShapeList();

        public Form1()
        {
            InitializeComponent();
            InitializeShapes();

            this.canvas.Paint += Canvas_Paint;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            shapeList.DrawAll(g);
        }

        private void InitializeShapes()
        {
            shapeList.Add(new Line(30, 60, 180, 160));
            shapeList.Add(new RectangleShape(220, 60, 130, 90));
            shapeList.Add(new Ellipse(400, 60, 110, 80));
            shapeList.Add(new Triangle(560, 60, 680, 160, 600, 180));
            shapeList.Add(new Square(30, 280, 110));
            shapeList.Add(new Circle(220, 330, 55));

            lblStatus.Text = "Status: 6 shapes loaded (static initialization)";
            canvas.Invalidate(); 
        }

        private void AddShape(IShape shape)
        {
            shapeList.Add(shape);
            canvas.Invalidate();
            lblStatus.Text = $"Status: Added {shape.ToString()}";
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            AddShape(new Line(50, 150, 200, 250));
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            AddShape(new RectangleShape(250, 100, 120, 80));
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            AddShape(new Ellipse(400, 100, 100, 70));
        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            AddShape(new Triangle(550, 150, 650, 250, 580, 280));
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            AddShape(new Square(50, 320, 100));
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            AddShape(new Circle(250, 370, 50));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            shapeList.Clear();
            canvas.Invalidate();
            lblStatus.Text = "Status: All shapes cleared";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            shapeList.PrintAllToConsole();
            lblStatus.Text = "Status: Printed to console";
        }
    }
}