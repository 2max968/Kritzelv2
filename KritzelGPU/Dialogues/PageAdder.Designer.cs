namespace Kritzel.Main.Dialogues
{
    partial class PageAdder
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblAddPDF = new System.Windows.Forms.LinkLabel();
            this.pnPDF = new System.Windows.Forms.Panel();
            this.lblPDF2 = new System.Windows.Forms.Label();
            this.lblPDF1 = new System.Windows.Forms.Label();
            this.pbPDFIcon = new System.Windows.Forms.PictureBox();
            this.lblAddImage = new System.Windows.Forms.LinkLabel();
            this.panelBottom.SuspendLayout();
            this.pnPDF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPDFIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 73;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(581, 81);
            this.comboBox1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(413, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(168, 125);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Newpage.add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelBottom.Controls.Add(this.btnAdd);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 635);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(581, 125);
            this.panelBottom.TabIndex = 5;
            // 
            // lblAddPDF
            // 
            this.lblAddPDF.AutoSize = true;
            this.lblAddPDF.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAddPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblAddPDF.Location = new System.Drawing.Point(0, 187);
            this.lblAddPDF.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAddPDF.Name = "lblAddPDF";
            this.lblAddPDF.Size = new System.Drawing.Size(388, 51);
            this.lblAddPDF.TabIndex = 6;
            this.lblAddPDF.TabStop = true;
            this.lblAddPDF.Text = "Newpage.fromPDF";
            this.lblAddPDF.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pnPDF
            // 
            this.pnPDF.Controls.Add(this.lblPDF2);
            this.pnPDF.Controls.Add(this.lblPDF1);
            this.pnPDF.Controls.Add(this.pbPDFIcon);
            this.pnPDF.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnPDF.Location = new System.Drawing.Point(0, 81);
            this.pnPDF.Margin = new System.Windows.Forms.Padding(4);
            this.pnPDF.Name = "pnPDF";
            this.pnPDF.Size = new System.Drawing.Size(581, 106);
            this.pnPDF.TabIndex = 8;
            this.pnPDF.Visible = false;
            // 
            // lblPDF2
            // 
            this.lblPDF2.AutoSize = true;
            this.lblPDF2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPDF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPDF2.Location = new System.Drawing.Point(133, 25);
            this.lblPDF2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPDF2.Name = "lblPDF2";
            this.lblPDF2.Size = new System.Drawing.Size(111, 26);
            this.lblPDF2.TabIndex = 2;
            this.lblPDF2.Text = "name.pdf";
            // 
            // lblPDF1
            // 
            this.lblPDF1.AutoSize = true;
            this.lblPDF1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPDF1.Location = new System.Drawing.Point(133, 0);
            this.lblPDF1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPDF1.Name = "lblPDF1";
            this.lblPDF1.Size = new System.Drawing.Size(139, 25);
            this.lblPDF1.TabIndex = 1;
            this.lblPDF1.Text = "X pages from";
            // 
            // pbPDFIcon
            // 
            this.pbPDFIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbPDFIcon.Location = new System.Drawing.Point(0, 0);
            this.pbPDFIcon.Margin = new System.Windows.Forms.Padding(4);
            this.pbPDFIcon.Name = "pbPDFIcon";
            this.pbPDFIcon.Size = new System.Drawing.Size(133, 106);
            this.pbPDFIcon.TabIndex = 0;
            this.pbPDFIcon.TabStop = false;
            // 
            // lblAddImage
            // 
            this.lblAddImage.AutoSize = true;
            this.lblAddImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAddImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddImage.Location = new System.Drawing.Point(0, 238);
            this.lblAddImage.Name = "lblAddImage";
            this.lblAddImage.Size = new System.Drawing.Size(422, 51);
            this.lblAddImage.TabIndex = 9;
            this.lblAddImage.TabStop = true;
            this.lblAddImage.Text = "Newpage.fromImage";
            this.lblAddImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAddImage_LinkClicked);
            // 
            // PageAdder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblAddImage);
            this.Controls.Add(this.lblAddPDF);
            this.Controls.Add(this.pnPDF);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panelBottom);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PageAdder";
            this.Size = new System.Drawing.Size(581, 760);
            this.panelBottom.ResumeLayout(false);
            this.pnPDF.ResumeLayout(false);
            this.pnPDF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPDFIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.LinkLabel lblAddPDF;
        private System.Windows.Forms.Panel pnPDF;
        private System.Windows.Forms.Label lblPDF2;
        private System.Windows.Forms.Label lblPDF1;
        private System.Windows.Forms.PictureBox pbPDFIcon;
        private System.Windows.Forms.LinkLabel lblAddImage;
    }
}