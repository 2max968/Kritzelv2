using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kritzel.Main.Renderer;

namespace Kritzel.Main.Forms
{
    public class Marker : Line, IBackground
    {
        public static readonly Bitmap BitmapMarker = ResManager.LoadIcon("tools/marker.svg", Util.GetGUISize());

        public override float CalcRad(float pressure, float thicknes)
        {
            return thicknes;
        }

        public override void Render(BaseRenderer g, float quality = 1, int start = 0, bool simple = false)
        {
            if (Points.Count < 2) return;
            float rad = Points[0].Rad;
            g.BeginRects(Brush);
            for(int i = 0; i < Points.Count - 1; i++)
            {
                for (float t = 0; t < 1; t += .1f)
                {
                    if(Spline != null)
                    {
                        double x, y;
                        Spline.GetPoint(i + t, out x, out y);
                        RectangleF rect = new RectangleF((float)x - rad / 2, (float)y - rad, rad, rad * 2);
                        g.Rect(rect);
                    }
                }
            }
            g.EndRects();
        }
    }
}
