namespace Kritzel.Main.Dialogues
{
    partial class LayoutEditor
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
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnC1 = new System.Windows.Forms.Button();
            this.btnC2 = new System.Windows.Forms.Button();
            this.cbShowDate = new System.Windows.Forms.CheckBox();
            this.iconlist = new System.Windows.Forms.ImageList(this.components);
            this.cbInvertColor = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lvBackgrounds = new Kritzel.Main.GUIElements.TListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnMinus
            // 
            this.btnMinus.FlatAppearance.BorderSize = 0;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinus.Location = new System.Drawing.Point(20, 15);
            this.btnMinus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(104, 80);
            this.btnMinus.TabIndex = 1;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlus.FlatAppearance.BorderSize = 0;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlus.Location = new System.Drawing.Point(536, 15);
            this.btnPlus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(104, 80);
            this.btnPlus.TabIndex = 2;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(132, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 80);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnC1
            // 
            this.btnC1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnC1.FlatAppearance.BorderSize = 0;
            this.btnC1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnC1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnC1.Location = new System.Drawing.Point(20, 667);
            this.btnC1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnC1.Name = "btnC1";
            this.btnC1.Size = new System.Drawing.Size(279, 62);
            this.btnC1.TabIndex = 4;
            this.btnC1.Text = "Layout.mainColor";
            this.btnC1.UseVisualStyleBackColor = true;
            this.btnC1.Click += new System.EventHandler(this.btnC1_Click);
            // 
            // btnC2
            // 
            this.btnC2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnC2.FlatAppearance.BorderSize = 0;
            this.btnC2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnC2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnC2.Location = new System.Drawing.Point(360, 667);
            this.btnC2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnC2.Name = "btnC2";
            this.btnC2.Size = new System.Drawing.Size(279, 62);
            this.btnC2.TabIndex = 5;
            this.btnC2.Text = "Layout.secondaryColor";
            this.btnC2.UseVisualStyleBackColor = true;
            this.btnC2.Click += new System.EventHandler(this.btnC2_Click);
            // 
            // cbShowDate
            // 
            this.cbShowDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowDate.AutoSize = true;
            this.cbShowDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowDate.Location = new System.Drawing.Point(20, 604);
            this.cbShowDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbShowDate.Name = "cbShowDate";
            this.cbShowDate.Size = new System.Drawing.Size(393, 55);
            this.cbShowDate.TabIndex = 1;
            this.cbShowDate.Text = "Layout.showTime";
            this.cbShowDate.UseVisualStyleBackColor = true;
            this.cbShowDate.CheckedChanged += new System.EventHandler(this.cbShowDate_CheckedChanged);
            // 
            // iconlist
            // 
            this.iconlist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.iconlist.ImageSize = new System.Drawing.Size(16, 16);
            this.iconlist.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbInvertColor
            // 
            this.cbInvertColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbInvertColor.AutoSize = true;
            this.cbInvertColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cbInvertColor.Location = new System.Drawing.Point(20, 541);
            this.cbInvertColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbInvertColor.Name = "cbInvertColor";
            this.cbInvertColor.Size = new System.Drawing.Size(302, 55);
            this.cbInvertColor.TabIndex = 7;
            this.cbInvertColor.Text = "Layout.invert";
            this.cbInvertColor.UseVisualStyleBackColor = true;
            this.cbInvertColor.CheckedChanged += new System.EventHandler(this.cbInvertColor_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(20, 736);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(619, 62);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Dialog.close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lvBackgrounds
            // 
            this.lvBackgrounds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBackgrounds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvBackgrounds.FullRowSelect = true;
            this.lvBackgrounds.HideSelection = false;
            this.lvBackgrounds.LargeImageList = this.iconlist;
            this.lvBackgrounds.Location = new System.Drawing.Point(20, 102);
            this.lvBackgrounds.Margin = new System.Windows.Forms.Padding(4);
            this.lvBackgrounds.MultiSelect = false;
            this.lvBackgrounds.Name = "lvBackgrounds";
            this.lvBackgrounds.Size = new System.Drawing.Size(619, 431);
            this.lvBackgrounds.SmallImageList = this.iconlist;
            this.lvBackgrounds.TabIndex = 6;
            this.lvBackgrounds.UseCompatibleStateImageBehavior = false;
            this.lvBackgrounds.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Backgrounds";
            this.columnHeader1.Width = 419;
            // 
            // LayoutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 853);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbInvertColor);
            this.Controls.Add(this.lvBackgrounds);
            this.Controls.Add(this.btnC2);
            this.Controls.Add(this.cbShowDate);
            this.Controls.Add(this.btnC1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(682, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(682, 71);
            this.Name = "LayoutEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Layout.title";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnC1;
        private System.Windows.Forms.Button btnC2;
        private System.Windows.Forms.CheckBox cbShowDate;
        private GUIElements.TListView lvBackgrounds;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList iconlist;
        private System.Windows.Forms.CheckBox cbInvertColor;
        private System.Windows.Forms.Button btnClose;
    }
}