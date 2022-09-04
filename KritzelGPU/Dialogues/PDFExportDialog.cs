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
    public partial class PDFExportDialog : Form
    {
        GUIElements.ItemSelector cb;

        public int Dpi = 300;

        public PDFExportDialog()
        {
            InitializeComponent();

            cb = new GUIElements.ItemSelector();
            cb.Dock = DockStyle.Top;
            cb.Items = new string[] { "High Quality (600 Dpi)", "Medium Quality (300 Dpi)", "Low Quality (150 Dpi)", "Vector (Experimental)" };
            cb.Height = cb.Items.Length * Util.GetGUISize();
            Controls.Add(cb);
            cb.SelectedIndex = 1;

            this.ClientSize = new Size(this.ClientSize.Width, (cb.Items.Length + 1) * Util.GetGUISize());
            btnOk.Location = new Point(this.ClientSize.Width - btnOk.Width, this.ClientSize.Height - btnOk.Height);
            btnCancel.Location = new Point(btnOk.Left - btnCancel.Width, this.ClientSize.Height - btnCancel.Height);

            Translator.Translate(this);
            this.BackColor = Style.Default.MenuBackground;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Dpi = new int[] { 600, 300, 150, -1 }[cb.SelectedIndex];
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
