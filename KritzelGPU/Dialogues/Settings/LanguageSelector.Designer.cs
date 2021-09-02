
namespace Kritzel.Main.Dialogues.Settings
{
    partial class LanguageSelector
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
            this.lvLangs = new System.Windows.Forms.ListView();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvLangs
            // 
            this.lvLangs.CheckBoxes = true;
            this.lvLangs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLangs.HideSelection = false;
            this.lvLangs.Location = new System.Drawing.Point(0, 0);
            this.lvLangs.Name = "lvLangs";
            this.lvLangs.Size = new System.Drawing.Size(923, 491);
            this.lvLangs.TabIndex = 0;
            this.lvLangs.UseCompatibleStateImageBehavior = false;
            this.lvLangs.View = System.Windows.Forms.View.List;
            this.lvLangs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvLangs_ItemCheck);
            this.lvLangs.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvLangs_ItemChecked);
            this.lvLangs.SelectedIndexChanged += new System.EventHandler(this.lvLangs_SelectedIndexChanged);
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.btnDelete);
            this.pnBottom.Controls.Add(this.btnImport);
            this.pnBottom.Controls.Add(this.btnOk);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 491);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(923, 100);
            this.pnBottom.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(709, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(202, 82);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Dialog.ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 6);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(202, 82);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Language.import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(220, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(202, 82);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Language.delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // LanguageSelector
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 591);
            this.Controls.Add(this.lvLangs);
            this.Controls.Add(this.pnBottom);
            this.Name = "LanguageSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LanguageSelector";
            this.pnBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvLangs;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnDelete;
    }
}