﻿using System;
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

            this.Font = new Font(this.Font.FontFamily, this.Font.Size * Util.GetScaleFactor());
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!Util.AskForSave(document)) return;
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
                    doc.LoadDocument(diagOpenDoc.FileName, log);
                    Console.WriteLine(log);
                    document = doc;
                    window.SetDocument(document);
                }
                else if (ext == ".pdf")
                {
                    Dialogues.PDFImporter imp = new Dialogues.PDFImporter(diagOpenDoc.FileName);
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
                /*else if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".png")
                {
                    Bitmap bmp = new Bitmap(diagOpenDoc.FileName);
                    Dialogues.ImageImporter ii = new Dialogues.ImageImporter(bmp);
                    if (ii.ShowDialog(window) == DialogResult.OK)
                    {
                        KPage p = new KPage();
                        p.Format = ii.Format;
                        p.BackgroundImage = new Renderer.Image(ii.EditetImage);
                        p.Background = null;
                        p.ShowDate = false;
                        document.Pages.Add(p);
                        control.LoadPage(p);
                    }
                }*/
                else
                {
                    MessageBox.Show("Error importing File");
                }
                CloseMenu?.Invoke();
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        public void SaveAs()
        { 
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
                        Dialogues.ProgressWindow wnd = new Dialogues.ProgressWindow("Save to PDF");
                        wnd.TopMost = true;
                        wnd.Show();
                        try
                        {
                            document.SavePDF(diagSaveDoc.FileName, wnd.ProgressBar);
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
            CloseMenu?.Invoke();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (document.FilePath == "") SaveAs();
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsDialog settings = new SettingsDialog();
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
    }
}
