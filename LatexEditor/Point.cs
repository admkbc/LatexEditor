
using System.IO;
using System.Windows;

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

    }
}
