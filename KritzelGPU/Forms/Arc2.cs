using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Forms
{
    public class Arc2 : Arc
    {
        public static Bitmap BitmapArc2 = ResManager.LoadIcon("tools/circ2.svg", Util.GetGUISize());

        LPoint startPoint;

        public override void AddPoint(float x, float y, float pressure)
        {
            if(Points.Count == 0)
            {
                startPoint = new LPoint(x, y, 1);
            }
            else
            {
                Points[0].X = (startPoint.X + x) / 2;
                Points[0].Y = (startPoint.Y + y) / 2;
            }
            base.AddPoint(x, y, pressure);
        }
    }
}
