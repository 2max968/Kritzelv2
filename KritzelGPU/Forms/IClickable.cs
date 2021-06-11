using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Forms
{
    public interface IClickable
    {
        void Click(InkControl parent);
        RectangleF GetClickableBounds();
    }
}
