using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main
{
    public class CopyPaster
    {
        public const string LineID = "KritzelLines";
        public const string DataType = "Kritzel.Copydata";

        public static void CopyLine(Line[] lines, int pageHash = 0)
        {
            if (lines.Length == 0) return;

            DataObject data = new DataObject();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1:X}\n", LineID, pageHash);
            foreach (Line line in lines)
                sb.AppendFormat("{0};{1};{2}\n",
                    ColorTranslator.ToHtml(line.Brush.GetColor()),
                    line.GetType().FullName,
                    line.ToParamString());
            data.SetData(DataType, sb.ToString());

            RectangleF rectF = Util.GetFullBounds(lines);
            Rectangle rect = new Rectangle((int)rectF.X, (int)rectF.Y, (int)rectF.Width, (int)rectF.Height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppRgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.TranslateTransform(-rect.X, -rect.Y);
                g.Clear(Color.White);
                var r = new Renderer.GdiRenderer(g);
                r.RenderSpecial = false;
                foreach (Line l in lines) l.Render(r);
            }
            data.SetImage(bmp);
            /*MemoryStream pngStream = new MemoryStream();
            bmp.Save(pngStream, ImageFormat.Png);
            pngStream.Position = 0;
            data.SetData("PNG", false, pngStream);
            pngStream.Dispose();*/

            Clipboard.SetDataObject(data, true);
            bmp.Dispose();
        }

        public static string PasteString()
        {
            if (!Clipboard.ContainsData(DataType))
                return "";
            return (string)Clipboard.GetData(DataType);
        }

        public static Line[] PasteLines(int pageHash = 0)
        {
            string text = PasteString();
            string[] lines = text.Split('\n', '\r');
            string header = lines[0].Trim();
            if (lines.Length < 2 || !header.StartsWith(LineID))
                return new Line[0];

            string hashStr = lines[0].Substring(LineID.Length);
            bool shiftLines = false;
            if (pageHash != 0 && pageHash.ToString("X") == hashStr)
                shiftLines = true;

            List<Line> ret = new List<Line>();

            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] parts = lines[i].Split(new string[] { ";" }, 3, StringSplitOptions.RemoveEmptyEntries);
                    string typeName = parts[1];
                    Type t = Assembly.GetCallingAssembly().GetType(typeName);
                    if (t != null)
                    {
                        Line line = t.GetConstructor(new Type[0]).Invoke(new object[0]) as Line;
                        if (line != null)
                        {
                            line.FromParamString(parts[2]);
                            if (shiftLines) line.Transform(Matrix3x3.Translation(16, 16));
                            line.CalcSpline();
                            line.CalculateBounds();
                        }
                        line.Brush = PBrush.CreateSolid(ColorTranslator.FromHtml(parts[0]));
                        line.Selected = true;
                        ret.Add(line);
                    }
                }
                catch (Exception)
                {

                }
            }

            return ret.ToArray();
        }

        public static bool CheckClipboard()
        {
            string text = PasteString();
            return text.StartsWith(LineID);
        }
    }
}
