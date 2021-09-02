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

namespace Kritzel.Main.Dialogues.Settings
{
    public partial class SettingsPageGeneral : UserControl, ISettingsPage
    {
        string currentLang;

        public SettingsPageGeneral()
        {
            InitializeComponent();
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
    }
}
