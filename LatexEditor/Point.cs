
using System.Windows;

namespace LatexEditor
{
	class LatexPoint : Component
	{
		/// <summary>
		/// X position.
		/// </summary>
		public double X { get; set; }
		
		/// <summary>
		/// Y position.
		/// </summary>
		public double Y { get; set; }


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

		/// <summary>
		/// Sets position.
		/// </summary>
		/// <param name="point">Position</param>
		public void SetPosition(Point point)
		{
			this.X = point.X;
			this.Y = point.Y;
		}
	}
}
