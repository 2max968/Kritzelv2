
namespace Kritzel.Main.Dialogues.Settings
{
    partial class SettingsPageInput
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
            this.cbMouseInput = new System.Windows.Forms.CheckBox();
            this.btnGammaMinus = new System.Windows.Forms.Button();
            this.btnGammaPlus = new System.Windows.Forms.Button();
            this.pnGamma = new System.Windows.Forms.Panel();
            this.lblGamma = new System.Windows.Forms.Label();
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.pnGamma.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // cbMouseInput
            // 
            this.cbMouseInput.AutoSize = true;
            this.cbMouseInput.Location = new System.Drawing.Point(3, 3);
            this.cbMouseInput.Name = "cbMouseInput";
            this.cbMouseInput.Size = new System.Drawing.Size(258, 29);
            this.cbMouseInput.TabIndex = 0;
            this.cbMouseInput.Text = "Settings.handleMouse";
            this.cbMouseInput.UseVisualStyleBackColor = true;
            // 
            // btnGammaMinus
            // 
            this.btnGammaMinus.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnGammaMinus.Location = new System.Drawing.Point(0, 0);
            this.btnGammaMinus.Name = "btnGammaMinus";
            this.btnGammaMinus.Size = new System.Drawing.Size(75, 100);
            this.btnGammaMinus.TabIndex = 1;
            this.btnGammaMinus.Text = "-";
            this.btnGammaMinus.UseVisualStyleBackColor = true;
            this.btnGammaMinus.Click += new System.EventHandler(this.btnGammaMinus_Click);
            // 
            // btnGammaPlus
            // 
            this.btnGammaPlus.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGammaPlus.Location = new System.Drawing.Point(351, 0);
            this.btnGammaPlus.Name = "btnGammaPlus";
            this.btnGammaPlus.Size = new System.Drawing.Size(75, 100);
            this.btnGammaPlus.TabIndex = 2;
            this.btnGammaPlus.Text = "+";
            this.btnGammaPlus.UseVisualStyleBackColor = true;
            this.btnGammaPlus.Click += new System.EventHandler(this.btnGammaPlus_Click);
            // 
            // pnGamma
            // 
            this.pnGamma.Controls.Add(this.lblGamma);
            this.pnGamma.Controls.Add(this.btnGammaMinus);
            this.pnGamma.Controls.Add(this.btnGammaPlus);
            this.pnGamma.Location = new System.Drawing.Point(3, 38);
            this.pnGamma.Name = "pnGamma";
            this.pnGamma.Size = new System.Drawing.Size(426, 100);
            this.pnGamma.TabIndex = 3;
            // 
            // lblGamma
            // 
            this.lblGamma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGamma.Location = new System.Drawing.Point(75, 0);
            this.lblGamma.Name = "lblGamma";
            this.lblGamma.Size = new System.Drawing.Size(276, 100);
            this.lblGamma.TabIndex = 3;
            this.lblGamma.Text = "label1";
            this.lblGamma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbGraph
            // 
            this.pbGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGraph.Location = new System.Drawing.Point(3, 141);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(901, 418);
            this.pbGraph.TabIndex = 4;
            this.pbGraph.TabStop = false;
            this.pbGraph.SizeChanged += new System.EventHandler(this.pbGraph_SizeChanged);
            // 
            // SettingsPageInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnGamma);
            this.Controls.Add(this.cbMouseInput);
            this.Controls.Add(this.pbGraph);
            this.Name = "SettingsPageInput";
            this.Size = new System.Drawing.Size(923, 577);
            this.pnGamma.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbMouseInput;
        private System.Windows.Forms.Button btnGammaMinus;
        private System.Windows.Forms.Button btnGammaPlus;
        private System.Windows.Forms.Panel pnGamma;
        private System.Windows.Forms.Label lblGamma;
        private System.Windows.Forms.PictureBox pbGraph;
    }
}
