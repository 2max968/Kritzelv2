using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.GUIElements
{
    public class GraphRenderer
    {
        public static Bitmap CreateGammaGraph(Size size, float gamma)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (Pen pen = new Pen(Brushes.Orange, 4))
                {
                    g.Clear(Color.White);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    PointF lF = new PointF(0, 0);

                    for (int i = 2; i < size.Width; i += 2)
                    {
                        float x = i / (float)size.Width;
                        float y = (float)Math.Pow(x, gamma);

                        PointF f = new PointF(x, y);
                        Point p1 = TransformToScreen(lF, size);
                        Point p2 = TransformToScreen(f, size);

                        g.DrawLine(pen, p1, p2);

                        var font = new Font("Arial", Util.GetFontSizePixel(), GraphicsUnit.Pixel);
                        var sf = new StringFormat()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Far
                        };
                        g.DrawString(Language.GetText("Settings.styluspreassure"), font, Brushes.Black, new RectangleF(0, 0, size.Width, size.Height), sf);
                        g.RotateTransform(-90);
                        sf = new StringFormat()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Near
                        };
                        g.DrawString(Language.GetText("Settings.lineSize"), font, Brushes.Black, new RectangleF(-size.Height, 0, size.Height, size.Width), sf);
                        g.ResetTransform();

                        lF = f;
                    }
                }
            }
            return bmp;
        }

        public static Point TransformToScreen(PointF point, Size size)
        {
            float x = point.X * size.Width;
            float y = (1 - point.Y) * size.Height;
            return new Point((int)x, (int)y);
        }
    }
}
