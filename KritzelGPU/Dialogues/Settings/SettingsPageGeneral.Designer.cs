
namespace Kritzel.Main.Dialogues.Settings
{
    partial class SettingsPageGeneral
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
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.btnSelectLanguage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbInfo
            // 
            this.tbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInfo.Location = new System.Drawing.Point(3, 3);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.ReadOnly = true;
            this.tbInfo.Size = new System.Drawing.Size(741, 226);
            this.tbInfo.TabIndex = 0;
            // 
            // btnSelectLanguage
            // 
            this.btnSelectLanguage.Location = new System.Drawing.Point(3, 235);
            this.btnSelectLanguage.Name = "btnSelectLanguage";
            this.btnSelectLanguage.Size = new System.Drawing.Size(176, 71);
            this.btnSelectLanguage.TabIndex = 1;
            this.btnSelectLanguage.Text = "button1";
            this.btnSelectLanguage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectLanguage.UseVisualStyleBackColor = true;
            this.btnSelectLanguage.Click += new System.EventHandler(this.btnSelectLanguage_Click);
            // 
            // SettingsPageGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectLanguage);
            this.Controls.Add(this.tbInfo);
            this.Name = "SettingsPageGeneral";
            this.Size = new System.Drawing.Size(747, 492);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Button btnSelectLanguage;
    }
}
