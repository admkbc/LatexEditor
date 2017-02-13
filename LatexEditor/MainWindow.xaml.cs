using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
namespace LatexEditor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Consts

		#endregion

		#region Variables

		/// <summary>
		/// First node/point.
		/// </summary>
		private Ellipse firstPoint;

		/// <summary>
		/// Currently dragged node/point
		/// </summary>
		private Ellipse draggedPoint;


		/// <summary>
		/// Collection of canvas elements
		/// </summary>
		private List<Tuple<LatexPoint, Ellipse>> canvasComponents;
		#endregion


		private List<Component> components;
		public MainWindow()
		{
			InitializeComponent();
			InitVariables();
		}

		#region Private functions
		private void InitVariables()
		{
			firstPoint = default(Ellipse);
			draggedPoint = default(Ellipse);
			canvasComponents = new List<Tuple<LatexPoint, Ellipse>>();
			components = new List<Component>();
		}
		#endregion

		#region Events
		/// <summary>
		/// Mouse left button pressed event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainCanvas_LeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Point mouseClick = Mouse.GetPosition(MainCanvas);
			if (draggedPoint != null)
			{
				draggedPoint.SetPosition(mouseClick);
				//MainCanvas.DrawBlob(draggedPoint);
			}
			else
			{
				if (e.OriginalSource is Ellipse)
				{
					Ellipse focusedNode = e.OriginalSource as Ellipse;
					MainCanvas.ClearBlob(focusedNode);
					draggedPoint = GetFocusedNode(focusedNode);
					MainCanvas.DrawFocusedBlob(draggedPoint);
				}
				else if (firstPoint == null)
				{
					firstPoint = new LatexPoint(mouseClick.X, mouseClick.Y);
					components.Add(firstPoint);
					MainCanvas.DrawBlob(firstPoint);
				}
				else
				{
					if (e.OriginalSource is Ellipse)
					{
						Ellipse node = e.OriginalSource as Ellipse;
						MainCanvas.ClearBlob(node);
						node.Fill = new SolidColorBrush(Colors.Red);
						node.Stroke = new SolidColorBrush(Colors.Red);

					}
					else
					{
						LatexPoint secondPoint = new LatexPoint(mouseClick.X, mouseClick.Y);
						MainCanvas.DrawBlob(secondPoint);
						LatexLine newLine = new LatexLine(firstPoint, secondPoint);
						MainCanvas.DrawLine(newLine);
						components.Add(secondPoint);
						components.Add(newLine);
						firstPoint = default(LatexPoint);
					}
				}
			}



			//if (IsMovePoint)
			//{
			//	MovePoint.Fill = new SolidColorBrush(Colors.Black);
			//	MovePoint.Stroke = new SolidColorBrush(Colors.Black);
			//	IsMovePoint = false;
			//	StatusBarTextBlock.Text = "Gotowy";
			//}
			//else
			//{
			//	if (e.OriginalSource is Ellipse)
			//	{
			//		MovePoint = e.OriginalSource as Ellipse;
			//		MovePoint.Fill = new SolidColorBrush(Colors.Red);
			//		MovePoint.Stroke = new SolidColorBrush(Colors.Red);
			//		IsMovePoint = true;
			//		StatusBarTextBlock.Text = "Przenoszenie węzła...";
			//	}
			//	else if (IsDrawnPointMoved)
			//	{
			//		// przenoszenie punktu
			//	}

			//	else if (e.OriginalSource is Canvas)
			//	{
			//		LatexLine newLine = null;
			//		if (!IsFirstPointSet)
			//		{
			//			Line line = new Line();
			//			line.X1 = FirstPoint.X + 2.5;
			//			line.Y1 = FirstPoint.Y + 2.5;
			//			line.X2 = mouseClick.X + 2.5;
			//			line.Y2 = mouseClick.Y + 2.5;
			//			line.Stroke = new SolidColorBrush(Colors.Black);
			//			line.StrokeThickness = 1;
			//			MainCanvas.Children.Add(line);
			//			IsFirstPointSet = true;
			//		}
			//		else
			//		{
			//			IsFirstPointSet = false;
			//			// stwórz nową linię
			//			newLine = new LatexLine();
			//		}

			//		Ellipse point = new Ellipse();
			//		point.Width = 5;
			//		point.Height = 5;
			//		point.Stroke = new SolidColorBrush(Colors.Black);
			//		point.Fill = new SolidColorBrush(Colors.Black);
			//		point.StrokeThickness = 5;
			//		Canvas.SetLeft(point, mouseClick.X);
			//		Canvas.SetTop(point, mouseClick.Y);
			//		MainCanvas.Children.Add(point);

			//		//FirstPoint = mouseClick;

			//		// dodawanie pierwszego punktu linii
			//		newLine.P1 = new LatexPoint(mouseClick.X, mouseClick.Y);
			//	}
		}

		private LatexPoint GetFocusedNode(Ellipse focusedNode)
		{
			foreach (var component in components)
			{
				if (component is LatexPoint)
				{
					LatexPoint point = component as LatexPoint;
					double x = Canvas.GetLeft(focusedNode);
					double y = Canvas.GetTop(focusedNode);
					if (x == point.X && y == point.Y)
						return point;
				}
			}
			return null;
		}

		private void MainCanvas_OnMouseMove(object sender, MouseEventArgs e)
		{
			if (draggedPoint != null)
			{
				Point mousePosition = Mouse.GetPosition(MainCanvas);
				draggedPoint.SetPosition(mousePosition);
				MainCanvas.DrawBlob(draggedPoint);
			}
		}

		private void SaveMenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "JPG Image|*.jpg|PDF file|*.pdf|Latex file|*.tex";
			var result = dlg.ShowDialog();
			if (result == true)
			{

			}
		}

		#endregion

		private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (draggedPoint != null)
			{
				if (e.OriginalSource is Ellipse)
				{
					Ellipse replacedBlob = e.OriginalSource as Ellipse;
					MainCanvas.ClearBlob(replacedBlob);
					MainCanvas.DrawBlob(draggedPoint);
					draggedPoint = default(LatexPoint);
				}

			}
		}
	}
}
