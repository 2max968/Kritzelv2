using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public class MsgBox
    {
        public static void ShowOk(string text)
        {
            Show(text, "Dialog.ok", null);
        }

        public static bool ShowYesNo(string text)
        {
            return Show(text, "Dialog.yes", "Dialog.no");
        }

        public static bool Show(string text, string btnOkText, string btnCancelText)
        {
            text = Language.CurrentLanguage.Get(text);
            MsgBoxDiag diag = new MsgBoxDiag(text, Language.GetText(btnOkText), Language.GetText(btnCancelText));
            return diag.ShowDialog() == DialogResult.OK;
        }

        public class MsgBoxDiag : Form
        {
            Button btnOkay = null;
            Button btnCancel = null;
            Label lblMessage;

            public MsgBoxDiag(string text, string btnOkayText, string btnCancelText)
            {
                this.Text = "Kritzel";
                this.ClientSize = new System.Drawing.Size(Util.GetGUISize() * 10, (int)(Util.GetGUISize() * 3.5));
                if (Application.OpenForms.Count > 0)
                {
                    this.StartPosition = FormStartPosition.CenterParent;
                    this.ShowInTaskbar = false;
                }
                else
                {
                    this.StartPosition = FormStartPosition.CenterScreen;
                    this.ShowInTaskbar = true;
                }
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.DialogResult = DialogResult.Cancel;
                this.MinimizeBox = false;
                this.MaximizeBox = false;
                this.Icon = Program.WindowIcon;
                if(btnOkayText != null)
                {
                    btnOkay = new Button();
                    btnOkay.Text = btnOkayText;
                    btnOkay.Bounds = new System.Drawing.Rectangle(
                        this.ClientSize.Width - (int)(7 * Util.GetGUISize()),
                        this.ClientSize.Height - (int)(1.5 * Util.GetGUISize()),
                        3 * Util.GetGUISize(),
                        1 * Util.GetGUISize());
                    this.Controls.Add(btnOkay);
                    this.AcceptButton = btnOkay;
                    btnOkay.Click += BtnOkay_Click;
                }
                if (btnCancelText != null)
                {
                    btnCancel = new Button();
                    btnCancel.Text = btnCancelText;
                    btnCancel.Bounds = new System.Drawing.Rectangle(
                        this.ClientSize.Width - (int)(3.5 * Util.GetGUISize()),
                        this.ClientSize.Height - (int)(1.5 * Util.GetGUISize()),
                        3 * Util.GetGUISize(),
                        1 * Util.GetGUISize());
                    this.Controls.Add(btnCancel);
                    this.CancelButton = btnCancel;
                    btnCancel.Click += BtnCancel_Click;
                }
                if(btnCancel == null && btnOkay != null)
                {
                    btnOkay.Bounds = new System.Drawing.Rectangle(
                        this.ClientSize.Width - (int)(3.5 * Util.GetGUISize()),
                        this.ClientSize.Height - (int)(1.5 * Util.GetGUISize()),
                        3 * Util.GetGUISize(),
                        1 * Util.GetGUISize());
                }
                lblMessage = new Label();
                lblMessage.Location = new System.Drawing.Point(Util.GetGUISize() / 2, Util.GetGUISize() / 2);
                lblMessage.Text = text;
                lblMessage.AutoSize = false;
                lblMessage.Size = new System.Drawing.Size(this.ClientRectangle.Width - Util.GetGUISize(),
                    this.ClientSize.Height - Util.GetGUISize());
                this.Controls.Add(lblMessage);
            }

            private void BtnCancel_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            private void BtnOkay_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
