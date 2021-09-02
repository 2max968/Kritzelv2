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
    public partial class SettingsPageView : UserControl, ISettingsPage
    {
        public SettingsPageView()
        {
            InitializeComponent();
        }

        public bool CheckValues()
        {
            return true;
        }

        public void LoadSettings()
        {
            cbDark.Checked = Configuration.DarkMode;
            cbLefthand.Checked = Configuration.LeftHanded;
            cbShowPenSize.Checked = Configuration.SizeOptionsInTitlebar;
        }

        public void PostSave()
        {
            
        }

        public void SaveSettings()
        {
            Configuration.DarkMode = cbDark.Checked;
            Configuration.LeftHanded = cbLefthand.Checked;
            Configuration.SizeOptionsInTitlebar = cbShowPenSize.Checked;
            Style.SetStyle();
        }
    }
}
