using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatexEditor
{
	class LatexLine : Component // Class only for line properties 
	{
        public string lineStyle { get; set; }
        public string lineSize { get; set; }
        public string lineEnd { get; set; }
        public string lineColor { get; set; }
       

        public LatexLine()
		{
            lineColor = "Black";
        }

        public override void SaveToLatex(string filePath)
        {
            StreamWriter file = File.AppendText(filePath);
            file.WriteLine("\\draw (" + X + "," + Y + ") -- (" + lineEnd + ");");  //endLine ma mieć format: x,y
            file.Close();
        }

    }
}
