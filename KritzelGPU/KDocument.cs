using Ionic.Zip;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Kritzel.Main
{
    public class KDocument : IDisposable
    {
        public List<KPage> Pages { get; private set; } = new List<KPage>();
        public List<KPage> Deletet { get; private set; } = new List<KPage>();
        public bool IsDisposed { get; private set; } = false;
        public string FilePath { get; set; } = "";
        public List<uint> SavedVersions { get; set; } = null;
        public string DefaultFormat;

        public static readonly KDocument EmptyDocument = new KDocument();

        public KDocument()
        {
            DefaultFormat = Configuration.DefaultFormat;
        }

        public async Task SavePDF(string path, Dialogues.ProgressWindow pb = null, int dpi = -1)
        {
            if(pb != null)
            {
                pb.ProgressBar.Maximum = Pages.Count;
                pb.ProgressBar.Value = 0;
            }
            MemoryStream mstream = new MemoryStream();
            PdfDocument doc = new PdfDocument();

            for (int i = 0; i < Pages.Count; i++)
            {
                if (Pages[i].OriginalPage != null)
                {
                    PdfPage page = Pages[i].OriginalPage;
                    doc.AddPage(page);
                }
                else
                {
                    PdfPage page = doc.AddPage();
                    page.Width = new XUnit(Pages[i].Format.Width, XGraphicsUnit.Millimeter);
                    page.Height = new XUnit(Pages[i].Format.Height, XGraphicsUnit.Millimeter);
                }
            }
            doc.Save(mstream);

            PdfDocument doc2 = PdfReader.Open(mstream, PdfDocumentOpenMode.Modify);
            for (int i = 0; i < doc2.Pages.Count; i++)
            {
                Task<Exception> t = new Task<Exception>(() =>
                {
                    try
                    {
                        doc2.Pages[i].Rotate = (doc2.Pages[i].Rotate + Pages[i].Orientation * 90) % 360;
                        XGraphics gfx = XGraphics.FromPdfPage(doc2.Pages[i]);
                        SizeF s = Pages[i].Format.GetPixelSize();
                        float sX = (float)doc2.Pages[i].Width.Point / s.Width;
                        float sY = (float)doc2.Pages[i].Height.Point / s.Height;
                        gfx.ScaleTransform(sX, sY);
                        for (int j = 0; j < doc2.Pages[i].Rotate; j += 90)
                        {
                            double shift = (doc2.Pages[i].Rotate / 90 + j) % 2 == 0 ? s.Width : s.Height;
                            gfx.RotateAtTransform(-90, new XPoint(shift / 2, shift / 2));
                        }
                        var pdfRenderer = new Renderer.PdfRenderer(gfx);
                        Renderer.BaseRenderer r = pdfRenderer;
                        Bitmap _bmp = null;
                        Graphics _g = null;
                        if (dpi > 0)
                        {
                            int _w = (int)(Pages[i].Format.Width * dpi / Util.MmPerInch);
                            int _h = (int)(Pages[i].Format.Height * dpi / Util.MmPerInch);
                            _bmp = new Bitmap(_w, _h);
                            _g = Graphics.FromImage(_bmp);
                            _g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            _g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            _g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            _g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            _g.ScaleTransform(Util.PointToMm(dpi / Util.MmPerInch), Util.PointToMm(dpi / Util.MmPerInch));
                            _g.Clear(Color.FromArgb(0, 255, 255, 255));
                            r = new Renderer.GdiRenderer(_g);
                        }
                        r.RenderSpecial = false;
                        var pSize = Pages[i].Format.GetPixelSize();
                        if (Pages[i].OriginalPage == null && Pages[i].BackgroundImage != null)
                            r.DrawImage(Pages[i].BackgroundImage, new RectangleF(0, 0, pSize.Width, pSize.Height));
                        Pages[i].Draw(r, pdfRenderer);
                        _g?.Dispose();
                        if (_bmp != null)
                        {
                            var oRect = new RectangleF(0, 0, Util.MmToPoint(Pages[i].Format.Width), Util.MmToPoint(Pages[i].Format.Height));
                            var _img = new Renderer.Image(_bmp);
                            pdfRenderer.DrawImage(_img, oRect);
                            _img.Dispose();
                        }
                        _bmp?.Dispose();
                    }
                    catch(Exception e)
                    {
                        return e;
                    }
                    return null;
                });
                t.Start();
                var exception = await t;
                if (exception != null)
                    throw exception;
                pb.ProgressBar.Value++;
                if(pb.Cancel)
                {
                    mstream.Close();
                    return;
                }
            }
            doc2.Save(path);

            mstream.Close();
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            foreach (KPage page in Pages)
                page.Dispose();
        }

        public bool SaveDocument(string path, string comment = null)
        {
            try
            {
                Console.WriteLine("Saving file to ", path != null ? path : "temp");
                MessageLog log = new MessageLog(Program.MainLog);
                if (path == null)
                    log.Add(MessageType.MSG, "Saving file to temp dir");
                else
                    log.Add(MessageType.MSG, "Saving file to {0}", path);

                List<FileInfo> files = new List<FileInfo>();
                DirectoryInfo dir = TmpManager.GetTmpDir();
                if (dir.Exists)
                    dir.RemoveAll();
                dir.Create();
                FileInfo docFile = new FileInfo(dir + "\\document.kritzel");
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                settings.NewLineChars = "\n";
                files.Add(docFile);
                using (XmlWriter writer = XmlWriter.Create(docFile.FullName, settings))
                {
                    PdfDocument pdfDoc = new PdfDocument();
                    string dispPath = path ?? FilePath;
                    writer.WriteStartElement("Document");
                    writer.WriteElementString("Filename", !string.IsNullOrWhiteSpace(dispPath) ? new FileInfo(dispPath).Name : "Autosave");
                    if (comment != null) writer.WriteElementString("Comment", comment);
                    writer.WriteElementString("Date", DateTime.Now.ToShortDateString());
                    writer.WriteElementString("Time", DateTime.Now.ToShortTimeString());
                    writer.WriteElementString("DefaultFormat", DefaultFormat);
                    writer.WriteStartElement("Pages");
                    for (int i = 0; i < Pages.Count; i++)
                    {
                        writer.WriteStartElement("Page");
                        FileInfo pageFile = new FileInfo(dir + "\\Page" + i + ".xml");
                        string page = Pages[i].SaveToString();
                        File.WriteAllText(pageFile.FullName, page);
                        files.Add(pageFile);
                        writer.WriteAttributeString("src", pageFile.Name);
                        string pdf = "-1";
                        string imgRef = "";
                        if (Pages[i].OriginalPage != null)
                        {
                            string pdfName = "Page" + i + ".pdf";
                            pdf = "" + pdfDoc.Pages.Count;
                            pdfDoc.AddPage(Pages[i].OriginalPage);
                        }
                        if (Pages[i].OriginalPage == null && Pages[i].BackgroundImage != null)
                        {
                            FileInfo imgFile = new FileInfo(dir + "\\Page" + i + ".png");
                            Pages[i].BackgroundImage.GdiBitmap.Save(imgFile.FullName);
                            imgRef = imgFile.Name;
                            files.Add(imgFile);
                        }
                        writer.WriteAttributeString("pdf", pdf);
                        writer.WriteAttributeString("img", imgRef);
                        writer.WriteEndElement(); // Page
                    }
                    writer.WriteEndElement(); // Pages
                    writer.WriteEndElement(); // Document
                    writer.Close();

                    if (pdfDoc.Pages.Count > 0)
                    {
                        files.Add(new FileInfo(dir + "\\.pdf"));
                        pdfDoc.Save(dir + "\\.pdf");
                    }

                    if (path != null)
                    {
                        ZipFile zFile = new ZipFile();
                        foreach (FileInfo file in files)
                            zFile.AddFile(file.FullName, "");
                        zFile.Save(path);
                        SetCurrentStateAsSaved();
                    }
                }
                if (path != null) this.FilePath = path;
            }
            catch(Exception e)
            {
                Program.MainLog.Add(e);
                Dialogues.MsgBox.ShowOk(Language.GetText("Error.saveFile") + "\n" + e.Message);
                return false;
            }
            return true;
        }

        public bool LoadDocument(string path, MessageLog log)
        {
            log?.Add(MessageType.MSG, "Loading File '{0}'", path);
            DirectoryInfo dir = TmpManager.GetTmpDir();
            if (path != null)
            {
                ZipFile zip = new ZipFile(path);
                if (dir.Exists) dir.RemoveAll();
                try
                {
                    zip.ExtractAll(dir.FullName);
                }
                catch(BadPasswordException)
                {
                    Dialogues.PasswordDialog dialog = new Dialogues.PasswordDialog();
                    var res = dialog.ShowDialog(out string pw, delegate (string _password)
                    {
                        try
                        {
                            zip.Password = _password;
                            zip.ExtractAll(dir.FullName);
                            return true;
                        }
                        catch(BadPasswordException)
                        {
                            return false;
                        }
                    });
                    if(res == DialogResult.Cancel)
                    {
                        return false;
                    }
                }
            }
            FileInfo docFile = new FileInfo(dir + "\\document.kritzel");
            XmlReader reader = XmlReader.Create(docFile.FullName);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlNode root = doc.LastChild;
            XmlNode pagesNode = root.GetNode("Pages");
            //Bitmap[] bgrs = new Bitmap[0];
            PdfDocument pdfDoc = null;
            bool loadPdf = false;
            if(File.Exists(dir + "\\.pdf"))
            {
                //bgrs = MupdfSharp.PageRenderer.Render(dir + "\\.pdf", Dialogues.PDFImporter.PAGETHEIGHTPIXEL);
                pdfDoc = PdfSharp.Pdf.IO.PdfReader.Open(dir + "\\.pdf", PdfDocumentOpenMode.Modify | PdfDocumentOpenMode.Import);
                loadPdf = true;
            }
            int ind = 1;
            foreach(XmlNode xmlPage in pagesNode.GetNodes("Page"))
            {
                string src = xmlPage.Attributes.GetNamedItem("src").InnerText;
                string text = File.ReadAllText(dir.FullName + "\\" + src);
                KPage page = new KPage(this);
                log?.Add(MessageType.MSG, "Loading Page {0}", ind++);
                page.LoadFromString(text, log);
                int pdfInd;
                int.TryParse(xmlPage.Attributes["pdf"].Value, out pdfInd);
                if(!string.IsNullOrWhiteSpace(xmlPage.Attributes["img"]?.Value))
                {
                    try
                    {
                        string filename = dir + "\\" + xmlPage.Attributes["img"].Value;
                        Bitmap tmpBmp = new Bitmap(filename);
                        Bitmap bmp = new Bitmap(tmpBmp.Width, tmpBmp.Height);
                        using (Graphics g = Graphics.FromImage(bmp))
                            g.DrawImage(tmpBmp, new PointF(0, 0));
                        tmpBmp.Dispose();
                        var image = new Renderer.Image(bmp);
                        page.BackgroundImage = image;
                    }
                    catch { }
                }
                if (pdfInd >= 0 && loadPdf)
                {
                    //page.BackgroundImage = new Renderer.Image(bgrs[pdfInd]);
                    page.OriginalPage = pdfDoc.Pages[pdfInd];
                    page.PdfRenderPath = TmpManager.NewFilename(TmpManager.GetTmpDir() + "\\render", "page", ".png");
                }
                Pages.Add(page);
            }

            var defForm = doc.GetElementsByTagName("DefaultFormat");
            if (defForm.Count > 0)
                DefaultFormat = defForm[0].InnerText;

            if (loadPdf)
            {
                pdfDoc.Close();
                pdfDoc.Dispose();
            }

            this.FilePath = path;
            SetCurrentStateAsSaved();
            return true;
        }

        public void RemovePage(KPage page)
        {
            Pages.Remove(page);
            page.Dispose();
        }

        public bool IsSaved()
        {
            if (SavedVersions == null || SavedVersions.Count != Pages.Count) return false;
            for(int i = 0; i < Pages.Count; i++)
            {
                if (SavedVersions[i] != Pages[i].Version) return false;
            }
            return true;
        }

        public bool IsSaved(List<uint> list)
        {
            if (list == null || list.Count != Pages.Count) return false;
            for (int i = 0; i < Pages.Count; i++)
            {
                if (list[i] != Pages[i].Version) return false;
            }
            return true;
        }

        public void SetCurrentStateAsSaved()
        {
            SavedVersions = new List<uint>();
            foreach (KPage page in Pages)
                SavedVersions.Add(page.Version);
        }

        public void SetCurrentStateAsSaved(ref List<uint> list)
        {
            list = new List<uint>();
            foreach (KPage page in Pages)
                list.Add(page.Version);
        }
    }
}
