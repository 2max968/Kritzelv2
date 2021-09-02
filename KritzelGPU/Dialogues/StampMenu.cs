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
using System.Xml;

namespace Kritzel.Main.Dialogues
{
    public partial class StampMenu : Form
    {
        public static string FILENAME
        {
            get
            {
                return $"{ResManager.CONFDIR}stamps.{Util.GetUsername()}.xml";
            }
        }

        List<LineCollection> stamps = new List<LineCollection>();

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
        MainWindow parentWindow;

        public StampMenu(InkControl parent, MainWindow parentWindow)
        {
            InitializeComponent();
            Translator.Translate(this);
            Translator.Translate(ctxStamp);
            Text = Language.GetText("Stamp.dialogTitle");
            this.parent = parent;
            this.parentWindow = parentWindow;

            if (Instance != null) Instance.Close();
            Instance = this;

            using (Bitmap ico = ResManager.LoadIcon("tools/stamp.svg", 32))
            {
                Icon = Icon.FromHandle(ico.GetHicon());
            }

            this.FormClosing += StampMenu_FormClosing;
            this.Shown += StampMenu_Shown;
            TextBoxInput.SetParent(Handle, parent.Handle);

            int icoSize = Math.Min(Util.GetGUISize() * 2, 256);
            imgStampThumbs.ImageSize = new Size(icoSize, icoSize);

            if(File.Exists(FILENAME))
            {
                using (XmlReader xml = XmlReader.Create(FILENAME))
                {
                    while(!xml.EOF)
                    {
                        var lines = Util.GetLines(xml, "Lines");
                        if(lines.Count > 0)
                            stamps.Add(new LineCollection(lines));
                    }
                }
            }

            foreach(LineCollection col in stamps)
            {
                Bitmap thumbnail = CreateThumbnail(col.Lines, imgStampThumbs.ImageSize);

                imgStampThumbs.Images.Add(thumbnail);
                ListViewItem itm = new ListViewItem("text");
                itm.ImageIndex = imgStampThumbs.Images.Count - 1;
                itm.Tag = col;
                lvStamps.Items.Add(itm);
            }
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
            Bitmap thumbnail = CreateThumbnail(clones, imgStampThumbs.ImageSize);

            imgStampThumbs.Images.Add(thumbnail);
            ListViewItem itm = new ListViewItem("text");
            itm.ImageIndex = imgStampThumbs.Images.Count - 1;
            itm.Tag = new LineCollection(clones);
            lvStamps.Items.Add(itm);

            stamps.Add(new LineCollection(clones));
            save();
        }

        public static Bitmap CreateThumbnail(List<Line> lines, Size thumbSize)
        {
            var bounds = Util.GetFullBounds(lines);
            Bitmap thumbnail = new Bitmap(thumbSize.Width, thumbSize.Height);
            using (Graphics g = Graphics.FromImage(thumbnail))
            {
                float scale = Math.Min(thumbSize.Width / bounds.Width, thumbSize.Height / bounds.Height);
                var r = g.GetRenderer();
                g.TranslateTransform(thumbSize.Width / 2, thumbSize.Height / 2);
                g.ScaleTransform(scale, scale);
                foreach (Line l in lines)
                {
                    l.Render(r);
                }
            }
            return thumbnail;
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

        void save()
        {
            using (XmlWriter xml = XmlWriter.Create(FILENAME))
            {
                xml.WriteStartElement("xml");
                foreach (LineCollection col in stamps)
                {
                    Util.SaveLines(xml, col.Lines);
                }
                xml.WriteEndElement();
            }
        }

        private void stampdeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(lvStamps.FocusedItem != null)
            {
                int ind = lvStamps.FocusedItem.Index;
                lvStamps.Items.RemoveAt(ind);
                stamps.RemoveAt(ind);
                save();
            }
        }

        private void stampaddQuickAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvStamps.FocusedItem != null && lvStamps.FocusedItem.Tag is LineCollection)
            {
                LineCollection lines = (LineCollection)lvStamps.FocusedItem.Tag;
                parentWindow.AddStampQuickAccess(lines.Lines);
            }
        }
    }
}
