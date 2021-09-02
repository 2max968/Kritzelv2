
namespace Kritzel.Main.Dialogues.Settings
{
    partial class SettingsPagePalettes
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
            this.lvColors = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnColorAdd = new System.Windows.Forms.Button();
            this.btnColorUp = new System.Windows.Forms.Button();
            this.btnColorDown = new System.Windows.Forms.Button();
            this.btnColorDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvColors
            // 
            this.lvColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvColors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvColors.FullRowSelect = true;
            this.lvColors.HideSelection = false;
            this.lvColors.Location = new System.Drawing.Point(3, 3);
            this.lvColors.Name = "lvColors";
            this.lvColors.OwnerDraw = true;
            this.lvColors.Size = new System.Drawing.Size(339, 488);
            this.lvColors.TabIndex = 0;
            this.lvColors.UseCompatibleStateImageBehavior = false;
            this.lvColors.View = System.Windows.Forms.View.Details;
            this.lvColors.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvColors_DrawItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 265;
            // 
            // btnColorAdd
            // 
            this.btnColorAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnColorAdd.Location = new System.Drawing.Point(3, 497);
            this.btnColorAdd.Name = "btnColorAdd";
            this.btnColorAdd.Size = new System.Drawing.Size(339, 69);
            this.btnColorAdd.TabIndex = 1;
            this.btnColorAdd.Text = "Settings.color.add";
            this.btnColorAdd.UseVisualStyleBackColor = true;
            this.btnColorAdd.Click += new System.EventHandler(this.btnColorAdd_Click);
            // 
            // btnColorUp
            // 
            this.btnColorUp.Location = new System.Drawing.Point(348, 3);
            this.btnColorUp.Name = "btnColorUp";
            this.btnColorUp.Size = new System.Drawing.Size(88, 83);
            this.btnColorUp.TabIndex = 2;
            this.btnColorUp.UseVisualStyleBackColor = true;
            this.btnColorUp.Click += new System.EventHandler(this.btnColorUp_Click);
            // 
            // btnColorDown
            // 
            this.btnColorDown.Location = new System.Drawing.Point(348, 92);
            this.btnColorDown.Name = "btnColorDown";
            this.btnColorDown.Size = new System.Drawing.Size(88, 81);
            this.btnColorDown.TabIndex = 3;
            this.btnColorDown.UseVisualStyleBackColor = true;
            this.btnColorDown.Click += new System.EventHandler(this.btnColorDown_Click);
            // 
            // btnColorDelete
            // 
            this.btnColorDelete.Location = new System.Drawing.Point(348, 179);
            this.btnColorDelete.Name = "btnColorDelete";
            this.btnColorDelete.Size = new System.Drawing.Size(88, 81);
            this.btnColorDelete.TabIndex = 4;
            this.btnColorDelete.UseVisualStyleBackColor = true;
            this.btnColorDelete.Click += new System.EventHandler(this.btnColorDelete_Click);
            // 
            // SettingsPagePalettes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnColorDelete);
            this.Controls.Add(this.btnColorDown);
            this.Controls.Add(this.btnColorUp);
            this.Controls.Add(this.btnColorAdd);
            this.Controls.Add(this.lvColors);
            this.Name = "SettingsPagePalettes";
            this.Size = new System.Drawing.Size(790, 569);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvColors;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnColorAdd;
        private System.Windows.Forms.Button btnColorUp;
        private System.Windows.Forms.Button btnColorDown;
        private System.Windows.Forms.Button btnColorDelete;
    }
}
