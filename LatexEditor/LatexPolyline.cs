using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        public List<LatexPoint> pointList { get; set; }
        private List<Line> lines;

        public LatexPolyline(Canvas mainCanvas)
        {
            pointList = new List<LatexPoint>();
            lines = new List<Line>();
            this.mainCanvas = mainCanvas;
        }

        public void AddPoint(LatexPoint latexPoint, Canvas mainCanvas)
        {
            pointList.Add(latexPoint);            
        }

        internal void Draw()
        {
           foreach (Line line in lines) // Remove all lines 
            {
                 mainCanvas.Children.Remove(line);
            }

            foreach (LatexPoint point in pointList) // Draw points
            {
                point.Draw();
            }

            for (int i =0; i<pointList.Count-1; i++) // Draw new lines
            {
                Line line = new Line();
                line.X1 = pointList[i].X + 3;
                line.Y1 = pointList[i].Y + 3;
                line.X2 = pointList[i+1].X + 3;
                line.Y2 = pointList[i+1].Y + 3;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                mainCanvas.Children.Add(line);
                lines.Add(line);

            }        
        }

        internal void UpdatePoint(Ellipse draggedPoint, LatexPoint latexPoint)
        {
            foreach (LatexPoint point in pointList) 
            {
                if (point.ellipse == draggedPoint)
                {
                    point.SetPosition(latexPoint);
                    Canvas.SetLeft(point.ellipse, latexPoint.X);
                    Canvas.SetTop(point.ellipse, latexPoint.Y);
                    break;
                }
            }           
        }

        public override bool Contain(Ellipse elipse)
        {
            foreach (LatexPoint point in pointList)
            {
                if (point.ellipse == elipse)
                    return true;
            }
            return false;
        }

        public override void SaveToLatex(string filePath)
        {
            StreamWriter file = File.AppendText(filePath);
            file.WriteLine("\\draw");                   
            foreach (LatexNode point in pointList)
            {
                if (point == pointList.ElementAt(0))
                    lineStyle = "  ";
                else
                    lineStyle = "--";

                file.WriteLine(lineStyle + " (" + point.X.ToString(new CultureInfo("en-US")) + " , " + RecalculateCoordinateY(point.Y).ToString(new CultureInfo("en-US")) + ") node {}");                
            }
            file.WriteLine(";");
            file.Close();
        }
    }
}
