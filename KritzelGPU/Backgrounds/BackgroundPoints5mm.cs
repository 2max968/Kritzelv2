using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kritzel.Main.Renderer;

namespace Kritzel.Main.Backgrounds
{
    [BName("Points 5mm")]
    public class BackgroundPoints5mm : Background
    {
        public override void Draw(BaseRenderer r, PageFormat format, float border, Color mainColor, Color secondaryColor)
        {
            PBrush brush = PBrush.CreateSolid(mainColor);
            r.BeginCircles(brush);
            for(float x = border; x < format.Width - border; x += 5)
            {
                float xpx = Util.MmToPoint(x);
                for(float y = border; y < format.Height - border; y += 5)
                {
                    float ypx = Util.MmToPoint(y);
                    r.Circle(xpx, ypx, 1);
                }
            }
            r.EndCircle();
        }
    }
}
