using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace LatexEditor
{
	class Component
	{
		public double X { get; set; }
        public double Y { get; set; }
        public string position { get; set; }
        public Component parentCopmonent { get; set; }

        /// <summary>
        /// Creates an instance with position 0:0
        /// </summary>
        public Component()
        {
        }

        /// <summary>
        /// Creates an instance with set position.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        public Component(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Creates an instance from existing Point class object.
        /// </summary>
        /// <param name="point">Position object</param>
        public Component(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }

        /// <summary>
        /// Sets position.
        /// </summary>
        /// <param name="point">Position</param>
        public void SetPosition(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }

        virtual public void SaveToLatex(string filePath) { }

        public virtual bool Contain(Ellipse elipse)
        {
            MessageBox.Show("sssss");
            return false;
        }
    }
}
