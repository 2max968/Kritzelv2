using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kritzel.Main.Renderer;

namespace Kritzel.Main.Backgrounds
{
    public abstract class Background
    {
        public abstract void Draw(Renderer.BaseRenderer r, PageFormat format, float border, Color mainColor, Color secondaryColor);
        public virtual void DrawThumbnail(Bitmap thumbnail)
        {
            float iconFactor = 2f / Util.GetScaleFactor();
            PageFormat format =
                        new PageFormat(Util.PointToMm(thumbnail.Width) * iconFactor, Util.PointToMm(thumbnail.Height) * iconFactor);
            using (Graphics g = Graphics.FromImage(thumbnail))
            {
                g.ScaleTransform(1 / iconFactor, 1 / iconFactor);
                this.Draw(g.GetRenderer(), format,
                    2, Color.LightGray, Color.Red);
            }
        }
    }

    public class BackgroundNull : Background
    {
        public override void Draw(BaseRenderer r, PageFormat format, float border, Color mainColor, Color secondaryColor)
        {
            
        }
    }

    public class BName : Attribute
    {
        public string Name { get; private set; }
        public BName(string name)
        {
            this.Name = name;
        }
    }
}
