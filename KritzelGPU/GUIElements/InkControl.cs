using Kritzel.Main.ScreenObject;
using Kritzel.PointerInputLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Kritzel.Main
{
    public enum InkMode { Pen, Line, Rect, Arc, Arc2, Marker };

    public class InkControl : PictureBox
    {
        KPage page = null;
        public KPage Page { get { return page; } }
        Line line = null;
        List<PointF> linePoints = null;
        PointF pos = new PointF(0, 0);
        PointerManager pm;
        System.Windows.Forms.Timer timer;
        bool active = true;
        public float Thicknes { get; set; } = 5;
        public PBrush Brush { get; set; } = PBrush.CreateSolid(Color.Black);
        public InkMode InkMode { get; set; } = InkMode.Pen;
        public PageFormat PageFormat
        {
            get
            {
                if (page == null) return null;
                return page.Format;
            }
            set
            {
                if (page == null) return;
                page.Format = value;
                var s = page.Format.GetPixelSize();
            }
        }
        volatile Matrix3x3 transform = new Matrix3x3();
        volatile Matrix3x3 stableTransform = new Matrix3x3();
        Matrix3x3 targetTransform = null;
        float targetT;
        bool lastMove = false;
        Point lastMovePoint = new Point(0, 0);
        bool lastTrans = false;
        FingerTransform lastTransInfo;
        Renderer.BaseRenderer renderer;
        public bool LockMove { get; set; } = false;
        public bool LockScale { get; set; } = false;
        public bool LockRotate { get; set; } = false;
        float bufferSize = 1;
        public float BufferSize
        {
            get { return bufferSize; }
            set
            {
                bufferSize = value;
                recreateBuffer();
            }
        }
        List<PointF> selections = null;
        bool lastMouseDown = false;
        Point lastMousePos = new Point(0, 0);
        public Renderer.GPURenderer gpuRenderer;
        Thread renderThread;
        Thread renderThread2;
        volatile bool running = true;
        volatile bool redraw = false;
        volatile bool fullredraw = false;
        volatile bool lockDraw = false;
        public bool LockDraw
        {
            get
            {
                return lockDraw;
            }
            set
            {
                bool tmp = lockDraw;
                lockDraw = value;
                redraw = true;
                if(value && !tmp)
                {
                    while (!waiting) ;
                }
            }
        }
        volatile bool waiting = false;
        Renderer.RenderBitmap rbmp = null;
        RectangleF rBounds = RectangleF.Empty;
        Line tmpLine = null;
        bool mouseDown = false;
        PointF mousePos;
        public List<BaseScreenObject> ScreenObjects { get; private set; } = new List<BaseScreenObject>();
        bool evaluateTmp = false;
        float transRotationValue = 0;
        float transScaleValue = 0;
        PointF transTranslateValue = new PointF(0, 0);
        bool transRotationLocked = true;
        bool transScaleLocked = true;
        bool transTranslateLocked = true;
        public Color EraserColor = Color.Transparent;
        Cursor blankCursor;
        bool cursorHidden = false;

        public InkControl()
        {
            pm = new PointerManager(this);
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            timer.Start();
            timer.Tick += timer_tick;
            this.Paint += this_paint;
            this.SizeChanged += InkControl_SizeChanged;
            this.VisibleChanged += InkControl_VisibleChanged;
            //SizeMode = PictureBoxSizeMode.StretchImage;
            renderer = new Renderer.GdiRenderer(CreateGraphics());
            gpuRenderer = Renderer.GPURenderer.Create(this, RenderMode.Software);

            this.MouseWheel += InkControl_MouseWheel;
            this.MouseMove += InkControl_MouseMove;
            this.MouseDown += InkControl_MouseDown;
            this.MouseUp += InkControl_MouseUp;

            recreateBufferFull();
            renderThread = new Thread(renderLoop);
            renderThread2 = new Thread(renderLoop2);

            /*MenuItem[] contextMenu = new MenuItem[]
            {
                new MenuItem("Copy"),
                new MenuItem("Paste")
            };
            this.ContextMenu = new ContextMenu(contextMenu);*/

            if (Process.GetCurrentProcess().ProcessName == "devenv")
                active = false;

            Stream stream = ResManager.GetStream("img/blank.cur");
            if (stream != null)
                blankCursor = new Cursor(stream);
            else
                blankCursor = Cursors.Default;
        }

        public Matrix3x3 GetTransform()
        {
            return transform.Clone();
        }

        private void InkControl_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (Configuration.HandleMouseInput && pm.Touches.Count == 0)
                {
                    mouseDown = true;
                    mousePos = e.Location;
                }
            }
        }

        private void InkControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }

        public void InitRenderer()
        {
            if (active)
            {
                gpuRenderer = Renderer.GPURenderer.Create(this, Configuration.Renderer);
                renderThread.Start();
                renderThread2.Start();
                recreateBufferFull();
            }
        }

        private void InkControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool onScreenObject = false;
            foreach (BaseScreenObject obj in ScreenObjects)
                if (obj.Collides(e.X, e.Y, Width, Height)) onScreenObject = true;
            if (e.Button == MouseButtons.Middle && Configuration.HandleMouseInput && !onScreenObject && pm.Touches.Count == 0)
            {
                if(!lastMouseDown)
                {
                    lastMouseDown = true;
                }
                else
                {
                    Point delta = new Point(e.X - lastMousePos.X, e.Y - lastMousePos.Y);
                    Matrix3x3 m = Matrix3x3.Translation(delta.X, delta.Y);
                    transform *= m;
                    recreateBufferFull();
                }
                lastMousePos = new Point(e.X, e.Y);
            }
            else if(e.Button == MouseButtons.Right && Configuration.HandleMouseInput && !onScreenObject && pm.Touches.Count == 0)
            {
                for (int i = 0; i < page.LineCount; i++)
                {
                    PointF pos = e.Location;
                    transform.GetInverse().Transform(ref pos);
                    if (page.GetLine(i).Collision(new LPoint(pos.X, pos.Y, 7)))
                    {
                        gpuRenderer.EditPage();
                        page.RemoveLine(i--);
                        gpuRenderer.EndEditPage();
                        recreateBufferFull();
                        break;
                    }
                }
            }
            else
            {
                lastMouseDown = false;
            }

            if(mouseDown)
            {
                mousePos = e.Location;
            }
        }

        private void InkControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pm.Touches.Count > 0) return;
            float factor = 1000 + e.Delta;
            if (factor > 1500) factor = 2000;
            if (factor < 500) factor = 500;
            factor /= 1000f;
            Matrix3x3 m = new Matrix3x3();
            m *= Matrix3x3.Translation(-e.X, -e.Y);
            m *= Matrix3x3.Scale(factor);
            m *= Matrix3x3.Translation(e.X, e.Y);
            transform *= m;
            recreateBufferFull();
        }

        private void InkControl_VisibleChanged(object sender, EventArgs e)
        {
            this.PageFormat = this.PageFormat;
        }

        private void InkControl_SizeChanged(object sender, EventArgs e)
        {
            recreateBuffer();
        }

        protected override void WndProc(ref Message m)
        {
            if(active) pm.HandleWndProc(ref m);
            if (Native.PreFilterMessage(ref m)) return;
            if(m.Msg == Native.WM_TABLET_QUERYSYSTEMGESTURESTATUS)
            {
                m.Result = (IntPtr)Native.TABLET_DISABLE_FLICKS;
                return;
            }
            base.WndProc(ref m);
        }

        private void timer_tick(object sender, EventArgs e)
        {
            // Search for Pen / Finger
            Touch pen = null;
            Touch mouse = null;
            List<Touch> fingers = new List<Touch>();

            foreach (Touch t in pm.Touches.Values)
            {
                if (t.TouchDevice == TouchDevice.Pen && t.Down)
                {
                    pen = t;
                    break;
                }
                if (t.TouchDevice == TouchDevice.Finger)
                    fingers.Add(t);
                if (t.TouchDevice == TouchDevice.Mouse && t.Down)
                    mouse = t;
            }
            if(mouseDown)
            {
                mouse = new Touch((int)mousePos.X, (int)mousePos.Y, Touch.MOUSE_ID, TouchDevice.Mouse, Touch.MAX_PREASSURE, PenFlags.NONE, true);
            }

            // Show / Hide Cursor
            if (pen != null && pen.Down && !cursorHidden)
            {
                this.Cursor = blankCursor;
                cursorHidden = true;

            }
            else if ((pen == null || !pen.Down) && cursorHidden)
            {
                this.Cursor = Cursors.Default;
                cursorHidden = false;
            }

            // Screen Objects
            for(int i = 0; i < ScreenObjects.Count; i++)
            {
                if (ScreenObjects[i].Think(fingers, ref pen, ref mouse, this.Width, this.Height))
                    recreateBuffer();
                if(ScreenObjects[i].Close)
                {
                    ScreenObjects[i].Dispose();
                    ScreenObjects.RemoveAt(i--);
                }
            }

            // Eraser
            if (pen != null && (pen.PenFlags & PenFlags.INVERTED) == PenFlags.INVERTED)
            {
                for (int i = 0; i < page.LineCount; i++)
                {
                    if (EraserColor != Color.Transparent && EraserColor != page.GetLine(i).Brush.GetColor())
                        continue;
                    PointF[] pts = new PointF[1];
                    pts[0] = new PointF(pen.X, pen.Y);
                    transform.GetInverse().Transform(pts);
                    if (page.GetLine(i).Collision(new LPoint(pts[0].X, pts[0].Y, pen.Pressure/100)))
                    {
                        gpuRenderer.EditPage();
                        page.RemoveLine(i--);
                        gpuRenderer.EndEditPage();
                        recreateBufferFull();
                        break;
                    }
                }

                pen = null;
            }

            // Select
            if (pen != null && (pen.PenFlags & PenFlags.BARREL) == PenFlags.BARREL)
            {
                if (selections == null)
                {
                    selections = new List<PointF>();
                    lock (selections)
                        selections.Add(new PointF(pen.X, pen.Y));
                }
                else
                {
                    lock (selections)
                        selections.Add(new PointF(pen.X, pen.Y));
                    recreateBuffer();
                }
                pen = null;
            }
            else if (selections != null)
            {
                PointF[] pts = selections.ToArray();
                selections = null;
                transform.GetInverse().Transform(pts);
                Page.SelectArea(pts);
                recreateBufferFull();
                ShowSelectionMenu();
            }

            // Move
            if (fingers.Count == 1 && !LockMove)
            {
                Point fp = new Point(fingers[0].X, fingers[0].Y);
                if (lastMove)
                {
                    int deltaX = fp.X - lastMovePoint.X;
                    int deltaY = fp.Y - lastMovePoint.Y;
                    transTranslateValue.X += deltaX;
                    transTranslateValue.Y += deltaY;
                    if (Math.Abs(transTranslateValue.X) > Configuration.TransformTranslateThreshold
                        || Math.Abs(transTranslateValue.Y) > Configuration.TransformTranslateThreshold)
                        transTranslateLocked = false;

                    if (!transTranslateLocked)
                    {
                        Matrix3x3 mat = Matrix3x3.Translation(deltaX, deltaY);
                        transform *= mat;
                    }
                }
                lastMove = true;
                lastMovePoint = fp;
                pen = null;
                if (Configuration.RefreshOnTransform) recreateBufferFull();
                else recreateBuffer();
            }
            else if(lastMove)
            {
                lastMove = false;
                recreateBufferFull();
                transTranslateLocked = true;
                transTranslateValue = new PointF(0, 0);
            }

            // Transform
            if (fingers.Count == 2
                && !(LockMove && LockScale && LockRotate))
            {
                FingerTransform t = new FingerTransform(fingers[0].X, fingers[0].Y, fingers[1].X, fingers[1].Y);
                if (lastTrans)
                {
                    float dd = 1 + (t.Distance - lastTransInfo.Distance) / lastTransInfo.Distance;
                    float dr = t.Rotation - lastTransInfo.Rotation;
                    Point dp = new Point(t.Position.X - lastTransInfo.Position.X,
                        t.Position.Y - lastTransInfo.Position.Y);

                    transRotationValue += dr;
                    if (Math.Abs(transRotationValue) > Configuration.TransformRotationThreshold)
                        transRotationLocked = false;
                    transScaleValue *= dd;
                    float sLim1 = 1 - Configuration.TransformScaleThreshold;
                    float sLim2 = 1 + Configuration.TransformScaleThreshold;
                    if (transScaleValue < sLim1 || transScaleValue > sLim2)
                        transScaleLocked = false;
                    transTranslateValue.X += dp.X;
                    transTranslateValue.Y += dp.Y;
                    if (Math.Abs(transTranslateValue.X) > Configuration.TransformTranslateThreshold
                        || Math.Abs(transTranslateValue.Y) > Configuration.TransformTranslateThreshold)
                        transTranslateLocked = false;

                    if (LockMove || transTranslateLocked) dp = new Point(0, 0);
                    if (LockScale || transScaleLocked) dd = 1;
                    if (LockRotate || transRotationLocked) dr = 0;

                    Matrix3x3 mat = new Matrix3x3();
                    mat.TransformTranslate(dp.X, dp.Y);
                    mat.TransformTranslate(-t.Position.X, -t.Position.Y);
                    mat.TransformScale(dd);
                    mat.TransformTranslate(t.Position.X, t.Position.Y);
                    mat.TransformRotateAt(-dr, t.Position.X, t.Position.Y);
                    transform *= mat;
                    if (Configuration.RefreshOnTransform) recreateBufferFull();
                    else recreateBuffer();
                }
                lastTrans = true;
                lastTransInfo = t;
            }
            else if(lastTrans)
            {
                lastTrans = false;
                recreateBufferFull();
                transRotationValue = 0;
                transScaleValue = 1;
                transRotationLocked = true;
                transScaleLocked = true;
                transTranslateLocked = true;
            }

            // Draw
            Touch drawDev = pen | mouse;
            if (line != null)
            {
                if (drawDev != null)
                {
                    float rad;
                    rad = line.CalcRad(drawDev.Pressure, Thicknes);
                    PointF[] p = new PointF[1];
                    p[0] = new PointF(drawDev.X, drawDev.Y);
                    linePoints.Add(p[0]);
                    transform.GetInverse().Transform(p);
                    line.AddPoint(p[0].X, p[0].Y, rad);
                    RecreateBuffer(Util.GetBounds(linePoints.ToArray()).Expand(8));
                    
                }
                else
                {
                    line.CalcSpline();
                    gpuRenderer.EditPage();
                    page.AddLine(line);
                    page.Deselect();
                    gpuRenderer.EndEditPage();
                    //while (gpuRenderer.Drawing) ;
                    //Util.WaitTimeout(gpuRenderer, gpuRenderer.GetType().GetProperty("Drawing"), 500);
                    tmpLine = line;
                    line = null;
                    linePoints = null;
                    recreateBufferFull();
                }
            }
            else if (drawDev != null)
            {
                pen?.Trail.Clear();
                PointF[] p = new PointF[1];
                long pressure;
                p[0] = new PointF(drawDev.X, drawDev.Y);
                pressure = drawDev.Pressure;
                transform.GetInverse().Transform(p);
                linePoints = new List<PointF>();
                if (InkMode == InkMode.Pen)
                {
                    line = new Line();
                    line.Brush = Brush;
                    line.AddPoint(p[0].X, p[0].Y, line.CalcRad(pressure, Thicknes));
                }
                else if(InkMode == InkMode.Line)
                {
                    line = new Forms.LinearLine();
                    line.Brush = Brush;
                    line.AddPoint(p[0].X, p[0].Y, line.CalcRad(pressure, Thicknes));
                }
                else if (InkMode == InkMode.Rect)
                {
                    line = new Forms.Rect();
                    line.Brush = Brush;
                    line.AddPoint(p[0].X, p[0].Y, line.CalcRad(pressure, Thicknes));
                }
                else if (InkMode == InkMode.Arc)
                {
                    line = new Forms.Arc();
                    line.Brush = Brush;
                    line.AddPoint(p[0].X, p[0].Y, line.CalcRad(pressure, Thicknes));
                }
                else if (InkMode == InkMode.Arc2)
                {
                    line = new Forms.Arc2();
                    line.Brush = Brush;
                    line.AddPoint(p[0].X, p[0].Y, line.CalcRad(pressure, Thicknes));
                }
                else if(InkMode == InkMode.Marker)
                {
                    line = new Forms.Marker();
                    line.Brush = Brush;
                    line.AddPoint(p[0].X, p[0].Y, line.CalcRad(pressure, Thicknes));
                }
            }

            // Evaluate Transformation
            if (page != null)
            {
                RectangleF rect = page.GetTransformedBoundaryBox(transform);
                float border = Util.GetGUISize() * 2;
                float force = .2f;
                float constForce = .3f;
                float minSize = .3f;
                if (rect.Right < border)
                {
                    transform.TransformTranslate((border - rect.Right) * force + constForce, 0);
                    if (Configuration.RefreshOnTransform) recreateBufferFull();
                    else recreateBuffer();
                    evaluateTmp = true;
                    fullredraw = false;
                }
                else if (rect.Bottom < border)
                {
                    transform.TransformTranslate(0, (border - rect.Bottom) * force + constForce);
                    if (Configuration.RefreshOnTransform) recreateBufferFull();
                    else recreateBuffer();
                    evaluateTmp = true;
                    fullredraw = false;
                }
                else if (rect.Left > this.Width - border)
                {
                    transform.TransformTranslate((this.Width - border - rect.Left) * force - constForce, 0);
                    if (Configuration.RefreshOnTransform) recreateBufferFull();
                    else recreateBuffer();
                    evaluateTmp = true;
                    fullredraw = false;
                }
                else if (rect.Top > this.Height - border)
                {
                    transform.TransformTranslate(0, (this.Height - border - rect.Top) * force - constForce);
                    if (Configuration.RefreshOnTransform) recreateBufferFull();
                    else recreateBuffer();
                    evaluateTmp = true;
                    fullredraw = false;
                }
                else if (rect.Width < this.Width * minSize
                    && rect.Height < this.Height * minSize)
                {
                    float scale1 = rect.Width / (this.Width * minSize);
                    float scale2 = rect.Height / (this.Height * minSize);
                    float scale = 1 + Math.Max(scale1, scale2) * force / 7;
                    var center = page.GetTransformedCenter(transform);
                    transform.TransformScaleAt(scale, scale, center.X, center.Y);

                    if (Configuration.RefreshOnTransform) recreateBufferFull();
                    else recreateBuffer();
                    evaluateTmp = true;
                    fullredraw = false;
                }
                else if (evaluateTmp)
                {
                    evaluateTmp = false;
                    recreateBufferFull();
                }
            }
        }

        private void this_paint(object sender, PaintEventArgs e)
        {
            recreateBufferFull();
        }

        void recreateBuffer()
        {
            this.rBounds = RectangleF.Empty;
            redraw = true;
        }

        void RecreateBuffer(RectangleF bounds)
        {
            this.rBounds = bounds;
            redraw = true;
        }

        void recreateBufferFull()
        {
            this.rBounds = RectangleF.Empty;
            redraw = true;
            fullredraw = true;
        }

        void RecreateBufferFull(RectangleF bounds)
        {
            this.rBounds = bounds;
            redraw = true;
            fullredraw = true;
        }

        public void Print(PrintDocument doc)
        {
            
        }

        public string SaveToString()
        {
            return page.SaveToString();
        }

        public void LoadPage(KPage page)
        {
            this.page?.OnHide();
            this.page?.BackgroundImage?.UnloadGPU();
            this.page = page;
            this.page.OnShow(this);
            //ResetTransformation(true);
            RefreshPage();
        }

        public void RefreshPage()
        {
            recreateBufferFull();
        }

        public void ResetRotation()
        {
            SizeF size = Page.Format.GetPixelSize();
            RectangleF pageRect = Page.GetTransformedBoundaryBox(transform);
            RectangleF screenRect = new RectangleF(0, 0, Width, Height);
            pageRect.Intersect(screenRect);
            PointF center = new PointF((pageRect.Left + pageRect.Right) / 2, (pageRect.Top + pageRect.Bottom) / 2);
            //transform.Transform(ref center);
            Matrix3x3 rotation = new Matrix3x3(transform);
            rotation.TransformRotateAt(-transform.GetRotation(), center.X, center.Y);
            targetTransform = rotation;
            targetT = 0;
        }

        public void CenterPage()
        {
            float sRatio = this.Width / (float)this.Height;
            float pRatio = Page.Format.Width / page.Format.Height;
            float px = 0, py = 0;
            float scale = 1;
            if(pRatio > sRatio)
            {
                SizeF s = page.Format.GetPixelSize();
                scale = this.Width / s.Width;
                py = (this.Height - s.Height * scale) / 2;
            }
            else
            {
                SizeF s = page.Format.GetPixelSize();
                scale = this.Height / s.Height;
                px = (this.Width - s.Width * scale) / 2;
            }
            Matrix3x3 newTrans = new Matrix3x3();
            newTrans.TransformScaleAt(scale, scale, 0, 0);
            newTrans.TransformTranslate(px, py);
            targetTransform = newTrans;
            targetT = 0;
        }

        public void ResetTransformation(bool imidiate)
        {
            float px = 0, py = 0;
            float scale = Util.GetRealScreenDPIFactor();
            SizeF s = page.Format.GetPixelSize();
            if (this.Height > s.Height * scale)
                py = (this.Height - s.Height * scale) / 2;
            else
                py = 0;
            if (this.Width > s.Width * scale)
                px = (this.Width - s.Width * scale) / 2;
            else
                px = 0;
            Matrix3x3 newTrans = new Matrix3x3();
            newTrans.TransformScaleAt(scale, scale, 0, 0);
            newTrans.TransformTranslate(px, py);
            if (imidiate)
            {
                transform = newTrans;
                recreateBufferFull();
            }
            else
            {
                targetTransform = newTrans;
                targetT = 0;
            }
        }

        public void ResetScale()
        {
            SizeF size = Page.Format.GetPixelSize();
            PointF center = new PointF(size.Width / 2, size.Height / 2);
            transform.Transform(ref center);
            float scale = 1f / transform.GetScale();
            transform.TransformScaleAt(scale, scale, center.X, center.Y);
            Refresh();
        }
        
        void renderLoop()
        {
            gpuRenderer.Init();
            while (running)
            {
                if (redraw || fullredraw || targetTransform != null)
                {
                    if(lockDraw)
                    {
                        waiting = true;
                        continue;
                    }
                    try
                    {
                        Matrix3x3 mat = transform;
                        if(targetTransform != null)
                        {
                            targetT += .03f;
                            if(targetT < 1)
                                mat = Matrix3x3.Slerp(transform, targetTransform, -(float)Math.Cos(targetT * Math.PI) * .5f + .5f);
                            else
                            {
                                fullredraw = true;
                                transform = mat = targetTransform;
                                targetTransform = null;
                            }

                            if (Configuration.RefreshOnTransform) fullredraw = true;
                        }
                        Matrix3x3 tMat = mat.Clone();
                        Matrix3x3 dMat = (stableTransform.GetInverse() * mat).Clone();

                        waiting = false;
                        redraw = false;
                        //if (this.Height <= 0 || this.Width <= 0) return;
                        int vW = (int)(this.Width * BufferSize);
                        int vH = (int)(this.Height * BufferSize);
                        if (rbmp == null || vW != gpuRenderer.Width || vH != gpuRenderer.Height)
                        {
                            gpuRenderer.Resize(new Size(vW, vH));
                            rbmp?.Dispose();
                            rbmp = gpuRenderer.CreateRenderTarget();
                        }
                        if (!gpuRenderer.Begin(Style.Default.Background)) return;
                        SizeF pSize = page.Format.GetPixelSize();
                        if (fullredraw)
                        {
                            stableTransform = transform;
                            dMat = new Matrix3x3();
                            rbmp.Begin();
                            fullredraw = false;
                            gpuRenderer.SetRenderTarget(rbmp);
                            gpuRenderer.Begin(SystemColors.ControlDark);
                            gpuRenderer.ResetTransform();
                            gpuRenderer.Transform(tMat);

                            if (page.OriginalPage == null)
                            {
                                if (page.BackgroundImage == null)
                                    gpuRenderer.FillRectangle(Util.ApplyFilter(Color.White, Page.Filter), new RectangleF(
                                        0, 0, pSize.Width, pSize.Height));
                                else
                                    gpuRenderer.DrawImage(page.BackgroundImage,
                                        new RectangleF(0, 0, pSize.Width, pSize.Height));
                            }
                            else
                            {
                                gpuRenderer.FillRectangle(Color.White, new RectangleF(
                                        0, 0, pSize.Width, pSize.Height));
                            }

                            page.DrawBackground(gpuRenderer);

                            if (page.OriginalPage != null
                                && page.BackgroundImage != null)
                            {
                                gpuRenderer.DrawImage(page.BackgroundImage,
                                    new RectangleF(0, 0, pSize.Width, pSize.Height));
                            }

                            page.Draw(gpuRenderer);
                            rbmp.End();
                            gpuRenderer.SetRenderTarget(null);
                            tmpLine = null;
                        }

                        gpuRenderer.ResetTransform();
                        gpuRenderer.Transform(dMat);
                        gpuRenderer.DrawRenderBitmap(rbmp);
                        gpuRenderer.ResetTransform();
                        gpuRenderer.Transform(tMat);
                        gpuRenderer.DrawRect(Color.Black, 1, new RectangleF(0, 0, pSize.Width, pSize.Height));
                        if(tmpLine != null)
                        {
                            tmpLine.Render(gpuRenderer);
                        }
                        if (line != null)
                        {
                            line.Render(gpuRenderer);
                        }
                        if (selections != null)
                        {
                            lock (selections)
                            {
                                PointF[] pts = selections.ToArray();
                                transform.GetInverse().Transform(pts);
                                gpuRenderer.DrawDashPolygon(pts);
                            }
                        }

                        // Screen Objects
                        foreach (BaseScreenObject bso in ScreenObjects)
                        {
                            gpuRenderer.ResetTransform();
                            bso.Draw(gpuRenderer, this.Width, this.Height);
                        }

                        gpuRenderer.ResetTransform();
                        gpuRenderer.Transform(Matrix3x3.Translation(0, 0));
                        gpuRenderer.DrawLine(Color.Black, 3, new PointF(0, 0), new PointF(this.Width, 0));
                        Renderer.Effects.DrawShadow(gpuRenderer, new RectangleF(0, 0, Width, 16));
                        if (Configuration.LeftHanded)
                        {
                            gpuRenderer.DrawLine(Color.Black, 3, new PointF(this.Width, 0), new PointF(this.Width, this.Height));
                            gpuRenderer.Transform(Matrix3x3.RotateD(-90));
                            gpuRenderer.Transform(Matrix3x3.Translation(Width, 0));
                            Renderer.Effects.DrawShadow(gpuRenderer, new RectangleF(0, 0, Height, 16));
                            gpuRenderer.ResetTransform();
                        }
                        else
                        {
                            gpuRenderer.DrawLine(Color.Black, 3, new PointF(0, 0), new PointF(0, this.Height));
                            gpuRenderer.Transform(Matrix3x3.RotateD(90));
                            Renderer.Effects.DrawShadow(gpuRenderer, new RectangleF(0, 0, -Height, 16));
                            gpuRenderer.ResetTransform();
                        }

                        Control _p = Parent;
                        Form _parentForm;
                        while (!(_p is Form))
                        {
                            _p = _p.Parent;
                        }
                        _parentForm = (Form)_p;

                        if(_parentForm.FormBorderStyle == FormBorderStyle.None)
                        {
                            string fullscreenInfo = "";
                            if (Configuration.ShowBattery)
                                fullscreenInfo += Language.GetText("Overlay.battery") + ": " + (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100) + "%\n";
                            if (Configuration.ShowTime)
                                fullscreenInfo += Language.GetText("Overlay.time") + ": " + DateTime.Now.ToShortTimeString() + "\n";
                            if (Configuration.ShowDate)
                                fullscreenInfo += Language.GetText("Overlay.date") + ": " + DateTime.Now.ToShortDateString() + "\n";

                            if (fullscreenInfo.Length > 1)
                            {
                                SizeF size = CreateGraphics().MeasureString(fullscreenInfo, new Font("Calibri", Util.MmToPoint(5)));
                                RectangleF rect = new RectangleF(Width - size.Width - 16, Height - size.Height - 16, size.Width, size.Height);
                                gpuRenderer.FillRectangle(Color.Black, rect);
                                gpuRenderer.DrawText(fullscreenInfo, 
                                    new PointF(Util.PointToMm(rect.X), Util.PointToMm(rect.Y)), 
                                    5, Color.White);
                            }
                        }

                        if (rBounds.Equals(RectangleF.Empty))
                            gpuRenderer.End();
                        else
                            gpuRenderer.End(rBounds);
                    }
                    catch(InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (FieldAccessException)
                    {
                        gpuRenderer.End();
                    }
                }
                Thread.Sleep(10);
            }
            gpuRenderer.Dispose();
        }

        void renderLoop2()
        {
            Graphics g = this.CreateGraphics();
            Renderer.BaseRenderer r = g.GetRenderer();
            while (running)
            {
                try
                {
                    //g.Transform = transform.CreateGdiMatrix();
                    //line?.Render(r);
                    //g.Dispose();
                }
                catch (Exception) { }
                Thread.Sleep(10);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(!this.IsDisposed)
            {
                Console.WriteLine("Stopping renderer");
                running = false;
                renderThread.Join();
                renderThread2.Join();
                rbmp?.Dispose();
                gpuRenderer?.Dispose();
                foreach (var obj in ScreenObjects)
                    obj.Dispose();
            }
            try
            {
                base.Dispose(disposing);
            }
            catch(InvalidOperationException)
            {

            }
        }

        public void CopySelection()
        {
            CopyPaster.CopyLine(page.GetSelectedLines().ToArray(), page.GetHashCode());
        }

        public void PasteSelection()
        {
            page.Deselect();
            Line[] lines = CopyPaster.PasteLines(page.GetHashCode());
            page.AddLines(lines);
            ShowSelectionMenu();
            RefreshPage();
        }

        public void RemoveSelection()
        {
            for(int i = 0; i < page.LineCount; i++)
            {
                if (page.GetLine(i).Selected)
                    page.RemoveLine(i--);
            }
            RefreshPage();
        }

        public void SelectAll()
        {
            foreach (Line l in page.EnumerateLines())
                l.Selected = true;
            ShowSelectionMenu();
            RefreshPage();
        }

        public void ShowSelectionMenu()
        {
            foreach (BaseScreenObject bso in ScreenObjects)
                if (bso is Transformer)
                    bso.Close = true;

            TransformerRotate rot = new TransformerRotate(this.page, this);
            TransformerScale scalX = new TransformerScale(this.page, this, 0);
            TransformerScale scalY = new TransformerScale(this.page, this, 90);
            TransformerScale scalXY = new TransformerScale(this.page, this, 45);
            TransformerTranslate trans = new TransformerTranslate(this.page, this, rot, scalX, scalY, scalXY);
            ScreenObjects.Add(rot);
            ScreenObjects.Add(scalX);
            ScreenObjects.Add(scalY);
            ScreenObjects.Add(scalXY);
            ScreenObjects.Add(trans);
        }

        public void Undo()
        {
            if (!HistoryManager.Undo(Page))
                SystemSounds.Beep.Play();
            else
                RefreshPage();
        }

        public void Redo()
        {
            if (!HistoryManager.Redo(Page))
                SystemSounds.Beep.Play();
            else
                RefreshPage();
        }
    }
}
