using Kritzel.PointerInputLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.ScreenObject
{
    public abstract class BaseScreenObject : IDisposable
    {
        public Matrix3x3 Transformation { get; protected set; } = new Matrix3x3();
        Matrix3x3 intTransformation = new Matrix3x3();
        public abstract void Draw(Renderer.GPURenderer renderer, int width, int height);
        public abstract bool Collides(float x, float y, int screenWidth, int screenHeight);
        public bool Disposed { get; private set; } = false;
        protected bool transformMove = true;
        protected bool transformScale = true;
        protected bool transformRotate = true;
        public bool Close { get; set; } = false;

        Point lastTouch = new Point(-1, -1);
        protected FingerTransform lastDblTouch = null;


        public virtual bool Think(List<Touch> allTouches, ref Touch stylus, ref Touch mouse, int screenWidth, int screenHeight)
        {
            bool changes = false;
            List<Touch> fingers = new List<Touch>();
            for(int i = 0; i < allTouches.Count; i++)
            {
                if (allTouches[i].TouchDevice == TouchDevice.Finger
                    && Collides(allTouches[i].X, allTouches[i].Y, screenWidth, screenHeight))
                {
                    fingers.Add(allTouches[i]);
                    allTouches.RemoveAt(i--);
                }
            }

            if(fingers.Count == 1 && lastTouch.X < 0)
            {
                // 1 Finger Down
                lastTouch = new Point(fingers[0].X, fingers[0].Y);
            }
            else if(fingers.Count == 1)
            {
                // 1 Finger Moved
                if (transformMove)
                {
                    int diffX = fingers[0].X - lastTouch.X;
                    int diffY = fingers[0].Y - lastTouch.Y;
                    lastTouch = new Point(fingers[0].X, fingers[0].Y);

                    Matrix3x3 translation = Matrix3x3.Translation(diffX, diffY);
                    intTransformation *= translation;
                    changes = true;
                }
            }
            else if(fingers.Count != 1 && lastTouch.X >= 0)
            {
                // 1 Finger Up
                lastTouch = new Point(-1, -1);
            }

            if(fingers.Count == 2 && lastDblTouch == null)
            {
                // 2 Fingers Down
                lastDblTouch = new FingerTransform(fingers[0].X, fingers[0].Y,
                    fingers[1].X, fingers[1].Y);
            }
            else if(fingers.Count == 2)
            {
                // 2 Fingers Move
                FingerTransform ft = new FingerTransform(fingers[0].X, fingers[0].Y,
                    fingers[1].X, fingers[1].Y);
                int pDiffX = ft.Position.X - lastDblTouch.Position.X;
                int pDiffY = ft.Position.Y - lastDblTouch.Position.Y;
                float rDiff = ft.Rotation - lastDblTouch.Rotation;
                float sDiff = 1 + (ft.Distance - lastDblTouch.Distance) / lastDblTouch.Distance;
                lastDblTouch = ft;

                Matrix3x3 mat = new Matrix3x3();
                if(transformMove) mat.TransformTranslate(pDiffX, pDiffY);
                if(transformRotate) mat.TransformRotateAt(-rDiff, ft.Position.X, ft.Position.Y);
                if (transformScale) mat.TransformScaleAt(sDiff, sDiff, ft.Position.X, ft.Position.Y);
                intTransformation *= mat;
                changes = true;
            }
            else if(fingers.Count != 2 && lastDblTouch != null)
            {
                // 2 Finger Up
                lastDblTouch = null;
            }

            if (changes) Transformation = new Matrix3x3(intTransformation);

            return changes;
        }

        public virtual void Dispose()
        {
            Disposed = true;
        }
    }
}
