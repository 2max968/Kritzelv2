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
    public partial class SettingsPageInput : UserControl, ISettingsPage
    {
        float[] gammaValues = new float[] { 1 / 5f, 1 / 4f, 1 / 3f, 1 / 2f, 1, 2, 3, 4, 5 };
        string[] gammaStrings = new string[] { "1/5", "1/4", "1/3", "1/2", "1", "2", "3", "4", "5" };
        int gammaIndex = 0;

        public SettingsPageInput()
        {
            InitializeComponent();

            btnGammaMinus.Size = btnGammaPlus.Size = new Size(Util.GetGUISize(), Util.GetGUISize());
            pnGamma.Height = Util.GetGUISize();
            lblGamma.Font = new Font(lblGamma.Font.FontFamily, Util.GetFontSizePixel() * 2, GraphicsUnit.Pixel);
        }

        public bool CheckValues()
        {
            return true;
        }

        public void LoadSettings()
        {
            cbMouseInput.Checked = Configuration.HandleMouseInput;
            for (int i = 0; i < gammaValues.Length; i++)
                if (Math.Abs(gammaValues[i] - Configuration.PreassureGamma) < 0.01f)
                    gammaIndex = i;

            try
            {
                pbGraph.Image = GUIElements.GraphRenderer.CreateGammaGraph(pbGraph.Size, gammaValues[gammaIndex]);
                lblGamma.Text = gammaStrings[gammaIndex];
            }
            catch (Exception) { }
        }

        public void PostSave()
        {
            
        }

        public void SaveSettings()
        {
            Configuration.HandleMouseInput = cbMouseInput.Checked;
            try
            {
                Configuration.PreassureGamma = gammaValues[gammaIndex];
            }
            catch (Exception) { }
        }

        private void btnGammaMinus_Click(object sender, EventArgs e)
        {
            if (gammaIndex > 0) gammaIndex--;
            try
            {
                pbGraph.Image = GUIElements.GraphRenderer.CreateGammaGraph(pbGraph.Size, gammaValues[gammaIndex]);
                lblGamma.Text = gammaStrings[gammaIndex];
            }
            catch (Exception) { }
        }

        private void btnGammaPlus_Click(object sender, EventArgs e)
        {
            if (gammaIndex < gammaValues.Length - 1) gammaIndex++;
            try
            {
                pbGraph.Image = GUIElements.GraphRenderer.CreateGammaGraph(pbGraph.Size, gammaValues[gammaIndex]);
                lblGamma.Text = gammaStrings[gammaIndex];
            }
            catch (Exception) { }
        }

        private void pbGraph_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                pbGraph.Image = GUIElements.GraphRenderer.CreateGammaGraph(pbGraph.Size, gammaValues[gammaIndex]);
            }
            catch (Exception) { }
        }
    }
}
