using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.GUIElements
{
    public class NumberFloatBox : TextBox
    {
        float value = 0;
        public float Min { get; set; } = 0;
        public float Max { get; set; } = 10;
        public float Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                this.Text = Util.FToS(value);
                this.BackColor = tmpBack;
            }
        }
        Color tmpBack;

        public NumberFloatBox()
        {
            tmpBack = this.BackColor;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            string str = this.Text.Replace(',', '.');
            float _v;
            if(Util.TrySToF(str, out _v))
            {
                if (_v < Min || _v > Max)
                {
                    this.BackColor = Color.Orange;
                }
                else
                {
                    this.BackColor = tmpBack;
                    value = _v;
                }
            }
            else
            {
                this.BackColor = Color.Orange;
            }
            base.OnTextChanged(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            Value = Value;
            base.OnLostFocus(e);
        }
    }
}
