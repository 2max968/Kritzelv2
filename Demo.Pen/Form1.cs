using Kritzel.PointerInputLibrary;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Pen
{
    public partial class Form1 : Form
    {
        PointerManager pm;
        List<Line> lines = new List<Line>();
        volatile Line current = null;
        WindowRenderTarget rt;
        Factory factory;
        SolidColorBrush brushBlack;
        SolidColorBrush brushWhite;
        Thread renderThread;
        bool running = true;
        List<Point> points = new List<Point>();
        SolidColorBrush pointBrush;

        public Form1()
        {
            InitializeComponent();

            pm = new PointerManager(this, Native.TWF.FINETOUCH);

            pm.PenDown += Pm_PenDown;
            pm.PenMove += Pm_PenMove;
            pm.PenUp += Pm_PenUp;

            factory = new Factory();
            rt = new WindowRenderTarget(factory, new RenderTargetProperties()
            {
                PixelFormat = new PixelFormat(SharpDX.DXGI.Format.R8G8B8A8_UNorm, AlphaMode.Unknown),
                Type = RenderTargetType.Default
            }, new HwndRenderTargetProperties()
            {
                Hwnd = this.Handle,
                PixelSize = new SharpDX.Size2(ClientSize.Width, ClientSize.Height),
                PresentOptions = PresentOptions.Immediately
            });
            brushBlack = new SolidColorBrush(rt, new SharpDX.Mathematics.Interop.RawColor4(0, 0, 0, 1));
            brushWhite = new SolidColorBrush(rt, new SharpDX.Mathematics.Interop.RawColor4(1, 1, 1, 1));
            pointBrush = new SolidColorBrush(rt, new SharpDX.Mathematics.Interop.RawColor4(0, 1, 0, 1));

            this.FormClosing += Form1_FormClosing;

            renderThread = new Thread(renderLoop);
            renderThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            renderThread.Join();
            brushBlack.Dispose();
            brushWhite.Dispose();
            pointBrush.Dispose();
            rt.Dispose();
            factory.Dispose();
        }

        private void Pm_PenUp(object sender, Touch e)
        {
            if(current != null)
            {
                current.Points.Add(new PointF(e.X, e.Y));
                lock (lines)
                {
                    lines.Add(current);
                }
                lock(current)
                    current = null;
            }
        }

        private void Pm_PenMove(object sender, Touch e)
        {
            if(current != null)
            {
                current.Points.Add(new PointF(e.X, e.Y));
                if(e.HistoryCount > 0)
                {
                    e.TranslateTrail(this);
                    lock(points)
                        points.AddRange(e.Trail);
                }
            }
        }

        private void Pm_PenDown(object sender, Touch e)
        {
            if (e.PenFlags == PenFlags.NONE)
            {
                current = new Line();
                current.Points.Add(new PointF(e.X, e.Y));
            }
        }

        protected override void WndProc(ref Message m)
        {
            pm?.HandleWndProc(ref m);
            base.WndProc(ref m);
        }

        void renderLoop()
        {
            while(running)
            {
                if(rt.Size.Width != ClientSize.Width || rt.Size.Height != ClientSize.Height)
                {
                    rt.Resize(new SharpDX.Size2(ClientSize.Width, ClientSize.Height));
                }
                rt.BeginDraw();
                rt.Clear(new SharpDX.Mathematics.Interop.RawColor4(1, 1, 1, 1));
                lock(lines)
                {
                    foreach(Line l in lines)
                    {
                        l.Draw(rt, brushBlack);
                    }
                }
                lock(points)
                {
                    foreach(Point p in points)
                    {
                        rt.DrawRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(p.X, p.Y, p.X, p.Y), pointBrush);
                    }
                }
                if (current != null)
                {

                    lock (current)
                        current.Draw(rt, brushBlack);
                }
                rt.EndDraw();
            }
        }
    }
}
