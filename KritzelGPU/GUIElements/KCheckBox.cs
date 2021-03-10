using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.GUIElements
{
    public partial class KCheckBox : UserControl
    {
        bool radioButton = false;
        string text;
        Brush textBrush;

        public bool RadioButton
        {
            get
            {
                return radioButton;
            }
            set
            {
                radioButton = value;
            }
        }
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                textBrush = new SolidBrush(value);
            }
        }


        public KCheckBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Bitmap buffer = new Bitmap(this.Width, this.Height))
            {
                using (Graphics g = Graphics.FromImage(buffer))
                {
                    g.Clear(this.BackColor);
                    int offset = this.Height - Margin.Top - Margin.Bottom;
                    Rectangle textRect = new Rectangle(offset + Margin.Left,
                        Margin.Top, this.Width - offset - Margin.Left - Margin.Right,
                        this.Height - Margin.Top - Margin.Bottom);
                    Rectangle imgRect = new Rectangle(Margin.Left, Margin.Top, offset, offset);

                    g.DrawString(Text, Font, textBrush, textRect);
                }
            }
        }
    }
}
