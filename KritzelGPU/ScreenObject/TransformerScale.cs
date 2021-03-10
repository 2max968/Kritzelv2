using Kritzel.Main.Renderer;
using Kritzel.PointerInputLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.ScreenObject
{
    public class TransformerScale : Transformer
    {
        KPage page;
        InkControl control;
        int x, y;
        Rectangle rect;
        uint currentTouchId = uint.MaxValue;
        PointF? posLast = null;
        float direction = 0;
        static Renderer.Image img = null;

        public TransformerScale(KPage page, InkControl control, int directionDeg)
        {
            this.page = page;
            this.control = control;
            this.direction = directionDeg / 180f * (float)Math.PI;
            rect = new Rectangle((int)(50 * Util.GetScaleFactor()), -(int)(8 * Util.GetScaleFactor()),
                (int)(48 * Util.GetScaleFactor()), (int)(16 * Util.GetScaleFactor()));
            this.Transformation = Matrix3x3.Rotate(this.direction);

            if (img == null)
            {
                Bitmap bmp = ResManager.LoadIcon("gui/scalator.svg", rect.Width, rect.Height);
                img = new Renderer.Image(bmp);
            }
        }

        public override bool Collides(float x, float y, int screenWidth, int screenHeight)
        {
            control.GetTransform().GetInverse().Transform(ref x, ref y);
            x -= this.x;
            y -= this.y;
            Transformation.GetInverse().Transform(ref x, ref y);
            bool collides = rect.Contains((int)x, (int)y);
            return collides;
        }

        public override void Draw(GPURenderer renderer, int width, int height)
        {
            float _x = this.x;
            float _y = this.y;
            control.GetTransform().Transform(ref _x, ref _y);
            renderer.Transform(Transformation * Matrix3x3.Translation(_x, _y));
            //renderer.FillRectangle(Color.White, rect);
            //renderer.DrawRect(Color.Black, 4, rect);
            renderer.DrawImage(img, rect);
        }

        public override void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Think(List<Touch> allTouches, ref Touch stylus, ref Touch mouse, int screenWidth, int screenHeight)
        {
            if (IsMouseDown()) return false;
            bool refresh = false;

            PointF? cursorPos = null;
            uint pointerId = 0;
            if (stylus != null) { cursorPos = new PointF(stylus.X, stylus.Y); pointerId = stylus.Id; }
            else if(mouse != null) { cursorPos = new PointF(mouse.X, mouse.Y); pointerId = mouse.Id; }
            else if (allTouches.Count > 0) { cursorPos = new PointF(allTouches[0].X, allTouches[0].Y); pointerId = allTouches[0].Id; }

            if (cursorPos.HasValue && Collides(cursorPos.Value.X, cursorPos.Value.Y, screenWidth, screenHeight))
            {
                currentTouchId = pointerId;
            }

            if (currentTouchId != uint.MaxValue)
            {
                Touch t = null;
                t = stylus | mouse;
                if (t == null)
                {
                    foreach (Touch touch in allTouches)
                    {
                        if (touch.Id == currentTouchId)
                        {
                            t = touch;
                            break;
                        }
                    }
                }

                if (t != null)
                {
                    float _x = this.x;
                    float _y = this.y;
                    control.GetTransform().Transform(ref _x, ref _y);
                    float dx = t.X - _x;
                    float dy = t.Y - _y;
                    float ang = (float)Math.Atan2(dy, dx);

                    if(!posLast.HasValue)
                    {
                        posLast = new PointF(t.X, t.Y);
                    }
                    else
                    {
                        PointF delta = new PointF(t.X - posLast.Value.X, t.Y - posLast.Value.Y);
                        posLast = new PointF(t.X, t.Y);
                        Transformation.GetInverse().Transform(ref delta);
                        delta.Y = 0;
                        Transformation.Transform(ref delta);
                        Matrix3x3 mat1 = Matrix3x3.Translation(x, y);
                        Matrix3x3 mat2 = control.GetTransform();
                        Matrix3x3 mat = mat1 * mat2;
                        page.TransformCurrentObjects(mat.GetInverse() * 
                            Matrix3x3.Scale(
                            (float)Math.Exp(delta.X / 100),
                            (float)Math.Exp(-delta.Y / 100)) * 
                            mat);
                        control.RefreshPage();
                    }
                }
                else
                {
                    currentTouchId = uint.MaxValue;
                    posLast = null;
                    HistoryManager.StoreState(page);
                }
                stylus = null;
                mouse = null;
                allTouches.Clear();
                refresh = true;
            }

            MouseDown = currentTouchId != uint.MaxValue;
            return refresh;
        }
    }
}
