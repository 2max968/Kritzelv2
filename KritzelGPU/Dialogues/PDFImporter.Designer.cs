namespace Kritzel.Main.Dialogues
{
    partial class PDFImporter
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
            this.lvPages = new System.Windows.Forms.ListView();
            this.imgPages = new System.Windows.Forms.ImageList(this.components);
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnDeselect = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnProgress = new System.Windows.Forms.Panel();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPages
            // 
            this.lvPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPages.CheckBoxes = true;
            this.lvPages.HideSelection = false;
            this.lvPages.LargeImageList = this.imgPages;
            this.lvPages.Location = new System.Drawing.Point(12, 12);
            this.lvPages.Name = "lvPages";
            this.lvPages.Size = new System.Drawing.Size(776, 367);
            this.lvPages.SmallImageList = this.imgPages;
            this.lvPages.TabIndex = 0;
            this.lvPages.UseCompatibleStateImageBehavior = false;
            // 
            // imgPages
            // 
            this.imgPages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgPages.ImageSize = new System.Drawing.Size(128, 128);
            this.imgPages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.Location = new System.Drawing.Point(12, 385);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(167, 53);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Select All";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeselect.Location = new System.Drawing.Point(185, 385);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(167, 53);
            this.btnDeselect.TabIndex = 2;
            this.btnDeselect.Text = "Deselect All";
            this.btnDeselect.UseVisualStyleBackColor = true;
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(621, 385);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(167, 53);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(448, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(167, 53);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnProgress
            // 
            this.pnProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnProgress.Controls.Add(this.lblProgress);
            this.pnProgress.Controls.Add(this.pbProgress);
            this.pnProgress.Location = new System.Drawing.Point(12, 12);
            this.pnProgress.Name = "pnProgress";
            this.pnProgress.Size = new System.Drawing.Size(776, 64);
            this.pnProgress.TabIndex = 5;
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbProgress.Location = new System.Drawing.Point(0, 0);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(776, 30);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProgress.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProgress.Location = new System.Drawing.Point(0, 30);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(66, 20);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "Loading";
            // 
            // PDFImporter
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnProgress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDeselect);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lvPages);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PDFImporter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "PDFImporter";
            this.pnProgress.ResumeLayout(false);
            this.pnProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPages;
        private System.Windows.Forms.ImageList imgPages;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnDeselect;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnProgress;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lblProgress;
    }
}