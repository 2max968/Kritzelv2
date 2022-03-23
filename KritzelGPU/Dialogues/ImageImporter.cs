using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class ImageImporter : Form
    {
        public PageFormat Format { get; private set; }
        public Bitmap EditetImage { get; private set; }
        Bitmap original;
        public PdfPage Page;

        public ImageImporter(Bitmap bmp)
        {
            InitializeComponent();
            original = bmp;
            this.DialogResult = DialogResult.OK;

            foreach(var format in PageFormat.GetFormats())
            {
                cbFormats.Items.Add(format.Key);
            }

            var stdformat = "A4";
            if (bmp.Height < bmp.Width) stdformat = "A4 Landscape";
            cbFormats.Text = stdformat;
            render(stdformat);
        }

        void render(string selected)
        {
            PageFormat format = PageFormat.GetFormats()[selected];
            this.Format = format;
            SizeF s = format.GetPixelSize();
            Bitmap bmp = new Bitmap((int)s.Width, (int)s.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                SizeF sX = new SizeF(s.Width, original.Height / (float)original.Width * s.Width);
                SizeF sY = new SizeF(original.Width / (float)original.Height * s.Height, s.Width);
                SizeF sS = (sX.Width < sY.Width) ? sX : sY;
                g.DrawImage(original, (s.Width - sS.Width) / 2f, (s.Height - sS.Height) / 2f, sS.Width, sS.Height);
                EditetImage = bmp;
                pbPreview.Image = bmp;

            }
        }

        private void cbFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            render(cbFormats.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
