using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Bezier
{
    public struct KVector2
    {
        public float X, Y;

        public KVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Length
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y);
            }
            set
            {
                float v = value / Length;
                X *= v;
                Y *= v;
            }
        }

        public static KVector2 operator +(KVector2 a, KVector2 b)
        {
            return new KVector2(a.X + b.X, a.Y + b.Y);
        }

        public static KVector2 operator -(KVector2 a, KVector2 b)
        {
            return new KVector2(a.X - b.X, a.Y - b.Y);
        }

        public static KVector2 operator *(KVector2 a, float b)
        {
            return new KVector2(a.X * b, a.Y * b);
        }

        public static KVector2 operator *(float b, KVector2 a)
        {
            return new KVector2(a.X * b, a.Y * b);
        }

        public static KVector2 operator /(KVector2 a, float b)
        {
            return new KVector2(a.X / b, a.Y / b);
        }

        public float DistanceTo(KVector2 point2)
        {
            return (point2 - this).Length;
        }

        public KVector2 Orthogonal
        {
            get
            {
                return new KVector2(-Y, X);
            }
        }

        public static KVector2[] FromArray(float[] x, float[] y)
        {
            if (x.Length != y.Length) throw new ArgumentException();
            KVector2[] v = new KVector2[x.Length];
            for(int i = 0; i < v.Length;i++)
            {
                v[i] = new KVector2(x[i], y[i]);
            }
            return v;
        }
    }
}
