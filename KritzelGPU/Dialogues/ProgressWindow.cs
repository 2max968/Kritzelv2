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
    public partial class ProgressWindow : Form
    {
        public bool Cancel { get; private set; } = false;

        public ProgressWindow(string text, bool canCancel)
        {
            InitializeComponent();
            this.Text = text;
            btnCancel.Text = Language.GetText(btnCancel.Text);
            btnCancel.Enabled = canCancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cancel = true;
            btnCancel.Enabled = false;
        }
    }
}
