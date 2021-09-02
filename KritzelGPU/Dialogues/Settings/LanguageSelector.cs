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
    public partial class LanguageSelector : Form
    {
        bool checkEvent = true;
        public string SelectedLanguage { get; private set; }

        public LanguageSelector(string currentLang)
        {
            InitializeComponent();
            SelectedLanguage = currentLang;
            DialogResult = DialogResult.Cancel;

            lvLangs.Font = new Font(lvLangs.Font.FontFamily, Util.GetFontSizePixel(), GraphicsUnit.Pixel);
            list();
            Translator.Translate(this);
            Text = Language.GetText("Language.select");
            Icon = Program.WindowIcon;
        }

        void list()
        {
            lvLangs.Items.Clear();
            lvLangs.Items.Add(new ListViewItem(Language.GetText("Settings.systemLang"))
            {
                Tag = ""
            });
            foreach (Language lang in Language.Languages.Values)
            {
                ListViewItem itm = new ListViewItem(lang.Name);
                itm.Tag = lang.Key;
                if (lang.Key == SelectedLanguage)
                    itm.Checked = true;
                lvLangs.Items.Add(itm);
            }
            if (SelectedLanguage == "")
                lvLangs.Items[0].Checked = true;
        }

        private void lvLangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkEvent = false;
            foreach (ListViewItem itm in lvLangs.Items)
            {
                itm.Checked = false;
            }
            lvLangs.FocusedItem.Checked = true;
            checkEvent = true;
        }

        private void lvLangs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkEvent)
            {
                checkEvent = false;
                foreach (ListViewItem itm in lvLangs.Items)
                {
                    if(itm != null)
                        itm.Checked = false;
                }
                checkEvent = true;
            }
        }

        private void lvLangs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(lvLangs.CheckedItems.Count > 0 && lvLangs.CheckedItems[0].Tag is string)
            {
                SelectedLanguage = (string)lvLangs.CheckedItems[0].Tag;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Ini File|*.ini";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                Language lang = new Language(ofd.FileName);
                if(lang.Key == "" || lang.Name == "")
                {
                    MsgBox.ShowOk("Language.missingInfo");
                    return;
                }

                ResManager.MakeLangDirWritable();
                DirectoryInfo di = new DirectoryInfo(".\\res\\lang");
                if (!di.Exists)
                    di.Create();
                FileInfo fi = new FileInfo(Path.Combine(di.FullName, $"{lang.Key}.ini"));
                if(fi.Exists)
                {
                    if (!MsgBox.ShowYesNo("Language.overwrite"))
                        return;
                }
                File.Copy(ofd.FileName, fi.FullName, true);
                Language.Init();
                list();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvLangs.CheckedItems.Count > 0 && lvLangs.CheckedItems[0].Tag is string)
            {
                var key = (string)lvLangs.CheckedItems[0].Tag;
                if (Language.Languages.ContainsKey(key))
                {
                    var lang = Language.Languages[key];
                    string text = Language.GetText("Language.shouldDelete").Replace("{0}", lang.Name);
                    if(lang.Path != null && MsgBox.ShowYesNo(text))
                    {
                        ResManager.MakeLangDirWritable();
                        lang.Path.Delete();
                        Language.Init();
                        list();
                    }
                }
            }
        }
    }
}
