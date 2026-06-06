namespace Lab1OOP
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            buttonPanel = new FlowLayoutPanel();
            btnLine = new Button();
            btnRectangle = new Button();
            btnEllipse = new Button();
            btnTriangle = new Button();
            btnSquare = new Button();
            btnCircle = new Button();
            btnClear = new Button();
            btnPrint = new Button();
            canvas = new PictureBox();
            lblStatus = new Label();
            buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
            SuspendLayout();
            // 
            // buttonPanel
            // 
            buttonPanel.BackColor = Color.LightGray;
            buttonPanel.Controls.Add(btnLine);
            buttonPanel.Controls.Add(btnRectangle);
            buttonPanel.Controls.Add(btnEllipse);
            buttonPanel.Controls.Add(btnTriangle);
            buttonPanel.Controls.Add(btnSquare);
            buttonPanel.Controls.Add(btnCircle);
            buttonPanel.Controls.Add(btnClear);
            buttonPanel.Controls.Add(btnPrint);
            buttonPanel.Dock = DockStyle.Top;
            buttonPanel.Location = new Point(0, 0);
            buttonPanel.Margin = new Padding(3, 4, 3, 4);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Padding = new Padding(6, 7, 6, 7);
            buttonPanel.Size = new Size(1125, 73);
            buttonPanel.TabIndex = 0;
            // 
            // btnLine
            // 
            btnLine.BackColor = Color.Black;
            btnLine.ForeColor = Color.White;
            btnLine.Location = new Point(9, 11);
            btnLine.Margin = new Padding(3, 4, 3, 4);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(114, 53);
            btnLine.TabIndex = 1;
            btnLine.Text = "Line";
            btnLine.UseVisualStyleBackColor = false;
            btnLine.Click += btnLine_Click;
            // 
            // btnRectangle
            // 
            btnRectangle.BackColor = Color.Blue;
            btnRectangle.ForeColor = Color.White;
            btnRectangle.Location = new Point(129, 11);
            btnRectangle.Margin = new Padding(3, 4, 3, 4);
            btnRectangle.Name = "btnRectangle";
            btnRectangle.Size = new Size(114, 53);
            btnRectangle.TabIndex = 2;
            btnRectangle.Text = "Rectangle";
            btnRectangle.UseVisualStyleBackColor = false;
            btnRectangle.Click += btnRectangle_Click;
            // 
            // btnEllipse
            // 
            btnEllipse.BackColor = Color.Red;
            btnEllipse.ForeColor = Color.White;
            btnEllipse.Location = new Point(249, 11);
            btnEllipse.Margin = new Padding(3, 4, 3, 4);
            btnEllipse.Name = "btnEllipse";
            btnEllipse.Size = new Size(114, 53);
            btnEllipse.TabIndex = 3;
            btnEllipse.Text = "Ellipse";
            btnEllipse.UseVisualStyleBackColor = false;
            btnEllipse.Click += btnEllipse_Click;
            // 
            // btnTriangle
            // 
            btnTriangle.BackColor = Color.Green;
            btnTriangle.ForeColor = Color.White;
            btnTriangle.Location = new Point(369, 11);
            btnTriangle.Margin = new Padding(3, 4, 3, 4);
            btnTriangle.Name = "btnTriangle";
            btnTriangle.Size = new Size(114, 53);
            btnTriangle.TabIndex = 4;
            btnTriangle.Text = "Triangle";
            btnTriangle.UseVisualStyleBackColor = false;
            btnTriangle.Click += btnTriangle_Click;
            // 
            // btnSquare
            // 
            btnSquare.BackColor = Color.Purple;
            btnSquare.ForeColor = Color.White;
            btnSquare.Location = new Point(489, 11);
            btnSquare.Margin = new Padding(3, 4, 3, 4);
            btnSquare.Name = "btnSquare";
            btnSquare.Size = new Size(114, 53);
            btnSquare.TabIndex = 5;
            btnSquare.Text = "Square";
            btnSquare.UseVisualStyleBackColor = false;
            btnSquare.Click += btnSquare_Click;
            // 
            // btnCircle
            // 
            btnCircle.BackColor = Color.Orange;
            btnCircle.ForeColor = Color.White;
            btnCircle.Location = new Point(609, 11);
            btnCircle.Margin = new Padding(3, 4, 3, 4);
            btnCircle.Name = "btnCircle";
            btnCircle.Size = new Size(114, 53);
            btnCircle.TabIndex = 6;
            btnCircle.Text = "Circle";
            btnCircle.UseVisualStyleBackColor = false;
            btnCircle.Click += btnCircle_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.DarkGray;
            btnClear.Location = new Point(729, 11);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(114, 53);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear All";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.DarkGray;
            btnPrint.Location = new Point(849, 11);
            btnPrint.Margin = new Padding(3, 4, 3, 4);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(137, 53);
            btnPrint.TabIndex = 8;
            btnPrint.Text = "Print to Console";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // canvas
            // 
            canvas.BackColor = Color.White;
            canvas.BorderStyle = BorderStyle.FixedSingle;
            canvas.Dock = DockStyle.Fill;
            canvas.Location = new Point(0, 73);
            canvas.Margin = new Padding(3, 4, 3, 4);
            canvas.Name = "canvas";
            canvas.Size = new Size(1125, 667);
            canvas.TabIndex = 1;
            canvas.TabStop = false;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.LightGray;
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Location = new Point(0, 740);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(6, 0, 0, 0);
            lblStatus.Size = new Size(1125, 47);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Status: Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 787);
            Controls.Add(canvas);
            Controls.Add(lblStatus);
            Controls.Add(buttonPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lab1 OOP";
            buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
            ResumeLayout(false);
        }

        // Объявление всех элементов (чтобы они были доступны в Form1.cs)
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Button btnTriangle;
        private System.Windows.Forms.Button btnSquare;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label lblStatus;
    }
}