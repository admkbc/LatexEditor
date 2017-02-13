using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatexEditor
{
	class LatexLine : Component
	{
		public LatexPoint P1 { get; set; }
		public LatexPoint P2 { get; set; }

		public LatexLine()
		{
		}

		public LatexLine(LatexPoint P1, LatexPoint P2)
		{
			this.P1 = P1;
			this.P2 = P2;
		}

		public LatexLine(double x1, double y1, double x2, double y2)
		{
			P1 = new LatexPoint(x1, y1);
			P2 = new LatexPoint(x2, y2);
		}
	}
}
