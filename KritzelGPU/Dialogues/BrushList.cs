using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class BrushList : Form
    {
        static Bitmap eraserWhite = null;
        static Bitmap eraserAll = null;

        InkControl control;

        public BrushList(InkControl control)
        {
            InitializeComponent();
            this.control = control;

            if(eraserWhite == null)
                eraserWhite = ResManager.LoadIcon("eraserWhite.svg", Util.GetGUISize());
            if (eraserAll == null)
                eraserAll = ResManager.LoadIcon("eraserAll.svg", Util.GetGUISize());

            var colors = control.Page.GetListOfBrushes();
            imageList1.ImageSize = new Size(Util.GetGUISize(), Util.GetGUISize());
            lvBrushes.Items.Add("All", 0);
            imageList1.Images.Add(eraserAll);
            foreach(Color c in colors)
            {
                ListViewItem itm = new ListViewItem(ColorTranslator.ToHtml(c));
                itm.ImageIndex = imageList1.Images.Count;
                itm.Tag = c;
                Bitmap icon = CreateIcon(c);
                imageList1.Images.Add(icon);
                lvBrushes.Items.Add(itm);
            }

            Icon = Program.WindowIcon;
        }

        public static Bitmap CreateIcon(Color c)
        {
            if (eraserWhite == null)
                eraserWhite = ResManager.LoadIcon("eraserWhite.svg", Util.GetGUISize());
            int btnSize = Util.GetGUISize();
            Bitmap bmp = new Bitmap(btnSize, btnSize);
            Graphics g = Graphics.FromImage(bmp);
            ImageAttributes ia = new ImageAttributes();
            var cm = new ColorMatrix(new float[][]
            {
              new float[] {c.R / 255f, 0, 0, 0, 0},
              new float[] {0, c.G / 255f, 0, 0, 0},
              new float[] {0, 0, c.B / 255f, 0, 0},
              new float[] {0, 0, 0, 1, 0},
              new float[] {0, 0, 0, 0, 1}
            });
            ia.SetColorMatrix(cm);
            g.DrawImage(eraserWhite, new Rectangle(0, 0, btnSize, btnSize),
                0, 0, btnSize, btnSize, GraphicsUnit.Pixel, ia);
            ia.Dispose();
            g.Dispose();
            return bmp;
        }

        private void lvBrushes_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem itm = lvBrushes.FocusedItem;
            if(itm.Tag != null && itm.Tag is Color)
            {
                Color c = (Color)itm.Tag;
                control.EraserColor = c;
            }
            else
            {
                control.EraserColor = Color.Transparent;
            }
            this.Close();
        }
    }
}
