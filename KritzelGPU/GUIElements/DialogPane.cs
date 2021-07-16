using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using Kritzel.GLRenderer;
using SharpDX;
using d2d = SharpDX.Direct2D1;

namespace Kritzel.Main.GUIElements
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class DialogPane : PictureBox
    {
        const int WM_KEYDOWN = 0x0100;

        Bitmap buffer;
        Timer timerFadeIn = new Timer();
        Timer timerFadeOut = new Timer();
        int alpha = 0;
        Stopwatch stp = new Stopwatch();
        MainWindow.Callback callback;
        Control dialog;
        Control[] controls;

        d2d.Factory factory;
        d2d.WindowRenderTarget renderTarget;
        d2d.Bitmap bmp;

        public DialogPane(Control dialog, Bitmap buffer, MainWindow.Callback callback, params Control[] controls)
        {
            InitializeComponent();
            timerFadeIn.Tick += Timer_Tick;
            timerFadeIn.Interval = 10;
            timerFadeOut.Tick += TimerFadeOut_Tick;
            this.Resize += DialogPane_Resize;

            // Create Shadow
            //using (Graphics g = Graphics.FromImage(buffer))
            //{
            //    Renderer.Effects.DrawSlowBoxShadow(g, dialog.Bounds, 8);
            //}

            this.buffer = buffer;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = (Bitmap)buffer.Clone();
            this.SizeMode = PictureBoxSizeMode.Normal;
            this.BackColor = Style.Default.Background;
            this.callback = callback;
            this.dialog = dialog;
            this.controls = controls;

            this.Location = new Point(0, 0);
            this.Size = new Size(buffer.Width, buffer.Height);
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            if (dialog != null)
            {
                Controls.Add(dialog);
                dialog.Show();
                if (dialog is IClosable)
                {
                    ((IClosable)dialog).AddCloseHandler(delegate ()
                    {
                        Close();
                    });
                }
            }

            MouseClick += new MouseEventHandler(delegate (object s, MouseEventArgs args)
            {
                Close();
            });

            timerFadeIn.Start();
            stp.Start();

            factory = new d2d.Factory(d2d.FactoryType.SingleThreaded);
            renderTarget = new d2d.WindowRenderTarget(factory, new d2d.RenderTargetProperties()
            {
                
            }, new d2d.HwndRenderTargetProperties()
            {
                Hwnd = this.Handle,
                PresentOptions = d2d.PresentOptions.Immediately,
                PixelSize = new Size2(this.ClientSize.Width, this.ClientSize.Height)
            });
            bmp = Renderer.Image.LoadBitmap(renderTarget, buffer);
            DialogPane_Paint(null, null);

            this.Paint += DialogPane_Paint;
            this.Disposed += DialogPane_Disposed;
        }

        private void DialogPane_Disposed(object sender, EventArgs e)
        {
            bmp?.Dispose();
            renderTarget?.Dispose();
            factory?.Dispose();
        }

        private void DialogPane_Resize(object sender, EventArgs e)
        {
            /*if (!this.Visible) return;
            Graphics gDst = Graphics.FromImage(buffer);
            IntPtr hDst = gDst.GetHdc();
            foreach(Control cltr in controls)
            {
                Graphics gSrc = cltr.CreateGraphics();
                IntPtr hSrc = gSrc.GetHdc();
                Gdi32.BitBlt(hDst, cltr.Left, cltr.Top, cltr.Width, cltr.Height,
                    hSrc, 0, 0, (uint)CopyPixelOperation.SourceCopy);
                gSrc.ReleaseHdc(hSrc);
                gSrc.Dispose();
            }
            gDst.ReleaseHdc(hDst);
            gDst.Dispose();

            this.Image = buffer;*/
            renderTarget?.Resize(new Size2(this.ClientSize.Width, this.ClientSize.Height));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.IsDisposed || !this.Visible) return;
            alpha = (int)(0.3f * stp.ElapsedMilliseconds);
            /*Bitmap newbuff = new Bitmap(buffer.Width, buffer.Height);
            Graphics g = Graphics.FromImage(newbuff);
            g.DrawImage(buffer, new PointF(0, 0));
            using (Brush b = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                g.FillRectangle(b, 0, 0, buffer.Width, buffer.Height);
            g.Dispose();

            Image?.Dispose();
            Image = newbuff;*/

            DialogPane_Paint(null, null);

            if (alpha >= 100)
            {
                timerFadeIn.Stop();
                Bitmap bmp = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(buffer, new RectangleF(0, 0, buffer.Width, buffer.Height));
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
                        g.FillRectangle(b, new RectangleF(0, 0, this.Width, this.Height));
                }
                var tmp = this.Image;
                this.Image = bmp;
                tmp?.Dispose();
                // Create Shadow
                using (Graphics g = Graphics.FromImage(this.Image))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Renderer.Effects.DrawSlowBoxShadow(g, dialog.Bounds, 16);
                }
            }
        }
        bool closeLock = false;
        public void Close()
        {
            if (closeLock) return;
            closeLock = true;
            callback?.Invoke(dialog);

            foreach (Control cltr in Controls)
            {
                cltr.Hide();
                cltr.Dispose();
            }

            timerFadeOut.Interval = 10;
            timerFadeOut.Start();
            stp.Restart();
            var tmp = this.Image;
            this.Image = buffer;
            tmp?.Dispose();
        }

        private void TimerFadeOut_Tick(object sender, EventArgs e)
        {
            alpha = 100 - (int)(stp.ElapsedMilliseconds);
            if (alpha <= 0)
            {
                timerFadeOut.Stop();

                //this.Image?.Dispose();
                //this.Image = buffer;
                this.Refresh();
                this.Hide();
                buffer?.Dispose();
                Parent?.Controls.Remove(this);
                Dispose();
            }
            else
            {
                /*Bitmap newbuff = new Bitmap(buffer.Width, buffer.Height);
                Graphics g = Graphics.FromImage(newbuff);
                g.DrawImage(buffer, new PointF(0, 0));
                using (Brush b = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                    g.FillRectangle(b, 0, 0, buffer.Width, buffer.Height);
                g.Dispose();

                Image?.Dispose();
                Image = newbuff;*/
                DialogPane_Paint(null, null);
            }
        }

        private void DialogPane_Paint(object sender, PaintEventArgs e)
        {
            renderTarget.BeginDraw();
            renderTarget.Clear(new SharpDX.Mathematics.Interop.RawColor4(0, 0, 0, 1));
            renderTarget.DrawBitmap(bmp, 1 - (alpha / 255f), d2d.BitmapInterpolationMode.NearestNeighbor);
            //Renderer.Effects.DrawSlowBoxShadow(renderTarget, dialog.Bounds, 8);
            renderTarget.EndDraw();
        }

        public void RecreateShadows()
        {


        }
    }
}
