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

        #endregion

        #region Variables

        private Point FirstPoint;
        private bool IsForstPoint;
        private Ellipse MovePoint;
        private bool IsMovePoint;
        
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            IsForstPoint = true;
            IsMovePoint = false;
        }

        #region Events
        private void MainCanvas_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMovePoint)
            {
                MovePoint.Fill = new SolidColorBrush(Colors.Black);
                MovePoint.Stroke = new SolidColorBrush(Colors.Black);
                IsMovePoint = false;
            }
            else
            {
                if (e.OriginalSource is Ellipse)
                {
                    MovePoint = e.OriginalSource as Ellipse;
                    MovePoint.Fill = new SolidColorBrush(Colors.Red);
                    MovePoint.Stroke = new SolidColorBrush(Colors.Red);
                    IsMovePoint = true;
                }
                else if (e.OriginalSource is Canvas)
                {
                    Point mouseClick = Mouse.GetPosition(MainCanvas);
                    if (!IsForstPoint)
                    {
                        Line line = new Line();
                        line.X1 = FirstPoint.X + 2.5;
                        line.Y1 = FirstPoint.Y + 2.5;
                        line.X2 = mouseClick.X + 2.5;
                        line.Y2 = mouseClick.Y + 2.5;
                        line.Stroke = new SolidColorBrush(Colors.Black);
                        line.StrokeThickness = 1;
                        MainCanvas.Children.Add(line);
                        IsForstPoint = true;
                    }
                    else
                    {
                        IsForstPoint = false;
                    }

                    Ellipse point = new Ellipse();
                    point.Width = 5;
                    point.Height = 5;
                    point.Stroke = new SolidColorBrush(Colors.Black);
                    point.Fill = new SolidColorBrush(Colors.Black);
                    point.StrokeThickness = 5;
                    Canvas.SetLeft(point, mouseClick.X);
                    Canvas.SetTop(point, mouseClick.Y);
                    MainCanvas.Children.Add(point);
                    FirstPoint = mouseClick;
                }
            }
        }


        #endregion

        private void MainCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMovePoint)
            {
                Point mousePosition = Mouse.GetPosition(MainCanvas);
                Canvas.SetLeft(MovePoint, mousePosition.X + 2.5);
                Canvas.SetTop(MovePoint, mousePosition.Y + 2.5);
            }
        }
    }
}
