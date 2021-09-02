using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace Kritzel.Main.GUIElements
{
    public partial class ColorPicker : UserControl
    {
        public delegate void SetColorEvent(Color c);
        public event SetColorEvent SetColor;

        List<Color> colors = new List<Color>();
        List<Button> buttons = new List<Button>();
        List<Bitmap> icons = new List<Bitmap>();
        int ocap = 0;
        int lsel = 0;
        Bitmap feltpen;
        Panel buttonContainer = null;
        bool enabled = true;
        Color? selectedColor = null;

        public ColorPicker()
        {
            InitializeComponent();
            if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                enabled = false;
                return;
            }
            this.Resize += new EventHandler(ColorPicker_Resize);
            btnAdd.BackgroundImage = ResManager.LoadIcon("add.svg", Util.GetGUISize());
            btnAdd.Text = "";
            btnExpand.Image = ResManager.LoadIcon("expand.svg", Util.GetGUISize());
            btnExpand.Text = "";
            feltpen = ResManager.LoadIcon("feltpen.svg", Util.GetGUISize());
            Style.StyleChanged += Style_StyleChanged;
            Style_StyleChanged(null, Style.Default);

            try
            {
                string[] colorStrings = Configuration.PenColors.Split(',');
                foreach (string colorString in colorStrings)
                    Add(ColorTranslator.FromHtml(colorString));
            }
            catch(Exception)
            {

            }
        }

        private void Style_StyleChanged(object sender, Style e)
        {
            if (!enabled) return;
            btnAdd.BackColor = btnExpand.BackColor = e.MenuBackground;
            btnAdd.ForeColor = btnExpand.ForeColor = e.MenuForeground;
            if(buttonContainer != null)
            {
                buttonContainer.ForeColor = e.MenuForeground;
                buttonContainer.BackColor = e.MenuBackground;
            }
            this.BackColor = e.MenuContrast;
            refresh(true);
        }

        private void ColorPicker_Resize(object o, EventArgs e)
        {
            if (!enabled) return;
            refresh(this.ParentForm != null && this.ParentForm.WindowState == FormWindowState.Maximized);
        }

        public void Add(Color color)
        {
            if (!enabled) return;
            Bitmap icon = createIcon(color);
            colors.Add(color);
            icons.Add(icon);
            refresh(true);
        }

        public void Remove(int index)
        {
            if (!enabled) return;
            if (index < 0 || index >= colors.Count || index >= icons.Count) return;
            colors.RemoveAt(index);
            Bitmap bmp = icons[index];
            icons.RemoveAt(index);
            bmp.Dispose();
        }

        public int GetCapacity()
        {
            int width = this.Width - 2 * Util.GetGUISize();
            return width / Util.GetGUISize();
        }

        void save()
        {
            List<string> colorString = new List<string>();
            foreach (Color c in colors)
                colorString.Add(ColorTranslator.ToHtml(c));
            Configuration.PenColors = string.Join(",", colorString);
            Configuration.SaveConfig();
        }

        public Bitmap createIcon(Color c)
        {
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
            g.DrawImage(feltpen, new Rectangle(0, 0, btnSize, btnSize),
                0, 0, btnSize, btnSize, GraphicsUnit.Pixel, ia);
            ia.Dispose();
            g.Dispose();
            return bmp;
        }

        void refresh(bool full = false)
        {
            if (!enabled) return;
            if (!full && GetCapacity() == ocap) return;
            ocap = GetCapacity();
            if (!full && ocap > colors.Count) return;
            
            buttonContainer?.Dispose();
            while(buttons.Count > 0)
            {
                if (!buttons[0].IsDisposed)
                {
                    buttons[0].Dispose();
                }
                buttons.RemoveAt(0);
            }

            buttonContainer = new Panel();
            buttonContainer.Dock = DockStyle.Left;
            buttonContainer.AutoSize = true;
            buttonContainer.BackColor = Style.Default.MenuBackground;
            buttonContainer.ForeColor = Style.Default.MenuForeground;
            int cap = GetCapacity();
            for(int i = 0; i < cap && i < colors.Count; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(Util.GetGUISize(), Util.GetGUISize());
                btn.Tag = colors[i];
                btn.Image = icons[i];
                btn.Text = "";
                buttons.Add(btn);
                btn.Dock = DockStyle.Left;
                btn.FlatStyle = FlatStyle.Flat;
                btn.MouseUp += Btn_MouseClick;
                btn.FlatAppearance.BorderSize = 0;
                if (colors[i] == selectedColor)
                    btn.BackColor = Style.Default.Selection;
                buttonContainer.Controls.Add(btn);
            }
            Controls.Add(buttonContainer);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].BringToFront();
            }
            btnAdd.BringToFront();

            btnExpand.Enabled = colors.Count > buttons.Count;
        }

        private void Btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = (Button)sender;
                if (e.Button == MouseButtons.Left)
                {
                    if (btn.Tag is Color)
                    {
                        Color c = (Color)btn.Tag;
                        foreach (Button b in buttons)
                        {
                            b.BackColor = Style.Default.MenuBackground;
                        }
                        btn.BackColor = Style.Default.Selection;
                        SetColor?.Invoke(c);
                        selectedColor = c;
                    }
                }
                else if(e.Button == MouseButtons.Right)
                {
                    lsel = buttons.IndexOf(btn);
                    penCtx.Show(Cursor.Position);
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int x = MainWindow.Instance.PointToClient(this.PointToScreen(btnAdd.Location)).X;
            ColorDialog _cd = new ColorDialog(x + btnAdd.Width / 2);
            MainWindow.Instance.OpenDialog(_cd, delegate(Control diag)
            {
                if(_cd.Result == DialogResult.OK)
                {
                    Add(_cd.SelectedColor);
                    save();
                }
            });
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            ColorpickerExtention cpe = new ColorpickerExtention(this, selectedColor);
            MainWindow.Instance.OpenDialog(cpe);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove(lsel);
            refresh(true);
            save();
        }

        private void ColorPicker_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible)
            {
                refresh(true);
            }
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = MainWindow.Instance.PointToClient(this.PointToScreen(buttons[lsel].Location)).X;
            ColorDialog cd = new ColorDialog(x + Util.GetGUISize() / 2);
            MainWindow.Instance.OpenDialog(cd, delegate (Control dialog)
                {
                    if (cd.Result == DialogResult.OK)
                    {
                        colors[lsel] = cd.SelectedColor;
                        icons[lsel].Dispose();
                        icons[lsel] = createIcon(cd.SelectedColor);
                        refresh(true);
                        save();
                    }
                });
        }

        public List<Color> GetColors()
        {
            return colors;
        }

        public void SetColorExtern(Color c)
        {
            selectedColor = c;
            refresh(true);
            SetColor?.Invoke(c);
        }

        public void RefreshPens()
        {
            colors.Clear();
            try
            {
                string[] colorStrings = Configuration.PenColors.Split(',');
                foreach (string colorString in colorStrings)
                    colors.Add(ColorTranslator.FromHtml(colorString));
            }
            catch (Exception)
            {

            }
            refresh(true);
        }
    }
}
