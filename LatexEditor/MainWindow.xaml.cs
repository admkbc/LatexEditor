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
        private List<Tuple<LatexPoint, Ellipse>> canvasComponents;
        private List<Component> components;
        private Component activeComponent;
        private Ellipse draggedPoint;

        public MainWindow()
		{
			InitializeComponent();
			InitVariables();
		}

		#region Private functions
		private void InitVariables()
		{
			components = new List<Component>();
            canvasComponents = new List<Tuple<LatexPoint, Ellipse>>();
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
                draggedPoint.Fill = new SolidColorBrush(Colors.Red);
                draggedPoint.Stroke = new SolidColorBrush(Colors.Red);
                StatusBarTextBlock.Text = "Przenoszenie węzła...";
            }
            else if (e.OriginalSource is Canvas)
            {
                if (activeComponent!= null)  
                {
                    LatexPolyline poly = activeComponent as LatexPolyline;
                    LatexPoint point = new LatexPoint(mouseClick.X, mouseClick.Y);
                    poly.AddPoint(point, MainCanvas);
                    poly.Draw(MainCanvas);
                }               
            }
                
         
        }


        private void MainCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPoint != null)
            {
                Point mousePosition = Mouse.GetPosition(MainCanvas);
                LatexPolyline poly = activeComponent as LatexPolyline;
                poly.UpdatePoint(draggedPoint, new LatexPoint(mousePosition.X, mousePosition.Y));
                poly.Draw(MainCanvas);
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
                string fileType = ParseFileType(dlg);
                if (fileType == "tex")
                    saveTex(dlg.FileName);
            }
        }

        private string ParseFileType(SaveFileDialog dlg)
        {
            string fileType = dlg.FileName;
            int dotPosition = fileType.IndexOf('.');
            return fileType.Substring(dotPosition + 1);
        }

        private void saveTex(string filePath)
        {
            StreamWriter fileBegin =  File.CreateText(filePath);
            fileBegin.WriteLine("\\documentclass{standalone}");
            fileBegin.WriteLine("\\usepackage{tikz}");
            fileBegin.WriteLine("\\begin{document}");
            fileBegin.WriteLine("\\begin{tikzpicture}");
            fileBegin.Close();
            foreach (Component component in components)
                component.SaveToLatex(filePath);
            StreamWriter fileEnd = File.AppendText(filePath);
            fileEnd.WriteLine("\\end{tikzpicture}");
            fileEnd.WriteLine("\\end{document}");
            fileEnd.Close();
        }

        #endregion
    }
}
