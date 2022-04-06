using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kritzel.PointerInputLibrary;

namespace Kritzel.Main.GUIElements
{
    public partial class ColorDialog : UserControl, IClosable
    {
        public event CloseDelegate Close;
        int btnSize = Util.GetGUISize();
        public DialogResult Result { get; private set; } = DialogResult.Cancel;
        public Color SelectedColor { get; private set; } = Color.White;
        float currentHue = -1;

        public ColorDialog(int centerX = -1)
        {
            InitializeComponent();

            panelTop.Height = (int)(1.5f * Util.GetGUISize());
            panelBottom.Height = Util.GetGUISize();
            int btnNum = Configuration.ColorPickerHueSteps + 1;
            this.Width = (int)((btnSize * 1.25) * (Configuration.ColorPickerSatSteps + 1.25));
            this.Height = panelTop.Height + panelBottom.Height + (int)((btnSize * 1.25) * (Configuration.ColorPickerValueSteps + .25));
            int huePickWidth = this.Width / (Configuration.ColorPickerHueSteps + 1);
            this.BackColor = Style.Default.MenuBackground;
            panelBottom.BackColor = Style.Default.MenuContrast;
            panelTop.BackColor = Style.Default.MenuBackground;
            btnMore.ForeColor = Style.Default.MenuForeground;
            btnOk.ForeColor = Style.Default.MenuForeground;

            if (centerX < 0) centerX = MainWindow.Instance.PointToClient(Cursor.Position).X;
            int locX = centerX - this.Width / 2;
            if (locX + Width >= MainWindow.Instance.ClientSize.Width) locX = MainWindow.Instance.ClientSize.Width - Width;
            if (locX < 0) locX = 0;
            this.Location = new Point(locX, Util.GetGUISize());

            List<Color> _colors = new List<Color>();
            List<float> _hues = new List<float>();
            for (int i = 0; i < btnNum; i++)
            {
                if (i < btnNum - 1)
                {
                    float hue = i * 360f / (float)Configuration.ColorPickerHueSteps;
                    _hues.Add(hue);
                    _colors.Add(HsvToRgb((int)hue, 100, 100));
                }
                else
                {
                    _hues.Add(-2);
                    _colors.Add(Color.Transparent);
                }
            }
            HueInput hueInput = new HueInput(_colors.ToArray(), _hues.ToArray());
            hueInput.Dock = DockStyle.Fill;
            hueInput.HueChangeEvent += HueInput_HueChangeEvent;
            panelTop.Controls.Add(hueInput);

            Dialogues.Translator.Translate(this);

            HueInput_HueChangeEvent(this, -1);
        }

