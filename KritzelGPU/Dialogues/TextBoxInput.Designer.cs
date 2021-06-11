namespace Kritzel.Main.Dialogues
{
    partial class TextBoxInput
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
            this.tbContent = new System.Windows.Forms.TextBox();
            this.cbFontFamilies = new System.Windows.Forms.ComboBox();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.cbAlignment = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // tbContent
            // 
            this.tbContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbContent.Location = new System.Drawing.Point(12, 46);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(507, 260);
            this.tbContent.TabIndex = 0;
            this.tbContent.TextChanged += new System.EventHandler(this.tbContent_TextChanged);
            // 
            // cbFontFamilies
            // 
            this.cbFontFamilies.FormattingEnabled = true;
            this.cbFontFamilies.Location = new System.Drawing.Point(12, 12);
            this.cbFontFamilies.Name = "cbFontFamilies";
            this.cbFontFamilies.Size = new System.Drawing.Size(216, 28);
            this.cbFontFamilies.TabIndex = 1;
            this.cbFontFamilies.Text = "Arial";
            this.cbFontFamilies.SelectedIndexChanged += new System.EventHandler(this.cbFontFamilies_SelectedIndexChanged);
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(234, 13);
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(120, 26);
            this.numFontSize.TabIndex = 2;
            this.numFontSize.ValueChanged += new System.EventHandler(this.numFontSize_ValueChanged);
            // 
            // cbAlignment
            // 
            this.cbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlignment.FormattingEnabled = true;
            this.cbAlignment.Location = new System.Drawing.Point(360, 12);
            this.cbAlignment.Name = "cbAlignment";
            this.cbAlignment.Size = new System.Drawing.Size(159, 28);
            this.cbAlignment.TabIndex = 3;
            this.cbAlignment.SelectedIndexChanged += new System.EventHandler(this.cbAlignment_SelectedIndexChanged);
            // 
            // TextBoxInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 318);
            this.Controls.Add(this.cbAlignment);
            this.Controls.Add(this.numFontSize);
            this.Controls.Add(this.cbFontFamilies);
            this.Controls.Add(this.tbContent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextBoxInput";
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.ComboBox cbFontFamilies;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.ComboBox cbAlignment;
    }
}