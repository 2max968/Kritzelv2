
namespace Kritzel.Main.Dialogues.Settings
{
    partial class SettingsPageView
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
            this.cbDark = new System.Windows.Forms.CheckBox();
            this.cbLefthand = new System.Windows.Forms.CheckBox();
            this.cbShowPenSize = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbDark
            // 
            this.cbDark.AutoSize = true;
            this.cbDark.Location = new System.Drawing.Point(3, 3);
            this.cbDark.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.cbDark.Name = "cbDark";
            this.cbDark.Size = new System.Drawing.Size(224, 29);
            this.cbDark.TabIndex = 0;
            this.cbDark.Text = "Settings.darkMode";
            this.cbDark.UseVisualStyleBackColor = true;
            // 
            // cbLefthand
            // 
            this.cbLefthand.AutoSize = true;
            this.cbLefthand.Location = new System.Drawing.Point(3, 50);
            this.cbLefthand.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.cbLefthand.Name = "cbLefthand";
            this.cbLefthand.Size = new System.Drawing.Size(205, 29);
            this.cbLefthand.TabIndex = 1;
            this.cbLefthand.Text = "Settings.lefthand";
            this.cbLefthand.UseVisualStyleBackColor = true;
            // 
            // cbShowPenSize
            // 
            this.cbShowPenSize.AutoSize = true;
            this.cbShowPenSize.Location = new System.Drawing.Point(3, 97);
            this.cbShowPenSize.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.cbShowPenSize.Name = "cbShowPenSize";
            this.cbShowPenSize.Size = new System.Drawing.Size(358, 29);
            this.cbShowPenSize.TabIndex = 2;
            this.cbShowPenSize.Text = "Settings.showPenSizesInTitlebar";
            this.cbShowPenSize.UseVisualStyleBackColor = true;
            // 
            // SettingsPageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbShowPenSize);
            this.Controls.Add(this.cbLefthand);
            this.Controls.Add(this.cbDark);
            this.Name = "SettingsPageView";
            this.Size = new System.Drawing.Size(669, 603);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbDark;
        private System.Windows.Forms.CheckBox cbLefthand;
        private System.Windows.Forms.CheckBox cbShowPenSize;
    }
}
