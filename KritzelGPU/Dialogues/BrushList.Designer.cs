namespace Kritzel.Main.Dialogues
{
    partial class BrushList
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lvBrushes = new Kritzel.Main.GUIElements.TListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lvBrushes
            // 
            this.lvBrushes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBrushes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvBrushes.FullRowSelect = true;
            this.lvBrushes.HideSelection = false;
            this.lvBrushes.LargeImageList = this.imageList1;
            this.lvBrushes.Location = new System.Drawing.Point(12, 12);
            this.lvBrushes.MultiSelect = false;
            this.lvBrushes.Name = "lvBrushes";
            this.lvBrushes.Size = new System.Drawing.Size(459, 445);
            this.lvBrushes.SmallImageList = this.imageList1;
            this.lvBrushes.TabIndex = 0;
            this.lvBrushes.UseCompatibleStateImageBehavior = false;
            this.lvBrushes.View = System.Windows.Forms.View.Details;
            this.lvBrushes.ItemActivate += new System.EventHandler(this.lvBrushes_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Brushes";
            this.columnHeader1.Width = 394;
            // 
            // BrushList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 469);
            this.Controls.Add(this.lvBrushes);
            this.MaximizeBox = false;
            this.Name = "BrushList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrushList";
            this.ResumeLayout(false);

        }

        #endregion

        private GUIElements.TListView lvBrushes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageList1;
    }
}