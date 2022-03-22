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
            cb.Items = new string[] { "High Quality (300 Dpi)", "Medium Quality (150 Dpi)", "Low Quality (75 Dpi)" };
            cb.Height = cb.Items.Length * Util.GetGUISize();
            Controls.Add(cb);
            cb.SelectedIndex = 1;

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
            Dpi = new int[] { 300, 150, 75 }[cb.SelectedIndex];
        }
    }
}
