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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pnPDF = new System.Windows.Forms.Panel();
            this.lblPDF2 = new System.Windows.Forms.Label();
            this.lblPDF1 = new System.Windows.Forms.Label();
            this.pbPDFIcon = new System.Windows.Forms.PictureBox();
            this.isPosition = new Kritzel.Main.GUIElements.ItemSelector();
            this.panelBottom.SuspendLayout();
            this.pnPDF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPDFIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 55;
            this.comboBox1.Location = new System.Drawing.Point(0, 181);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(436, 63);
            this.comboBox1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(310, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(126, 100);
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
            this.panelBottom.Location = new System.Drawing.Point(0, 508);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(436, 100);
            this.panelBottom.TabIndex = 5;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.linkLabel1.Location = new System.Drawing.Point(0, 329);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(290, 37);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Newpage.fromPDF";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pnPDF
            // 
            this.pnPDF.Controls.Add(this.lblPDF2);
            this.pnPDF.Controls.Add(this.lblPDF1);
            this.pnPDF.Controls.Add(this.pbPDFIcon);
            this.pnPDF.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnPDF.Location = new System.Drawing.Point(0, 244);
            this.pnPDF.Name = "pnPDF";
            this.pnPDF.Size = new System.Drawing.Size(436, 85);
            this.pnPDF.TabIndex = 8;
            this.pnPDF.Visible = false;
            // 
            // lblPDF2
            // 
            this.lblPDF2.AutoSize = true;
            this.lblPDF2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPDF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPDF2.Location = new System.Drawing.Point(100, 20);
            this.lblPDF2.Name = "lblPDF2";
            this.lblPDF2.Size = new System.Drawing.Size(84, 20);
            this.lblPDF2.TabIndex = 2;
            this.lblPDF2.Text = "name.pdf";
            // 
            // lblPDF1
            // 
            this.lblPDF1.AutoSize = true;
            this.lblPDF1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPDF1.Location = new System.Drawing.Point(100, 0);
            this.lblPDF1.Name = "lblPDF1";
            this.lblPDF1.Size = new System.Drawing.Size(104, 20);
            this.lblPDF1.TabIndex = 1;
            this.lblPDF1.Text = "X pages from";
            // 
            // pbPDFIcon
            // 
            this.pbPDFIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbPDFIcon.Location = new System.Drawing.Point(0, 0);
            this.pbPDFIcon.Name = "pbPDFIcon";
            this.pbPDFIcon.Size = new System.Drawing.Size(100, 85);
            this.pbPDFIcon.TabIndex = 0;
            this.pbPDFIcon.TabStop = false;
            // 
            // isPosition
            // 
            this.isPosition.Dock = System.Windows.Forms.DockStyle.Top;
            this.isPosition.Items = new string[0];
            this.isPosition.Location = new System.Drawing.Point(0, 0);
            this.isPosition.Name = "isPosition";
            this.isPosition.SelectedIndex = 0;
            this.isPosition.Size = new System.Drawing.Size(436, 181);
            this.isPosition.TabIndex = 7;
            this.isPosition.Text = "isPosition";
            // 
            // PageAdder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pnPDF);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.isPosition);
            this.Name = "PageAdder";
            this.Size = new System.Drawing.Size(436, 608);
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
        private System.Windows.Forms.LinkLabel linkLabel1;
        private GUIElements.ItemSelector isPosition;
        private System.Windows.Forms.Panel pnPDF;
        private System.Windows.Forms.Label lblPDF2;
        private System.Windows.Forms.Label lblPDF1;
        private System.Windows.Forms.PictureBox pbPDFIcon;
    }
}