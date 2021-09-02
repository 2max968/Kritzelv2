
namespace Kritzel.Main.Dialogues
{
    partial class StampMenu
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
            this.btnStamp = new System.Windows.Forms.Button();
            this.lvStamps = new System.Windows.Forms.ListView();
            this.ctxStamp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stampdeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stampaddQuickAccessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgStampThumbs = new System.Windows.Forms.ImageList(this.components);
            this.ctxStamp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStamp
            // 
            this.btnStamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStamp.Location = new System.Drawing.Point(12, 574);
            this.btnStamp.Name = "btnStamp";
            this.btnStamp.Size = new System.Drawing.Size(175, 60);
            this.btnStamp.TabIndex = 0;
            this.btnStamp.Text = "Stamp.create";
            this.btnStamp.UseVisualStyleBackColor = true;
            this.btnStamp.Click += new System.EventHandler(this.btnStamp_Click);
            // 
            // lvStamps
            // 
            this.lvStamps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStamps.ContextMenuStrip = this.ctxStamp;
            this.lvStamps.HideSelection = false;
            this.lvStamps.LabelEdit = true;
            this.lvStamps.LargeImageList = this.imgStampThumbs;
            this.lvStamps.Location = new System.Drawing.Point(12, 12);
            this.lvStamps.Name = "lvStamps";
            this.lvStamps.Size = new System.Drawing.Size(776, 556);
            this.lvStamps.SmallImageList = this.imgStampThumbs;
            this.lvStamps.TabIndex = 1;
            this.lvStamps.UseCompatibleStateImageBehavior = false;
            this.lvStamps.SelectedIndexChanged += new System.EventHandler(this.lvStamps_SelectedIndexChanged);
            // 
            // ctxStamp
            // 
            this.ctxStamp.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxStamp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stampdeleteToolStripMenuItem,
            this.stampaddQuickAccessToolStripMenuItem});
            this.ctxStamp.Name = "ctxStamp";
            this.ctxStamp.Size = new System.Drawing.Size(334, 124);
            // 
            // stampdeleteToolStripMenuItem
            // 
            this.stampdeleteToolStripMenuItem.Name = "stampdeleteToolStripMenuItem";
            this.stampdeleteToolStripMenuItem.Size = new System.Drawing.Size(333, 38);
            this.stampdeleteToolStripMenuItem.Text = "Stamp.delete";
            this.stampdeleteToolStripMenuItem.Click += new System.EventHandler(this.stampdeleteToolStripMenuItem_Click);
            // 
            // stampaddQuickAccessToolStripMenuItem
            // 
            this.stampaddQuickAccessToolStripMenuItem.Name = "stampaddQuickAccessToolStripMenuItem";
            this.stampaddQuickAccessToolStripMenuItem.Size = new System.Drawing.Size(333, 38);
            this.stampaddQuickAccessToolStripMenuItem.Text = "Stamp.addQuickAccess";
            this.stampaddQuickAccessToolStripMenuItem.Click += new System.EventHandler(this.stampaddQuickAccessToolStripMenuItem_Click);
            // 
            // imgStampThumbs
            // 
            this.imgStampThumbs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgStampThumbs.ImageSize = new System.Drawing.Size(16, 16);
            this.imgStampThumbs.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // StampMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 646);
            this.Controls.Add(this.lvStamps);
            this.Controls.Add(this.btnStamp);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StampMenu";
            this.Text = "StampMenu";
            this.ctxStamp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStamp;
        private System.Windows.Forms.ListView lvStamps;
        private System.Windows.Forms.ImageList imgStampThumbs;
        private System.Windows.Forms.ContextMenuStrip ctxStamp;
        private System.Windows.Forms.ToolStripMenuItem stampdeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stampaddQuickAccessToolStripMenuItem;
    }
}