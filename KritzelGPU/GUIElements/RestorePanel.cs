using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.GUIElements
{
    public partial class RestorePanel : UserControl, IClosable
    {
        public delegate void CloseMenuEvent();
        public event CloseMenuEvent CloseMenu;

        MainWindow window;

        public RestorePanel(MainWindow window)
        {
            InitializeComponent();
            this.window = window;

            this.Font = new Font(this.Font.FontFamily, Util.GetFontSizePixel(), GraphicsUnit.Pixel);
            this.Width = 12 * Util.GetGUISize();
            this.BackColor = Style.Default.MenuBackground;
            this.ForeColor = Style.Default.MenuForeground;
            lblTitle.Text = "\n" + Language.GetText(lblTitle.Text);
            lblSubtitle.Text = Language.GetText(lblSubtitle.Text);
            lblTitle.Font = new Font(this.Font.FontFamily, Util.GetFontSizePixel() * 2, FontStyle.Bold, GraphicsUnit.Pixel);

            btnClose.Bounds = new Rectangle(this.Width - Util.GetGUISize() * 3 / 2, Util.GetGUISize() / 2, Util.GetGUISize(), Util.GetGUISize());
            lblSubtitle.MaximumSize = new Size(this.Width - Util.GetGUISize() * 2, 0);
            btnClose.BackColor = Style.Default.MenuContrast;
            btnClose.Image = ResManager.LoadIcon("decline.svg", Util.GetGUISize());

            var docs = TmpManager.GetRecoverableDocuments();
            int x = Util.GetGUISize() / 2 + lblTitle.Height + lblSubtitle.Height;
            if (docs.Count > 0)
            {
                foreach (var doc in docs)
                {
                    Label lbl = new Label();
                    lbl.Text = doc.Name;
                    if (doc.Comment != null)
                        lbl.Text += $" ({doc.Comment})";
                    lbl.Font = new Font(Font.FontFamily, Font.Size / 2);
                    lbl.AutoSize = true;
                    Label lbl2 = new Label();
                    lbl2.Text = $"{doc.Date} - {doc.Time}";
                    lbl2.Font = new Font(Font.FontFamily, Font.Size / 4);
                    lbl2.AutoSize = true;
                    Button btnLoad = new Button();
                    Button btnDelete = new Button();
                    btnLoad.Text = "";
                    btnLoad.Image = ResManager.LoadIcon("accept.svg", Util.GetGUISize());
                    btnDelete.Text = "";
                    btnDelete.Image = ResManager.LoadIcon("actions/delete.svg", Util.GetGUISize());
                    btnDelete.FlatStyle = btnLoad.FlatStyle = FlatStyle.Flat;
                    btnDelete.FlatAppearance.BorderSize = btnLoad.FlatAppearance.BorderSize = 0;
                    btnDelete.BackColor = btnLoad.BackColor = Style.Default.MenuContrast;
                    btnLoad.Size = btnDelete.Size = new Size(Util.GetGUISize(), Util.GetGUISize());
                    lbl.Location = new Point(4, x + 4);
                    lbl2.Location = new Point(4, x + Util.GetGUISize() * 3 / 4);
                    btnLoad.Location = new Point(Width - Util.GetGUISize() * 3, x);
                    btnDelete.Location = new Point(Width - Util.GetGUISize() * 3 / 2, x);
                    Controls.Add(lbl);
                    Controls.Add(lbl2);
                    Controls.Add(btnDelete);
                    Controls.Add(btnLoad);
                    x += (int)(Util.GetGUISize() * 1.5);

                    btnLoad.Tag = doc;
                    btnLoad.Click += BtnLoad_Click;
                    btnDelete.Tag = doc;
                    btnDelete.Click += BtnDelete_Click;
                }
            }
            else
            {
                Label lblNodoc = new Label();
                lblNodoc.Location = new Point(4, x + 4);
                lblNodoc.Text = Language.GetText("Recovery.nodocs");
                lblNodoc.AutoSize = true;
                lblNodoc.MaximumSize = new Size(this.Width - Util.GetGUISize() * 2, 0);
                Controls.Add(lblNodoc);
            }
        }

        public void AddCloseHandler(CloseDelegate handler)
        {
            this.CloseMenu += new CloseMenuEvent(handler);
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            var doc = (sender as Control)?.Tag as TmpManager.RecoverableFileInfo;
            if (doc == null)
                return;

            string text = Language.GetText("Recovery.delete?");
            text = text.Replace("{0}", doc.Name);
            if(Dialogues.MsgBox.ShowYesNo(text))
            {
                doc.Directory.Delete(true);
                CloseMenu?.Invoke();
                await Task.Delay(10);
                RestorePanel rp = new RestorePanel(window);
                rp.Dock = DockStyle.Left;
                window.OpenDialog(rp);
            }
        }

        private async void BtnLoad_Click(object sender, EventArgs e)
        {
            var doc = (sender as Control)?.Tag as TmpManager.RecoverableFileInfo;
            if (doc == null)
                return;
            await window.RecoverFile(doc);
            CloseMenu?.Invoke();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseMenu?.Invoke();
        }
    }
}
