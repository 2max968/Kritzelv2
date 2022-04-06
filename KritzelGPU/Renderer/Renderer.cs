using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Renderer
{
    public abstract class BaseRenderer
    {
        public enum TextAlign { Left, Center, Right }
        public bool RenderSpecial = true;
        public abstract void DrawLine(Color c, float width, PointF p1, PointF p2, bool capStart = false, bool capEnd = false);
        public abstract void FillEllipse(PBrush c, RectangleF rect);
        public abstract void DrawEllipse(PBrush c, float width, RectangleF rect);
        public abstract void DrawRoundedLine(PBrush c, float width, PointF p1, PointF p2);
        public abstract void DrawRoundedRectangle(PBrush c, float width, RectangleF rect);
        public abstract void DrawRect(Color c, float width, RectangleF rect);
        public abstract void DrawImage(Image img, RectangleF rect);
        public abstract void FillPolygon(PBrush b, PointF[] pts);
        public abstract void DrawText(string text, PBrush brush, RectangleF rect, float size);
        public abstract void BeginCircles(PBrush brush);
        public abstract void Circle(float x, float y, float r);
        public abstract void EndCircle();
        public abstract void BeginRects(PBrush brush);
        public abstract void Rect(RectangleF rect);
        public abstract void EndRects();
        public abstract void DrawText(string text, Color color, float x, float y, string fontFamily, float size, TextAlign align);
        public abstract void DrawStroke(PBrush brush, IEnumerable<LPoint> points);
    }
}
