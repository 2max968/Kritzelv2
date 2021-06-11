using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class LayoutEditor : Form
    {
        public static int PSIZE
        {
            get
            {
                return Util.GetGUISize();
            }
        }
        KPage page;
        InkControl inkControl;

        public LayoutEditor(KPage page, InkControl inkControl = null)
        {
            InitializeComponent();
            this.page = page;
            this.inkControl = inkControl;
            //backgroundSelectPanel1.ItemClicked += BackgroundSelectPanel1_ItemClicked;
            label1.Text = (page.Border / 10f) + "cm";
            this.StartPosition = FormStartPosition.CenterParent;
            cbShowDate.Checked = page.ShowDate;
            cbInvertColor.Checked = page.Filter != ColorFilter.Normal;
            lvBackgrounds.SelectedIndexChanged += LvBackgrounds_SelectedIndexChanged;
            createBackgroundList(page.Background != null ? page.Background.GetType() : null);

            this.BackColor = Style.Default.MenuBackground;
            this.ForeColor = Style.Default.MenuForeground;
            foreach(Control control in Controls)
            {
                if (control is Button) control.BackColor = Style.Default.MenuContrast;
            }

            Translator.Translate(this);
        }

        private void LvBackgrounds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvBackgrounds.FocusedItem == null) return;
            Type bgrType = (Type)lvBackgrounds.FocusedItem.Tag;
            Backgrounds.Background bgr = (Backgrounds.Background)bgrType.GetConstructor(new Type[0])
                .Invoke(new object[0]);
            page.Background = bgr;
            inkControl?.RefreshPage();
        }

        private void BackgroundSelectPanel1_ItemClicked(GUIElements.BackgroundSelectPanel sender, Type bgrType)
        {
            Backgrounds.Background bgr = (Backgrounds.Background)bgrType.GetConstructor(new Type[0])
                .Invoke(new object[0]);
            page.Background = bgr;
            inkControl?.RefreshPage();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (page.Border < 30)
                page.Border += 5f;
            label1.Text = (page.Border / 10f) + "cm";
            inkControl?.RefreshPage();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (page.Border > 0)
                page.Border -= 5f;
            label1.Text = (page.Border / 10f) + "cm";
            inkControl?.RefreshPage();
        }

        void bColor()
        {
            Color c1 = page.BackgroundColor1, c2 = page.BackgroundColor2;
            Bitmap bmp1 = new Bitmap(16, 16), bmp2 = new Bitmap(16, 16);
            Graphics g1 = Graphics.FromImage(bmp1), g2 = Graphics.FromImage(bmp2);
            g1.Clear(c1);
            g2.Clear(c2);
            btnC1.Image = bmp1;
            btnC2.Image = bmp2;
        }

        private void btnC1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = page.BackgroundColor1;
            if(cd.ShowDialog() == DialogResult.OK)
            {
                page.BackgroundColor1 = cd.Color;
            }
            bColor();
            inkControl?.RefreshPage();
        }

        private void btnC2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = page.BackgroundColor2;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                page.BackgroundColor2 = cd.Color;
            }
            bColor();
            inkControl?.RefreshPage();
        }

        private void cbShowDate_CheckedChanged(object sender, EventArgs e)
        {
            page.ShowDate = cbShowDate.Checked;
            inkControl?.RefreshPage();
        }

        void createBackgroundList(Type current)
        {
            Assembly asm = Assembly.GetCallingAssembly();
            List<Type> bgrTypes = new List<Type>();
            List<string> names = new List<string>();
            List<Bitmap> icons = new List<Bitmap>();

            bgrTypes.Add(typeof(Backgrounds.BackgroundNull));
            names.Add("None");
            icons.Add(new Bitmap(PSIZE, PSIZE));
            float iconFactor = 1.5f;

            foreach (Type t in asm.GetTypes())
            {
                if (t.IsSubclassOf(typeof(Backgrounds.Background))
                    && t.GetCustomAttribute<Backgrounds.BName>() != null)
                {
                    bgrTypes.Add(t);
                    names.Add(t.GetCustomAttribute<Backgrounds.BName>().Name);
                    PageFormat format =
                        new PageFormat(Util.PointToMm(PSIZE) * iconFactor, Util.PointToMm(PSIZE) * iconFactor);
                    Backgrounds.Background bgr =
                        (Backgrounds.Background)t.GetConstructor(new Type[0]).Invoke(new object[0]);
                    Bitmap bmp = new Bitmap(PSIZE, PSIZE);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.ScaleTransform(1 / iconFactor, 1 / iconFactor);
                        bgr.Draw(g.GetRenderer(), format,
                            2, Color.LightGray, Color.Red);
                    }
                    icons.Add(bmp);
                }
            }

            iconlist.ImageSize = new Size(PSIZE, PSIZE);
            iconlist.Images.Clear();
            lvBackgrounds.Items.Clear();
            for(int i = 0; i < names.Count; i++)
            {
                iconlist.Images.Add(icons[i]);
                ListViewItem itm = new ListViewItem(names[i], i);
                itm.Tag = bgrTypes[i];
                lvBackgrounds.Items.Add(itm);
                if (current == bgrTypes[i])
                    lvBackgrounds.FocusedItem = itm;
            }
        }

        private void cbInvertColor_CheckedChanged(object sender, EventArgs e)
        {
            page.Filter = cbInvertColor.Checked ? ColorFilter.InvertLuminosity : ColorFilter.Normal;
            inkControl.RefreshPage();
        }
    }
}
