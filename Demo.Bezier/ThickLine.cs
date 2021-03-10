using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Bezier
{
    public class ThickLine
    {
        public List<KVector2> points;
        public List<KVector2> normals;
        public KVector2[] interpolatedNormals;
        public List<float> rads;
        public KVector2[] Contour = new KVector2[0];

        public ThickLine()
        {
            points = new List<KVector2>();
            rads = new List<float>();
            normals = new List<KVector2>();
            interpolatedNormals = new KVector2[0];
        }

        public void AddPoint(float x, float y, float rad)
        {
            this.points.Add(new KVector2(x, y));
            this.rads.Add(rad);

            int i = points.Count - 1;
            if(i > 0)
            {
                KVector2 direction = points[i] - points[i - 1];
                direction.Length = 1;
                normals.Add(direction.Orthogonal);
            }
        }

        public void InterpolateNormals()
        {
            interpolatedNormals = new KVector2[normals.Count + 1];
            interpolatedNormals[0] = normals[0];
            //interpolatedNormals[normals.Count - 1] = normals[normals.Count - 1];
            for(int i = 1; i < normals.Count;i++)
            {
                float length1 = points[i].DistanceTo(points[i - 1]);
                float length2 = points[i].DistanceTo(points[i + 1]);
                interpolatedNormals[i] = (normals[i - 1] * length2 + normals[i] * length1)
                    / (length1 + length2);
            }
            interpolatedNormals[normals.Count] = normals[normals.Count - 1];
        }

        public void CalcContour()
        {
            InterpolateNormals();
            int n = points.Count * 2;
            Contour = new KVector2[n];
            for(int i = 0; i < points.Count; i++)
            {
                KVector2 normal = i >= interpolatedNormals.Length ? interpolatedNormals[i-1] : interpolatedNormals[i];
                Contour[i] 
                    = points[i] + normal * rads[i];
                Contour[n - i - 1] 
                    = points[i] - normal * rads[i];
            }
        }
    }
}
