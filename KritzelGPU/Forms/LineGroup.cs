using Kritzel.Main.Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Forms
{
    public class LineGroup : Line
    {
        public static readonly Bitmap Stamp = ResManager.LoadIcon("tools/stamp.svg", Util.GetGUISize());

        List<Line> lines = new List<Line>();

        public LineGroup()
        {

        }

        public LineGroup(IEnumerable<Line> lines)
        {
            foreach(Line line in lines)
            {
                Line clone = line.Clone();
                this.lines.Add(clone);
            }
        }

        public override void Render(BaseRenderer g, float quality = 1, int start = 0, bool simple = false)
        {
            foreach(Line l in lines)
            {
                l.Render(g, quality, start, simple);
            }
        }

        public override void AddPoint(float x, float y, float pressure)
        {
            LPoint pt = new LPoint(x, y, 1);
            if(Points.Count > 0)
            {
                float dx = x - Points[0].X;
                float dy = y - Points[0].Y;
                Transform(Matrix3x3.Translation(dx, dy));
                Points.Clear();
            }
            else
            {
                float dx = x;
                float dy = y;
                Transform(Matrix3x3.Translation(dx, dy));
            }
            Points.Add(pt);
        }

        public IEnumerable<Line> GetLines()
        {
            foreach(Line l in lines)
            {
                yield return l;
            }
        }

        public override void Transform(Matrix3x3 mat)
        {
            foreach(Line l in lines)
            {
                l.Transform(mat);
                l.CalculateBounds();
            }
        }
    }
}
