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
    public partial class PasswordDialog : Form
    {
        public delegate bool CheckPasswordCallback(string password);

        CheckPasswordCallback callback = null;

        public PasswordDialog()
        {
            InitializeComponent();
            Translator.Translate(this);
            this.Text = Language.GetText("Dialog.password");
            this.Height = Util.GetGUISize();
            btnCancel.Width = Util.GetGUISize() * 2;
            btnOk.Width = Util.GetGUISize() * 2;
            this.Shown += PasswordDialog_Shown;
            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size * Util.GetScaleFactor());
        }

        private void PasswordDialog_Shown(object sender, EventArgs e)
        {
            this.ClientSize = new Size(Util.GetGUISize() * 10, Util.GetGUISize());
        }

        public DialogResult ShowDialog(out string password, CheckPasswordCallback callback)
        {
            this.callback = callback;
            var res = base.ShowDialog();
            password = textBox1.Text;
            return res;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(callback == null || callback.Invoke(textBox1.Text))
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MsgBox.ShowOk("Dialog.wrongPassword");
            }
        }
    }
}
