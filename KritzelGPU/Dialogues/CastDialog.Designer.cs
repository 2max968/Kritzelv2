namespace Kritzel.Main.Dialogues
{
    partial class CastDialog
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
            this.btnStartHTTP = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnWebStart = new System.Windows.Forms.Button();
            this.tbWebId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbWebHostname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCurrentIP = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbHttpPort = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartHTTP
            // 
            this.btnStartHTTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartHTTP.Location = new System.Drawing.Point(244, 145);
            this.btnStartHTTP.Name = "btnStartHTTP";
            this.btnStartHTTP.Size = new System.Drawing.Size(211, 63);
            this.btnStartHTTP.TabIndex = 0;
            this.btnStartHTTP.Text = "Cast.start";
            this.btnStartHTTP.UseVisualStyleBackColor = true;
            this.btnStartHTTP.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(471, 249);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnWebStart);
            this.tabPage2.Controls.Add(this.tbWebId);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.tbWebHostname);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(463, 216);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cast.web";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnWebStart
            // 
            this.btnWebStart.Location = new System.Drawing.Point(228, 146);
            this.btnWebStart.Name = "btnWebStart";
            this.btnWebStart.Size = new System.Drawing.Size(227, 62);
            this.btnWebStart.TabIndex = 4;
            this.btnWebStart.Text = "Cast.start";
            this.btnWebStart.UseVisualStyleBackColor = true;
            this.btnWebStart.Click += new System.EventHandler(this.btnWebStart_Click);
            // 
            // tbWebId
            // 
            this.tbWebId.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbWebId.Location = new System.Drawing.Point(3, 69);
            this.tbWebId.Name = "tbWebId";
            this.tbWebId.Size = new System.Drawing.Size(457, 26);
            this.tbWebId.TabIndex = 3;
            this.tbWebId.Text = "1234";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Cast.web.id";
            // 
            // tbWebHostname
            // 
            this.tbWebHostname.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbWebHostname.Location = new System.Drawing.Point(3, 23);
            this.tbWebHostname.Name = "tbWebHostname";
            this.tbWebHostname.Size = new System.Drawing.Size(457, 26);
            this.tbWebHostname.TabIndex = 2;
            this.tbWebHostname.Text = "kritzel.maxpage.de";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cast.web.host";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblCurrentIP);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbHttpPort);
            this.tabPage1.Controls.Add(this.btnStartHTTP);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(463, 216);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cast.http";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblCurrentIP
            // 
            this.lblCurrentIP.AutoSize = true;
            this.lblCurrentIP.Location = new System.Drawing.Point(8, 42);
            this.lblCurrentIP.Name = "lblCurrentIP";
            this.lblCurrentIP.Size = new System.Drawing.Size(92, 20);
            this.lblCurrentIP.TabIndex = 2;
            this.lblCurrentIP.TabStop = true;
            this.lblCurrentIP.Text = "lblCurrentIP";
            this.lblCurrentIP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCurrentIP_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cast.http.port";
            // 
            // tbHttpPort
            // 
            this.tbHttpPort.Location = new System.Drawing.Point(147, 6);
            this.tbHttpPort.Name = "tbHttpPort";
            this.tbHttpPort.Size = new System.Drawing.Size(100, 26);
            this.tbHttpPort.TabIndex = 2;
            this.tbHttpPort.Text = "80";
            // 
            // CastDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 249);
            this.Controls.Add(this.tabControl1);
            this.Name = "CastDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cast.title";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartHTTP;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHttpPort;
        private System.Windows.Forms.LinkLabel lblCurrentIP;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnWebStart;
        private System.Windows.Forms.TextBox tbWebId;
        private System.Windows.Forms.TextBox tbWebHostname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}