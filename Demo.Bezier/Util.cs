using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Bezier
{
    public static class Util
    {
        public static void Normalize(this RawVector2 vector2)
        {
            float length = (float)Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y);
            vector2.X /= length;
            vector2.Y /= length;
        }

        public static float Length(this RawVector2 vector2)
        {
            return (float)Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y);
        }

        public static RawVector2 Subtract(RawVector2 a, RawVector2 b)
        {
            return new RawVector2(a.X - b.X, a.Y - b.Y);
        }

        public static RawVector2 Add(RawVector2 a, RawVector2 b)
        {
            return new RawVector2(a.X + b.X, a.Y + b.Y);
        }

        public static RawVector2 Mult(RawVector2 a, float b)
        {
            return new RawVector2(a.X * b, a.Y * b);
        }

        public static void FromPoints(this PathGeometry geometry, KVector2[] points, KVector2[] normals, bool bezier)
        {
            float strength = 10;
            using (var sink = geometry.Open())
            {
                sink.BeginFigure(new RawVector2(points[0].X,points[0].Y), FigureBegin.Filled);
                for(int i = 1; i < points.Length; i++)
                {
                    if (bezier)
                    {
                        KVector2 normal = i >= normals.Length ? normals[normals.Length - 1] : normals[i];
                        BezierSegment segment = new BezierSegment();
                        KVector2 p1 = points[i - 1] - normal.Orthogonal * strength;
                        KVector2 p2 = points[i] + normal.Orthogonal * strength;
                        KVector2 p3 = points[i];
                        segment.Point1 = new RawVector2(p1.X, p1.Y);
                        segment.Point2 = new RawVector2(p2.X, p2.Y);
                        segment.Point3 = new RawVector2(p3.X, p3.Y);
                        sink.AddBezier(segment);
                    }
                    else
                    {
                        sink.AddLine(new RawVector2(points[i].X, points[i].Y));
                    }
                }
                sink.EndFigure(FigureEnd.Closed);
                sink.Close();
            }
        }
    }
}
