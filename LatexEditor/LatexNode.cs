using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LatexEditor
{
    class LatexNode : Component
    {
        /// <summary>
		/// Creates an instance
		/// </summary>
        public LatexNode() { }

        public LatexNode(Canvas mainCanvas)
        {
            this.mainCanvas = mainCanvas;
        }
    }
}
