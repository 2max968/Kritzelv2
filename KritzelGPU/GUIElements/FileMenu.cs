using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Kritzel.Main.Dialogues;
using System.Drawing.Printing;

namespace Kritzel.Main.GUIElements
{
    public partial class FileMenu : UserControl, IClosable
    {
        public delegate void CloseMenuEvent();
        public event CloseMenuEvent CloseMenu;
        public event EventHandler<int> SettingsClosed;

        InkControl control;
        KDocument document;
        Form parent;
        MainWindow window;

        public FileMenu(InkControl control, KDocument document, Form parent, MainWindow window)
        {
            InitializeComponent();

            this.Font = new Font(this.Font.FontFamily, Util.GetFontSizePixel(), GraphicsUnit.Pixel);
            this.Width = 5 * Util.GetGUISize();
            this.control = control;
            this.document = document;
            this.parent = parent;
            this.window = window;
            this.BackColor = Style.Default.MenuBackground;
            this.ForeColor = Style.Default.MenuForeground;

            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    c.Height = Util.GetGUISize();
                    c.Text = Language.GetText(c.Text);
                }
            }

            btnClose.Image = ResManager.LoadIcon("hamburger.svg", Util.GetGUISize());
            /*btnNew.Image = ResManager.LoadIcon("new.svg", Util.GetGUISize());
            btnOpen.Image = ResManager.LoadIcon("open.svg", Util.GetGUISize());
            btnSave.Image = ResManager.LoadIcon("null", Util.GetGUISize());
            btnSaveAs.Image = ResManager.LoadIcon("save.svg", Util.GetGUISize());
            btnAddPage.Image = ResManager.LoadIcon("addPage.svg", Util.GetGUISize());*/

            btnSave.Enabled = document.FilePath != "";

            bool casting = HTTPCast.IsCasting | WebCast.IsCasting;
            if (casting)
                btnCast.Text = Language.GetText("File.stopCast");
            btnSave.Enabled = !document.IsSaved();

            if(!Configuration.ShowBrokenFunctions)
            {
                btnPrint.Hide();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseMenu?.Invoke();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if(Util.AskForSave(document))
            {
                /*KDocument doc = new KDocument();
                doc.Pages.Add(new KPage());
                doc.SetCurrentStateAsSaved();
                window.SetDocument(doc);
                CloseMenu?.Invoke();*/
                NewDocumentDialog pa = new NewDocumentDialog(control, document, window);
                pa.Width = 5 * Util.GetGUISize();
                pa.Dock = DockStyle.Left;
                this.Parent.Controls.Add(pa);
                pa.BringToFront();
                pa.Show();
                this.Hide();
                pa.Close += new CloseDelegate(delegate ()
                {
                    CloseMenu?.Invoke();
                });
                this.parent.Refresh();
            }
        }

        private async void btnOpen_Click(object sender, EventArgs e)
        {
            if (!Util.AskForSave(document)) return;
            CloseMenu?.Invoke();
            await Task.Delay(0);
            OpenFileDialog diagOpenDoc = new OpenFileDialog();
            //diagOpenDoc.Filter = "Supportet Files|*.krit;*.pdf;*.jpg;*.jpeg;*.png;*.bmp|Kritzel Documents|*.zip|PDF Files|*.pdf|Images|*.jpg;*.jpeg;*.bmp;*.png";
            diagOpenDoc.Filter = "Supportet Files|*.krit;*.pdf|Kritzel Documents|*.krit|PDF Files|*.pdf";
            if (diagOpenDoc.ShowDialog(window) == DialogResult.OK)
            {
                FileInfo info = new FileInfo(diagOpenDoc.FileName);
                string ext = info.Extension.ToLower();
                if (ext == ".krit")
                {
                    KDocument doc = new KDocument();
                    MessageLog log = new MessageLog();
                    if (doc.LoadDocument(diagOpenDoc.FileName, log))
                    {
                        Console.WriteLine(log);
                        document = doc;
                        window.SetDocument(document);
                    }
                }
                else if (ext == ".pdf")
                {
                    PDFImporter imp = new PDFImporter(diagOpenDoc.FileName);
                    if (imp.ShowDialog(window) == DialogResult.OK)
                    {
                        KDocument doc = new KDocument();
                        doc.Pages.AddRange(imp.Pages);
                        foreach (KPage page in imp.Pages)
                            page.ChangeDocument(doc);
                        window.SetDocument(doc);
                        //document.Pages.AddRange(imp.Pages);
                    }
                }
                else
                {
                    MessageBox.Show("Error importing File");
                }
            }
        }

        private async void btnSaveAs_Click(object sender, EventArgs e)
        {
            await SaveAs();
        }

