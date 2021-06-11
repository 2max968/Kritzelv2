using Kritzel.Main.Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Forms
{
    public class TextBox : Line, IClickable
    {
        public static readonly Bitmap BitmapTB = ResManager.LoadIcon("tools/text.svg", Util.GetGUISize());
        public static readonly Encoding ENCODING = Encoding.Unicode;

        string text;
        SizeF size;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                size = Util.MeasureString(text, new Font(FontFamily, FontSize * Util.GetScaleFactor()));
            }
        }
        public string FontFamily { get; set; } = "Arial";
        public float FontSize { get; set; } = 12;
        public BaseRenderer.TextAlign Align { get; set; } = BaseRenderer.TextAlign.Left;

        public TextBox()
        {
            Text = "Text";
        }

        public override void AddPoint(float x, float y, float pressure)
        {
            if (Points.Count == 0)
                Points.Add(new LPoint(x, y, 1));
            else
                Points.Last().Move(x, y);
            bounds = new RectangleF(x, y, 1, 1);
        }

        public override void Render(BaseRenderer g, float quality = 1, int start = 0, bool simple = false)
        {
            if (Points.Count < 0) return;
            float x = (Points[0].X - size.Width / 2);
            float y = (Points[0].Y - size.Height / 2);
            float w = (size.Width);
            float h = (size.Height);
            //g.DrawText(Text, Brush,
            //    new System.Drawing.RectangleF(x, y, 1000, 1000), FontSize);
            g.DrawText(Text, Brush.GetColor(), x, y, FontFamily, FontSize, Align);
            if(Selected && g is GPURenderer)
            {
                var renderer = (GPURenderer)g;
                renderer.DrawDashPolygon(new PointF[]
                {
                    new PointF(x,y),
                    new PointF(x+w,y),
                    new PointF(x+w,y+h),
                    new PointF(x,y+h),
                    new PointF(x,y)
                });
            }
        }

        public override string ToParamString()
        {
            float x = Points[0].X;
            float y = Points[0].Y;
            string base64text = Convert.ToBase64String(ENCODING.GetBytes(Text));
            return $"{FontFamily};{Util.FToS(FontSize)};{Util.FToS(x)};{Util.FToS(y)};{base64text}";
        }

        public override void FromParamString(string txt)
        {
            string[] parts = txt.Split(';');
            if(parts.Length == 5)
            {
                FontFamily = parts[0];
                FontSize = Util.SToF(parts[1]);
                float x = Util.SToF(parts[2]) - size.Width / 2;
                float y = Util.SToF(parts[3]);
                Points.Clear();
                Points.Add(new LPoint(x, y, 1));
                byte[] base64data = Convert.FromBase64String(parts[4]);
                Text = ENCODING.GetString(base64data);
            }
        }

        public void Click(InkControl parent)
        {
            parent.SelectLines(new Line[] { this });
            var editor = new Dialogues.TextBoxInput(this, parent);
            editor.Show();
            parent.HideSelectionMenu();
        }

        public RectangleF GetClickableBounds()
        {
            float x = Util.PointToMm(Points[0].X - size.Width / 2);
            float y = Util.PointToMm(Points[0].Y - size.Height / 2);
            return new RectangleF(x, y, Util.PointToMm(size.Width), Util.PointToMm(size.Height));
        }
    }
}
