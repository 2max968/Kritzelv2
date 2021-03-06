﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MupdfSharp;
using PdfSharp.Pdf;
using pdf = PdfSharp.Pdf;
using pdf_io = PdfSharp.Pdf.IO;

namespace Kritzel.Main.Dialogues
{
    public partial class PDFImporter : Form
    {
        public const int PAGETHEIGHTPIXEL = 3450;
        string path;
        public KPage[] Pages { get; private set; }

        public PDFImporter(string path)
        {
            InitializeComponent();
            this.path = path;
            loadPages(path);
        }

        private async void loadPages(string path)
        {
            using (PageRenderer pr = new PageRenderer(path))
            {
                pbProgress.Maximum = pr.PageCount;
                for (int i = 0; i < pr.PageCount; i++)
                {
                    ListViewItem itm = new ListViewItem("Page " + (i + 1));
                    itm.ImageIndex = i;
                    itm.Checked = true;
                    lvPages.Items.Add(itm);
                }
                for(int i = 0; i < pr.PageCount; i++)
                {
                    Task<Bitmap> t = new Task<Bitmap>(() => pr.RenderPage(i, imgPages.ImageSize.Height));
                    t.Start();
                    imgPages.Images.Add(await t);
                    pbProgress.Value++;
                    lblProgress.Text = $"{i + 1}/{pr.PageCount}";
                }
                pnProgress.Hide();
                btnCancel.Enabled = true;
                btnOk.Enabled = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem itm in lvPages.Items)
                itm.Checked = true;
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem itm in lvPages.Items)
                itm.Checked = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            List<int> pageIndexes = new List<int>();
            for (int i = 0; i < lvPages.Items.Count; i++)
            {
                if (lvPages.Items[i].Checked)
                {
                    pageIndexes.Add(i);
                }
            }
            /*Bitmap[] bmps = MupdfSharp.PageRenderer.Render(path, PAGETHEIGHTPIXEL, pageIndexes.ToArray());
            for(int i = 0; i < bmps.Length; i++)
            {
                Bitmap tmp = bmps[i];
                bmps[i] = tmp.MakeBackgroundTransparent(Color.White);
                tmp.Dispose();
            }*/

            if (!Directory.Exists(TmpManager.GetTmpDir() + "\\render"))
                Directory.CreateDirectory(TmpManager.GetTmpDir() + "\\render");

            Pages = new KPage[pageIndexes.Count];
            pdf.PdfDocument pdfdoc = pdf_io.PdfReader.Open(path, pdf_io.PdfDocumentOpenMode.Modify | pdf_io.PdfDocumentOpenMode.Import);
            
            for (int i = 0; i < pageIndexes.Count; i++)
            {
                int p = pageIndexes[i];
                KPage page = new KPage(KDocument.EmptyDocument);
                pdf.PdfPage pPage = pdfdoc.Pages[p];
                float w = (float)pPage.Width.Millimeter;
                float h = (float)pPage.Height.Millimeter;
                if(pPage.Rotate == 90 || pPage.Rotate == 270)
                    Util.Swap(ref w, ref h);
                page.Format = new PageFormat(w, h);
                page.Background = null;
                page.ShowDate = false;
                page.OriginalPage = pPage;
                /*page.BackgroundImage 
                    = new Renderer.Image(bmps[i]);*/

                page.PdfRenderPath = TmpManager.NewFilename(TmpManager.GetTmpDir() + "\\render", "page", ".png");
                Pages[i] = page;
            }
            pdfdoc.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
