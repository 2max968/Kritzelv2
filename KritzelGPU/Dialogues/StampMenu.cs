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
    public partial class StampMenu : Form
    {
        class LineCollection
        {
            public List<Line> Lines;

            public LineCollection(IEnumerable<Line> lines)
            {
                Lines = new List<Line>();
                Lines.AddRange(lines);
            }
        }

        public static StampMenu Instance { get; private set; } = null;

        InkControl parent;

        public StampMenu(InkControl parent)
        {
            InitializeComponent();
            this.parent = parent;

            if (Instance != null) Instance.Close();
            Instance = this;

            using (Bitmap ico = ResManager.LoadIcon("tools/stamp.svg", 32))
            {
                Icon = Icon.FromHandle(ico.GetHicon());
            }

            this.FormClosing += StampMenu_FormClosing;
            this.Shown += StampMenu_Shown;
            TextBoxInput.SetParent(Handle, parent.Handle);

            int icoSize = Math.Max(Util.GetGUISize() * 2, 256);
            imgStampThumbs.ImageSize = new Size(icoSize, icoSize);
        }

        private void StampMenu_Shown(object sender, EventArgs e)
        {
            Instance = this;
        }

        private void StampMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Instance = null;
        }

        private void btnStamp_Click(object sender, EventArgs e)
        {
            var lines = parent.Page.GetSelectedLines();
            List<Line> clones = new List<Line>();
            var bounds = Util.GetFullBounds(lines);
            PointF center = new PointF((bounds.Left + bounds.Right) / 2, (bounds.Top + bounds.Bottom) / 2);
            Matrix3x3 trans = Matrix3x3.Translation(-center.X, -center.Y);
            foreach (Line line in lines)
            {
                Line clone = line.Clone();
                clone.Transform(trans);
                clones.Add(clone);
            }

            // Create Thumbnail
            Size thumbSize = imgStampThumbs.ImageSize;
            Bitmap thumbnail = new Bitmap(thumbSize.Width, thumbSize.Height);
            using (Graphics g = Graphics.FromImage(thumbnail))
            {
                float scale = Math.Min(thumbSize.Width / bounds.Width, thumbSize.Height / bounds.Height);
                var r = g.GetRenderer();
                g.TranslateTransform(thumbSize.Width / 2, thumbSize.Height / 2);
                g.ScaleTransform(scale, scale);
                foreach(Line l in clones)
                {
                    l.Render(r);
                }
            }

            imgStampThumbs.Images.Add(thumbnail);
            ListViewItem itm = new ListViewItem("text");
            itm.ImageIndex = imgStampThumbs.Images.Count - 1;
            itm.Tag = new LineCollection(clones);
            lvStamps.Items.Add(itm);
        }

        private void lvStamps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvStamps.FocusedItem != null && lvStamps.FocusedItem.Tag is LineCollection)
            {
                LineCollection lines = (LineCollection)lvStamps.FocusedItem.Tag;
                parent.InkMode = InkMode.Stamp;
                parent.Stamp = lines.Lines;
            }
        }
    }
}
