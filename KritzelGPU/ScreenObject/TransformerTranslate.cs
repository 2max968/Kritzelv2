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
    public class TransformerTranslate : Transformer
    {
        int x, y;
        int innerRad;
        InkControl control;
        KPage page;
        uint pageVersion;
        uint currentTouchId = uint.MaxValue;
        Transformer[] transformer;
        bool[] selectedLines;
        static Renderer.Image img = null;
        bool firstcall = true;

        public TransformerTranslate(KPage page, InkControl control, params Transformer[] transformer)
        {
            innerRad = (int)(20 * Util.GetScaleFactor());
            if(img == null || img.IsDisposed)
            {
                Bitmap bmp = ResManager.LoadIcon("gui/translator.svg", innerRad);
                img = new Renderer.Image(bmp);
            }

            this.control = control;
            this.page = page;
            this.pageVersion = page.Version;
            this.transformer = transformer;
            int c = 0;
            float _x = 0, _y = 0;
            selectedLines = new bool[page.LineCount];
            for(int i = 0; i < page.LineCount; i++)
            {
                Line l = page.GetLine(i);
                if(l.Selected)
                {
                    PointF center = new PointF(l.Bounds.X + l.Bounds.Width / 2,
                        l.Bounds.Y + l.Bounds.Height / 2);
                    _x += center.X;
                    _y += center.Y;
                    c++;
                }
                selectedLines[i] = l.Selected;
            }
            _x /= c;
            _y /= c;
            //control.GetTransform().Transform(ref _x, ref _y);
            //PointF pf = control.GetTransform().GetTranslation();
            x = (int)(_x);
            y = (int)(_y);
            this.Others = new List<Transformer>(this.transformer);
            this.Others.Add(this);
            foreach (Transformer trans in this.transformer)
            {
                trans.SetPosition(x, y);
                trans.Others = this.Others;
            }
        }

        public override bool Collides(float x, float y, int screenWidth, int screenHeight)
        {
            float _x_ = this.x;
            float _y_ = this.y;
            control.GetTransform().Transform(ref _x_, ref _y_);
            float _x = x - _x_;
            float _y = y - _y_;
            return _x * _x + _y * _y <= innerRad * innerRad;
        }

        public override void Draw(GPURenderer renderer, int width, int height)
        {
            int _x = x;
            int _y = y;
            control.GetTransform().Transform(ref _x, ref _y);
            renderer.Transform(Transformation);
            PBrush brushI = PBrush.CreateSolid(Color.White);
            PBrush brushO = PBrush.CreateSolid(Color.Black);
            RectangleF rect = new RectangleF(_x - innerRad, _y - innerRad, innerRad * 2, innerRad * 2);
            renderer.FillEllipse(brushI, rect);
            renderer.DrawEllipse(brushO, 4, rect);
            renderer.DrawImage(img, rect);
        }

        public override bool Think(List<Touch> allTouches, ref Touch stylus, ref Touch mouse, int screenWidth, int screenHeight)
        {
            if (IsMouseDown()) return false;
            if(page.LineCount != selectedLines.Length) Close = true;
            else
            {
                for(int i = 0; i < page.LineCount; i++)
                {
                    if (selectedLines[i] != page.GetLine(i).Selected) Close = true;
                }
            }
            if (control.Page != page) Close = true;

            bool refresh = false;

            PointF? cursorPos = null;
            uint pointerId = 0;
            if (stylus != null) {cursorPos = new PointF(stylus.X, stylus.Y); pointerId = stylus.Id; }
            else if(mouse != null) { cursorPos = new PointF(mouse.X, mouse.Y); pointerId = mouse.Id; }
            else if (allTouches.Count > 0) { cursorPos = new PointF(allTouches[0].X, allTouches[0].Y); pointerId = allTouches[0].Id; }

            if(cursorPos.HasValue && Collides(cursorPos.Value.X, cursorPos.Value.Y, screenWidth, screenHeight))
            {
                currentTouchId = pointerId;
            }

            if(currentTouchId != uint.MaxValue)
            {
                Touch t = null;
                t = stylus | mouse;
                if(t == null)
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

                if(t != null)
                {
                    int ox = this.x;
                    int oy = this.y;
                    this.x = t.X;
                    this.y = t.Y;
                    control.GetTransform().GetInverse().Transform(ref this.x, ref this.y);
                    page.TransformCurrentObjects(Matrix3x3.Translation(x - ox, y - oy));
                    foreach (Transformer trans in this.transformer)
                        trans.SetPosition(x, y);
                    control.RefreshPage();
                }
                else
                {
                    currentTouchId = uint.MaxValue;
                    HistoryManager.StoreState(page);
                }
                stylus = null;
                mouse = null;
                allTouches.Clear();
                refresh = true;
            }

            if (Close)
            {
                foreach (Transformer trans in this.transformer)
                    trans.Close = true;
            }
            if(firstcall)
            {
                firstcall = false;
                refresh = true;
            }

            MouseDown = currentTouchId != uint.MaxValue;
            return refresh;
        }

        public override void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
