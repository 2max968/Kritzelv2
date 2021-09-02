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
    public partial class SettingsPagePerformance : UserControl, ISettingsPage
    {
        public SettingsPagePerformance()
        {
            InitializeComponent();
        }

        public bool CheckValues()
        {
            if(!int.TryParse(tbAutosaveInterval.Text, out _))
            {
                Dialogues.MsgBox.ShowOk("Settings.invalidValue");
                return false;
            }
            return true;
        }

        public void LoadSettings()
        {
            cbCalcSpline.Checked = Configuration.CalculateSplinesDuringDrawing;
            cbRefresh.Checked = Configuration.RefreshOnTransform;
            tbAutosaveInterval.Text = "" + Configuration.AutosaveInterval;
        }

        public void PostSave()
        {
            
        }

        public void SaveSettings()
        {
            Configuration.CalculateSplinesDuringDrawing = cbCalcSpline.Checked;
            Configuration.RefreshOnTransform = cbRefresh.Checked;
            int.TryParse(tbAutosaveInterval.Text, out Configuration.AutosaveInterval);
        }
    }
}
