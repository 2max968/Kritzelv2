using Kritzel.Main.Dialogues;
using Kritzel.Main.Renderer;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Kritzel.Main
{
    public class KPage : IDisposable
    {
        public PageFormat Format;
        List<Line> lines;
        public int LineCount { get { return lines.Count; } }
        public Renderer.Image BackgroundImage = null;
        public PdfPage OriginalPage = null;
        Backgrounds.Background background = new Backgrounds.BackgroundQuadPaper5mm();
        public string PdfRenderPath = "";
        public ColorFilter Filter = ColorFilter.Normal;
        public Backgrounds.Background Background
        {
            get { return background; }
            set { background = value; Version++; }
        }
        Color[] backgroundColor = new Color[] { Color.LightSteelBlue, Color.OrangeRed };
        public Color BackgroundColor1
        {
            get { return backgroundColor[0]; }
            set { backgroundColor[0] = value; Version++; }
        }
        public Color BackgroundColor2
        {
            get { return backgroundColor[1]; }
            set { backgroundColor[1] = value; Version++; }
        }
        float border = 15f;
        public float Border { get { return border; } set { border = value; Version++; } }
        public bool IsDisposed { get; private set; } = false;
        public DateTime CreationTime { get; private set; }
        bool showDate = true;
        public bool ShowDate { get { return showDate; } set { showDate = value; Version++; } }
        public string Name { get; private set; } = "";
        public Thread loaderThread;
        public uint Version { get; private set; } = 0;
        uint lineId = 1;
        KDocument document;

        public KPage(KDocument document)
        {
            var formats = PageFormat.GetFormats();
            if (formats.ContainsKey(Configuration.DefaultFormat))
                Format = formats[Configuration.DefaultFormat];
            else
                Format = PageFormat.A4;
            lines = new List<Line>();
            CreationTime = DateTime.Now;
            this.document = document;
        }

        public string SaveToString()
        {
            StringBuilder output = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = " ",
                NewLineChars = "\n"
            };
            using (XmlWriter xml = XmlWriter.Create(output, settings))
            {
                xml.WriteStartElement("Ink");
                xml.WriteStartElement("Name");
                xml.WriteAttributeString("value", Name);
                xml.WriteEndElement();
                xml.WriteStartElement("Format");
                string bgrName = "null";
                if (Background != null)
                    bgrName = Background.GetType().FullName;
                xml.WriteAttributeString("w", Util.FToS(Format.Width));
                xml.WriteAttributeString("h", Util.FToS(Format.Height));
                xml.WriteAttributeString("background", bgrName);
                xml.WriteAttributeString("border", Util.FToS(Border));
                xml.WriteEndElement();
                xml.WriteStartElement("CreationTime");
                xml.WriteAttributeString("show", ShowDate ? "true" : "false");
                xml.WriteAttributeString("date", CreationTime.ToFileTime().ToString());
                xml.WriteEndElement();
                xml.WriteElementString("Filter", Filter.ToString());
                foreach (Line l in lines)
                {
                    xml.WriteStartElement("Line");
                    xml.WriteAttributeString("color", ColorTranslator.ToHtml(l.Brush.GetRawColor()));
                    xml.WriteAttributeString("type", l.GetType().FullName);
                    xml.WriteAttributeString("params", l.ToParamString());
                    /*xml.WriteStartElement("Brush");
                    xml.WriteAttributeString("type", l.Brush.SType());
                    xml.WriteAttributeString("color", l.Brush.SColors());
                    xml.WriteAttributeString("nums", l.Brush.SFloats());
                    xml.WriteEndElement();*/
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
            }
            return output.ToString();
        }

        public void LoadFromString(string txt, MessageLog log)
        {
            lock(this)
            { 
                StringReader input = new StringReader(txt);
                Line line = null;
                lines.Clear();
                using (XmlReader xml = XmlReader.Create(input))
                {
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element)
                        {
                            if (xml.Name == "Line")
                            {
                                string typeN = xml.GetAttribute("type");
                                string param = xml.GetAttribute("params");
                                string color = null;
                                try
                                {
                                    color = xml.GetAttribute("color");
                                }
                                catch (Exception) { }
                                Type t = Assembly.GetCallingAssembly().GetType(typeN);
                                line = t.GetConstructor(new Type[0]).Invoke(new object[0]) as Line;
                                if (line != null)
                                {
                                    line.FromParamString(param);
                                    line.CalcSpline();
                                    line.CalculateBounds();
                                }
                                if (color == null)
                                    line.Brush = PBrush.CreateSolid(Color.Black);
                                else
                                    line.Brush = PBrush.CreateSolid(ColorTranslator.FromHtml(color));
                                lines.Add(line);
                            }
                            else if (xml.Name == "Brush")
                            {
                                PBrush brush = PBrush.FromStrings(
                                    xml.GetAttribute("type"),
                                    xml.GetAttribute("color"),
                                    xml.GetAttribute("nums"));
                                if (line != null) line.Brush = brush;
                            }
                            else if (xml.Name == "Format")
                            {
                                float w = 1;
                                float h = 1;
                                float border = 15;
                                Util.TrySToF(xml.GetAttribute("w"), out w);
                                Util.TrySToF(xml.GetAttribute("h"), out h);
                                Util.TrySToF(xml.GetAttribute("border"), out border);
                                Border = border;
                                Format = new PageFormat(w, h);
                                string backgroundName = xml.GetAttribute("background");
                                Type backgroundType = Type.GetType(backgroundName);
                                Backgrounds.Background bgr;
                                if (backgroundType == null)
                                {
                                    bgr = null;
                                    if (backgroundName != "null")
                                        log?.Add(MessageType.WARN, "Background '{0}'", backgroundName);
                                }
                                else
                                {
                                    bgr = (Backgrounds.Background)backgroundType
                                        .GetConstructor(new Type[0])
                                        .Invoke(new object[0]);
                                }
                                Background = bgr;
                            }
                            else if (xml.Name == "CreationTime")
                            {
                                bool show = xml.GetAttribute("show") == "true";
                                long time = 0;
                                long.TryParse(xml.GetAttribute("date"), out time);
                                ShowDate = show;
                                CreationTime = DateTime.FromFileTime(time);
                            }
                            else if (xml.Name == "Name")
                            {
                                Name = xml.GetAttribute("value");
                            }
                            else if(xml.Name =="Filter")
                            {
                                string filterName = xml.ReadElementContentAsString();
                                if (Enum.TryParse<ColorFilter>(filterName, out ColorFilter filter))
                                    Filter = filter;
                                else
                                    log?.Add(0, MessageType.WARN, "Cant parse the ColorFilter '{0}'", filterName);
                            }
                        }
                        else if (xml.NodeType == XmlNodeType.EndElement)
                        {
                            if (xml.Name == "Line")
                            {
                                line = null;
                            }
                        }
                    }
                }
            }
        }

        public void SelectArea(PointF[] points)
        {
            lock (this)
            {
                Deselect();
                if (points.Length <= 1)
                    return;
                RectangleF fbounds = Util.GetBounds(points);
                Rectangle bounds = new Rectangle((int)fbounds.X, (int)fbounds.Y, (int)fbounds.Width + 1, (int)fbounds.Height + 1);

                SizeF s = Format.GetPixelSize();
                Bitmap sBuffer = new Bitmap(bounds.Width, bounds.Height);
                using (Graphics g = Graphics.FromImage(sBuffer))
                {
                    g.Clear(Color.Black);
                    g.TranslateTransform(-bounds.X, -bounds.Y);
                    g.FillPolygon(Brushes.White, points);
                }

                for (int i = 0; i < lines.Count; i++)
                {
                    lines[i].Selected = lines[i].Points.Count > 0;
                    for (int j = 0; j < lines[i].Points.Count; j++)
                    {
                        int x = (int)lines[i].Points[j].X;
                        int y = (int)lines[i].Points[j].Y;
                        if (x > bounds.Left && x < bounds.Right && y > bounds.Top && y < bounds.Bottom)
                        {
                            bool v = sBuffer.GetPixel(x - bounds.X, y - bounds.Y).R > 127;
                            if (!v)
                            {
                                lines[i].Selected = false;
                                break;
                            }
                        }
                        else
                        {
                            lines[i].Selected = false;
                            break;
                        }
                    }
                }

                sBuffer.Dispose();
            }
        }

        public void Deselect()
        {
            lock (this)
            {
                foreach (Line l in lines)
                    l.Selected = false;
            }
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            if (BackgroundImage != null)
                BackgroundImage.Dispose();
        }

        public void DrawBackground(Renderer.BaseRenderer r)
        {
            for (int i = 0; i < lines.Count; i++)
                if (lines[i] is Forms.IBackground)
                    lines[i].Render(r);
        }

        public void Draw(Renderer.BaseRenderer r)
        {
            lock (this)
            {
                SizeF pSize = Format.GetPixelSize();

                if (Background != null)
                    Background.Draw(r, Format, Border,
                        Util.ApplyFilter(BackgroundColor1, this.Filter), Util.ApplyFilter(BackgroundColor2, this.Filter));

                if (ShowDate)
                    r.DrawText(CreationTime.ToLongDateString() + " " + CreationTime.ToShortTimeString(),
                        PBrush.CreateSolid(Util.ApplyFilter(BackgroundColor1, this.Filter)),
                        new RectangleF(Border + 1, Border - 5, 300, 50), 2);

                for (int i = 0; i < lines.Count; i++)
                {
                    if (!(lines[i] is Forms.IBackground))
                    {
                        lines[i].Render(r);
                        if (Configuration.ShowLineBoundingBoxes)
                            r.DrawRect(Color.Green, 1, lines[i].Bounds);
                    }
                }
            }
        }

        public Bitmap GetThumbnail(int width, int height, Color background, Color border, int borderWidth)
        {
            lock (this)
            {
                Bitmap thumb = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(thumb);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Renderer.GdiRenderer r = new Renderer.GdiRenderer(g);
                g.Clear(background);
                float isRatio = Format.Width / Format.Height;
                float shouldRatio = width / (float)height;
                float scale = 1;
                SizeF ps = Format.GetPixelSize();
                if (isRatio > shouldRatio)
                {
                    float w = ps.Width;
                    scale = width / w;
                }
                else
                {
                    float h = ps.Height;
                    scale = height / h;
                }
                g.TranslateTransform((width - ps.Width * scale) / 2, (height - ps.Height * scale) / 2);
                g.ScaleTransform(scale, scale);
                if (this.BackgroundImage != null) r.DrawImage(BackgroundImage,
                    new RectangleF(new PointF(0, 0), Format.GetPixelSize()));
                Color bakCol = this.BackgroundColor1;
                this.BackgroundColor1 = Color.Transparent;
                this.Draw(r);
                this.BackgroundColor1 = bakCol;
                g.ResetTransform();
                using (Pen p = new Pen(border, borderWidth))
                {
                    g.DrawRectangle(p, new Rectangle(borderWidth / 2, borderWidth / 2,
                        width - borderWidth, height - borderWidth));
                }
                return thumb;
            }
        }

        public RectangleF GetTransformedBoundaryBox(Matrix3x3 mat)
        {
            SizeF size = Format.GetPixelSize();
            PointF[] p = new PointF[]
            {
                new PointF(0,0),
                new PointF(size.Width,0),
                new PointF(size.Width,size.Height),
                new PointF(0,size.Height)
            };
            mat.Transform(p);
            RectangleF r = new RectangleF(p[0], new SizeF(0, 0));
            for(int i = 1; i < p.Length; i++)
            {
                if(p[i].X<r.Left)
                {
                    float diff = r.Left - p[i].X;
                    r.X -= diff;
                    r.Width += diff;
                }
                if(p[i].Y < r.Top)
                {
                    float diff = r.Top - p[i].Y;
                    r.Y -= diff;
                    r.Height += diff;
                }
                if(p[i].X > r.Right)
                {
                    float diff = p[i].X - r.Right;
                    r.Width += diff;
                }
                if (p[i].Y > r.Bottom)
                {
                    float diff = p[i].Y - r.Bottom;
                    r.Height += diff;
                }
            }
            return r;
        }

        public PointF GetTransformedCenter(Matrix3x3 mat)
        {
            SizeF size = Format.GetPixelSize();
            PointF[] p = new PointF[]
            {
                new PointF(0,0),
                new PointF(size.Width,0),
                new PointF(size.Width,size.Height),
                new PointF(0,size.Height)
            };
            mat.Transform(p);
            PointF center = new PointF(0, 0);
            foreach(PointF pt in p)
            {
                center.X += pt.X;
                center.Y += pt.Y;
            }
            center.X /= p.Length;
            center.Y /= p.Length;
            return center;
        }

        public List<Color> GetListOfBrushes()
        {
            List<Color> colors = new List<Color>();
            for(int i = 0; i < lines.Count; i++)
            {
                if (!colors.Contains(lines[i].Brush.GetColor()))
                    colors.Add(lines[i].Brush.GetColor());
            }
            return colors;
        }

        public void OnShow(InkControl control)
        {
            if(OriginalPage != null && BackgroundImage == null)
            {                string path = TmpManager.GetTmpDir() + "\\render.pdf";
                PdfDocument doc = new PdfDocument();
                doc.Pages.Add(OriginalPage);
                doc.Save(path);
                Bitmap[] bmp = MupdfSharp.PageRenderer.Render(path, 50, 0);
                BackgroundImage = new Renderer.Image(bmp[0]);

                loaderThread = new Thread(delegate ()
                {
                    Bitmap[] bmp2 = MupdfSharp.PageRenderer.Render(path, PDFImporter.PAGETHEIGHTPIXEL, 0);
                    BackgroundImage.Dispose();
                    BackgroundImage = new Renderer.Image(bmp2[0]);
                    control.RefreshPage();
                });
                loaderThread.Start();
            }
        }

        public void OnHide()
        {
            if (OriginalPage != null && BackgroundImage != null)
            {
                if (loaderThread != null) loaderThread.Join();
                BackgroundImage.Dispose();
                BackgroundImage = null;
            }
        }

        public void CreatePDFBackground()
        {
            if(OriginalPage != null && PdfRenderPath != "")
            {
                if(!File.Exists(PdfRenderPath))
                {

                }
            }
        }

        public void AddLine(Line ln)
        {
            lock(this)
                lines.Add(ln);
            ln.Id = lineId++;
            Version++;
            HistoryManager.StoreState(this);
        }

        public void AddLines(IEnumerable<Line> lns)
        {
            lock (this)
                lines.AddRange(lns);
            foreach(Line ln in lns)
                ln.Id = lineId++;
            Version++;
            HistoryManager.StoreState(this);
        }

        public Line GetLine(int ind)
        {
            return lines[ind];
        }

        public void RemoveLine(int ind)
        {
            lock (this)
            {
                Line line = GetLine(ind);
                if (line.Selected)
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Selected)
                        {
                            lines.RemoveAt(i--);
                        }
                    }
                }
                else
                {
                    lines.RemoveAt(ind);
                }
                Version++;
            }
            HistoryManager.StoreState(this);
        }

        public IEnumerable<Line> EnumerateLines()
        {
            return lines;
        }

        public IEnumerable<Line> GetSelectedLines()
        {
            foreach(Line l in lines)
            {
                if (l.Selected)
                    yield return l;
            }
        }

        public void TransformCurrentObjects(Matrix3x3 mat)
        {
            // TODO: Maybe remove lock
            lock (this)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Selected)
                    {
                        lines[i].Transform(mat);
                    }
                }
                Version++;
            }
        }

        public void SetSelectionBrush(PBrush brush)
        {
            lock(this)
            {
                for(int i = 0; i < lines.Count; i++)
                    if (lines[i].Selected)
                        lines[i].Brush = brush;
                HistoryManager.StoreState(this);
                Version++;
            }
        }

        public KDocument GetDocument()
        {
            return document;
        }

        public void ChangeDocument(KDocument doc)
        {
            this.document = doc;
        }
    }
}
