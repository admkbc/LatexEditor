using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LatexEditor
{
    class LatexPolyline : LatexLine
    {
       //private List<LatexPoint> pointList;
        //private Dictionary<Ellipse, LatexPoint> pointList;
        private List<Tuple<LatexPoint, Ellipse>> pointList;
        private List<Line> lines;

        public LatexPolyline()
        {
            pointList = new List<Tuple<LatexPoint, Ellipse>>();
            lines = new List<Line>();
        }

        public void AddPoint(LatexPoint latexPoint, Canvas mainCanvas)
        {
            Ellipse elipse = new Ellipse();
            elipse.Width = 6;
            elipse.Height = 6;
            elipse.Stroke = new SolidColorBrush(Colors.Black);
            elipse.Fill = new SolidColorBrush(Colors.Black);
            elipse.StrokeThickness = 6;
            Canvas.SetLeft(elipse, latexPoint.X);
            Canvas.SetTop(elipse, latexPoint.Y);
            pointList.Add(Tuple.Create(latexPoint, elipse));
            mainCanvas.Children.Add(elipse);        
        }

        internal void Draw(Canvas mainCanvas)
        {
           foreach (Line line in lines) // Remove all lines 
            {
                 mainCanvas.Children.Remove(line);
            }

            for (int i =0; i<pointList.Count-1; i++) // Draw new lines
            {
                Line line = new Line();
                line.X1 = pointList[i].Item1.X + 3;
                line.Y1 = pointList[i].Item1.Y + 3;
                line.X2 = pointList[i+1].Item1.X + 3;
                line.Y2 = pointList[i+1].Item1.Y + 3;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                mainCanvas.Children.Add(line);
                lines.Add(line);
            }        
        }

        internal void UpdatePoint(Ellipse draggedPoint, LatexPoint latexPoint)
        {
            foreach (Tuple<LatexPoint, Ellipse> pair in pointList) 
            {
                if (pair.Item2 == draggedPoint)
                {
                    pair.Item1.SetPosition(latexPoint);
                    Canvas.SetLeft(pair.Item2, latexPoint.X);
                    Canvas.SetTop(pair.Item2, latexPoint.Y);
                    break;
                }
            }
           
        }

        public bool Contain(Ellipse elipse)
        {
            foreach (Tuple<LatexPoint, Ellipse> pair in pointList)
            {
                if (pair.Item2 == elipse)
                    return true;
            }
            return false;
        }
    }
}
