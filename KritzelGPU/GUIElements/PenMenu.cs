using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kritzel.Main.ScreenObject;
using System.IO;

namespace Kritzel.Main.GUIElements
{
    public partial class PenMenu : UserControl, IClosable
    {
        public event CloseDelegate OnClose;
        InkControl control;
        public InkMode Mode;
        static Bitmap bmpEraser = null;

        bool hasRuler;
        bool hasCompass;

        public PenMenu(InkControl control)
        {
            InitializeComponent();
            this.control = control;
            this.Mode = control.InkMode;

            this.Font = new Font(this.Font.FontFamily, Util.GetFontSizePixel(), GraphicsUnit.Pixel);
            int guiSize = Util.GetGUISize();
            this.Width = guiSize * Configuration.PenSizeNum;
            panelBottom.Height = guiSize;
            BackColor = Style.Default.MenuBackground;
            ForeColor = Style.Default.MenuForeground;
            panel1.BackColor = Style.Default.MenuForeground;

            foreach(Control cltr in Controls)
            {
                if(cltr is Button)
                {
                    cltr.Height = Util.GetGUISize();
                    cltr.Text = Language.GetText(cltr.Text);
                }
            }

            if (bmpEraser == null)
                bmpEraser = ResManager.LoadIcon("eraserAll.svg", Util.GetGUISize());
            int btnSize = Util.GetGUISize();
            btnLine.Image = Forms.LinearLine.BitmapLL;
            btnRect.Image = Forms.Rect.BitmapRect;
            btnCircle.Image = Forms.Arc.BitmapArc;
            btnArc2.Image = Forms.Arc2.BitmapArc2;
            btnStroke.Image = Line.BitmapStrk;
            btnRuler.Image = Ruler.Icon;
            btnCompass.Image = Compass.Icon;
            btnTextBox.Image = Forms.TextBox.BitmapTB;
            btnAddImage.Image = Forms.ImageObject.IconPhoto;
            btnStamp.Image = Forms.LineGroup.Stamp;
            if (control.EraserColor == Color.Transparent)
                btnEraser.Image = bmpEraser;
            else
                btnEraser.Image = Dialogues.BrushList.CreateIcon(control.EraserColor);

            for(int i = 0; i < Configuration.PenSizeNum; i++)
            {
                float size = Configuration.PenSizeMin + i * (Configuration.PenSizeMax - Configuration.PenSizeMin) / (float)(Configuration.PenSizeNum - 1) * Util.GetScaleFactor();
                Button btn = new Button();
                Bitmap bmp = new Bitmap(guiSize, guiSize);
                Graphics g = Graphics.FromImage(bmp);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.FillEllipse(Brushes.Black, new RectangleF(guiSize / 2 - size, guiSize / 2 - size, size * 2, size * 2));
                g.Dispose();
                btn.BackgroundImage = bmp;
                btn.Bounds = new Rectangle(i * guiSize, 0, guiSize, guiSize);
                panelBottom.Controls.Add(btn);
                btn.Tag = size;
                btn.Click += Btn_Click;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;

                if (control.Thicknes == size)
                    btn.BackColor = Style.Default.Selection;
            }

            hasRuler = false;
            hasCompass = false;
            foreach (var obj in control.ScreenObjects)
            {
                if (obj is Ruler)
                    hasRuler = true;
                if (obj is Compass)
                    hasCompass = true;
            }
            if (hasRuler) btnRuler.BackColor = Style.Default.MenuContrast;
            if (hasCompass) btnCompass.BackColor = Style.Default.MenuContrast;

            this.Dock = DockStyle.Right;
        }

        public void AddCloseHandler(CloseDelegate handler)
        {
            OnClose += handler;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if(sender is Button)
            {
                if(((Button)sender).Tag is float)
                {
                    float size = (float)((Button)sender).Tag;
                    control.Thicknes = size;
                    Configuration.PenCurrentSize = size;
                    OnClose?.Invoke();
                }
            }
        }

        private void btnStroke_Click(object sender, EventArgs e)
        {
            Mode = InkMode.Pen;
            OnClose?.Invoke();
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            Mode = InkMode.Line;
            OnClose?.Invoke();
        }

        private void btnRect_Click(object sender, EventArgs e)
        {
            Mode = InkMode.Rect;
            OnClose?.Invoke();
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            Mode = InkMode.Arc;
            OnClose?.Invoke();
        }

        private void btnRuler_Click(object sender, EventArgs e)
        {
            if(hasRuler)
            {
                for(int i = 0; i < control.ScreenObjects.Count; i++)
                {
                    if(control.ScreenObjects[i] is Ruler)
                    {
                        control.ScreenObjects[i].Dispose();
                        control.ScreenObjects.RemoveAt(i);
                        break;
                    }
                }
            }
            else
            {
                control.ScreenObjects.Add(new Ruler(control));
            }
            OnClose?.Invoke();
        }

        private void btnCompass_Click(object sender, EventArgs e)
        {
            if (hasCompass)
            {
                for (int i = 0; i < control.ScreenObjects.Count; i++)
                {
                    if (control.ScreenObjects[i] is Compass)
                    {
                        control.ScreenObjects[i].Dispose();
                        control.ScreenObjects.RemoveAt(i);
                        break;
                    }
                }
            }
            else
            {
                control.ScreenObjects.Add(new Compass(control));
            }
            OnClose?.Invoke();
        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            Dialogues.BrushList brushes = new Dialogues.BrushList(control);
            OnClose?.Invoke();
            brushes.ShowDialog();
        }

        private void btnArc2_Click(object sender, EventArgs e)
        {
            Mode = InkMode.Arc2;
            OnClose?.Invoke();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mode = InkMode.Text;
            OnClose?.Invoke();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                Renderer.Image img;
                using (var stream = File.OpenRead(ofd.FileName))
                {
                    img = new Renderer.Image(new Bitmap(stream));
                }
                float s1 = control.Page.Format.Width / img.GdiBitmap.Width;
                float s2 = control.Page.Format.Height / img.GdiBitmap.Height;
                float s = Math.Min(s1, s2);
                var imObj = new Forms.ImageObject(img, new RectangleF(0, 0, s * img.GdiBitmap.Width, s * img.GdiBitmap.Height));
                control.Page.AddLine(imObj);
                OnClose?.Invoke();
            }
        }

        private async void btnStamp_Click(object sender, EventArgs e)
        {
            OnClose?.Invoke();
            await Task.Delay(0);
            if(Dialogues.StampMenu.Instance != null)
            {
                Dialogues.StampMenu.Instance.Close();
            }
            else
            {
                var stampmenu = new Dialogues.StampMenu(control);
                stampmenu.Show();
            }
        }
    }
}
