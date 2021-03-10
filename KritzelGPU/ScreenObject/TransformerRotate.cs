using Kritzel.Main.Renderer;
using Kritzel.PointerInputLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.ScreenObject
{
    public class TransformerRotate : Transformer
    {
        int rad;
        int width;
        KPage page;
        InkControl control;
        int x, y;
        uint currentTouchId = uint.MaxValue;
        float angleStart = float.NaN;
        float ringRot = 0;
        static Renderer.Image img = null;
        static Rectangle imRect;

        public TransformerRotate(KPage page, InkControl control)
        {
            rad = (int)(42 * Util.GetScaleFactor());
            width = (int)(10 * Util.GetScaleFactor());

            this.page = page;
            this.control = control;

            if(img == null)
            {
                imRect = new Rectangle(width * -2, -rad - width, width * 4, width * 2);
                Bitmap bmp = ResManager.LoadIcon("gui/rotator.svg", imRect.Width, imRect.Height);
                img = new Renderer.Image(bmp);
            }
        }

        public override bool Collides(float x, float y, int screenWidth, int screenHeight)
        {
            float _x_ = this.x;
            float _y_ = this.y;
            control.GetTransform().Transform(ref _x_, ref _y_);
            float _x = x - _x_;
            float _y = y - _y_;
            float len_sq = _x * _x + _y * _y;
            return len_sq <= (rad + width) * (rad + width) && len_sq >= (rad - width) * (rad - width);
        }

        public override void Draw(GPURenderer renderer, int width, int height)
        {
            renderer.Transform(Transformation);
            int _x = x;
            int _y = y;
            control.GetTransform().Transform(ref _x, ref _y);

            Rectangle rect = new Rectangle(_x - rad, _y - rad, 2 * rad, 2 * rad);
            renderer.DrawEllipse(PBrush.CreateSolid(Color.White), this.width, rect);
            rect.Extend(-this.width / 2);
            renderer.DrawEllipse(PBrush.CreateSolid(Color.Black), 4, rect);
            rect.Extend(this.width);
            renderer.DrawEllipse(PBrush.CreateSolid(Color.Black), 4, rect);
            Matrix3x3 mat = new Matrix3x3();
            mat.TransformRotateAt(ringRot, _x, _y);
            renderer.Transform(mat);
            renderer.DrawImage(img, new RectangleF(imRect.X + _x, imRect.Y + _y, imRect.Width, imRect.Height));
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

                    if (float.IsNaN(angleStart))
                    {
                        angleStart = ang;
                    }
                    else
                    {
                        float rel = ang - angleStart;
                        angleStart = ang;
                        Matrix3x3 mat = new Matrix3x3();
                        ringRot -= rel;
                        mat.TransformRotateAt(-rel, this.x, this.y);
                        page.TransformCurrentObjects(mat);
                        Console.WriteLine(mat);
                        control.RefreshPage();
                    }
                }
                else
                {
                    currentTouchId = uint.MaxValue;
                    angleStart = float.NaN;
                    HistoryManager.StoreState(page);
                }
                stylus = null;
                mouse = null;
                allTouches.Clear();
                refresh = true;
            }

            MouseDown = currentTouchId != uint.MaxValue;
            if (Close) refresh = true;
            return refresh;
        }
    }
}
