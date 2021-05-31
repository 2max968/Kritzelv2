using Kritzel.Main.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Forms
{
    public abstract class TransformableForm : Line
    {
        public Matrix3x3 Transformation { get; set; } = new Matrix3x3();

        public override void Transform(Matrix3x3 mat)
        {
            Transformation *= mat;
            /*mat.Split(out Matrix3x3 move, out Matrix3x3 transform);
            for (int i = 0; i < Points.Count; i++)
            {
                float x = Points[i].X;
                float y = Points[i].Y;
                move.Transform(ref x, ref y);
                Points[i].X = x;
                Points[i].Y = y;
            }*/
        }

        public Matrix3x3 GetScaledTranform()
        {
            float scale = Util.GetScaleFactor();
            return Matrix3x3.Scale(scale) * Transformation;
        }

        public void RenderTransformed(BaseRenderer r)
        {
            if (r is GPURenderer)
            {
                GPURenderer gr = (GPURenderer)r;
                Matrix3x3 mat = gr.GetCurrentTransform();
                gr.ResetTransform();
                gr.Transform(this.Transformation * mat);
                this.Render(r);
                gr.ResetTransform();
                gr.Transform(mat);
            }
            else
            {
                
            }

            r.BeginCircles(PBrush.CreateSolid(System.Drawing.Color.Lime));
            foreach(var point in Points)
            {
                r.Circle(point.X, point.Y, 4);
            }
            r.EndCircle();
        }
    }
}
