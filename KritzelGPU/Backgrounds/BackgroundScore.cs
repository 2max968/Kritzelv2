using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kritzel.Main.Renderer;

namespace Kritzel.Main.Backgrounds
{
    [BName("Score")]
    public class BackgroundScore : Background
    {
        public override void Draw(BaseRenderer r, PageFormat format, float border, Color mainColor, Color secondaryColor)
        {
            float borderpx = Util.MmToPoint(border);
            float lineHeight = 32;
            int numLines = (int)((format.Height - 2 * border) / lineHeight);
            float offset = 10;
            float lineDist = 3;

            for(int i = 0; i < numLines; i++)
            {
                for(int b = 0; b < 5; b++)
                {
                    float pos = border + i * lineHeight + offset + b * lineDist;
                    float pt = Util.MmToPoint(pos);
                    r.DrawLine(mainColor, 1, new PointF(borderpx, pt),
                    new PointF(format.GetPixelSize().Width - borderpx, pt));
                }
            }
        }
    }
}