        public async Task SaveAs()
        {
            CloseMenu?.Invoke();
            await Task.Delay(0);
            try
            {
                SaveFileDialog diagSaveDoc = new SaveFileDialog();
                diagSaveDoc.Filter = "Kritzel Documents|*.krit|PDF Files|*.pdf|JPEG (Current Page)|*.jpg|Windows Bitmap (Current Page)|*.bmp|Portable Network Graphic (Current Page)|*.png";
                if (diagSaveDoc.ShowDialog(window) == DialogResult.OK)
                {
                    FileInfo info = new FileInfo(diagSaveDoc.FileName);
                    string ext = info.Extension.ToLower();
                    if (ext == ".krit")
                    {
                        document.SaveDocument(diagSaveDoc.FileName);
                    }
                    else if (ext == ".pdf")
                    {
                        PDFExportDialog ped = new PDFExportDialog();
                        if(ped.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        ProgressWindow wnd = new ProgressWindow("Save to PDF");
                        wnd.TopMost = true;
                        wnd.Show();
                        try
                        {
                            document.SavePDF(diagSaveDoc.FileName, wnd.ProgressBar, ped.Dpi);
                            //inkControl1.Page.SavePDF(sfd.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while saving File:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        wnd.Close();
                    }
                    else if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".png")
                    {
                        float factor = 5;
                        KPage page = control.Page;
                        SizeF size = page.Format.GetPixelSize();
                        Bitmap bmp = new Bitmap((int)(size.Width * factor), (int)(size.Height * factor));
                        Graphics g = Graphics.FromImage(bmp);
                        g.Clear(Color.White);
                        Renderer.GdiRenderer r = g.GetRenderer();
                        g.ScaleTransform(factor, factor);
                        page.DrawPDFHQ(r, bmp.Height);
                        page.Draw(r);
                        bmp.Save(diagSaveDoc.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Error export File");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPage_Click(object sender, EventArgs e)
        {
            Dialogues.PageAdder pa = new Dialogues.PageAdder(control, document);
            //pa.Location = new Point((Parent.Width - pa.Width) / 2, (Parent.Height - pa.Height) / 2);
            pa.Width = 5 * Util.GetGUISize();
            pa.Dock = DockStyle.Left;
            this.Parent.Controls.Add(pa);
            pa.BringToFront();
            pa.Show();
            this.Hide();
            pa.Close += new CloseDelegate(delegate ()
            {
                CloseMenu?.Invoke();
            });
            this.parent.Refresh();
        }

        public void AddCloseHandler(CloseDelegate handler)
        {
            this.CloseMenu += new CloseMenuEvent(handler);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (document.FilePath == "") await SaveAs();
                else
                {
                    document.SaveDocument(document.FilePath);
                    CloseMenu?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSettings_Click(object sender, EventArgs e)
        {
            CloseMenu?.Invoke();
            await Task.Delay(0);
            Dialogues.Settings.SettingsWindow settings = new Dialogues.Settings.SettingsWindow();
            settings.ShowDialog();
            SettingsClosed?.Invoke(this, 0);
            CloseMenu?.Invoke();
        }

        private void btnCast_Click(object sender, EventArgs e)
        {
            if(HTTPCast.IsCasting)
            {
                HTTPCast.StopCasting();
            }
            else if(WebCast.IsCasting)
            {
                WebCast.StopCasting();
            }
            else
            {
                CastDialog cd = new CastDialog(control);
                cd.ShowDialog();
            }
            CloseMenu?.Invoke();
        }

        private async void btnPrint_Click(object sender, EventArgs e)
        {
            CloseMenu?.Invoke();
            await Task.Delay(0);
            PrintDialog pd = new PrintDialog();
            pd.AllowCurrentPage = true;
            pd.AllowSomePages = true;
            pd.Document = new PrintDocument();
            pd.UseEXDialog = true;
            pd.PrinterSettings.FromPage = 1;
            pd.PrinterSettings.ToPage = document.Pages.Count;
            pd.PrinterSettings.MaximumPage = document.Pages.Count;
            pd.PrinterSettings.MinimumPage = 1;
            int currentPage = 0;
            pd.Document.PrintPage += (_sender, _e) =>
            {
                if (pd.PrinterSettings.PrintRange == PrintRange.AllPages)
                {
                    Renderer.GdiRenderer r = new Renderer.GdiRenderer(_e.Graphics);
                    document.Pages[currentPage].DrawPDFHQ(r, (int)Util.MmToPoint(_e.PageBounds.Height));
                    document.Pages[currentPage++].Draw(r);
                    _e.HasMorePages = currentPage < document.Pages.Count;
                }
                else if (pd.PrinterSettings.PrintRange == PrintRange.CurrentPage)
                {
                    Renderer.GdiRenderer r = new Renderer.GdiRenderer(_e.Graphics);
                    control.Page.DrawPDFHQ(r, (int)Util.MmToPoint(_e.PageBounds.Height));
                    control.Page.Draw(r);
                    _e.HasMorePages = false;
                }
                else if (pd.PrinterSettings.PrintRange == PrintRange.SomePages)
                {
                    int fromPage = Math.Max(0, pd.PrinterSettings.FromPage - 1);
                    int toPage = Math.Min(document.Pages.Count - 1, pd.PrinterSettings.ToPage - 1);
                    Renderer.GdiRenderer r = new Renderer.GdiRenderer(_e.Graphics);
                    int realPage = currentPage++ + fromPage;
                    document.Pages[realPage].DrawPDFHQ(r, (int)Util.MmToPoint(_e.PageBounds.Height));
                    document.Pages[realPage].Draw(r);
                    _e.HasMorePages = realPage < toPage;
                }
            };
            if(pd.ShowDialog() == DialogResult.OK)
            {
                pd.Document.Print();
            }
        }
    }
}
