
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LatexEditor
{
	class LatexPoint : Component
	{
		/// <summary>
		/// Creates an instance with position 0:0
		/// </summary>
		public LatexPoint ()
		{
		}

		/// <summary>
		/// Creates an instance with set position.
		/// </summary>
		/// <param name="x">X position.</param>
		/// <param name="y">Y position.</param>
		public LatexPoint (double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Creates an instance from existing Point class object.
		/// </summary>
		/// <param name="point">Position object</param>
		public LatexPoint (Point point)
		{
			this.X = point.X;
			this.Y = point.Y;
		}

        public void SetPosition(LatexPoint point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }

        public override void SaveToLatex(string filePath)
        {
            StreamWriter file = File.AppendText(filePath);
            file.Write("\\draw(" + X + "," +  Y + ") circle(0.2pt) node{ }");
            file.Close();
        }

        internal void Draw(Canvas mainCanvas)
        {
            Ellipse elipse = new Ellipse();
            elipse.Width = 6;
            elipse.Height = 6;
            elipse.Stroke = new SolidColorBrush(Colors.Black);
            elipse.Fill = new SolidColorBrush(Colors.Black);
            elipse.StrokeThickness = 6;
            Canvas.SetLeft(elipse, X);
            Canvas.SetTop(elipse, Y);
            mainCanvas.Children.Add(elipse);
        }

    }
}
