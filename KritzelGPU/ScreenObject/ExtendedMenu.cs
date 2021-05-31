using Kritzel.Main.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kritzel.Main.ScreenObject
{
    public class ExtendedMenu : BaseScreenObject
    {
        PBrush border = PBrush.CreateSolid(Style.Default.MenuForeground);
        PBrush color = PBrush.CreateSolid(Style.Default.MenuBackground);

        public override bool Collides(float x, float y, int screenWidth, int screenHeight)
        {
            return false;
        }

        public override void Draw(GPURenderer renderer, int width, int height)
        {
            renderer.ResetTransform();
            // Back Button
            {
                float x = width - 4 * Util.GetGUISize();
                float y = Util.GetGUISize();
                RectangleF rect = new RectangleF(x, y, Util.GetGUISize(), Util.GetGUISize());
                renderer.FillEllipse(color, rect);
                renderer.DrawEllipse(border, 4, rect);
            }
            // Forward Button
            {
                float x = width - 2 * Util.GetGUISize();
                float y = Util.GetGUISize();
                renderer.DrawEllipse(border, 4, new RectangleF(x, y, Util.GetGUISize(), Util.GetGUISize()));
            }
        }
    }
}
