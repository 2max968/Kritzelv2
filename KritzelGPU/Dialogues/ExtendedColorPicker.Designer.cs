namespace Kritzel.Main.Dialogues
{
    partial class ExtendedColorPicker
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbRed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbGreen = new System.Windows.Forms.TrackBar();
            this.tbBlue = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.pnPreview = new System.Windows.Forms.Panel();
            this.tbRGB = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(725, 489);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbRGB);
            this.tabPage1.Controls.Add(this.tbBlue);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbGreen);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tbRed);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(717, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "RGB";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 67);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HSL";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.pnPreview);
            this.pnBottom.Controls.Add(this.btnCancel);
            this.pnBottom.Controls.Add(this.btnOk);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 388);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(725, 101);
            this.pnBottom.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(650, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 101);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Dialog.ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(575, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 101);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Dialog.cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbRed
            // 
            this.tbRed.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbRed.Location = new System.Drawing.Point(3, 23);
            this.tbRed.Maximum = 255;
            this.tbRed.Name = "tbRed";
            this.tbRed.Size = new System.Drawing.Size(711, 69);
            this.tbRed.TabIndex = 0;
            this.tbRed.Value = 255;
            this.tbRed.ValueChanged += new System.EventHandler(this.tbRed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Red:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Green:";
            // 
            // tbGreen
            // 
            this.tbGreen.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbGreen.Location = new System.Drawing.Point(3, 112);
            this.tbGreen.Maximum = 255;
            this.tbGreen.Name = "tbGreen";
            this.tbGreen.Size = new System.Drawing.Size(711, 69);
            this.tbGreen.TabIndex = 3;
            this.tbGreen.Value = 255;
            this.tbGreen.ValueChanged += new System.EventHandler(this.tbRed_ValueChanged);
            // 
            // tbBlue
            // 
            this.tbBlue.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbBlue.Location = new System.Drawing.Point(3, 201);
            this.tbBlue.Maximum = 255;
            this.tbBlue.Name = "tbBlue";
            this.tbBlue.Size = new System.Drawing.Size(711, 69);
            this.tbBlue.TabIndex = 4;
            this.tbBlue.Value = 255;
            this.tbBlue.ValueChanged += new System.EventHandler(this.tbRed_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Blue:";
            // 
            // pnPreview
            // 
            this.pnPreview.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnPreview.Location = new System.Drawing.Point(0, 0);
            this.pnPreview.Name = "pnPreview";
            this.pnPreview.Size = new System.Drawing.Size(214, 101);
            this.pnPreview.TabIndex = 2;
            // 
            // tbRGB
            // 
            this.tbRGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbRGB.Location = new System.Drawing.Point(3, 270);
            this.tbRGB.Name = "tbRGB";
            this.tbRGB.Size = new System.Drawing.Size(711, 26);
            this.tbRGB.TabIndex = 6;
            this.tbRGB.TextChanged += new System.EventHandler(this.tbRGB_TextChanged);
            // 
            // ExtendedColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 489);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.tabControl1);
            this.Name = "ExtendedColorPicker";
            this.Text = "ExtendedColorPicker";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBlue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TrackBar tbBlue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbGreen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbRed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnPreview;
        private System.Windows.Forms.TextBox tbRGB;
    }
}