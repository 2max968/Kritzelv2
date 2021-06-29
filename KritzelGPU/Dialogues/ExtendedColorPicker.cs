using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class ExtendedColorPicker : Form
    {
        bool events = true;
        Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                events = false;
                color = value;
                tbRed.Value = color.R;
                tbGreen.Value = color.G;
                tbBlue.Value = color.B;
                tbRGB.Text = $"{color.R:X2}{color.G:X2}{color.B:X2}";


                pnPreview.BackColor = color;
                events = true;
            }
        }

        public ExtendedColorPicker()
        {
            InitializeComponent();
            Translator.Translate(this);
            Color = Color.White;

            pnBottom.Height = Util.GetGUISize();
            btnOk.Width = btnCancel.Width = Util.GetGUISize() * 2;
            pnPreview.Width = Util.GetGUISize() * 4;
            btnOk.ForeColor = btnCancel.ForeColor = Style.Default.MenuForeground;
            btnOk.FlatAppearance.BorderColor = btnCancel.FlatAppearance.BorderColor = Style.Default.MenuForeground;
            this.BackColor = Style.Default.MenuBackground;
            pnBottom.BackColor = Style.Default.MenuContrast;
        }

        private void tbRed_ValueChanged(object sender, EventArgs e)
        {
            if (!events) return;
            Color c = Color.FromArgb(tbRed.Value, tbGreen.Value, tbBlue.Value);
            Color = c;
        }

        private void tbRGB_TextChanged(object sender, EventArgs e)
        {
            if (!events) return;
            string text = tbRGB.Text;
            if (text.Length != 6) return;
            string strR = text.Substring(0, 2);
            string strG = text.Substring(2, 2);
            string strB = text.Substring(4, 2);
            if (!byte.TryParse(strR, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte r)) return;
            if (!byte.TryParse(strG, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte g)) return;
            if (!byte.TryParse(strB, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte b)) return;

            Color = Color.FromArgb(r, g, b);
        }
    }
}
