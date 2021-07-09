using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Pen
{
    public class Line
    {
        public List<PointF> Points;

        public Line()
        {
            Points = new List<PointF>();
        }

        public void Draw(RenderTarget rt, SharpDX.Direct2D1.Brush brush)
        {
            for(int i = 1; i < Points.Count; i++)
            {
                RawVector2 p1 = new RawVector2(Points[i - 1].X, Points[i - 1].Y);
                RawVector2 p2 = new RawVector2(Points[i].X, Points[i].Y);
                rt.DrawLine(p1, p2, brush);
            }
        }
    }
}
