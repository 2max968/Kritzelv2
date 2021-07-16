using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kritzel.Main.Renderer;
using System.Drawing;
using Kritzel.PointerInputLibrary;

namespace Kritzel.Main.ScreenObject
{
    public class Compass : BaseScreenObject
    {
        static Bitmap icon = null;
        public static Bitmap Icon
        {
            get
            {
                if (icon == null)
                    icon = ResManager.LoadIcon("tools/compass.svg", Util.GetGUISize());
                return icon;
            }
        }

        float rad, ballRad;
        float angle = 0;
        bool locked = false;
        bool fingerLocked = false;
        InkControl parent;

        public Compass(InkControl control)
        {
            ballRad = Util.MmToPoint(10);
            rad = Util.MmToPoint(50);
            parent = control;

            base.transformRotate = false;
            base.transformScale = false;
        }

        public override bool Collides(float x, float y, int screenWidth, int screenHeight)
        {
            Transformation.GetInverse().Transform(ref x, ref y);
            return Math.Sqrt(x * x + y * y) <= ballRad * 2;
        }

        public override void Draw(GPURenderer renderer, int width, int height)
        {
            Matrix3x3 rotate = Matrix3x3.Rotate(angle);
            renderer.Transform(rotate);
            renderer.Transform(Transformation);
            PBrush bgrColor = PBrush.CreateSolid(Color.FromArgb(100, 200, 200, 200));
            PBrush fgrColor = PBrush.CreateSolid(Color.Black);
            PBrush fntColor = PBrush.CreateSolid(Color.Tomato);
            RectangleF rect1 = new RectangleF(-ballRad, -ballRad, ballRad * 2, ballRad * 2);
            RectangleF rect2 = rect1;
            rect2.X += rad;
            renderer.FillEllipse(bgrColor, rect1);
            renderer.FillEllipse(bgrColor, rect2);
            renderer.DrawEllipse(fgrColor, 2, rect1);
            renderer.DrawEllipse(fgrColor, 2, rect2);
            renderer.DrawLine(Color.Black, 2, new PointF(ballRad, 0), 
                new PointF(rad - ballRad, 0));
            renderer.DrawLine(Color.Tomato, 1, new PointF(-ballRad, 0), new PointF(ballRad, 0));
            renderer.DrawLine(Color.Tomato, 1, new PointF(0, -ballRad), new PointF(0, ballRad));
            renderer.DrawLine(Color.Tomato, 1, new PointF(-ballRad + rad, 0), new PointF(ballRad + rad, 0));
            renderer.DrawLine(Color.Tomato, 1, new PointF(rad, -ballRad), new PointF(rad, ballRad));

            float pageScale = parent.GetTransform().GetScale();
            float pageRot = parent.GetTransform().GetRotation();
            string strRad = "" + Math.Round(Util.PointToMm(rad / pageScale) * 100.0) / 100.0;
            string strDeg = "" + Math.Round((pageRot - angle) * 18000.0 / Math.PI) / 100.0;
            renderer.DrawText($"{strRad}mm\n{strDeg}°", 
                fntColor,
                new RectangleF(Util.PointToMm(ballRad), Util.PointToMm(-ballRad), 500, 50), 
                10);

            renderer.ResetTransform();
        }

        public override bool Think(List<Touch> allTouches, ref Touch stylus, ref Touch mouse, int screenWidth, int screenHeight)
        {
            bool change = false;
            
            if(allTouches.Count == 1)
            {
                float x = allTouches[0].X;
                float y = allTouches[0].Y;
                Transformation.GetInverse().Transform(ref x, ref y);
                float xq = x;
                float yq = y;
                Matrix3x3.Rotate(-angle).Transform(ref xq, ref yq);
                xq -= rad;
                float dist = (float)Math.Sqrt(xq * xq + yq * yq);
                if (dist < ballRad * 2)
                {
                    change = true;
                    fingerLocked = true;
                }
            }
            
            if(allTouches.Count != 1)
            {
                fingerLocked = false;
            }

            if(fingerLocked)
            {
                float x = allTouches[0].X;
                float y = allTouches[0].Y;
                allTouches.RemoveAt(0);
                Transformation.GetInverse().Transform(ref x, ref y);

                if (fingerLocked)
                {
                    float r, phi;
                    Util.CartToPole(out r, out phi, x, y);
                    angle = -phi;
                    float pageScale = parent.GetTransform().GetScale();
                    float pageRot = parent.GetTransform().GetRotation();
                    rad = r;
                    float rDiff = Util.PointToMm(r / pageScale);
                    float rDiffR = (float)Math.Round(rDiff);
                    rDiff = Util.MmToPoint(rDiff - rDiffR);
                    r -= rDiff;
                }
            }

            if (stylus != null)
            {
                float x = stylus.X;
                float y = stylus.Y;
                Transformation.GetInverse().Transform(ref x, ref y);
                if (!locked)
                { 

                    float xq = x;
                    float yq = y;
                    Matrix3x3.Rotate(-angle).Transform(ref xq, ref yq);
                    xq -= rad;
                    float dist = (float)Math.Sqrt(xq * xq + yq * yq);
                    if (dist < ballRad)
                    {
                        locked = true;
                    }
                }
                
                if(locked)
                {
                    float r, phi;
                    Util.CartToPole(out r, out phi, x, y);
                    r = rad;
                    Util.PoleToCart(out x, out y, r, phi);
                    Transformation.Transform(ref x, ref y);
                    stylus.X = (int)x;
                    stylus.Y = (int)y;
                    angle = -phi;
                }
            }
            else
            {
                locked = false;
            }

            bool baseChange = base.Think(allTouches, ref stylus, ref mouse, screenWidth, screenHeight);
            return baseChange || change;
        }

        public override bool ManipulateInput(ref float x, ref float y, int screenWidth, int screenHeight)
        {
            if (Collides(x, y, screenWidth, screenHeight))
                return false;
            float _x = x, _y = y;
            Transformation.GetInverse().Transform(ref _x, ref _y);
            if (!locked)
            {

                float xq = _x;
                float yq = _y;
                Matrix3x3.Rotate(-angle).Transform(ref xq, ref yq);
                xq -= rad;
                float dist = (float)Math.Sqrt(xq * xq + yq * yq);
                if (dist < ballRad)
                {
                    locked = true;
                }
            }

            if (locked)
            {
                float r, phi;
                Util.CartToPole(out r, out phi, _x, _y);
                r = rad;
                Util.PoleToCart(out _x, out _y, r, phi);
                Transformation.Transform(ref _x, ref _y);
                x = _x;
                y = _y;
                angle = -phi;
            }
            return true;
        }
    }
}
