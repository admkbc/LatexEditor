using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LatexEditor
{
	static class CanvasExtensionMethods
	{
		/// <summary>
		/// Draws blob in area of canvas specified by point parameter.
		/// </summary>
		/// <param name="canvas">Extension method parameter</param>
		/// <param name="point">area if canvas the blob is created at</param>
		public static void DrawBlob(this Canvas canvas, LatexPoint point)
		{
			Ellipse blob = new Ellipse();
			blob.Width = 5;
			blob.Height = 5;
			blob.Stroke = new SolidColorBrush(Colors.Black);
			blob.Fill = new SolidColorBrush(Colors.Black);
			blob.StrokeThickness = 5;
			Canvas.SetLeft(blob, point.X);
			Canvas.SetTop(blob, point.Y);
			canvas.Children.Add(blob);
		}

		/// <summary>
		/// Draws a line recreated from line argument.
		/// </summary>
		/// <param name="canvas">Extension method parameter</param>
		/// <param name="line">drawn object</param>
		public static void DrawLine(this Canvas canvas, LatexLine line)
		{
			Line newLine = new Line();
			newLine.X1 = line.P1.X + 2.5;
			newLine.Y1 = line.P1.Y + 2.5;
			newLine.X2 = line.P2.X + 2.5;
			newLine.Y2 = line.P2.Y + 2.5;
			newLine.Stroke = new SolidColorBrush(Colors.Black);
			newLine.StrokeThickness = 1;
			canvas.Children.Add(newLine);
		}

		public static void ClearBlob(this Canvas canvas, Ellipse ellipse)
		{
			canvas.Children.Remove(ellipse);
		}

		public static void DrawFocusedBlob(this Canvas canvas, LatexPoint point)
		{
			Ellipse focusedBlob = new Ellipse();
			focusedBlob.Width = 5;
			focusedBlob.Height = 5;
			focusedBlob.Stroke = new SolidColorBrush(Colors.Red);
			focusedBlob.Fill = new SolidColorBrush(Colors.Red);
			focusedBlob.StrokeThickness = 5;
			Canvas.SetLeft(focusedBlob, point.X);
			Canvas.SetTop(focusedBlob, point.Y);
			canvas.Children.Add(focusedBlob);
		}
	}
}
