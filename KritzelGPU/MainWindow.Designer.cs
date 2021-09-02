namespace Kritzel.Main
{
    partial class MainWindow
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTopMid = new System.Windows.Forms.Panel();
            this.pnSizes = new System.Windows.Forms.Panel();
            this.panelTopRight = new System.Windows.Forms.Panel();
            this.btnLayout = new System.Windows.Forms.Button();
            this.btnFormType = new System.Windows.Forms.Button();
            this.btnFullscreen = new System.Windows.Forms.Button();
            this.panelTopLeft = new System.Windows.Forms.Panel();
            this.panelPage = new System.Windows.Forms.Panel();
            this.btnFile = new System.Windows.Forms.Button();
            this.menuFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFormType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.strokeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagOpenDoc = new System.Windows.Forms.OpenFileDialog();
            this.ctxMovePage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.actionResetRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionmatchToWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionresetTransformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsetDPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmAutosave = new System.Windows.Forms.Timer(this.components);
            this.panelSide = new System.Windows.Forms.Panel();
            this.btnBreakout = new System.Windows.Forms.Button();
            this.panelMovePage = new System.Windows.Forms.Panel();
            this.btnResetTransform = new System.Windows.Forms.Button();
            this.btnMatchWindow = new System.Windows.Forms.Button();
            this.btnResetRotation = new System.Windows.Forms.Button();
            this.panelTransform = new System.Windows.Forms.Panel();
            this.btnRot = new System.Windows.Forms.Button();
            this.btnScale = new System.Windows.Forms.Button();
            this.btnShift = new System.Windows.Forms.Button();
            this.panelPageControl = new System.Windows.Forms.Panel();
            this.btnDeletePage = new System.Windows.Forms.Button();
            this.btnPageDown = new System.Windows.Forms.Button();
            this.btnPageSelect = new System.Windows.Forms.Button();
            this.btnPageUp = new System.Windows.Forms.Button();
            this.pnInkControlContainer = new System.Windows.Forms.Panel();
            this.pnBack = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnForward = new System.Windows.Forms.Panel();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.toolStripStamps = new System.Windows.Forms.ToolStrip();
            this.ctxQuickAccessMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stampdeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPicker1 = new Kritzel.Main.GUIElements.ColorPicker();
            this.panelTop.SuspendLayout();
            this.panelTopMid.SuspendLayout();
            this.panelTopRight.SuspendLayout();
            this.panelTopLeft.SuspendLayout();
            this.menuFiles.SuspendLayout();
            this.menuFormType.SuspendLayout();
            this.ctxMovePage.SuspendLayout();
            this.panelSide.SuspendLayout();
            this.panelMovePage.SuspendLayout();
            this.panelTransform.SuspendLayout();
            this.panelPageControl.SuspendLayout();
            this.pnInkControlContainer.SuspendLayout();
            this.pnBack.SuspendLayout();
            this.pnForward.SuspendLayout();
            this.ctxQuickAccessMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Control;
            this.panelTop.Controls.Add(this.panelTopMid);
            this.panelTop.Controls.Add(this.panelTopRight);
            this.panelTop.Controls.Add(this.panelTopLeft);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1037, 80);
            this.panelTop.TabIndex = 9;
            // 
            // panelTopMid
            // 
            this.panelTopMid.Controls.Add(this.colorPicker1);
            this.panelTopMid.Controls.Add(this.pnSizes);
            this.panelTopMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopMid.Location = new System.Drawing.Point(85, 0);
            this.panelTopMid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTopMid.Name = "panelTopMid";
            this.panelTopMid.Size = new System.Drawing.Size(697, 80);
            this.panelTopMid.TabIndex = 2;
            // 
            // pnSizes
            // 
            this.pnSizes.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnSizes.Location = new System.Drawing.Point(438, 0);
            this.pnSizes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnSizes.Name = "pnSizes";
            this.pnSizes.Size = new System.Drawing.Size(259, 80);
            this.pnSizes.TabIndex = 1;
            // 
            // panelTopRight
            // 
            this.panelTopRight.AutoSize = true;
            this.panelTopRight.Controls.Add(this.btnLayout);
            this.panelTopRight.Controls.Add(this.btnFormType);
            this.panelTopRight.Controls.Add(this.btnFullscreen);
            this.panelTopRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTopRight.Location = new System.Drawing.Point(782, 0);
            this.panelTopRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTopRight.Name = "panelTopRight";
            this.panelTopRight.Size = new System.Drawing.Size(255, 80);
            this.panelTopRight.TabIndex = 1;
            // 
            // btnLayout
            // 
            this.btnLayout.BackColor = System.Drawing.Color.White;
            this.btnLayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLayout.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLayout.FlatAppearance.BorderSize = 0;
            this.btnLayout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLayout.Location = new System.Drawing.Point(0, 0);
            this.btnLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLayout.Name = "btnLayout";
            this.btnLayout.Size = new System.Drawing.Size(85, 80);
            this.btnLayout.TabIndex = 1;
            this.btnLayout.UseVisualStyleBackColor = false;
            this.btnLayout.Click += new System.EventHandler(this.btnLayout_Click);
            // 
            // btnFormType
            // 
            this.btnFormType.BackColor = System.Drawing.Color.White;
            this.btnFormType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFormType.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFormType.FlatAppearance.BorderSize = 0;
            this.btnFormType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormType.Location = new System.Drawing.Point(85, 0);
            this.btnFormType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFormType.Name = "btnFormType";
            this.btnFormType.Size = new System.Drawing.Size(85, 80);
            this.btnFormType.TabIndex = 2;
            this.btnFormType.UseVisualStyleBackColor = false;
            this.btnFormType.Click += new System.EventHandler(this.btnFormType_Click);
            // 
            // btnFullscreen
            // 
            this.btnFullscreen.BackColor = System.Drawing.Color.White;
            this.btnFullscreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFullscreen.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFullscreen.FlatAppearance.BorderSize = 0;
            this.btnFullscreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullscreen.Location = new System.Drawing.Point(170, 0);
            this.btnFullscreen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFullscreen.Name = "btnFullscreen";
            this.btnFullscreen.Size = new System.Drawing.Size(85, 80);
            this.btnFullscreen.TabIndex = 0;
            this.btnFullscreen.Text = "Full";
            this.btnFullscreen.UseVisualStyleBackColor = false;
            this.btnFullscreen.Click += new System.EventHandler(this.btnFullscreen_Click);
            // 
            // panelTopLeft
            // 
            this.panelTopLeft.AutoSize = true;
            this.panelTopLeft.Controls.Add(this.panelPage);
            this.panelTopLeft.Controls.Add(this.btnFile);
            this.panelTopLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTopLeft.Location = new System.Drawing.Point(0, 0);
            this.panelTopLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTopLeft.Name = "panelTopLeft";
            this.panelTopLeft.Size = new System.Drawing.Size(85, 80);
            this.panelTopLeft.TabIndex = 0;
            // 
            // panelPage
            // 
            this.panelPage.AutoSize = true;
            this.panelPage.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelPage.Location = new System.Drawing.Point(85, 0);
            this.panelPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelPage.Name = "panelPage";
            this.panelPage.Size = new System.Drawing.Size(0, 80);
            this.panelPage.TabIndex = 1;
            // 
            // btnFile
            // 
            this.btnFile.BackColor = System.Drawing.Color.White;
            this.btnFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFile.FlatAppearance.BorderSize = 0;
            this.btnFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFile.Location = new System.Drawing.Point(0, 0);
            this.btnFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(85, 80);
            this.btnFile.TabIndex = 0;
            this.btnFile.UseVisualStyleBackColor = false;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // menuFiles
            // 
            this.menuFiles.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem});
            this.menuFiles.Name = "menuFiles";
            this.menuFiles.Size = new System.Drawing.Size(173, 156);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(172, 38);
            this.saveToolStripMenuItem1.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            this.saveAsToolStripMenuItem.Text = "&Save As";
            // 
            // menuFormType
            // 
            this.menuFormType.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuFormType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.strokeToolStripMenuItem,
            this.lineToolStripMenuItem,
            this.rectangleToolStripMenuItem});
            this.menuFormType.Name = "menuFormType";
            this.menuFormType.Size = new System.Drawing.Size(203, 118);
            // 
            // strokeToolStripMenuItem
            // 
            this.strokeToolStripMenuItem.Image = global::Kritzel.Main.Properties.Resources.Pen;
            this.strokeToolStripMenuItem.Name = "strokeToolStripMenuItem";
            this.strokeToolStripMenuItem.Size = new System.Drawing.Size(202, 38);
            this.strokeToolStripMenuItem.Text = "Stroke";
            this.strokeToolStripMenuItem.Click += new System.EventHandler(this.strokeToolStripMenuItem_Click);
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Image = global::Kritzel.Main.Properties.Resources.Line;
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(202, 38);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.lineToolStripMenuItem_Click);
            // 
            // rectangleToolStripMenuItem
            // 
            this.rectangleToolStripMenuItem.Image = global::Kritzel.Main.Properties.Resources.Rect;
            this.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            this.rectangleToolStripMenuItem.Size = new System.Drawing.Size(202, 38);
            this.rectangleToolStripMenuItem.Text = "Rectangle";
            this.rectangleToolStripMenuItem.Click += new System.EventHandler(this.rectangleToolStripMenuItem_Click);
            // 
            // diagOpenDoc
            // 
            this.diagOpenDoc.Filter = "Supportet Files|*.zip;*.pdf;*.jpg;*.jpeg;*.png;*.bmp|Kritzel Documents|*.zip|PDF " +
    "Files|*.pdf|Images|*.jpg;*.jpeg;*.bmp;*.png";
            // 
            // ctxMovePage
            // 
            this.ctxMovePage.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctxMovePage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionResetRotationToolStripMenuItem,
            this.actionmatchToWindowToolStripMenuItem,
            this.actionresetTransformationToolStripMenuItem,
            this.actionsetDPIToolStripMenuItem});
            this.ctxMovePage.Name = "ctxMovePage";
            this.ctxMovePage.Size = new System.Drawing.Size(376, 156);
            // 
            // actionResetRotationToolStripMenuItem
            // 
            this.actionResetRotationToolStripMenuItem.Name = "actionResetRotationToolStripMenuItem";
            this.actionResetRotationToolStripMenuItem.Size = new System.Drawing.Size(375, 38);
            this.actionResetRotationToolStripMenuItem.Text = "Action.resetRotation";
            this.actionResetRotationToolStripMenuItem.Click += new System.EventHandler(this.actionResetRotationToolStripMenuItem_Click);
            // 
            // actionmatchToWindowToolStripMenuItem
            // 
            this.actionmatchToWindowToolStripMenuItem.Name = "actionmatchToWindowToolStripMenuItem";
            this.actionmatchToWindowToolStripMenuItem.Size = new System.Drawing.Size(375, 38);
            this.actionmatchToWindowToolStripMenuItem.Text = "Action.matchToWindow";
            this.actionmatchToWindowToolStripMenuItem.Click += new System.EventHandler(this.actionmatchToWindowToolStripMenuItem_Click);
            // 
            // actionresetTransformationToolStripMenuItem
            // 
            this.actionresetTransformationToolStripMenuItem.Name = "actionresetTransformationToolStripMenuItem";
            this.actionresetTransformationToolStripMenuItem.Size = new System.Drawing.Size(375, 38);
            this.actionresetTransformationToolStripMenuItem.Text = "Action.resetTransformation";
            this.actionresetTransformationToolStripMenuItem.Click += new System.EventHandler(this.actionresetTransformationToolStripMenuItem_Click);
            // 
            // actionsetDPIToolStripMenuItem
            // 
            this.actionsetDPIToolStripMenuItem.Name = "actionsetDPIToolStripMenuItem";
            this.actionsetDPIToolStripMenuItem.Size = new System.Drawing.Size(375, 38);
            this.actionsetDPIToolStripMenuItem.Text = "Action.setDPI";
            // 
            // tmAutosave
            // 
            this.tmAutosave.Enabled = true;
            this.tmAutosave.Interval = 10000;
            this.tmAutosave.Tick += new System.EventHandler(this.tmAutosave_Tick);
            // 
            // panelSide
            // 
            this.panelSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panelSide.Controls.Add(this.btnBreakout);
            this.panelSide.Controls.Add(this.panelMovePage);
            this.panelSide.Controls.Add(this.panelTransform);
            this.panelSide.Controls.Add(this.panelPageControl);
            this.panelSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSide.Location = new System.Drawing.Point(0, 80);
            this.panelSide.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSide.Name = "panelSide";
            this.panelSide.Size = new System.Drawing.Size(104, 600);
            this.panelSide.TabIndex = 10;
            // 
            // btnBreakout
            // 
            this.btnBreakout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBreakout.FlatAppearance.BorderSize = 0;
            this.btnBreakout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBreakout.Location = new System.Drawing.Point(0, 519);
            this.btnBreakout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBreakout.Name = "btnBreakout";
            this.btnBreakout.Size = new System.Drawing.Size(104, 44);
            this.btnBreakout.TabIndex = 7;
            this.btnBreakout.Text = "Action.breakout";
            this.btnBreakout.UseVisualStyleBackColor = true;
            this.btnBreakout.Click += new System.EventHandler(this.btnBreakout_Click);
            // 
            // panelMovePage
            // 
            this.panelMovePage.AutoSize = true;
            this.panelMovePage.Controls.Add(this.btnResetTransform);
            this.panelMovePage.Controls.Add(this.btnMatchWindow);
            this.panelMovePage.Controls.Add(this.btnResetRotation);
            this.panelMovePage.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMovePage.Location = new System.Drawing.Point(0, 393);
            this.panelMovePage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMovePage.Name = "panelMovePage";
            this.panelMovePage.Size = new System.Drawing.Size(104, 126);
            this.panelMovePage.TabIndex = 13;
            // 
            // btnResetTransform
            // 
            this.btnResetTransform.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnResetTransform.FlatAppearance.BorderSize = 0;
            this.btnResetTransform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetTransform.Location = new System.Drawing.Point(0, 84);
            this.btnResetTransform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResetTransform.Name = "btnResetTransform";
            this.btnResetTransform.Size = new System.Drawing.Size(104, 42);
            this.btnResetTransform.TabIndex = 2;
            this.btnResetTransform.Text = "Action.resetTransformation";
            this.btnResetTransform.UseVisualStyleBackColor = true;
            this.btnResetTransform.Click += new System.EventHandler(this.actionresetTransformationToolStripMenuItem_Click);
            // 
            // btnMatchWindow
            // 
            this.btnMatchWindow.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMatchWindow.FlatAppearance.BorderSize = 0;
            this.btnMatchWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatchWindow.Location = new System.Drawing.Point(0, 42);
            this.btnMatchWindow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMatchWindow.Name = "btnMatchWindow";
            this.btnMatchWindow.Size = new System.Drawing.Size(104, 42);
            this.btnMatchWindow.TabIndex = 1;
            this.btnMatchWindow.Text = "Action.matchToWindow";
            this.btnMatchWindow.UseVisualStyleBackColor = true;
            this.btnMatchWindow.Click += new System.EventHandler(this.actionmatchToWindowToolStripMenuItem_Click);
            // 
            // btnResetRotation
            // 
            this.btnResetRotation.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnResetRotation.FlatAppearance.BorderSize = 0;
            this.btnResetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetRotation.Location = new System.Drawing.Point(0, 0);
            this.btnResetRotation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResetRotation.Name = "btnResetRotation";
            this.btnResetRotation.Size = new System.Drawing.Size(104, 42);
            this.btnResetRotation.TabIndex = 0;
            this.btnResetRotation.Text = "Action.resetRotation";
            this.btnResetRotation.UseVisualStyleBackColor = true;
            this.btnResetRotation.Click += new System.EventHandler(this.actionResetRotationToolStripMenuItem_Click);
            // 
            // panelTransform
            // 
            this.panelTransform.AutoSize = true;
            this.panelTransform.Controls.Add(this.btnRot);
            this.panelTransform.Controls.Add(this.btnScale);
            this.panelTransform.Controls.Add(this.btnShift);
            this.panelTransform.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTransform.Location = new System.Drawing.Point(0, 240);
            this.panelTransform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTransform.Name = "panelTransform";
            this.panelTransform.Size = new System.Drawing.Size(104, 153);
            this.panelTransform.TabIndex = 12;
            // 
            // btnRot
            // 
            this.btnRot.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRot.FlatAppearance.BorderSize = 0;
            this.btnRot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRot.Location = new System.Drawing.Point(0, 102);
            this.btnRot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRot.Name = "btnRot";
            this.btnRot.Size = new System.Drawing.Size(104, 51);
            this.btnRot.TabIndex = 2;
            this.btnRot.UseVisualStyleBackColor = true;
            // 
            // btnScale
            // 
            this.btnScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnScale.FlatAppearance.BorderSize = 0;
            this.btnScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScale.Location = new System.Drawing.Point(0, 51);
            this.btnScale.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnScale.Name = "btnScale";
            this.btnScale.Size = new System.Drawing.Size(104, 51);
            this.btnScale.TabIndex = 1;
            this.btnScale.UseVisualStyleBackColor = true;
            // 
            // btnShift
            // 
            this.btnShift.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnShift.FlatAppearance.BorderSize = 0;
            this.btnShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShift.Location = new System.Drawing.Point(0, 0);
            this.btnShift.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnShift.Name = "btnShift";
            this.btnShift.Size = new System.Drawing.Size(104, 51);
            this.btnShift.TabIndex = 0;
            this.btnShift.UseVisualStyleBackColor = true;
            // 
            // panelPageControl
            // 
            this.panelPageControl.AutoSize = true;
            this.panelPageControl.Controls.Add(this.btnDeletePage);
            this.panelPageControl.Controls.Add(this.btnPageDown);
            this.panelPageControl.Controls.Add(this.btnPageSelect);
            this.panelPageControl.Controls.Add(this.btnPageUp);
            this.panelPageControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPageControl.Location = new System.Drawing.Point(0, 0);
            this.panelPageControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelPageControl.Name = "panelPageControl";
            this.panelPageControl.Size = new System.Drawing.Size(104, 240);
            this.panelPageControl.TabIndex = 11;
            // 
            // btnDeletePage
            // 
            this.btnDeletePage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDeletePage.FlatAppearance.BorderSize = 0;
            this.btnDeletePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePage.Location = new System.Drawing.Point(0, 176);
            this.btnDeletePage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeletePage.Name = "btnDeletePage";
            this.btnDeletePage.Size = new System.Drawing.Size(104, 64);
            this.btnDeletePage.TabIndex = 14;
            this.btnDeletePage.Text = "Delete";
            this.btnDeletePage.UseVisualStyleBackColor = true;
            this.btnDeletePage.Click += new System.EventHandler(this.btnDeletePage_Click);
            // 
            // btnPageDown
            // 
            this.btnPageDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPageDown.FlatAppearance.BorderSize = 0;
            this.btnPageDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageDown.Location = new System.Drawing.Point(0, 117);
            this.btnPageDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPageDown.Name = "btnPageDown";
            this.btnPageDown.Size = new System.Drawing.Size(104, 59);
            this.btnPageDown.TabIndex = 2;
            this.btnPageDown.Text = "\\/";
            this.btnPageDown.UseVisualStyleBackColor = true;
            this.btnPageDown.Click += new System.EventHandler(this.btnPageDown_Click);
            // 
            // btnPageSelect
            // 
            this.btnPageSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPageSelect.FlatAppearance.BorderSize = 0;
            this.btnPageSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageSelect.Location = new System.Drawing.Point(0, 62);
            this.btnPageSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPageSelect.Name = "btnPageSelect";
            this.btnPageSelect.Size = new System.Drawing.Size(104, 55);
            this.btnPageSelect.TabIndex = 1;
            this.btnPageSelect.Text = "Select";
            this.btnPageSelect.UseVisualStyleBackColor = true;
            this.btnPageSelect.Click += new System.EventHandler(this.btnPageSelect_Click);
            // 
            // btnPageUp
            // 
            this.btnPageUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPageUp.FlatAppearance.BorderSize = 0;
            this.btnPageUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageUp.Location = new System.Drawing.Point(0, 0);
            this.btnPageUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPageUp.Name = "btnPageUp";
            this.btnPageUp.Size = new System.Drawing.Size(104, 62);
            this.btnPageUp.TabIndex = 0;
            this.btnPageUp.Text = "/\\";
            this.btnPageUp.UseVisualStyleBackColor = true;
            this.btnPageUp.Click += new System.EventHandler(this.btnPageUp_Click);
            // 
            // pnInkControlContainer
            // 
            this.pnInkControlContainer.Controls.Add(this.toolStripStamps);
            this.pnInkControlContainer.Controls.Add(this.pnBack);
            this.pnInkControlContainer.Controls.Add(this.pnForward);
            this.pnInkControlContainer.Controls.Add(this.btnCut);
            this.pnInkControlContainer.Controls.Add(this.btnCopy);
            this.pnInkControlContainer.Controls.Add(this.btnPaste);
            this.pnInkControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInkControlContainer.Location = new System.Drawing.Point(104, 80);
            this.pnInkControlContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnInkControlContainer.Name = "pnInkControlContainer";
            this.pnInkControlContainer.Size = new System.Drawing.Size(933, 600);
            this.pnInkControlContainer.TabIndex = 11;
            // 
            // pnBack
            // 
            this.pnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBack.Controls.Add(this.btnBack);
            this.pnBack.Location = new System.Drawing.Point(709, 8);
            this.pnBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnBack.Name = "pnBack";
            this.pnBack.Size = new System.Drawing.Size(100, 94);
            this.pnBack.TabIndex = 6;
            // 
            // btnBack
            // 
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 94);
            this.btnBack.TabIndex = 1;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pnForward
            // 
            this.pnForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnForward.Controls.Add(this.btnForward);
            this.pnForward.Location = new System.Drawing.Point(817, 8);
            this.pnForward.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnForward.Name = "pnForward";
            this.pnForward.Size = new System.Drawing.Size(100, 94);
            this.pnForward.TabIndex = 5;
            // 
            // btnForward
            // 
            this.btnForward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForward.Location = new System.Drawing.Point(0, 0);
            this.btnForward.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(100, 94);
            this.btnForward.TabIndex = 0;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnCut
            // 
            this.btnCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCut.Location = new System.Drawing.Point(224, 8);
            this.btnCut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(100, 94);
            this.btnCut.TabIndex = 4;
            this.btnCut.Text = "Cut";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Location = new System.Drawing.Point(116, 8);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 94);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaste.Location = new System.Drawing.Point(8, 8);
            this.btnPaste.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(100, 94);
            this.btnPaste.TabIndex = 2;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // toolStripStamps
            // 
            this.toolStripStamps.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripStamps.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripStamps.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripStamps.Location = new System.Drawing.Point(335, 8);
            this.toolStripStamps.Name = "toolStripStamps";
            this.toolStripStamps.Size = new System.Drawing.Size(202, 50);
            this.toolStripStamps.TabIndex = 7;
            this.toolStripStamps.Text = "toolStrip1";
            this.toolStripStamps.Visible = false;
            // 
            // ctxQuickAccessMenu
            // 
            this.ctxQuickAccessMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxQuickAccessMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stampdeleteToolStripMenuItem});
            this.ctxQuickAccessMenu.Name = "ctxQuickAccessMenu";
            this.ctxQuickAccessMenu.Size = new System.Drawing.Size(301, 86);
            // 
            // stampdeleteToolStripMenuItem
            // 
            this.stampdeleteToolStripMenuItem.Name = "stampdeleteToolStripMenuItem";
            this.stampdeleteToolStripMenuItem.Size = new System.Drawing.Size(300, 38);
            this.stampdeleteToolStripMenuItem.Text = "Stamp.delete";
            this.stampdeleteToolStripMenuItem.Click += new System.EventHandler(this.stampdeleteToolStripMenuItem_Click);
            // 
            // colorPicker1
            // 
            this.colorPicker1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colorPicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorPicker1.Location = new System.Drawing.Point(0, 0);
            this.colorPicker1.Margin = new System.Windows.Forms.Padding(5);
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.Size = new System.Drawing.Size(438, 80);
            this.colorPicker1.TabIndex = 0;
            this.colorPicker1.Load += new System.EventHandler(this.colorPicker1_Load);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 680);
            this.Controls.Add(this.pnInkControlContainer);
            this.Controls.Add(this.panelSide);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainWindow";
            this.Text = "Kritzel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelTopMid.ResumeLayout(false);
            this.panelTopRight.ResumeLayout(false);
            this.panelTopLeft.ResumeLayout(false);
            this.panelTopLeft.PerformLayout();
            this.menuFiles.ResumeLayout(false);
            this.menuFormType.ResumeLayout(false);
            this.ctxMovePage.ResumeLayout(false);
            this.panelSide.ResumeLayout(false);
            this.panelSide.PerformLayout();
            this.panelMovePage.ResumeLayout(false);
            this.panelTransform.ResumeLayout(false);
            this.panelPageControl.ResumeLayout(false);
            this.pnInkControlContainer.ResumeLayout(false);
            this.pnInkControlContainer.PerformLayout();
            this.pnBack.ResumeLayout(false);
            this.pnForward.ResumeLayout(false);
            this.ctxQuickAccessMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelTopRight;
        private System.Windows.Forms.Button btnFullscreen;
        private System.Windows.Forms.Panel panelTopLeft;
        private System.Windows.Forms.Button btnLayout;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.ContextMenuStrip menuFiles;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Button btnFormType;
        private System.Windows.Forms.ContextMenuStrip menuFormType;
        private System.Windows.Forms.ToolStripMenuItem strokeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangleToolStripMenuItem;
        private System.Windows.Forms.Panel panelTopMid;
        private System.Windows.Forms.Panel panelPage;
        private System.Windows.Forms.OpenFileDialog diagOpenDoc;
        private GUIElements.ColorPicker colorPicker1;
        private System.Windows.Forms.ContextMenuStrip ctxMovePage;
        private System.Windows.Forms.ToolStripMenuItem actionResetRotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionmatchToWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionresetTransformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsetDPIToolStripMenuItem;
        private System.Windows.Forms.Timer tmAutosave;
        private System.Windows.Forms.Panel panelSide;
        private System.Windows.Forms.Button btnPageDown;
        private System.Windows.Forms.Button btnPageSelect;
        private System.Windows.Forms.Button btnPageUp;
        private System.Windows.Forms.Panel panelTransform;
        private System.Windows.Forms.Button btnRot;
        private System.Windows.Forms.Button btnScale;
        private System.Windows.Forms.Button btnShift;
        private System.Windows.Forms.Panel panelPageControl;
        private System.Windows.Forms.Panel panelMovePage;
        private System.Windows.Forms.Button btnResetTransform;
        private System.Windows.Forms.Button btnMatchWindow;
        private System.Windows.Forms.Button btnResetRotation;
        private System.Windows.Forms.Panel pnInkControlContainer;
        private System.Windows.Forms.Button btnDeletePage;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.Panel pnSizes;
        private System.Windows.Forms.Panel pnBack;
        private System.Windows.Forms.Panel pnForward;
        private System.Windows.Forms.Button btnBreakout;
        private System.Windows.Forms.ToolStrip toolStripStamps;
        private System.Windows.Forms.ContextMenuStrip ctxQuickAccessMenu;
        private System.Windows.Forms.ToolStripMenuItem stampdeleteToolStripMenuItem;
    }
}