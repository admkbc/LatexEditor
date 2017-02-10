using System;
using System.Collections.Generic;
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

namespace LatexEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Consts

        private const string FirstPointName = "fPoint";
        
        #endregion

        #region Variables

        private Point FirstPoint;
        private bool IsForstPoint;
        
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            IsForstPoint = true;
        }

        #region Events
        private void MainCanvas_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseClick = Mouse.GetPosition(MainCanvas);
            if (IsForstPoint)
            {
                Ellipse point = new Ellipse();
                point.Width = 3;
                point.Height = 3;
                point.Stroke = new SolidColorBrush(Colors.Black);
                point.Fill = new SolidColorBrush(Colors.Black);
                point.StrokeThickness = 3;
                point.Name = FirstPointName;
                // Set Canvas position
                Canvas.SetLeft(point, mouseClick.X);
                Canvas.SetTop(point, mouseClick.Y);
                MainCanvas.Children.Add(point);
                IsForstPoint = false;
                FirstPoint = mouseClick;
            }
            else
            {
                Line line = new Line();
                line.X1 = FirstPoint.X;
                line.Y1 = FirstPoint.Y;
                line.X2 = mouseClick.X;
                line.Y2 = mouseClick.Y;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 3;
                var fPoint = (UIElement)LogicalTreeHelper.FindLogicalNode(MainCanvas, FirstPointName);
                MainCanvas.Children.Remove(fPoint);
                MainCanvas.Children.Add(line);
                IsForstPoint = true;
            }
        }


        #endregion

    }
}
