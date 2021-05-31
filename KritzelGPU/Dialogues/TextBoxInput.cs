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
    public partial class TextBoxInput : Form
    {
        Forms.TextBox tb;
        InkControl ink;

        public TextBoxInput(Forms.TextBox tb, InkControl parent)
        {
            InitializeComponent();
            Translator.Translate(this);
            this.tb = tb;
            tbContent.Text = tb.Text;
            this.ink = parent;
        }

        private void tbContent_TextChanged(object sender, EventArgs e)
        {
            if (ink == null) return;
            tb.Text = tbContent.Text;
            ink.Refresh();
        }
    }
}
