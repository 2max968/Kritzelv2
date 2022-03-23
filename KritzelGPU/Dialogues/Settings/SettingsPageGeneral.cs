using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues.Settings
{
    public partial class SettingsPageGeneral : UserControl, ISettingsPage
    {
        string currentLang;
        int clickCount = 0;
        Stopwatch stp;

        public SettingsPageGeneral()
        {
            InitializeComponent();

            stp = new Stopwatch();
            stp.Start();
        }

        public bool CheckValues()
        {
            return true;
        }

        public void LoadSettings()
        {
            currentLang = Configuration.Language;

            tbInfo.Text = "Kritzel v" + Application.ProductVersion
                + "\r\n" + ResManager.GetText("buildtime.txt")
                + "\r\n" + new FileInfo(Configuration.FILENAME).FullName;

            btnSelectLanguage.Image = ResManager.LoadIcon("globe.svg", Util.GetGUISize() / 2);
            btnSelectLanguage.Size = new Size(Util.GetGUISize() * 4, Util.GetGUISize());
            btnSelectLanguage.Text = Language.GetText("Settings.language") + "\r\n";
            if (Configuration.Language == "")
                btnSelectLanguage.Text += Language.GetText("Settings.systemLang") + " >>";
            else
            {
                btnSelectLanguage.Text += Language.CurrentLanguage.Name + " >>";
            }
        }

        public void SaveSettings()
        {
            Configuration.Language = currentLang;
        }

        public void PostSave()
        {
            Language.SelectLanguage();
        }

        private void btnSelectLanguage_Click(object sender, EventArgs e)
        {
            LanguageSelector langsel = new LanguageSelector(currentLang);
            if(langsel.ShowDialog() == DialogResult.OK)
            {
                currentLang = langsel.SelectedLanguage;
                btnSelectLanguage.Text = Language.GetText("Settings.language") + "\r\n";
                if (currentLang == "")
                    btnSelectLanguage.Text += Language.GetText("Settings.systemLang") + " >>";
                else
                {
                    btnSelectLanguage.Text += Language.Languages[currentLang].Name + " >>";
                }
            }
        }

        private async void tbInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (stp.ElapsedMilliseconds > 1000)
                clickCount = 0;
            stp.Restart();
            clickCount++;
            if(clickCount >= 15)
            {
                FindForm().Close();
                clickCount = 0;
                await Task.Delay(1);
                DebugSetup ds = new DebugSetup();
                ds.ShowDialog();
            }
        }

        private void tbInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
