using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kritzel.Main.Renderer;
using Kritzel.PointerInputLibrary;

namespace Kritzel.Main.ScreenObject
{
    public class Ruler : BaseScreenObject
    {
        enum LockState { Up, Down, None };
        static Bitmap icon = null;
        public static Bitmap Icon
        {
            get
            {
                if (icon == null)
                    icon = ResManager.LoadIcon("tools/ruler.svg", Util.GetGUISize());
                return icon;
            }
        }

        float size;
        float grip;
        LockState lockState = LockState.None;
        InkControl parent;

        public Ruler(InkControl parent)
        {
            this.parent = parent;
            size = Util.MmToPoint(Configuration.RulerSize * Util.GetScaleFactor() / 1.5f);
            grip = Util.MmToPoint(5);
            base.transformScale = false;
        }

        public override bool Collides(float x, float y, int screenWidth, int screenHeight)
        {
            Transformation.GetInverse().Transform(ref x, ref y);
            return y <= size && y >= 0;
        }

        public override void Draw(GPURenderer renderer, int width, int height)
        {
            renderer.Transform(Transformation);
            RectangleF rect = new RectangleF(0, 0, width, size);
            renderer.FillRectangle(Color.FromArgb(100, 200, 200, 200), rect);
            renderer.DrawRect(Color.Black, 1, rect);

            float scale = parent.GetTransform().GetScale();
            int linMM = (int)(Util.PointToMm(width) / scale);
            float len1 = Util.MmToPoint(3);
            float len2 = Util.MmToPoint(5);
            float len3 = Util.MmToPoint(10);
            renderer.BeginLines(Color.Black, 1);
            for (int i = 0; i <= linMM; i++)
            {
                float x = Util.MmToPoint(i) * scale;
                float y1 = len1;
                if (i % 5 == 0) y1 = len2;
                if (i % 10 == 0) y1 = len3;
                renderer.BatchedLine(new PointF(x, 0), new PointF(x, y1));
                renderer.BatchedLine(new PointF(x, size), new PointF(x, size - y1));
                //renderer.DrawLine(Color.Black, 1, new PointF(x, 0), new PointF(x, y1));
                //renderer.DrawLine(Color.Black, 1, new PointF(x, size), new PointF(x, size - y1));
            }
            renderer.EndLines();

            float tx = width / 2;
            double deg = Math.Abs(
                Math.Round(
                    (Transformation.GetRotation() - parent.GetTransform().GetRotation())
                    * 18000.0 / Math.PI) / 100.0);
            if (deg > 90) deg = 180 - deg;
            renderer.DrawText("" + deg + "°", PBrush.CreateSolid(Color.Tomato),
                new RectangleF(Util.PointToMm(tx), 10, 50, 50), 20);
        }

        public override bool Think(List<Touch> allTouches, ref Touch stylus, ref Touch mouse, int screenWidth, int screenHeight)
        {
            bool changes = base.Think(allTouches, ref stylus, ref mouse, screenWidth, screenHeight);

            // Transform to Degree
            float thisRot = Transformation.GetRotation();
            float rot = thisRot;
            float rotRound = (float)(Math.Round(rot * 180f / Math.PI) * Math.PI / 180.0);
            float rotDiff = rotRound - rot;
            if (rotDiff != 0)
            {
                float x = 0, y = 0;
                if (lastDblTouch != null)
                {
                    x = lastDblTouch.Position.X;
                    y = lastDblTouch.Position.Y;
                }
                Transformation.TransformRotateAt(rotDiff, x, y);
                changes = true;
            }

            // Transform Input
            Touch inpDev = stylus | mouse;
            if (inpDev != null && inpDev.PenFlags == PenFlags.NONE)
            {
                ManipulateInput(inpDev, screenWidth, screenHeight);
            }
            else
            {
                lockState = LockState.None;
            }

            return changes;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override bool ManipulateInput(ref float x, ref float y, int screenWidth, int screenHeight)
        {
            Transformation.GetInverse().Transform(ref x, ref y);
            if (lockState == LockState.None && y >= -grip && y < size / 2)
            {
                y = 0;
                lockState = LockState.Down;
            }
            else if (lockState == LockState.None && y >= size / 2 && y <= size + grip)
            {
                y = size;
                lockState = LockState.Up;
            }
            else if (lockState == LockState.Down)
            {
                if (y < -grip)
                    lockState = LockState.None;
                else
                    y = 0;
            }
            else if (lockState == LockState.Up)
            {
                if (y > size + grip)
                    lockState = LockState.None;
                else
                    y = size;
            }

            Transformation.Transform(ref x, ref y);
            return true;
        }
    }
}
