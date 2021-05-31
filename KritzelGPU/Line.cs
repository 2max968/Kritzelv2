using LineLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main
{
    public class LPoint
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Rad { get; set; }
        public float dX { get; set; } = 0;
        public float dY { get; set; } = 0;
        public bool Outside = false;
        public int Index { get; set; } = -1;

        public LPoint(float x, float y, float rad)
        {
            this.X = x;
            this.Y = y;
            this.Rad = rad;
        }

        public float DistSquared(float x, float y)
        {
            float dx = this.X - x;
            float dy = this.Y - y;
            return (dx * dx + dy * dy);
        }

        public float Dist(float x, float y)
        {
            float dx = this.X - x;
            float dy = this.Y - y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public float DistSquared(LPoint pt)
        {
            float dx = this.X - pt.X;
            float dy = this.Y - pt.Y;
            return (dx * dx + dy * dy);
        }

        public float Dist(LPoint pt)
        {
            float dx = this.X - pt.X;
            float dy = this.Y - pt.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public float Angle(LPoint pt)
        {
            float dx = this.X - pt.X;
            float dy = this.Y - pt.Y;
            return (float)Math.Atan2(dy, dx);
        }

        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }

        public LPoint Move(float x, float y)
        {
            this.X = x;
            this.Y = y;
            return this;
        }

        public LPoint SetRad(float rad)
        {
            this.Rad = rad;
            return this;
        }

        public override string ToString()
        {
            return "(" + X + "; " + Y + ")";
        }
    }

    public class Line
    {
        public static readonly Bitmap BitmapStrk = ResManager.LoadIcon("tools/stroke.svg", Util.GetGUISize());

        public PBrush Brush { get; set; }
        public List<LPoint> Points { get; private set; } = new List<LPoint>();
        protected RectangleF bounds = new RectangleF(0, 0, 0, 0);
        public int RenderPos { get; private set; } = 0;
        public Spline2D Spline { get; private set; } = null;
        public RectangleF Bounds
        {
            get
            {
                return bounds;
            }
        }
        protected bool checkDist = true;
        public bool Selected { get; set; } = false;
        public uint Id { get; set; } = 0;

        public virtual void AddPoint(float x, float y, float pressure)
        {
            if (Points.Count == 0)
                bounds = new RectangleF(x, y, 1, 1);

            if(checkDist && Points.Count > 0 && Points.Last().DistSquared(x,y) < 4)
            {
                return;
            }

            if (x < bounds.Left) { bounds.Width += bounds.X - x; bounds.X = x; }
            if (x > bounds.Right) bounds.Width = x - bounds.X;
            if (y < bounds.Top) { bounds.Height += bounds.Y - y; bounds.Y = y; }
            if (y > bounds.Bottom) bounds.Height = y - bounds.Y;

            LPoint n = new LPoint(x, y, pressure);
            Points.Add(n);
            if (Configuration.CalculateSplinesDuringDrawing) CalcSpline();
        }

        public virtual void Render(Renderer.BaseRenderer g, float quality = 1, int start = 0, bool simple = false)
        {
            if(g.RenderSpecial && Selected)
            {
                renderFull(g, PBrush.CreateSolid(Color.Black), 4, quality, start, simple);
                renderFull(g, PBrush.CreateSolid(Color.White), 2, quality, start, simple);
            }
            renderFull(g, Brush, 0, quality, start, simple);
        }

        void renderFull(Renderer.BaseRenderer g, PBrush brush, float border, float quality, int start, bool simple)
        {
            if (Points.Count == 1)
            {
                g.BeginCircles(brush);
                LPoint pt = Points[0];
                renderPoint(g, pt, border);
                g.EndCircle();
            }
            else if (Points.Count > 1 && !simple)
            {
                g.BeginCircles(brush);
                renderPoint(g, Points[0], border);
                if (start < 1) start = 1;
                for (int i = start; i < Points.Count; i++)
                {
                    if (Points[i] == null) continue;
                    renderPoint(g, Points[i], border);
                    renderSegment(g, Points[i - 1], Points[i], quality,
                        border);
                    PointF p1 = new PointF(Points[i].X - Points[i].dX * 16,
                        Points[i].Y - Points[i].dY * 16);
                    PointF p2 = new PointF(Points[i].X + Points[i].dX * 16,
                        Points[i].Y + Points[i].dY * 16);
                    //g.DrawLine(Color.Lime, 1, p1, p2, false, true);
                }
                RenderPos = Points.Count - 1;
                g.EndCircle();
            }
            else if(Points.Count > 1)
            {
                for(int i = start + 1; i < Points.Count; i++)
                {
                    g.DrawLine(Color.Black, 1, Points[i - 1].ToPointF(), Points[i].ToPointF());
                }
            }
        }

        void renderPoint(Renderer.BaseRenderer g, LPoint pt, float border)
        {
            g.Circle(pt.X, pt.Y, pt.Rad + border);
        }

        void renderSegment(Renderer.BaseRenderer g, LPoint p1, LPoint p2, float quality, float border)
        {
            if (p1.Outside && p2.Outside) return;
            if (Spline != null)
            {
                int n = (int)(p1.Dist(p2) * quality);
                float dx = (p2.X - p1.X) / n;
                float dy = (p2.Y - p1.Y) / n;
                float dr = (p2.Rad - p1.Rad) / n;
                for (int i = 0; i < n; i++)
                {
                    double x, y;
                    Spline.GetPoint(p1.Index + i / (float)n, out x, out y);
                    LPoint p = new LPoint((float)x, (float)y, p1.Rad + dr * i);
                    renderPoint(g, p, border);
                }
            }
            else
            {
                int n = (int)(p1.Dist(p2) * quality);
                float dx = (p2.X - p1.X) / n;
                float dy = (p2.Y - p1.Y) / n;
                float dr = (p2.Rad - p1.Rad) / n;
                for (int i = 0; i < n; i++)
                {
                    LPoint p = new LPoint(p1.X + dx * i, p1.Y + dy * i, p1.Rad + dr * i);
                    renderPoint(g, p, border);
                }
            }
        }

        public virtual bool Collision(LPoint pt)
        {
            if (!Bounds.Expand(pt.Rad).Contains(pt.X, pt.Y))
                return false;

            double x, y;
            if (Points.Count > 1)
            {
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    float dist = Points[i].Dist(Points[i + 1]);
                    float rad = Math.Max(Points[i].Rad, Points[i + 1].Rad);
                    for (float t = i; t < i + 1; t += 10f / dist)
                    {
                        if (Spline != null)
                            Spline.GetPoint(t, out x, out y);
                        else
                        {
                            x = Points[i].X * (t - i) + Points[i + 1].X * (1 - t + i);
                            y = Points[i].Y * (t - i) + Points[i + 1].Y * (1 - t + i);
                        }
                        if (pt.Dist((float)x, (float)y) < rad + pt.Rad)
                            return true;
                    }
                }
            }
            else if(Points.Count == 1)
            {
                if(pt.Dist(Points[0]) < pt.Rad + pt.Rad)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual float CalcRad(float pressure, float thicknes)
        {
            return pressure / 2000f * thicknes;
        }

        public bool CalcSpline()
        {
            if (Points.Count < 4) return false;
            double[] px = new double[Points.Count];
            double[] py = new double[Points.Count];
            for(int i = 0; i < Points.Count; i++)
            {
                px[i] = Points[i].X;
                py[i] = Points[i].Y;
                Points[i].Index = i;
            }
            Spline = new Spline2D(px, py);
            return true;
        }

        public virtual string ToParamString()
        {
            string txt = "";
            foreach(LPoint p in Points)
            {
                txt += string.Format("{0},{1},{2};", 
                    Util.FToS(p.X), Util.FToS(p.Y), Util.FToS(p.Rad));
            }
            return txt;
        }

        public virtual void FromParamString(string txt)
        {
            Points.Clear();
            string[] pts = txt.Split(';');
            foreach(string pt in pts)
            {
                if (pt == "") continue;
                string[] dats = pt.Split(',');
                float x = Util.SToF(dats[0]);
                float y = Util.SToF(dats[1]);
                float rad = Util.SToF(dats[2]);
                LPoint lp = new LPoint(x, y, rad);
                Points.Add(lp);
            }
        }

        public void CalculateBounds()
        {
            if (Points.Count <= 0) return;
            RectangleF bounds = new RectangleF(Points[0].ToPointF(), new SizeF(0, 0));
            float maxRad = 0;
            for(int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Outside) continue;
                float x = Points[i].X;
                float y = Points[i].Y;
                if (x < bounds.Left) { bounds.Width += bounds.X - x; bounds.X = x; }
                if (x > bounds.Right) bounds.Width = x - bounds.X;
                if (y < bounds.Top) { bounds.Height += bounds.Y - y; bounds.Y = y; }
                if (y > bounds.Bottom) bounds.Height = y - bounds.Y;
                if (Points[i].Rad > maxRad) maxRad = Points[i].Rad;
            }
            this.bounds = bounds.Expand(maxRad);
        }

        public virtual bool RefreshInEditor()
        {
            return false;
        }

        public virtual void Transform(Matrix3x3 mat)
        {
            for(int i = 0; i < Points.Count; i++)
            {
                float x = Points[i].X;
                float y = Points[i].Y;
                mat.Transform(ref x, ref y);
                Points[i].X = x;
                Points[i].Y = y;
            }
            CalcSpline();
            CalculateBounds();
        }
    }
}
