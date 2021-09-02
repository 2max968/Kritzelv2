
namespace Kritzel.Main.Dialogues.Settings
{
    partial class SettingsPagePerformance
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbRefresh = new System.Windows.Forms.CheckBox();
            this.cbCalcSpline = new System.Windows.Forms.CheckBox();
            this.tbAutosaveInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbRefresh
            // 
            this.cbRefresh.AutoSize = true;
            this.cbRefresh.Location = new System.Drawing.Point(9, 9);
            this.cbRefresh.Margin = new System.Windows.Forms.Padding(9);
            this.cbRefresh.Name = "cbRefresh";
            this.cbRefresh.Size = new System.Drawing.Size(320, 29);
            this.cbRefresh.TabIndex = 0;
            this.cbRefresh.Text = "Settings.refreshOnTransform";
            this.cbRefresh.UseVisualStyleBackColor = true;
            // 
            // cbCalcSpline
            // 
            this.cbCalcSpline.AutoSize = true;
            this.cbCalcSpline.Location = new System.Drawing.Point(9, 56);
            this.cbCalcSpline.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.cbCalcSpline.Name = "cbCalcSpline";
            this.cbCalcSpline.Size = new System.Drawing.Size(359, 29);
            this.cbCalcSpline.TabIndex = 1;
            this.cbCalcSpline.Text = "Settings.calcSplineWhileDrawing";
            this.cbCalcSpline.UseVisualStyleBackColor = true;
            // 
            // tbAutosaveInterval
            // 
            this.tbAutosaveInterval.Location = new System.Drawing.Point(263, 103);
            this.tbAutosaveInterval.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.tbAutosaveInterval.Name = "tbAutosaveInterval";
            this.tbAutosaveInterval.Size = new System.Drawing.Size(128, 31);
            this.tbAutosaveInterval.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Settings.autosaveInterval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(388, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "s";
            // 
            // SettingsPagePerformance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAutosaveInterval);
            this.Controls.Add(this.cbCalcSpline);
            this.Controls.Add(this.cbRefresh);
            this.Controls.Add(this.label2);
            this.Name = "SettingsPagePerformance";
            this.Size = new System.Drawing.Size(770, 571);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbRefresh;
        private System.Windows.Forms.CheckBox cbCalcSpline;
        private System.Windows.Forms.TextBox tbAutosaveInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
