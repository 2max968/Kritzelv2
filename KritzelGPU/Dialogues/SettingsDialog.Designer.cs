namespace Kritzel.Main.Dialogues
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbConfPath = new System.Windows.Forms.TextBox();
            this.lblBuildTime = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pbGammaGraph = new System.Windows.Forms.PictureBox();
            this.pnSlider = new System.Windows.Forms.Panel();
            this.lblGamma = new System.Windows.Forms.Label();
            this.cbHandleMouse = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbShowPenSizes = new System.Windows.Forms.CheckBox();
            this.cbCalcSpline = new System.Windows.Forms.CheckBox();
            this.cbRefreshTransform = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.numAutosaveInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.numGuiScale = new Kritzel.Main.GUIElements.NumberFloatBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLefthand = new System.Windows.Forms.CheckBox();
            this.cbDarkMode = new System.Windows.Forms.CheckBox();
            this.pnBottom.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGammaGraph)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosaveInterval)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLanguage
            // 
            this.cbLanguage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbLanguage.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(273, 0);
            this.cbLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(268, 45);
            this.cbLanguage.TabIndex = 0;
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.btnApply);
            this.pnBottom.Controls.Add(this.btnCancel);
            this.pnBottom.Controls.Add(this.btnOk);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 678);
            this.pnBottom.Margin = new System.Windows.Forms.Padding(4);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(1067, 94);
            this.pnBottom.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(425, 19);
            this.btnApply.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(200, 58);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Dialog.apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(636, 19);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 58);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Dialog.cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(849, 19);
            this.btnOk.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(200, 58);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Dialog.ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(128, 50);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1067, 678);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbConfPath);
            this.tabPage1.Controls.Add(this.lblBuildTime);
            this.tabPage1.Controls.Add(this.lblVersion);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(8, 58);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage1.Size = new System.Drawing.Size(1051, 612);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings.general";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbConfPath
            // 
            this.tbConfPath.BackColor = System.Drawing.SystemColors.Window;
            this.tbConfPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbConfPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbConfPath.Location = new System.Drawing.Point(5, 126);
            this.tbConfPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbConfPath.Name = "tbConfPath";
            this.tbConfPath.ReadOnly = true;
            this.tbConfPath.Size = new System.Drawing.Size(1041, 37);
            this.tbConfPath.TabIndex = 3;
            this.tbConfPath.Text = "C:\\...\\conf.*.ini";
            // 
            // lblBuildTime
            // 
            this.lblBuildTime.AutoSize = true;
            this.lblBuildTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBuildTime.Location = new System.Drawing.Point(5, 89);
            this.lblBuildTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuildTime.Name = "lblBuildTime";
            this.lblBuildTime.Size = new System.Drawing.Size(145, 37);
            this.lblBuildTime.TabIndex = 4;
            this.lblBuildTime.Text = "buildtime";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVersion.Location = new System.Drawing.Point(5, 52);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(129, 37);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Kritzel v";
            this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbLanguage);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 6);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1041, 46);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Settings.language";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pbGammaGraph);
            this.tabPage2.Controls.Add(this.pnSlider);
            this.tabPage2.Controls.Add(this.lblGamma);
            this.tabPage2.Controls.Add(this.cbHandleMouse);
            this.tabPage2.Location = new System.Drawing.Point(8, 58);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage2.Size = new System.Drawing.Size(1051, 612);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings.input";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pbGammaGraph
            // 
            this.pbGammaGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGammaGraph.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbGammaGraph.Location = new System.Drawing.Point(5, 192);
            this.pbGammaGraph.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pbGammaGraph.Name = "pbGammaGraph";
            this.pbGammaGraph.Size = new System.Drawing.Size(738, 414);
            this.pbGammaGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGammaGraph.TabIndex = 3;
            this.pbGammaGraph.TabStop = false;
            // 
            // pnSlider
            // 
            this.pnSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnSlider.Location = new System.Drawing.Point(5, 84);
            this.pnSlider.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnSlider.Name = "pnSlider";
            this.pnSlider.Size = new System.Drawing.Size(1041, 108);
            this.pnSlider.TabIndex = 2;
            // 
            // lblGamma
            // 
            this.lblGamma.AutoSize = true;
            this.lblGamma.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGamma.Location = new System.Drawing.Point(5, 47);
            this.lblGamma.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblGamma.Name = "lblGamma";
            this.lblGamma.Size = new System.Drawing.Size(398, 37);
            this.lblGamma.TabIndex = 1;
            this.lblGamma.Text = "Settings.preassureGamma";
            // 
            // cbHandleMouse
            // 
            this.cbHandleMouse.AutoSize = true;
            this.cbHandleMouse.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbHandleMouse.Location = new System.Drawing.Point(5, 6);
            this.cbHandleMouse.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cbHandleMouse.Name = "cbHandleMouse";
            this.cbHandleMouse.Size = new System.Drawing.Size(1041, 41);
            this.cbHandleMouse.TabIndex = 0;
            this.cbHandleMouse.Text = "Settings.handleMouse";
            this.cbHandleMouse.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cbShowPenSizes);
            this.tabPage3.Controls.Add(this.cbCalcSpline);
            this.tabPage3.Controls.Add(this.cbRefreshTransform);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.cbLefthand);
            this.tabPage3.Controls.Add(this.cbDarkMode);
            this.tabPage3.Location = new System.Drawing.Point(8, 58);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1051, 612);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings.performance";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbShowPenSizes
            // 
            this.cbShowPenSizes.AutoSize = true;
            this.cbShowPenSizes.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbShowPenSizes.Location = new System.Drawing.Point(4, 253);
            this.cbShowPenSizes.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowPenSizes.Name = "cbShowPenSizes";
            this.cbShowPenSizes.Size = new System.Drawing.Size(1043, 41);
            this.cbShowPenSizes.TabIndex = 10;
            this.cbShowPenSizes.Text = "Settings.showPenSizesInTitlebar";
            this.cbShowPenSizes.UseVisualStyleBackColor = true;
            // 
            // cbCalcSpline
            // 
            this.cbCalcSpline.AutoSize = true;
            this.cbCalcSpline.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCalcSpline.Location = new System.Drawing.Point(4, 212);
            this.cbCalcSpline.Margin = new System.Windows.Forms.Padding(4);
            this.cbCalcSpline.Name = "cbCalcSpline";
            this.cbCalcSpline.Size = new System.Drawing.Size(1043, 41);
            this.cbCalcSpline.TabIndex = 1;
            this.cbCalcSpline.Text = "Settings.calcSplineWhileDrawing";
            this.cbCalcSpline.UseVisualStyleBackColor = true;
            // 
            // cbRefreshTransform
            // 
            this.cbRefreshTransform.AutoSize = true;
            this.cbRefreshTransform.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbRefreshTransform.Location = new System.Drawing.Point(4, 171);
            this.cbRefreshTransform.Margin = new System.Windows.Forms.Padding(4);
            this.cbRefreshTransform.Name = "cbRefreshTransform";
            this.cbRefreshTransform.Size = new System.Drawing.Size(1043, 41);
            this.cbRefreshTransform.TabIndex = 0;
            this.cbRefreshTransform.Text = "Settings.refreshOnTransform";
            this.cbRefreshTransform.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.numAutosaveInterval);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 132);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 39);
            this.panel1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(533, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 37);
            this.label5.TabIndex = 4;
            this.label5.Text = "s";
            // 
            // numAutosaveInterval
            // 
            this.numAutosaveInterval.Dock = System.Windows.Forms.DockStyle.Left;
            this.numAutosaveInterval.Location = new System.Drawing.Point(373, 0);
            this.numAutosaveInterval.Margin = new System.Windows.Forms.Padding(4);
            this.numAutosaveInterval.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.numAutosaveInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numAutosaveInterval.Name = "numAutosaveInterval";
            this.numAutosaveInterval.Size = new System.Drawing.Size(160, 44);
            this.numAutosaveInterval.TabIndex = 3;
            this.numAutosaveInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(373, 37);
            this.label4.TabIndex = 2;
            this.label4.Text = "Settings.autosaveInterval";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.numGuiScale);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 86);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1043, 46);
            this.panel3.TabIndex = 6;
            // 
            // numGuiScale
            // 
            this.numGuiScale.BackColor = System.Drawing.SystemColors.Window;
            this.numGuiScale.Dock = System.Windows.Forms.DockStyle.Left;
            this.numGuiScale.Location = new System.Drawing.Point(263, 0);
            this.numGuiScale.Margin = new System.Windows.Forms.Padding(4);
            this.numGuiScale.Max = 2F;
            this.numGuiScale.Min = 0.5F;
            this.numGuiScale.Name = "numGuiScale";
            this.numGuiScale.Size = new System.Drawing.Size(132, 44);
            this.numGuiScale.TabIndex = 8;
            this.numGuiScale.Text = "0";
            this.numGuiScale.Value = 0F;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 37);
            this.label2.TabIndex = 7;
            this.label2.Text = "Settings.guiScale";
            // 
            // cbLefthand
            // 
            this.cbLefthand.AutoSize = true;
            this.cbLefthand.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbLefthand.Location = new System.Drawing.Point(4, 45);
            this.cbLefthand.Margin = new System.Windows.Forms.Padding(4);
            this.cbLefthand.Name = "cbLefthand";
            this.cbLefthand.Size = new System.Drawing.Size(1043, 41);
            this.cbLefthand.TabIndex = 8;
            this.cbLefthand.Text = "Settings.lefthand";
            this.cbLefthand.UseVisualStyleBackColor = true;
            // 
            // cbDarkMode
            // 
            this.cbDarkMode.AutoSize = true;
            this.cbDarkMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDarkMode.Location = new System.Drawing.Point(4, 4);
            this.cbDarkMode.Margin = new System.Windows.Forms.Padding(4);
            this.cbDarkMode.Name = "cbDarkMode";
            this.cbDarkMode.Size = new System.Drawing.Size(1043, 41);
            this.cbDarkMode.TabIndex = 9;
            this.cbDarkMode.Text = "Settings.darkMode";
            this.cbDarkMode.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 772);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnBottom);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings.settings";
            this.pnBottom.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGammaGraph)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosaveInterval)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblGamma;
        private System.Windows.Forms.CheckBox cbHandleMouse;
        private System.Windows.Forms.Panel pnSlider;
        private System.Windows.Forms.PictureBox pbGammaGraph;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox cbRefreshTransform;
        private System.Windows.Forms.CheckBox cbCalcSpline;
        private System.Windows.Forms.NumericUpDown numAutosaveInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblBuildTime;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private GUIElements.NumberFloatBox numGuiScale;
        private System.Windows.Forms.CheckBox cbLefthand;
        private System.Windows.Forms.TextBox tbConfPath;
        private System.Windows.Forms.CheckBox cbDarkMode;
        private System.Windows.Forms.CheckBox cbShowPenSizes;
    }
}