        private void HueInput_HueChangeEvent(object sender, float hue)
        {
            if (hue < 0)
            {
                while (panelContent.Controls.Count > 0)
                {
                    Control cltr = panelContent.Controls[0];
                    cltr.Hide();
                    cltr.Dispose();
                    panelContent.Controls.Remove(cltr);
                }

                for (int i = 0; i <= Configuration.ColorPickerSatSteps; i++)
                {
                    TouchInput colorPanel = new TouchInput();
                    colorPanel.Bounds = new Rectangle((int)((.25 + 1.25 * i) * btnSize), (int)(.25 * btnSize), btnSize, btnSize);
                    int sat = 0;
                    int val = i * 100 / Configuration.ColorPickerSatSteps;
                    Color c = HsvToRgb(0, sat, val);
                    colorPanel.BackColor = c;
                    panelContent.Controls.Add(colorPanel);
                    colorPanel.InputEvent += ColorPanel_InputEvent;
                    colorPanel.BorderStyle = BorderStyle.FixedSingle;
                }

                var colors = Main.Colors_Cfg.GetColors();
                for(int i = 0; i < colors.Count;i++)
                {
                    TouchInput colorPanel = new TouchInput();
                    int x = i % (Configuration.ColorPickerSatSteps+1);
                    int y = i / (Configuration.ColorPickerSatSteps+1);
                    colorPanel.Bounds = new Rectangle((int)((.25 + 1.25 * x) * btnSize), (int)((.25 + 1.25 * (y+1)) * btnSize), btnSize, btnSize);
                    int val = i * 100 / Configuration.ColorPickerSatSteps;
                    colorPanel.BackColor = colors[i];
                    panelContent.Controls.Add(colorPanel);
                    colorPanel.InputEvent += ColorPanel_InputEvent;
                    colorPanel.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            else
            {
                currentHue = hue;
                Control[] buttons = new Control[panelContent.Controls.Count];
                for (int i = 0; i < panelContent.Controls.Count; i++)
                {
                    buttons[i] = panelContent.Controls[i];
                }

                for (int i_val = 0; i_val < Configuration.ColorPickerValueSteps; i_val++)
                {
                    for (int i_sat = 0; i_sat <= Configuration.ColorPickerSatSteps; i_sat++)
                    {
                        TouchInput colorPanel = new TouchInput();
                        colorPanel.Bounds = new Rectangle((int)((.25 + 1.25 * i_sat) * btnSize), (int)((.25 + 1.25 * i_val) * btnSize), btnSize, btnSize);
                        int sat = i_sat * 100 / Configuration.ColorPickerSatSteps;
                        int val = (i_val + 1) * 100 / Configuration.ColorPickerValueSteps;
                        Color c = HsvToRgb((int)hue, sat, val);
                        colorPanel.BackColor = c;
                        panelContent.Controls.Add(colorPanel);
                        colorPanel.InputEvent += ColorPanel_InputEvent;
                        colorPanel.BorderStyle = BorderStyle.FixedSingle;
                    }
                }

                foreach (Control cltr in buttons)
                {
                    cltr.Hide();
                    cltr.Dispose();
                    panelContent.Controls.Remove(cltr);
                }
            }
        }

        private void ColorPanel_InputEvent(TouchInput sender)
        {
            btnOk.BackColor = sender.BackColor;
            SelectedColor = sender.BackColor;
            btnOk.Enabled = true;
        }

        public Color HsvToRgb(int H, int S, int V)
        {
            float h = H / 360f;
            float s = S / 100f;
            float v = V / 100f;

            float r, g, b, i, f, p, q, t;
            i = (float)Math.Floor(h * 6);
            f = h * 6 - i;
            p = v * (1 - s);
            q = v * (1 - f * s);
            t = v * (1 - (1 - f) * s);
            switch (i % 6)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                case 5: r = v; g = p; b = q; break;
                default: r = 0; g = 0; b = 0; break;
            }
            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        public void AddCloseHandler(CloseDelegate handler)
        {
            Close += handler;
        }

        public class TouchInput : Panel
        {
            public delegate void InputEventDelegate(TouchInput sender);
            public event InputEventDelegate InputEvent;

            public TouchInput()
            {
                base.MouseClick += TouchInput_MouseClick;
                this.Cursor = Cursors.Hand;
            }

            private void TouchInput_MouseClick(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left) InputEvent?.Invoke(this);
            }
        }

        public class HueInput : Panel
        {
            public event EventHandler<float> HueChangeEvent;
            Color[] colors;
            float[] hues;
            public int PickerMargin = 16;
            int index = -1;

            public HueInput(Color[] colors, float[] hues)
            {
                this.colors = colors;
                this.hues = hues;

                this.Paint += HueInput_Paint;
                this.MouseMove += HueInput_MouseEvent;
                this.MouseDown += HueInput_MouseEvent;

                index = hues.Length - 1;
            }

            private void HueInput_MouseEvent(object sender, MouseEventArgs e)
            {
                if(e.Button == MouseButtons.Left)
                {
                    for(int i = 0; i < hues.Length; i++)
                    {
                        if(getRect(i).Contains(e.Location))
                        {
                            if(i != index)
                            {
                                index = i;
                                Graphics g = CreateGraphics();
                                HueInput_Paint(null, new PaintEventArgs(g,
                                    new Rectangle(0, 0, this.Width, this.Height)));
                                g.Dispose();
                                HueChangeEvent?.Invoke(this, hues[i]);
                            }
                        }
                    }
                }
            }

            private void HueInput_Paint(object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                for(int i = 0; i < colors.Length; i++)
                {
                    RectangleF rect = getRect(i);
                    RectangleF outerRect = new RectangleF(rect.X, 0, rect.Width, this.Height);
                    if(i == index)
                    {
                        rect.Y = 0;
                        rect.Height = this.Height;
                    }
                    using(Brush b = new SolidBrush(Style.Default.MenuBackground))
                        g.FillRectangle(b, outerRect);
                    if (colors[i].A > 0)
                        using (Brush b = new SolidBrush(colors[i]))
                            g.FillRectangle(b, rect);
                    else
                        using (Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(
                            new PointF(rect.Left, rect.Top), new PointF(rect.Right, rect.Bottom),
                            Color.Magenta, Color.Yellow))
                                g.FillRectangle(b, rect);
                }
            }

            RectangleF getRect(int i)
            {
                return new RectangleF(i * this.Width / colors.Length, PickerMargin,
                            this.Width / colors.Length + 1, this.Height - 2 * PickerMargin);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            Close?.Invoke();
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            /*var ecp = new Dialogues.ExtendedColorPicker();
            ecp.Color = SelectedColor;
            if(ecp.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = ecp.Color;
                btnOk.BackColor = ecp.Color;
                btnOk.Enabled = true;
            }*/

            var cd = new System.Windows.Forms.ColorDialog();
            cd.Color = SelectedColor;
            if(cd.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = cd.Color;
                btnOk.BackColor = cd.Color;
                btnOk.Enabled = true;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
