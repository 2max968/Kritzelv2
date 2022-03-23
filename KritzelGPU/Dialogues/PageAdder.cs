using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kritzel.Main.GUIElements;

namespace Kritzel.Main.Dialogues
{
    public partial class PageAdder : UserControl, IClosable
    {
        public event CloseDelegate Close;
        InkControl control;
        KDocument document;
        Dictionary<string, PageFormat> formats;
        List<KPage> pages = null;
        private GUIElements.ItemSelector isPosition;

        public PageAdder(InkControl control, KDocument document)
        {
            InitializeComponent();

            isPosition = new ItemSelector();
            isPosition.Dock = DockStyle.Top;
            Controls.Add(isPosition);
            isPosition.SendToBack();

            this.control = control;
            this.document = document;
            this.Width = 5 * Util.GetGUISize();
            this.Dock = DockStyle.Left;

            comboBox1.Height = Util.GetGUISize();
            comboBox1.Font = new Font(comboBox1.Font.FontFamily, Util.GetGUISize() / 2, GraphicsUnit.Pixel);
            panelBottom.Height = Util.GetGUISize();
            btnAdd.Width = 3 * Util.GetGUISize();
            isPosition.Height = 3 * Util.GetGUISize();
            this.ClientSize = new Size(8 * Util.GetGUISize(), 6 * Util.GetGUISize());
            var padding = this.Padding;
            padding.Top = Util.GetGUISize() / 2;
            this.Padding = padding;

            this.BackColor = Style.Default.MenuBackground;
            panelBottom.BackColor = Style.Default.MenuContrast;
            this.ForeColor = Style.Default.MenuForeground;
            comboBox1.BackColor = Style.Default.MenuContrast;
            comboBox1.ForeColor = Style.Default.MenuForeground;
            isPosition.ForeColor = Style.Default.MenuForeground;
            lblAddPDF.LinkColor = Style.Default.Selection;
            lblAddImage.LinkColor = Style.Default.Selection;

            formats = PageFormat.GetFormats();
            foreach (var f in formats)
            {
                comboBox1.Items.Add(f.Key);
            }

            if (formats.ContainsKey(document.DefaultFormat))
                comboBox1.Text = document.DefaultFormat;
            else
                comboBox1.Text = comboBox1.Items[0].ToString();

            Dialogues.Translator.Translate(this);
            isPosition.Items = new string[] {
                Language.GetText("Newpage.beforeCurrent"),
                Language.GetText("Newpage.afterCurrent"),
                Language.GetText("Newpage.atEnd")
            };
            isPosition.SelectedIndex = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public int GetInsertIndex()
        {
            int pindex = document.Pages.IndexOf(control.Page);
            int index = pindex + 1;
            if (isPosition.SelectedIndex == 0)
                index = pindex;
            else if (isPosition.SelectedIndex == 2)
                index = document.Pages.Count;
            return index;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int index = GetInsertIndex();

            if (pages == null)
            {
                KPage page = new KPage(document);
                page.Format = formats[comboBox1.Text];
                document.Pages.Insert(index, page);
                control.LoadPage(page);
                Close?.Invoke();
            }
            else
            {
                document.Pages.InsertRange(index, pages);
                foreach (KPage page in pages)
                    page.ChangeDocument(document);
                Close?.Invoke();
            }
        }

        public void AddCloseHandler(CloseDelegate handler)
        {
            Close += handler;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF Document|*.pdf";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                PDFImporter imp = new PDFImporter(ofd.FileName);
                if(imp.ShowDialog() == DialogResult.OK)
                {
                    pages = new List<KPage>();
                    pages.AddRange(imp.Pages);
                    Icon ico = Icon.ExtractAssociatedIcon(ofd.FileName);
                    showImportPageInfo(ico, "" + imp.Pages.Length + " pages from", new FileInfo(ofd.FileName).Name);
                }
            }
        }

        private void lblAddImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.png;*.bmp;*.jpg;*.jpeg";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                ImageImporter ii = new ImageImporter(new Bitmap(ofd.FileName));
                if(ii.ShowDialog() == DialogResult.OK)
                {
                    KPage page = new KPage(document);
                    page.Format = ii.Format;
                    page.ShowDate = false;
                    page.Background = null;
                    page.BackgroundImage = new Renderer.Image(ii.EditetImage);
                    pages = new List<KPage>();
                    pages.Add(page);

                    Icon ico = Icon.ExtractAssociatedIcon(ofd.FileName);
                    showImportPageInfo(ico, new FileInfo(ofd.FileName).Name, "");
                }
            }
        }

        void showImportPageInfo(Icon ico, string txt1, string txt2)
        {
            pnPDF.Visible = true;
            pnPDF.Height = Util.GetGUISize();
            pbPDFIcon.Width = Util.GetGUISize();
            pbPDFIcon.Image = ico.ToBitmap();
            ico.Dispose();
            lblPDF1.Text = txt1;
            lblPDF2.Text = txt2;
            lblPDF2.Font = new Font(lblPDF1.Font, FontStyle.Bold);
            comboBox1.Visible = false;
            lblAddPDF.Visible = false;
            lblAddImage.Visible = false;
        }
    }
}
