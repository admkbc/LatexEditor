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
        private List<Component> components;
        private Component activeComponent;
        private Ellipse draggedPoint;
        private LatexPoint firstPoint;

        public MainWindow()
		{
			InitializeComponent();
			InitVariables();
		}

		#region Private functions
		private void InitVariables()
		{
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
            if (e.OriginalSource is Ellipse)
            {
                draggedPoint = e.OriginalSource as Ellipse;
                if (firstPoint != null)
                {
                    components.Add(activeComponent);
                    LatexPolyline poly = new LatexPolyline();
                    poly.AddPoint(firstPoint, MainCanvas);
                    LatexPoint point = new LatexPoint(mouseClick.X, mouseClick.Y);
                    poly.AddPoint(point, MainCanvas);
                    poly.Draw(MainCanvas);
                    activeComponent = poly;
                    firstPoint = null;
                    var fPoint = (UIElement)LogicalTreeHelper.FindLogicalNode(MainCanvas, "fpoint");
                    MainCanvas.Children.Remove(fPoint);
                    MainCanvas.Children.Remove(draggedPoint);
                }               
                else
                {
                    firstPoint = new LatexPoint(mouseClick);
                    draggedPoint.Name = "fpoint";
                    draggedPoint.Fill = new SolidColorBrush(Colors.Red);
                    draggedPoint.Stroke = new SolidColorBrush(Colors.Red);
                    StatusBarTextBlock.Text = "Przenoszenie węzła...";
                }
            }
            else if (e.OriginalSource is Canvas)
            {
                if (activeComponent != null)  
                {
                    if (activeComponent is LatexPolyline)
                    {
                        LatexPolyline poly = activeComponent as LatexPolyline;
                        LatexPoint point = new LatexPoint(mouseClick.X, mouseClick.Y);
                        poly.AddPoint(point, MainCanvas);
                        poly.Draw(MainCanvas);
                    }
                    else if (activeComponent is LatexPoint)
                    {
                        LatexPoint node = activeComponent as LatexPoint;
                        LatexPoint point = new LatexPoint(mouseClick.X, mouseClick.Y);
                        node.SetPosition(point);
                        node.Draw(MainCanvas);
                    }
                }               
            }          
        }

        private void MainCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPoint != null)
            {
                if (activeComponent is LatexPolyline)
                {
                    activeComponent = FindActiveComponent(draggedPoint); // Select component which contain draggedPoint 
                    Point mousePosition = Mouse.GetPosition(MainCanvas);
                    LatexPolyline poly = activeComponent as LatexPolyline;
                    poly.UpdatePoint(draggedPoint, new LatexPoint(mousePosition.X, mousePosition.Y));
                    poly.Draw(MainCanvas);
                    firstPoint = null;
                }
            }
        }


        private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (draggedPoint != null)
            {
                draggedPoint.Fill = new SolidColorBrush(Colors.Black);
                draggedPoint.Stroke = new SolidColorBrush(Colors.Black);
                StatusBarTextBlock.Text = "Gotowy.";
                draggedPoint = null;
            }
        }
        
        private void NodeButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            activeComponent = new LatexPoint();
            components.Add(activeComponent);
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            activeComponent = new LatexPolyline();
            components.Add(activeComponent);            
        }

        private void SaveMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "JPG Image|*.jpg|PDF file|*.pdf|Latex file|*.tex";
            var result = dlg.ShowDialog();
            if (result == true)
            {                
                string fileType = System.IO.Path.GetExtension(dlg.FileName);               
                if (fileType == ".tex")
                    Save.saveTex(dlg.FileName, components);
                if (fileType == ".jpg")
                    Save.saveJpg(dlg.FileName, MainCanvas);  
            }
        }

        private Component FindActiveComponent(Ellipse draggedPoint)
        {
            foreach (Component component in components)
            {
                if (typeof(LatexPolyline) == component.GetType())
                {
                    LatexPolyline poly = component as LatexPolyline;
                    if (poly.Contain(draggedPoint))
                        return component;
                }                
            }
            return null; // error jak przesuwamy punkt; 
        }

        #endregion

	}
}
