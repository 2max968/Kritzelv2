using Kritzel.Main.Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.Forms
{
    public class TextBox : Line
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
                size = Util.MeasureString(text, new Font(fontFamily, fontSize * Util.GetScaleFactor()));
            }
        }
        string fontFamily = "Arial";
        float fontSize = 12;

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
            float x = Util.PointToMm(Points[0].X - size.Width / 2);
            float y = Util.PointToMm(Points[0].Y - size.Height / 2);
            g.DrawText(Text, Brush,
                new System.Drawing.RectangleF(x, y, 1000, 1000), fontSize);
        }

        public override string ToParamString()
        {
            float x = Points[0].X;
            float y = Points[0].Y;
            string base64text = Convert.ToBase64String(ENCODING.GetBytes(Text));
            return $"{fontFamily};{Util.FToS(fontSize)};{Util.FToS(x)};{Util.FToS(y)};{base64text}";
        }

        public override void FromParamString(string txt)
        {
            string[] parts = txt.Split(';');
            if(parts.Length == 5)
            {
                fontFamily = parts[0];
                fontSize = Util.SToF(parts[1]);
                float x = Util.SToF(parts[2]) - size.Width / 2;
                float y = Util.SToF(parts[3]);
                Points.Clear();
                Points.Add(new LPoint(x, y, 1));
                byte[] base64data = Convert.FromBase64String(parts[4]);
                Text = ENCODING.GetString(base64data);
            }
        }
    }
}
