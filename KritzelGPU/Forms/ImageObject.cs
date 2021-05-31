using Kritzel.Main.Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Forms
{
    public class ImageObject : TransformableForm, IDisposable
    {
        public static Bitmap IconPhoto = ResManager.LoadIcon("tools/photo.svg", Util.GetGUISize());
        Renderer.Image image;

        public ImageObject()
        {
            Brush = PBrush.CreateSolid(Color.White);
        }

        public ImageObject(Renderer.Image image, RectangleF rect)
        {
            Brush = PBrush.CreateSolid(Color.White);
            this.image = image;
            AddPoint(rect.Left, rect.Top, 1);
            AddPoint(rect.Right, rect.Top, 1);
            AddPoint(rect.Right, rect.Bottom, 1);
            AddPoint(rect.Left, rect.Bottom, 1);
        }

        public void Dispose()
        {
            image.Dispose();
        }

        ~ImageObject()
        {
            Dispose();
        }

        public override void Render(BaseRenderer g, float quality = 1, int start = 0, bool simple = false)
        {
            if(Points.Count >= 4 && image != null)
            {
                float x = Util.MmToPoint(Points[0].X);
                float y = Util.MmToPoint(Points[0].Y);
                float w = Util.MmToPoint(Points[2].X) - x;
                float h = Util.MmToPoint(Points[2].Y) - y;
                g.DrawImage(image, new RectangleF(x, y, w, h));
            }
        }
    }
}
