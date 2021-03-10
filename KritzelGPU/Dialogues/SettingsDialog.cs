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

namespace Kritzel.Main.Dialogues
{
    public partial class SettingsDialog : Form
    {
        TrackBar tbGamma;

        float[] gammaValues = new float[] { 1 / 5f, 1 / 4f, 1 / 3f, 1 / 2f, 1, 2, 3, 4, 5 };
        string[] gammaStrings = new string[] { "1/5", "1/4", "1/3", "1/2", "1", "2", "3", "4", "5" };

        int debugClicks = 0;

        public SettingsDialog()
        {
            InitializeComponent();

            tbConfPath.Text = new FileInfo(Configuration.FILENAME).FullName;
            
            this.Icon = Program.WindowIcon;
            /*tabControl1.Font = new Font(this.Font.FontFamily, 
                this.Font.Size * Util.GetScaleFactor() * 2, 
                this.Font.Style);*/

            // ---- Tab: General
            cbLanguage.Items.Add(Language.GetText("Settings.systemLang"));
            cbLanguage.SelectedIndex = 0;
            foreach(KeyValuePair<string, Language> lang in Language.Languages)
            {
                cbLanguage.Items.Add(lang.Value);
                if (lang.Key == Configuration.Language)
                    cbLanguage.SelectedIndex = cbLanguage.Items.Count - 1;
            }
            lblVersion.Text += Application.ProductVersion;
            lblBuildTime.Text = ResManager.GetText("buildtime.txt");

            // ---- Tab: Input
            cbHandleMouse.Checked = Configuration.HandleMouseInput;
            tbGamma = new TrackBar();
            tbGamma.Minimum = 0;
            tbGamma.Maximum = gammaValues.Length - 1;
            for (int i = 0; i < gammaValues.Length; i++)
                if (Math.Abs(gammaValues[i] - Configuration.PreassureGamma) < 0.01f)
                    tbGamma.Value = i;
            tbGamma.Dock = DockStyle.Fill;
            tbGamma.ValueChanged += TbGamma_ValueChanged;
            pnSlider.Controls.Add(tbGamma);
            pbGammaGraph.Image = GUIElements.GraphRenderer.CreateGammaGraph(pbGammaGraph.Size, Configuration.PreassureGamma);
            lblGamma.Text = Language.GetText("Settings.preassureGamma")
                + ": " + gammaStrings[tbGamma.Value];

            // ---- Tab: Performance
            cbRefreshTransform.Checked = Configuration.RefreshOnTransform;
            cbCalcSpline.Checked = Configuration.CalculateSplinesDuringDrawing;
            numAutosaveInterval.Value = Configuration.AutosaveInterval;
            numGuiScale.Value = Configuration.GUIScaleFactor;
            cbBattery.Checked = Configuration.ShowBattery;
            cbTime.Checked = Configuration.ShowTime;
            cbDate.Checked = Configuration.ShowDate;
            cbLefthand.Checked = Configuration.LeftHanded;
            cbDarkMode.Checked = Configuration.DarkMode;

            Translator.Translate(this);
            Configuration.SetState();
        }

        private void TbGamma_ValueChanged(object sender, EventArgs e)
        {
            float gamma = gammaValues[tbGamma.Value];
            lblGamma.Text = Language.GetText("Settings.preassureGamma") + ": " + gammaStrings[tbGamma.Value];
            pbGammaGraph.Image?.Dispose();
            pbGammaGraph.Image = GUIElements.GraphRenderer.CreateGammaGraph(pbGammaGraph.Size, gamma);
        }

        void saveSettings()
        {
            // ---- Tab: General
            if(cbLanguage.SelectedItem is Language)
                Configuration.Language = ((Language)cbLanguage.SelectedItem).Key;
            else
                Configuration.Language = "";

            // ---- Tab: Input
            Configuration.HandleMouseInput = cbHandleMouse.Checked;
            Configuration.PreassureGamma = gammaValues[tbGamma.Value];

            // ---- Tab: Performance
            Configuration.RefreshOnTransform = cbRefreshTransform.Checked;
            Configuration.CalculateSplinesDuringDrawing = cbCalcSpline.Checked;
            Configuration.AutosaveInterval = (int)numAutosaveInterval.Value;
            Configuration.GUIScaleFactor = numGuiScale.Value;
            Configuration.ShowBattery = cbBattery.Checked;
            Configuration.ShowTime = cbTime.Checked;
            Configuration.ShowDate = cbDate.Checked;
            Configuration.LeftHanded = cbLefthand.Checked;
            Configuration.DarkMode = cbDarkMode.Checked;

            Configuration.SaveConfig();
            Language.SelectLanguage();

            string rsConf;
            if(Configuration.CheckRestart(out rsConf))
            {
                MsgBox.ShowOk("Settings.restartRequired");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            saveSettings();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            saveSettings();
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {
            debugClicks++;
            if(debugClicks >= 10)
            {
                this.Close();
                debugClicks = 0;
                DebugSetup ds = new DebugSetup();
                ds.ShowDialog();
            }
        }
    }
}
