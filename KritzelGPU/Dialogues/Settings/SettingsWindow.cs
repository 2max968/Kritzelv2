using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues.Settings
{
    public partial class SettingsWindow : Form
    {
        (string, UserControl)[] pages = new (string, UserControl)[]
        {
            ("Settings.general", new SettingsPageGeneral()),
            ("Settings.input", new SettingsPageInput()),
            ("Settings.view", new SettingsPageView()),
            ("Settings.performance", new SettingsPagePerformance()),
            ("Settings.palettes", new SettingsPagePalettes())
        };

        object lastClicked = null;
        int clickCount = 0;

        public SettingsWindow()
        {
            InitializeComponent();
            Translator.Translate(this);
            Icon = Program.WindowIcon;
            Text = Language.GetText("Settings.settings");

            int btnsW = Util.GetGUISize() * 2;
            int btnsH = Util.GetGUISize() * 2 / 3;
            int btnsY = ClientSize.Height - btnsH - 8;
            int btnsX = ClientSize.Width - btnsW - 8;
            btnOk.Bounds = new Rectangle(btnsX, btnsY, btnsW, btnsH);
            btnsX -= btnsW + 8;
            btnCancel.Bounds = new Rectangle(btnsX, btnsY, btnsW, btnsH);
            btnsX -= btnsW + 8;
            btnApply.Bounds = new Rectangle(btnsX, btnsY, btnsW, btnsH);

            flowPages.Width = (int)(Util.GetGUISize() * 4.5f);
            lblTitle.Font = new Font(lblTitle.Font.FontFamily, Util.GetFontSizePixel() * 2, GraphicsUnit.Pixel);
            lblTitle.Left = flowPages.Right + 4;
            pnContainer.Left = lblTitle.Left;
            pnContainer.Top = lblTitle.Bottom + 4;
            pnContainer.Width = ClientSize.Width - flowPages.Width - 12;
            pnContainer.Height = ClientSize.Height - lblTitle.Height - btnsH - 16;
            foreach((var pageName, var pageForm) in pages)
            {
                Button btn = new Button();
                btn.Text = Language.GetText(pageName);
                btn.Size = new Size(Util.GetGUISize() * 4, Util.GetGUISize());
                btn.Tag = new Tuple<string, UserControl>(btn.Text, pageForm);
                btn.Click += Btn_Click;
                flowPages.Controls.Add(btn);

                if(pageForm is ISettingsPage)
                {
                    ((ISettingsPage)pageForm).LoadSettings();
                }

                pnContainer.Controls.Add(pageForm);
                pageForm.Dock = DockStyle.Fill;
                Translator.Translate(pageForm);
                pageForm.Hide();
            }

            lblTitle.Text = Language.GetText(pages[0].Item1);
            pages[0].Item2.Show();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            foreach ((_, var pageForm) in pages)
            {
                pageForm.Hide();
            }

            if (sender is Button)
            {
                Button btn = (Button)sender;
                if (btn.Tag is Tuple<string, UserControl>)
                {
                    (string name, UserControl control) = (Tuple<string, UserControl>)btn.Tag;
                    lblTitle.Text = name;
                    control.Show();
                }
            }

            if(sender == lastClicked)
            {
                clickCount++;
                if(clickCount >= 10)
                {
                    DebugSetup ds = new DebugSetup();
                    ds.ShowDialog();
                }
            }
            else
            {
                clickCount = 0;
            }
            lastClicked = sender;
        }

        bool save()
        {
            foreach ((_, UserControl ctrl) in pages)
            {
                if (ctrl is ISettingsPage)
                {
                    if (!((ISettingsPage)ctrl).CheckValues())
                        return false;
                }
            }
            foreach ((_, UserControl ctrl) in pages)
            {
                if (ctrl is ISettingsPage)
                {
                    ((ISettingsPage)ctrl).SaveSettings();
                }
            }
            Configuration.SaveConfig();
            foreach ((_, UserControl ctrl) in pages)
            {
                if (ctrl is ISettingsPage)
                {
                    ((ISettingsPage)ctrl).PostSave();
                }
            }
            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (save())
                this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
