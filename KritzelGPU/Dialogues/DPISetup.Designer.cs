namespace Kritzel.Main.Dialogues
{
    partial class DPISetup
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
            this.components = new System.ComponentModel.Container();
            this.tmInput = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmInput
            // 
            this.tmInput.Enabled = true;
            this.tmInput.Interval = 30;
            this.tmInput.Tick += new System.EventHandler(this.tmInput_Tick);
            // 
            // DPISetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 527);
            this.Name = "DPISetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DPISetup";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmInput;
    }
}