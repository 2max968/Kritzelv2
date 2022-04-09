using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Kritzel.Main
{
    public static class Util
    {
        public static float guiScale = 0;
        public const float MmPerInch = 25.4f;

        public delegate void DelayedAction();

        [DllImport("shell32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsUserAnAdmin();

        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        public static RectangleF CreateRect(PointF p1, PointF p2)
        {
            RectangleF rect = new RectangleF(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            if(rect.Width < 0)
            {
                rect.Width *= -1;
                rect.X -= rect.Width;
            }
            if(rect.Height < 0)
            {
                rect.Height *= -1;
                rect.Y -= rect.Height;
            }
            return rect;
        }

        public static string FToS(float f)
        {
            return f.ToString(CultureInfo.InvariantCulture);
        }

        public static float SToF(string s)
        {
            return float.Parse(s, CultureInfo.InvariantCulture);
        }

        public static bool TrySToF(string s, out float f)
        {
            return float.TryParse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out f);
        }

        public static Matrix GetInverseMatrix(this Matrix mat)
        {
            // [d/(a* d - b* c),           -b/(a* d - b* c), 0]
            // [          -c/(a* d - b* c), a/(a* d - b* c), 0]
            // [ (c * f - d * e)/(a* d - b* c), -(a* f - b* e)/(a* d - b* c), 1]
            /*float a = mat.Elements[0];
            float b = mat.Elements[1];
            float c = mat.Elements[2];
            float d = mat.Elements[3];
            float e = mat.Elements[4];
            float f = mat.Elements[5];
            return new Matrix(d / (a * d - b * c), -b / (a * d - b * c),
                -c / (a * d - b * c), a / (a * d - b * c),
                (c * f - d * e) / (a * d - b * c), -(a * f - b * e) / (a * d - b * c));*/
            Matrix inv = mat.Clone();
            inv.Invert();
            return inv;
        }

        public static void Transform(this Matrix mat, ref float x, ref float y)
        {
            // [e + a*x + c*y, f + b*x + d*y, 1]
            float a = mat.Elements[0];
            float b = mat.Elements[1];
            float c = mat.Elements[2];
            float d = mat.Elements[3];
            float e = mat.Elements[4];
            float f = mat.Elements[5];
            x = e + a * x + c * y;
            y = f + b * x + d * y;
        }

        public static void Transform(this Matrix mat, LPoint p)
        {
            // [a*x + c*y, b*x + d*y, 0]
            float a = mat.Elements[0];
            float b = mat.Elements[1];
            float c = mat.Elements[2];
            float d = mat.Elements[3];
            float e = mat.Elements[4];
            float f = mat.Elements[5];
            float x = p.X;
            float y = p.Y;
            x = e + a * x + c * y;
            y = f + b * x + d * y;
            p.X = x;
            p.Y = y;
        }

        public static string Run(string cmd, string args)
        {
            ProcessStartInfo psi = new ProcessStartInfo(cmd, args);
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            Process p = Process.Start(psi);
            p.WaitForExit();
            string txt = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
            return txt;
        }

        public static Renderer.GdiRenderer GetRenderer(this Graphics g)
        {
            return new Renderer.GdiRenderer(g);
        }

        public static float MmToPoint(float mm)
        {
            float inch = 0.0393701f;
            float dpi = 96;
            return mm * dpi * inch;
        }

        public static RectangleF MmToPoint(RectangleF rect)
        {
            return new RectangleF(MmToPoint(rect.X), MmToPoint(rect.Y), MmToPoint(rect.Width), MmToPoint(rect.Height));
        }

        public static float PointToMm(float point)
        {
            float inch = 0.0393701f;
            float dpi = 96;
            return point / (dpi * inch);
        }

        public static PointF GetTranslation(this Matrix m)
        {
            return new PointF(m.Elements[4], m.Elements[5]);
        }

        public static PointF GetScale(this Matrix m)
        {
            float lx = (float)Math.Sqrt(m.Elements[0] * m.Elements[0] 
                + m.Elements[1] * m.Elements[1]);
            float ly = (float)Math.Sqrt(m.Elements[2] * m.Elements[2]
                + m.Elements[3] * m.Elements[3]);
            return new PointF(lx, ly);
        }

        public static Matrix GetRotationMatrix(this Matrix m)
        {
            PointF scale = GetScale(m);
            float a = m.Elements[0] / scale.X;
            float b = m.Elements[1] / scale.X;
            float c = m.Elements[2] / scale.Y;
            float d = m.Elements[3] / scale.Y;
            return new Matrix(a, b, c, d, 0, 0);
        }

        public static void TransformInverse(this Matrix m, PointF[] pts)
        {
            using (Matrix inv = m.GetInverseMatrix())
            {
                inv.TransformPoints(pts);
            }
        }

        public static RectangleF GetBounds(PointF[] pts)
        {
            if (pts.Length == 0) return new RectangleF(0, 0, 0, 0);
            RectangleF bounds = new RectangleF(pts[0].X, pts[0].Y, 0, 0);
            foreach(PointF pt in pts)
            {
                float x = pt.X;
                float y = pt.Y;
                if (x < bounds.Left) { bounds.Width += bounds.X - x; bounds.X = x; }
                if (x > bounds.Right) bounds.Width = x - bounds.X;
                if (y < bounds.Top) { bounds.Height += bounds.Y - y; bounds.Y = y; }
                if (y > bounds.Bottom) bounds.Height = y - bounds.Y;
            }
            return bounds;
        }

        public static float GetScaleFactor()
        {
            var effDpi = Screen.PrimaryScreen.GetDpi(DpiType.Effective);
            return effDpi / 96f;
        }

        public static float GetFontSizePixel()
        {
            return 16 * Util.GetScaleFactor();
        }

        public static bool Contains<T>(this T[] array, T obj)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(obj))
                    return true;
            return false;
        }

        public static bool WaitTimeout(object obj, string fieldName, int millis)
        {
            Type t = obj.GetType();
            FieldInfo field = t.GetField(fieldName);
            if(field != null)
            {
                return WaitTimeout(obj, field, millis);
            }
            PropertyInfo prop = t.GetProperty(fieldName);
            if(prop != null)
            {
                return WaitTimeout(obj, prop, millis);
            }
            return false;
        }

        public static bool WaitTimeout(object obj, FieldInfo field, int millis)
        {
            if (field.FieldType != typeof(bool))
                return false;

            int i = 0;
            while((bool)field.GetValue(obj) != true)
            {
                if (i++ > millis)
                {
                    Console.WriteLine("Timeout");
                    return false;
                }
                Thread.Sleep(1);
            }
            return true;
        }

        public static bool WaitTimeout(object obj, PropertyInfo property, int millis)
        {
            if (property.PropertyType != typeof(bool))
                return false;

            int i = 0;
            while ((bool)property.GetValue(obj) != true)
            {
                if (i++ > millis)
                {
                    Console.WriteLine("Timeout");
                    return false;
                }
                Thread.Sleep(1);
            }
            return true;
        }

        public static RectangleF Expand(this RectangleF rect, float f)
        {
            return new RectangleF(rect.X - f, rect.Y - f, rect.Width + 2 * f, rect.Height + 2 * f);
        }

        public static XmlNode GetNode(this XmlNode node, string name)
        {
            foreach(XmlNode nd in node.ChildNodes)
            {
                if(nd.Name == name)
                {
                    return nd;
                }
            }
            return null;
        }

        public static List<XmlNode> GetNodes(this XmlNode node, string name)
        {
            List<XmlNode> nodes = new List<XmlNode>();
            foreach (XmlNode nd in node.ChildNodes)
            {
                if (nd.Name == name)
                {
                    nodes.Add(nd);
                }
            }
            return nodes;
        }

        public static void RemoveAll(this DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in files)
            {
                try
                {
                    file.Delete();
                }
                catch (Exception) { }
            }
        }

        public static int GetGUISize()
        {
            if(guiScale <= 0)
            {
                guiScale = Configuration.GUIScaleFactor;
            }
            return (int)(48 * Util.GetScaleFactor() * guiScale);
        }

        public static void SetAllButtonSizes(Control cltr, int size)
        {
            foreach(Control sub in cltr.Controls)
            {
                if(sub is Button)
                {
                    switch(sub.Dock)
                    {
                        case DockStyle.Bottom: case DockStyle.Top:
                            sub.Height = size;
                            break;
                        case DockStyle.Left: case DockStyle.Right:
                            sub.Width = size;
                            break;
                        case DockStyle.None:
                            sub.Width = sub.Height = size;
                            break;
                    }
                }
                SetAllButtonSizes(sub, size);
            }
        }

        public static string GetUsername()
        {
            return Environment.UserName;
        }

        public static float GetRealScreenDPIFactor()
        {
            if (Configuration.ScreenDPIFactor > 0)
                return Configuration.ScreenDPIFactor;
            return Util.GetScaleFactor();
        }

        public static void CartToPole(out float r, out float phi, float x, float y)
        {
            r = (float)Math.Sqrt(x * x + y * y);
            phi = (float)Math.Atan2(y, x);
        }

        public static void PoleToCart(out float x, out float y, float r, float phi)
        {
            x = (float)Math.Cos(phi) * r;
            y = (float)Math.Sin(phi) * r;
        }

        public static void PoleToCart(out PointF point, float r, float phi)
        {
            point = new PointF();
            point.X = (float)Math.Cos(phi) * r;
            point.Y = (float)Math.Sin(phi) * r;
        }

        public static void InvokeDelayed(this Control control, int millisecondsTimeout, DelayedAction action)
        {
            Thread t = new Thread(delegate ()
            {
                Thread.Sleep(millisecondsTimeout);
                if(!control.IsDisposed)
                {
                    control.Invoke(action);
                }
            });
            t.Start();
        }

        public static void Write(this Stream stream, string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            stream.Write(data, 0, data.Length);
        }

        public static Bitmap MakeBackgroundTransparent(this Bitmap source, Color background)
        {
            int color = background.ToArgb();
            int transparent = Color.FromArgb(0, background.R, background.G, background.B).ToArgb();
            Bitmap dst = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
            BitmapData lSrc = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData lDst = dst.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int[] dSrc = new int[source.Width * source.Height];
            int[] dDst = new int[source.Width * source.Height];
            Marshal.Copy(lSrc.Scan0, dSrc, 0, dSrc.Length);
            for (int i = 0; i < dSrc.Length; i++)
            {
                if(dSrc[i] == color)
                {
                    dDst[i] = transparent;
                }
                else
                {
                    dDst[i] = dSrc[i];
                }
            }
            Marshal.Copy(dDst, 0, lDst.Scan0, dDst.Length);
            source.UnlockBits(lSrc);
            dst.UnlockBits(lDst);
            return dst;
        }

        public static void Extend(this ref Rectangle rect, int amount)
        {
            rect.X += amount;
            rect.Y += amount;
            rect.Width -= amount * 2;
            rect.Height -= amount * 2;
        }

        public static bool AskForSave(KDocument doc)
        {
            bool cancel = false;
            if (!doc.IsSaved())
            {
                DialogResult res = MessageBox.Show(Language.GetText("System.save?"), "Kritzel", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (res == DialogResult.Cancel) cancel = true;
                else if (res == DialogResult.Yes)
                {
                    if (doc.FilePath == "")
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Kritzel Document|*.krit";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            cancel = !doc.SaveDocument(sfd.FileName);
                        }
                        else
                        {
                            cancel = true;
                        }
                    }
                    else
                    {
                        cancel = !doc.SaveDocument(doc.FilePath);
                    }
                }
            }
            return !cancel;
        }

        public static Color ApplyFilter(Color input, ColorFilter filter)
        {
            if (filter == ColorFilter.InvertLuminosity)
                return HSLColor.InvertColor(input);
            return input;
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        public static SizeF MeasureString(string text, Font font)
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                return g.MeasureString(text, font);
            }
        }

        public static RectangleF GetFullBounds(IEnumerable<Line> lines)
        {
            if (lines.Count() == 0) return new RectangleF(0, 0, 1, 1);
            var _line = lines.GetEnumerator();
            _line.MoveNext();
            float x1 = _line.Current.Bounds.Left, x2 = _line.Current.Bounds.Right;
            float y1 = _line.Current.Bounds.Top, y2 = _line.Current.Bounds.Bottom;
            while(_line.MoveNext())
            {
                Line line = _line.Current;
                if (line.Bounds.Left < x1) x1 = line.Bounds.Left;
                if (line.Bounds.Right > x2) x2 = line.Bounds.Right;
                if (line.Bounds.Top < y1) y1 = line.Bounds.Top;
                if (line.Bounds.Bottom > y2) y2 = line.Bounds.Bottom;
            }
            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
        }

        public static void SaveLines(XmlWriter xml, List<Line> lines, bool createRoot = true)
        {
            if(createRoot)
                xml.WriteStartElement("Lines");

            foreach(Line l in lines)
            {
                xml.WriteStartElement("Line");
                xml.WriteAttributeString("type", l.GetType().FullName);
                xml.WriteAttributeString("brush", ColorTranslator.ToHtml(l.Brush.GetColor()));
                xml.WriteAttributeString("params", l.ToParamString());
                xml.WriteEndElement();
            }

            if (createRoot)
                xml.WriteEndElement();
        }

        public static List<Line> GetLines(XmlReader xml, string closingTag)
        {
            List<Line> lines = new List<Line>();

            while(xml.Read())
            {
                if (xml.NodeType == XmlNodeType.EndElement && xml.Name == closingTag)
                    break;

                if(xml.NodeType == XmlNodeType.Element && xml.Name == "Line")
                {
                    try
                    {
                        Type type = Assembly.GetAssembly(typeof(Line)).GetType(xml.GetAttribute("type"));
                        Line line = (Line)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                        PBrush brush = PBrush.CreateSolid(ColorTranslator.FromHtml(xml.GetAttribute("brush")));
                        string pars = xml.GetAttribute("params");
                        line.FromParamString(pars);
                        line.Brush = brush;
                        line.CalcSpline();
                        line.CalculateBounds();
                        lines.Add(line);
                    }
                    catch (Exception) { }
                }
            }
            return lines;
        }

        public static Bitmap TrimBitmap(Bitmap bmp, out Rectangle rect)
        {
            BitmapData bDat = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixels = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(bDat.Scan0, pixels, 0, pixels.Length);
            bmp.UnlockBits(bDat);

            int fX = 0, lX = bmp.Height, fY = 0, lY = bmp.Width;

            // Find first x
            bool breakloop = false;
            for(int x = 0; x <= bmp.Width && !breakloop; x++)
            {
                if (x == bmp.Width)
                {
                    rect = new Rectangle(0, 0, 0, 0);
                    return null;
                }
                else
                {
                    for (int y = 0; y < bmp.Height && !breakloop; y++)
                    {
                        int alpha = pixels[(y * bmp.Width) + x * 4];
                        if (alpha > 0)
                        {
                            breakloop = true;
                            fX = x;
                        }
                    }
                }
            }

            // Find first y
            breakloop = false;
            for (int y = 0; y < bmp.Height && !breakloop; y++)
            {
                for (int x = 0; x < bmp.Width && !breakloop; x++)
                {
                    int alpha = pixels[(y * bmp.Width + x) * 4];
                    if (alpha > 0)
                    {
                        breakloop = true;
                        fY = y;
                    }
                }
            }

            // Find last x
            breakloop = false;
            for (int x = bmp.Width - 1; x >= 0 && !breakloop; x--)
            {
                for (int y = 0; y < bmp.Height && !breakloop; y++)
                {
                    int alpha = pixels[(y * bmp.Width + x) * 4];
                    if (alpha > 0)
                    {
                        breakloop = true;
                        lX = x;
                    }
                }
            }

            // Find last y
            breakloop = false;
            for (int y = bmp.Height - 1; y >= 0 && !breakloop; y--)
            {
                for (int x = 0; x < bmp.Width && !breakloop; x++)
                {
                    int alpha = pixels[(y * bmp.Width + x) * 4];
                    if (alpha > 0)
                    {
                        breakloop = true;
                        lY = y;
                    }
                }
            }

            // Copy area to new Bitmap
            rect = new Rectangle(fX, fY, lX - fX, lY - fY);
            Bitmap nbmp = new Bitmap(rect.Width, rect.Height);
            /*BitmapData bDat2 = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bDat3 = nbmp.LockBits(new Rectangle(0, 0, rect.Width, rect.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            byte[] buffer = new byte[rect.Width * rect.Height * 4];
            Marshal.Copy(bDat2.Scan0, buffer, 0, buffer.Length);
            Marshal.Copy(buffer, 0, bDat3.Scan0, buffer.Length);
            //CopyMemory(bDat3.Scan0, bDat2.Scan0, (uint)(rect.Width * rect.Height * 4));
            bmp.UnlockBits(bDat2);
            nbmp.UnlockBits(bDat3);*/
            using (Graphics g = Graphics.FromImage(nbmp))
            {
                g.DrawImage(bmp, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }

            return nbmp;
        }

        public static Bitmap TrimBitmap(Bitmap bmp, ref RectangleF rect)
        {
            var bmp2 = Util.TrimBitmap(bmp, out Rectangle trimrect);
            if(bmp2 == null)
            {
                rect = new RectangleF(rect.X, rect.Y, 0, 0);
                return null;
            }
            float scaleX = rect.Width / bmp.Width;
            float scaleY = rect.Height / bmp.Height;
            float offsX = rect.X;
            float offsY = rect.Y;
            rect = new RectangleF(offsX + trimrect.X * scaleX, offsY + trimrect.Y * scaleY, trimrect.Width * scaleX, trimrect.Height * scaleY);
            return bmp2;
        }
    }
}
