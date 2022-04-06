using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Renderer
{
    public class PdfRenderer : BaseRenderer
    {
        XGraphics g;
        XBrush cBrush;
        static int imgCounter = 0;

        public PdfRenderer(XGraphics g)
        {
            this.g = g;
        }

        public override void DrawLine(Color c, float width, PointF p1, PointF p2, bool capStart = false, bool capEnd = false)
        {
            var p = new XPen(c.Pdf(), width);
            //if (capStart) p.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            //if (capEnd) p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(p, p1.Pdf(), p2.Pdf());
        }

        public override void FillEllipse(PBrush c, RectangleF rect)
        {
            g.DrawEllipse(createBrush(c), rect.Pdf());
        }

        public override void DrawRoundedLine(PBrush c, float width, PointF p1, PointF p2)
        {
            var pen = new XPen(c.GetColor().Pdf(), width);
            pen.LineCap = XLineCap.Round;
            g.DrawLine(pen, p1.Pdf(), p2.Pdf());
        }

        public override void DrawEllipse(PBrush c, float width, RectangleF rect)
        {
            g.DrawEllipse(new XPen(c.GetColor().Pdf(), width), rect.Pdf());
        }

        public override void DrawRoundedRectangle(PBrush c, float width, RectangleF rect)
        {
            g.DrawRoundedRectangle(new XPen(c.GetColor().Pdf(), width), rect.Pdf(), new XSize(width, width));
        }

        public override void DrawRect(Color c, float width, RectangleF rect)
        {
            g.DrawRectangle(new XPen(c.Pdf(), width),
                rect.X, rect.Y, rect.Width, rect.Height);
        }

        public override void DrawImage(Image img, RectangleF rect)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.GdiBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                XImage _bmp = XImage.FromStream(ms);
                g.DrawImage(_bmp, rect.Pdf());
                _bmp.Dispose();
            }
        }

        public override void FillPolygon(PBrush b, PointF[] pts)
        {
            XPoint[] _pts = new XPoint[pts.Length];
            for (int i = 0; i < pts.Length; i++)
                _pts[i] = pts[i].Pdf();
            g.DrawPolygon(createBrush(b), _pts, XFillMode.Alternate);
        }

        XBrush createBrush(PBrush pBrush)
        {
            XBrush brush = new XSolidBrush(pBrush.GetMainColor().Pdf());
            return brush;
        }

        public override void DrawText(string text, PBrush brush, RectangleF rect, float size)
        {
            float x = Util.MmToPoint(rect.X);
            float y = Util.MmToPoint(rect.Y);
            float w = Util.MmToPoint(rect.Width);
            float h = 0;
            g.DrawString(text, new XFont("Calibri", Util.MmToPoint(size) * 1.3), createBrush(brush),
                new XRect(x, y, w, h));
        }

        public override void BeginCircles(PBrush brush)
        {
            cBrush = new XSolidBrush(brush.GetColor().Pdf());
        }

        public override void Circle(float x, float y, float r)
        {
            var rect = new XRect(x - r, y - r, 2 * r, 2 * r);
            g.DrawEllipse(cBrush, rect);
        }

        public override void EndCircle()
        {
            
        }

        public override void BeginRects(PBrush brush)
        {
            cBrush = new XSolidBrush(brush.GetColor().Pdf());
        }

        public override void Rect(RectangleF rect)
        {
            g.DrawRectangle(cBrush, rect.Pdf());
        }

        public override void EndRects()
        {
            
        }

        public override void DrawText(string text, Color color, float x, float y, string fontFamily, float size, TextAlign align)
        {
            XFont ft = new XFont(fontFamily, Util.MmToPoint(size));
            XSolidBrush b = new XSolidBrush(color.Pdf());
            XStringFormat sf = new XStringFormat();
            switch (align)
            {
                case TextAlign.Left: sf.Alignment = XStringAlignment.Near; break;
                case TextAlign.Center: sf.Alignment = XStringAlignment.Center; break;
                case TextAlign.Right: sf.Alignment = XStringAlignment.Far; break;
            }
            var textSize = g.MeasureString(text, ft, sf);
            g.DrawString(text, ft, b, new XRect(x, y, (float)textSize.Width, (float)textSize.Height), sf);
        }

        public override void DrawStroke(PBrush brush, IEnumerable<LPoint> points)
        {
            var b = brush.GetColor().Pdf();

            var enumerator = points.GetEnumerator();
            if (!enumerator.MoveNext())
                return;
            LPoint lastPoint = enumerator.Current;
            while (enumerator.MoveNext())
            {
                LPoint pt = enumerator.Current;
                float width = pt.Rad * 2;
                var p1 = new XPoint(lastPoint.X, lastPoint.Y);
                var p2 = new XPoint(pt.X, pt.Y);
                XPen p = new XPen(b, width);
                p.LineCap = XLineCap.Round;
                g.DrawLine(p, p1, p2);
                lastPoint = pt;
            }
        }
    }
}
