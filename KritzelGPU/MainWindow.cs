using Kritzel.PointerInputLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main
{
    public partial class MainWindow : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        [DllImport("User32.dll")]
        public static extern bool ChangeClipboardChain(IntPtr hWnd, IntPtr hWndNewNext);

        public delegate void Callback(Control dialog);
        public static MainWindow Instance { get; private set; } = null;

        PBrush[] brushes = new PBrush[] {PBrush.CreateSolid(Color.Black),
            PBrush.CreateSolid(Color.FromArgb(255,106,0)),
            PBrush.CreateSolid(Color.FromArgb(0,148,255)),
            PBrush.CreateSolid(Color.FromArgb(38,127,0)) };
        FormWindowState tmpWindowState = FormWindowState.Normal;
        KDocument doc = new KDocument();
        Bitmap icoFullscreen, icoFullscreenEnd;
        List<uint> autosavedList = new List<uint>();
        IntPtr clipboardViewerNewNext;
        Button[] sizeButtons;
        Form wndBreakout = null;

        public InkControl inkControl1;

        public MainWindow()
        {
            InitializeComponent();
            inkControl1 = new InkControl();
            inkControl1.Dock = DockStyle.Fill;
            pnInkControlContainer.Controls.Add(inkControl1);
            this.Icon = Program.WindowIcon;
            panelTop.Height = Util.GetGUISize();
            panelSide.Width = Util.GetGUISize();
            MainWindow.Instance = this;
            Util.SetAllButtonSizes(panelTop, Util.GetGUISize());
            Util.SetAllButtonSizes(panelSide, Util.GetGUISize());
            icoFullscreen = ResManager.LoadIcon("fullscreen.svg", Util.GetGUISize());
            icoFullscreenEnd = ResManager.LoadIcon("fullscreenEnd.svg", Util.GetGUISize());
            btnFullscreen.Image = icoFullscreen;
            btnFullscreen.Text = "";
            tmAutosave.Interval = Configuration.AutosaveInterval * 1000;

            btnFile.BackgroundImage = ResManager.LoadIcon("hamburger.svg", Util.GetGUISize());

            btnPageDown.Image = ResManager.LoadIcon("gui/down.svg", Util.GetGUISize());
            btnPageUp.Image = ResManager.LoadIcon("gui/up.svg", Util.GetGUISize());
            btnPageSelect.Image = ResManager.LoadIcon("gui/select.svg", Util.GetGUISize());
            btnPageSelect.Text = btnPageUp.Text = btnPageDown.Text = "";
            btnShift.Image = ResManager.LoadIcon("actions/transMove.svg", Util.GetGUISize());
            btnScale.Image = ResManager.LoadIcon("actions/transScale.svg", Util.GetGUISize());
            btnRot.Image = ResManager.LoadIcon("actions/transRotate.svg", Util.GetGUISize());
            btnBack.Image = ResManager.LoadIcon("actions/undo.svg", Util.GetGUISize());
            btnForward.Image = ResManager.LoadIcon("actions/redo.svg", Util.GetGUISize());
            btnDeletePage.Image = ResManager.LoadIcon("actions/delete.svg", Util.GetGUISize());
            btnDeletePage.Text = "";
            btnCopy.Image = ResManager.LoadIcon("actions/copy.svg", Util.GetGUISize());
            btnCopy.Text = "";
            btnPaste.Image = ResManager.LoadIcon("actions/paste.svg", Util.GetGUISize());
            btnPaste.Text = "";
            btnCut.Image = ResManager.LoadIcon("actions/cut.svg", Util.GetGUISize());
            btnCut.Text = "";
            btnBreakout.Image = ResManager.LoadIcon("actions/breakout.svg", Util.GetGUISize());
            btnBreakout.Text = "";
            btnShift.Click += TransformButton_Click;
            btnScale.Click += TransformButton_Click;
            btnRot.Click += TransformButton_Click;
            btnScale.Tag = btnShift.Tag = btnRot.Tag = true;
            setCbColor(btnShift, btnScale, btnRot);
            panelSide.Dock = Configuration.LeftHanded ? DockStyle.Right : DockStyle.Left;
            btnResetRotation.Image = ResManager.LoadIcon("actions/resetRotation.svg", Util.GetGUISize());
            btnMatchWindow.Image = ResManager.LoadIcon("actions/matchScreen.svg", Util.GetGUISize());
            btnResetTransform.Image = ResManager.LoadIcon("actions/resetPosScale.svg", Util.GetGUISize());
            btnLayout.Image = ResManager.LoadIcon("layout.svg", Util.GetGUISize());
            btnResetTransform.Text = btnResetRotation.Text = btnMatchWindow.Text = "";
            btnFormType.BackgroundImage = Line.BitmapStrk;

            bool restore = false;
            if(Program.RestoreData)
            {
                restore = Dialogues.MsgBox.ShowYesNo("System.restoreData?");
            }

            if(restore)
            {
                doc.LoadDocument(null, new MessageLog());
                inkControl1.LoadPage(doc.Pages[0]);
                inkControl1.Thicknes = Configuration.PenCurrentSize;
            }
            else if (Environment.GetCommandLineArgs().Length <= 1)
            {
                doc.Pages.Add(new KPage(doc));
                doc.SetCurrentStateAsSaved();
                inkControl1.LoadPage(doc.Pages[0]);
                inkControl1.Thicknes = Configuration.PenCurrentSize;
            }
            else
            {
                string filename = Environment.GetCommandLineArgs()[1];
                if(!File.Exists(filename))
                {
                    MessageBox.Show("Cant open file '{0}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    doc.LoadDocument(filename, new MessageLog());
                    inkControl1.LoadPage(doc.Pages[0]);
                    inkControl1.Thicknes = Configuration.PenSizeMin;
                }
            }

            sizeButtons = new Button[Configuration.PenSizeNum];
            for (int i = 0; i < Configuration.PenSizeNum; i++)
            {
                float size = Configuration.PenSizeMin + i * (Configuration.PenSizeMax - Configuration.PenSizeMin) / (float)(Configuration.PenSizeNum - 1);
                Button btnSize = new Button();
                btnSize.FlatStyle = FlatStyle.Flat;
                btnSize.FlatAppearance.BorderSize = 0;
                btnSize.Size = new Size(Util.GetGUISize(), Util.GetGUISize());
                btnSize.Dock = DockStyle.Right;
                btnSize.Tag = size;
                btnSize.Click += BtnSize_Click;
                pnSizes.Controls.Add(btnSize);
                sizeButtons[i] = btnSize;
                Bitmap bmp = new Bitmap(Util.GetGUISize(), Util.GetGUISize());
                float rad = (float)sizeButtons[i].Tag * Util.GetScaleFactor();
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillEllipse(Brushes.Black, new RectangleF(
                        Util.GetGUISize() / 2 - rad, Util.GetGUISize() / 2 - rad,
                        rad * 2, rad * 2));
                }
                btnSize.Image = bmp;
            }
            pnSizes.Width = Configuration.PenSizeNum * Util.GetGUISize();
            pnSizes.Visible = Configuration.CalculateSplinesDuringDrawing;

            this.Shown += MainWindow_Shown;
            inkControl1.SelectionChanged += InkControl1_SelectionChanged;
            inkControl1.PageLoaded += InkControl1_PageLoaded;
            inkControl1.InkModeChanged += InkControl1_InkModeChanged;
            Style.StyleChanged += Style_StyleChanged;
            Style_StyleChanged(null, Style.Default);

            colorPicker1.SetColor += ColorPicker1_SetColor;

            foreach(Control control in Controls)
            {
                if (control is Button)
                    control.Text = Language.GetText(control.Text);
            }
            foreach (ToolStripItem item in ctxMovePage.Items)
                item.Text = Language.GetText(item.Text);

            this.KeyPreview = true;
            HistoryManager.RegisterHistoryButtons(btnBack, btnForward);

            toolStripStamps.ImageScalingSize = new Size(Util.GetGUISize(), Util.GetGUISize());
        }

        private void InkControl1_InkModeChanged(object sender, InkMode e)
        {
            switch (e)
            {
                case InkMode.Pen: btnFormType.BackgroundImage = Line.BitmapStrk; break;
                case InkMode.Line: btnFormType.BackgroundImage = Forms.LinearLine.BitmapLL; break;
                case InkMode.Rect: btnFormType.BackgroundImage = Forms.Rect.BitmapRect; break;
                case InkMode.Arc: btnFormType.BackgroundImage = Forms.Arc.BitmapArc; break;
                case InkMode.Arc2: btnFormType.BackgroundImage = Forms.Arc2.BitmapArc2; break;
                case InkMode.Marker: btnFormType.BackgroundImage = Forms.Marker.BitmapMarker; break;
                case InkMode.Text: btnFormType.BackgroundImage = Forms.TextBox.BitmapTB; break;
                case InkMode.Stamp: btnFormType.BackgroundImage = Forms.LineGroup.Stamp; break;
                default: btnFormType.BackgroundImage = ResManager.GetErrorBmp(16, 16); break;
            }
        }

        private void InkControl1_PageLoaded(object sender, KPage e)
        {
            HistoryManager.SetButtonVisibility(e);
        }

        private void BtnSize_Click(object sender, EventArgs e)
        {
            inkControl1.Thicknes = (float)((Button)sender).Tag;
            Configuration.PenCurrentSize = inkControl1.Thicknes;
            setSizeButton();
        }

        void setSizeButton()
        {
            for (int i = 0; i < sizeButtons.Length; i++)
            {
                float rad = (float)sizeButtons[i].Tag;
                sizeButtons[i].BackColor = (rad == inkControl1.Thicknes)
                    ? Style.Default.Selection : Style.Default.MenuBackground;
            }
        }

        private void InkControl1_SelectionChanged(object sender, Line[] e)
        {
            bool viewButtons = e.Length > 0;
            btnCopy.Visible = viewButtons;
            btnCut.Visible = viewButtons;
            Dialogues.TextBoxInput.CloseAll();
        }

        private void TransformButton_Click(object sender, EventArgs e)
        {
            if(sender is Button)
            {
                bool val = true;
                if(((Button)sender).Tag is bool)
                {
                    val = !(bool)((Button)sender).Tag;
                }
                ((Button)sender).Tag = val;
                setCbColor((Button)sender);
            }

            bool trans = btnShift.Tag.Equals(true);
            bool scale = btnScale.Tag.Equals(true);
            bool rot = btnRot.Tag.Equals(true);

            inkControl1.LockMove = !trans;
            inkControl1.LockScale = !scale;
            inkControl1.LockRotate = !rot;
        }

        private void Style_StyleChanged(object sender, Style e)
        {
            panelTop.BackColor = e.MenuContrast;
            btnFile.BackColor = btnFullscreen.BackColor = btnLayout.BackColor
                = btnFormType.BackColor = e.MenuBackground;
            panelSide.BackColor = e.MenuBackground;
            btnCopy.BackColor = btnPaste.BackColor = btnCut.BackColor
                = btnBack.BackColor = btnForward.BackColor = e.MenuBackground;
            colorPicker1.BackColor = e.MenuContrast;
            pnSizes.BackColor = e.MenuBackground;
            pnInkControlContainer.BackColor = e.Background;
            setSizeButton();
        }

        private void ColorPicker1_SetColor(Color c)
        {
            inkControl1.Brush = PBrush.CreateSolid(c);
            if (inkControl1.Page.GetSelectedLines().Count() > 0)
            {
                inkControl1.Page.SetSelectionBrush(inkControl1.Brush);
                inkControl1.RefreshPage();
            }
        }

        private async void MainWindow_Shown(object sender, EventArgs e)
        {
            clipboardViewerNewNext = SetClipboardViewer(this.Handle);
            try
            {
                inkControl1.InitRenderer();
                inkControl1.ResetTransformation(true);
            }
            catch(Exception ex)
            {
                Program.MainLog.Add(ex);
                Dialogues.MsgBox.ShowOk(ex.Message);
                Application.Exit();
            }
            await Task.Delay(500);
            btnPaste.Visible = CopyPaster.CheckClipboard();
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool cancel = false;
            bool user = false;
            if(e.CloseReason == CloseReason.UserClosing)
            {
                if(Util.AskForSave(doc))
                {
                    user = true;
                }
                else
                {
                    cancel = true;
                }
            }
            if (!cancel)
            {
                ChangeClipboardChain(this.Handle, clipboardViewerNewNext);
                if (user)
                {
                    var tempdir = TmpManager.GetTmpDir();
                    if (tempdir.Exists)
                        tempdir.Delete(true);
                }
                doc.Dispose();
                Configuration.SaveConfig();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnFullscreen_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = tmpWindowState;
                btnFullscreen.Image = icoFullscreen;
            }
            else
            {
                tmpWindowState = this.WindowState;
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                btnFullscreen.Image = icoFullscreenEnd;
            }
        }

        private void btnLayout_Click(object sender, EventArgs e)
        {
            Dialogues.LayoutEditor ed = new Dialogues.LayoutEditor(inkControl1.Page, inkControl1);
            ed.ShowDialog();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            GUIElements.FileMenu menu = new GUIElements.FileMenu(inkControl1, doc, this, this);
            menu.Dock = DockStyle.Left;
            menu.SettingsClosed += Menu_SettingsClosed;
            OpenDialog(menu);
        }

        private void Menu_SettingsClosed(object sender, int e)
        {
            panelSide.Dock = Configuration.LeftHanded ? DockStyle.Right : DockStyle.Left;
            pnSizes.Visible = Configuration.SizeOptionsInTitlebar;
            colorPicker1.RefreshPens();
        }

        private void btnFormType_Click(object sender, EventArgs e)
        {
            GUIElements.PenMenu menu = new GUIElements.PenMenu(inkControl1, this);
            OpenDialog(menu, delegate (Control dialog)
            {
                inkControl1.InkMode = menu.Mode;
                setSizeButton();
            });
        }

        private void strokeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFormType.BackgroundImage = Properties.Resources.Pen;
            inkControl1.InkMode = InkMode.Pen;
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFormType.BackgroundImage = Properties.Resources.Line;
            inkControl1.InkMode = InkMode.Line;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFormType.BackgroundImage = Properties.Resources.Rect;
            inkControl1.InkMode = InkMode.Rect;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc = new KDocument();

        }

        public void SetDocument(KDocument document)
        {
            doc = document;
            inkControl1.LoadPage(doc.Pages[0]);
        }

        public void OpenDialog(Control dialog)
        {
            OpenDialog(dialog, null);
        }

        private void actionResetRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inkControl1.ResetRotation();
        }

        private void actionmatchToWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inkControl1.CenterPage();
        }

        private void actionresetTransformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inkControl1.ResetTransformation(false);
        }

        private void tmAutosave_Tick(object sender, EventArgs e)
        {
            if (doc != null)
            {
                if (doc.IsSaved(autosavedList)) return;
                try
                {
                    doc.SaveDocument(null);
                    doc.SetCurrentStateAsSaved(ref autosavedList);
                    Console.WriteLine("saved");
                }
                catch (Exception ex)
                {
                    Dialogues.MsgBox.ShowOk(ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        private void btnPageUp_Click(object sender, EventArgs e)
        {
            int current = -1;
            for (int i = 0; i < doc.Pages.Count; i++)
                if (doc.Pages[i] == inkControl1.Page)
                    current = i;
            if (current < 1) return;
            inkControl1.LoadPage(doc.Pages[current - 1]);
        }

        private void btnPageSelect_Click(object sender, EventArgs e)
        {
            GUIElements.PageOverview po = new GUIElements.PageOverview(doc, this, inkControl1.Page);
            po.ItemSelected += new EventHandler<int>(delegate (object selSender, int selInd)
            {
                if (selInd < doc.Pages.Count)
                {
                    inkControl1.LoadPage(doc.Pages[selInd]);
                }
                else if (selInd == doc.Pages.Count)
                {
                    KPage page = new KPage(doc);
                    var formats = PageFormat.GetFormats();
                    if (formats.ContainsKey(doc.DefaultFormat))
                        page.Format = formats[doc.DefaultFormat];
                    else
                        page.Format = PageFormat.A4;
                    page.Background = new Backgrounds.BackgroundQuadPaper5mm();
                    doc.Pages.Add(page);
                    inkControl1.LoadPage(page);
                }
            });
            OpenDialog(po);
        }

        private void btnPageDown_Click(object sender, EventArgs e)
        {
            int current = -1;
            for (int i = 0; i < doc.Pages.Count; i++)
                if (doc.Pages[i] == inkControl1.Page)
                    current = i;
            if (current < 0 || current > doc.Pages.Count - 2) return;
            inkControl1.LoadPage(doc.Pages[current + 1]);
        }

        public void OpenDialog(Control dialog, Callback callback)
        { 
            Bitmap buffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics src = CreateGraphics();
            IntPtr srcHdc = src.GetHdc();
            Graphics g = Graphics.FromImage(buffer);
            IntPtr gHdc = g.GetHdc();
            Point topleft = this.PointToScreen(new Point(0, 0));
            //g.CopyFromScreen(topleft.X, topleft.Y, 0, 0, this.ClientSize, CopyPixelOperation.SourceCopy);
            GLRenderer.Gdi32.BitBlt(gHdc, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height,
                srcHdc, 0, 0, (uint)CopyPixelOperation.SourceCopy);
            g.ReleaseHdc(gHdc);
            g.Dispose();
            src.ReleaseHdc(srcHdc);
            src.Dispose();

            var pane = new GUIElements.DialogPane(dialog, buffer, callback, panelTop);
            Controls.Add(pane);
            pane.BringToFront();
        }

        static void setCbColor(Control cltr)
        {
            if(cltr.Tag is bool)
            {
                bool val = (bool)cltr.Tag;
                if (val) cltr.BackColor = Style.Default.Selection;
                else cltr.BackColor = Style.Default.MenuBackground;
            }
            else
            {
                cltr.BackColor = Style.Default.MenuBackground;
            }
        }

        static void setCbColor(params Control[] controls)
        {
            foreach (Control c in controls)
                setCbColor(c);
        }

        private void btnDeletePage_Click(object sender, EventArgs e)
        {
            if(doc.Pages.Count <= 1)
            {
                Dialogues.MsgBox.ShowOk("Action.cantDelLast");
            }
            else
            {
                if(Dialogues.MsgBox.ShowYesNo("Action.delPage?"))
                {
                    int currentInd = -1;
                    for(int i = 0; i < doc.Pages.Count; i++)
                    {
                        if (doc.Pages[i] == inkControl1.Page)
                        {
                            currentInd = i;
                            break;
                        }
                    }

                    if(currentInd >= 0)
                    {
                        KPage page = doc.Pages[currentInd];
                        doc.Pages.RemoveAt(currentInd);
                        page.Dispose();

                        currentInd--;
                        if (currentInd < 0) currentInd = 0;

                        inkControl1.LoadPage(doc.Pages[currentInd]);
                    }
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.C)
            {
                inkControl1.CopySelection();
            }
            else if(e.Control && e.KeyCode == Keys.V)
            {
                inkControl1.PasteSelection();
            }
            else if(e.Control && e.KeyCode == Keys.X)
            {
                inkControl1.CopySelection();
                inkControl1.RemoveSelection();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                inkControl1.SelectAll();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                inkControl1.Page.Deselect();
                inkControl1.RefreshPage();
            }
            else if(e.Control && e.KeyCode == Keys.Z)
            {
                inkControl1.Undo();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                inkControl1.Redo();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                inkControl1.RemoveSelection();
            }
            base.OnKeyDown(e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            inkControl1.Undo();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            inkControl1.Redo();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            inkControl1.PasteSelection();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            inkControl1.CopySelection();
        }

        public KDocument GetDocument()
        {
            return doc;
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            inkControl1.CopySelection();
            inkControl1.RemoveSelection();
        }

        private void colorPicker1_Load(object sender, EventArgs e)
        {

        }

        private void btnBreakout_Click(object sender, EventArgs e)
        {
            if (wndBreakout == null)
            {
                wndBreakout = new Form();
                wndBreakout.TopMost = true;
                wndBreakout.MinimizeBox = wndBreakout.MaximizeBox = wndBreakout.ShowInTaskbar = false;
                wndBreakout.ClientSize = new Size((int)(600 * Util.GetScaleFactor()), (int)(400 * Util.GetScaleFactor()));
                wndBreakout.StartPosition = FormStartPosition.Manual;
                var screenBounds = Screen.PrimaryScreen.WorkingArea;
                wndBreakout.Location = new Point(screenBounds.Right - wndBreakout.Size.Width - 32,
                    screenBounds.Bottom - wndBreakout.Size.Height - 32);
                wndBreakout.Controls.Add(inkControl1);
                wndBreakout.Show();
                wndBreakout.Icon = Program.WindowIcon;
                wndBreakout.Text = "Kritzel - Overlay";

                wndBreakout.FormClosing += (_sender, _e) =>
                {
                    pnInkControlContainer.Controls.Add(inkControl1);
                    wndBreakout = null;
                    if (WindowState == FormWindowState.Minimized)
                        WindowState = FormWindowState.Maximized;
                    this.Activate();
                    inkControl1.Refresh();
                };
                wndBreakout.KeyDown += (_sender, _e) =>
                {
                    OnKeyDown(_e);
                };
            }
            else
            {
                wndBreakout.Close();
            }
        }

        protected override void WndProc(ref Message m)
        {
            uint WM_DRAWCLIPBOARD = 0x0308;
            if(m.Msg == WM_DRAWCLIPBOARD)
            {
                btnPaste.Visible = CopyPaster.CheckClipboard();
            }
            base.WndProc(ref m);
        }

        public void AddStampQuickAccess(List<Line> lines)
        {
            toolStripStamps.Show();
            ToolStripButton btn = new ToolStripButton();
            btn.Size = new Size(Util.GetGUISize(), Util.GetGUISize());
            btn.Image = Dialogues.StampMenu.CreateThumbnail(lines, new Size(Util.GetGUISize(), Util.GetGUISize()));
            btn.Click += Btn_Click;
            btn.Tag = lines;
            btn.MouseUp += Btn_MouseUp;
            toolStripStamps.Items.Add(btn);
        }

        private void Btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && sender is ToolStripItem)
            {
                ctxQuickAccessMenu.Tag = sender;
                ctxQuickAccessMenu.Show(toolStripStamps, e.Location);
            }
        }

        private void stampdeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ctxQuickAccessMenu.Tag is ToolStripItem)
            {
                toolStripStamps.Items.Remove(ctxQuickAccessMenu.Tag as ToolStripItem);
                if (toolStripStamps.Items.Count == 0)
                    toolStripStamps.Hide();
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if(sender is ToolStripItem && ((ToolStripItem)sender).Tag is List<Line>)
            {
                inkControl1.InkMode = InkMode.Stamp;
                inkControl1.Stamp = (List<Line>)((ToolStripItem)sender).Tag;
            }
        }
    }
}
