using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitmap = System.Drawing.Bitmap;

namespace Kritzel.Main.Renderer
{
    public static class Effects
    {
        static Image shadow = null;
        static readonly Color sc1 = Color.FromArgb(50, Color.Black);
        static readonly Color sc2 = Color.FromArgb(0, Color.Black);

        public static void DrawShadow(this Renderer.BaseRenderer renderer, RectangleF rect)
        {
            if(shadow == null)
            {
                Bitmap bmp = new Bitmap(16, 16);
                using (System.Drawing.Drawing2D.LinearGradientBrush b = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Point(0, 0), new Point(0, bmp.Height), sc1, sc2))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.FillRectangle(b, new RectangleF(0, 0, bmp.Width, bmp.Height));
                    }
                }
                shadow = new Image(bmp);
            }

            renderer.DrawImage(shadow, rect);
        }

        public static void DrawSlowBoxShadow(Graphics g, Rectangle rect, int width, int brightness = 0)
        {
            for(int i = 0; i < width; i++)
            {
                Pen p = new Pen(Slerp(sc1, sc2, i / (float)width, brightness), 1);
                //g.DrawRectangle(p, rect);
                g.DrawRoundedRectangle(p, rect, width + i);
                p.Dispose();
                rect.Extend(-1);
            }
        }

        public static void DrawSlowBoxShadow(RenderTarget g, Rectangle rect, int width)
        {
            float f = Util.GetScaleFactor();
            RectangleF rectf = new RectangleF(rect.X / f, rect.Y / f, rect.Width / f, rect.Height / f);
            for (int i = 0; i < width; i++)
            {
                Color _c = Slerp(sc1, sc2, i / (float)width); ;
                SolidColorBrush b = new SolidColorBrush(g,
                    new SharpDX.Mathematics.Interop.RawColor4(_c.R / 255f, _c.G / 255f, _c.B / 255f, _c.A / 255f));
                var rrect = new SharpDX.Mathematics.Interop.RawRectangleF(rectf.Left, rectf.Top, rectf.Right, rectf.Bottom);
                g.DrawRectangle(rrect, b, 1);
                b.Dispose();
                rectf = rectf.Expand(1);
            }
        }

        public static Color Slerp(Color a, Color b, float t, int brightness = 0)
        {
            int _a = a.A + (byte)((b.A - a.A) * t);
            int _r = a.R + (byte)((b.R - a.R) * t) + brightness;
            int _g = a.G + (byte)((b.G - a.G) * t) + brightness;
            int _b = a.B + (byte)((b.B - a.B) * t) + brightness;
            return Color.FromArgb((byte)_a, (byte)_r, (byte)_g, (byte)_b);
        }

        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle rect, int rad)
        {
            Point p11 = new Point(rect.Left + rad, rect.Top);
            Point p12 = new Point(rect.Right - rad, rect.Top);
            Point p21 = new Point(rect.Left, rect.Top + rad);
            Point p22 = new Point(rect.Left, rect.Bottom - rad);
            Point p31 = new Point(p11.X, rect.Bottom);
            Point p32 = new Point(p12.X, rect.Bottom);
            Point p41 = new Point(rect.Right, p21.Y);
            Point p42 = new Point(rect.Right, p22.Y);

            g.DrawLine(pen, p11, p12);
            g.DrawLine(pen, p21, p22);
            g.DrawLine(pen, p31, p32);
            g.DrawLine(pen, p41, p42);

            g.DrawArc(pen, new Rectangle(rect.Left, rect.Top, rad * 2, rad * 2), 180, 90);
            g.DrawArc(pen, new Rectangle(rect.Left, rect.Bottom - rad * 2, rad * 2, rad * 2), 90, 90);
            g.DrawArc(pen, new Rectangle(rect.Right - rad * 2, rect.Top, rad * 2, rad * 2), 270, 90);
            g.DrawArc(pen, new Rectangle(rect.Right - rad * 2, rect.Bottom - rad * 2, rad * 2, rad * 2), 0, 90);
        }
    }
}
