using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Forms
{
    public class LinearLine : Line
    {
        public static readonly Bitmap BitmapLL = ResManager.LoadIcon("tools/line.svg", Util.GetGUISize());

        public LinearLine()
        {
            base.checkDist = false;
        }

        public override void AddPoint(float x, float y, float pressure)
        {
            if(Points.Count < 2)
                base.AddPoint(x, y, pressure);
            else
            {
                Points.Last().Move(x, y);
            }
        }

        public override float CalcRad(float pressure, float thicknes)
        {
            return .5f * thicknes;
        }

        public override void Render(Renderer.BaseRenderer g, float quality = 1, int start = 0, bool simple = false)
        {
            if (Points.Count < 2)
                base.Render(g, quality, start);
            else
                g.DrawRoundedLine(Brush, Points[0].Rad * 2, Points[0].ToPointF(), Points[1].ToPointF());
        }

        public override bool RefreshInEditor()
        {
            return true;
        }

        public override bool Collision(LPoint pt)
        {
            if (Points.Count < 2) return base.Collision(pt);
            LPoint p1 = Points[0];
            LPoint p2 = Points[1];
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float angle = (float)Math.Atan2(dy, dx);
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            Matrix3x3 normalizeMatrix = Matrix3x3.Translation(-p1.X, -p1.Y);
            normalizeMatrix.TransformRotate(angle);
            normalizeMatrix.TransformScale(1 / distance);

            float thickness = Points[0].Rad * 2;
            float ptx = pt.X, pty = pt.Y;
            normalizeMatrix.Transform(ref ptx, ref pty);
            if (ptx < 0 || ptx > 1) return false;
            if (pty > thickness / distance || pty < -thickness / distance) return false;
            return true;
        }
    }
}
