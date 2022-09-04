using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gdi = System.Drawing;
using PointF = System.Drawing.PointF;
using RectangleF = System.Drawing.RectangleF;
using SharpDX;
using SharpDX.Direct2D1;
using gdi2d = System.Drawing.Drawing2D;
using text = SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace Kritzel.Main.Renderer
{
    public static class GPURenderTargetExt
    {
        public static RawColor4 ToColor4(this gdi.Color color)
        {
            return new RawColor4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

        public static Size2F ToSize2F(this gdi.SizeF size)
        {
            return new Size2F(size.Width, size.Height);
        }

        public static Size2 ToSize2(this gdi.Size size)
        {
            return new Size2(size.Width, size.Height);
        }

        public static RawVector2 ToVector(this gdi.PointF size)
        {
            return new RawVector2(size.X, size.Y);
        }

        public static RawRectangleF ToRectangleF(this gdi.RectangleF rect)
        {
            return new RawRectangleF(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
    }

    public class GPURenderer1 : GPURenderer
    {
        //public static RenderTarget RenderTarget { get; private set; } = null;
        static List<GPURenderer> refTrack = new List<GPURenderer>();
        static SolidColorBrush brush_gray = null;
        static Factory factory = null;
        static int factoryAccesCount = 0;

        Dictionary<int, Brush> brushes = new Dictionary<int, Brush>();
        text.Factory writeFactory;
        RenderTarget renderTarget;
        WindowRenderTarget mainRenderTarget;
        System.Windows.Forms.Control cltr;
        bool stateDraw = false;
        bool tLock = false;
        Matrix3x3 currentTransform = new Matrix3x3();
        Brush circleBrush = null;
        Brush lineBrush = null;
        float lineWidth = 1;

        public GPURenderer1(System.Windows.Forms.Control cltr)
        {
            Program.MainLog.Add(MessageType.MSG, "Creating D2D Renderer");
            IsHardware = true;
            refTrack.Add(this);
            float scale = GetScaleFactor();
            this.cltr = cltr;
            if (factory == null || factory.IsDisposed)
            {
                factory = new Factory(FactoryType.MultiThreaded, DebugLevel.Error);
                Program.MainLog.Add(MessageType.MSG, "Factory created: {0}", factory.NativePointer);
                factoryAccesCount = 1;
            }
            else
            {
                factoryAccesCount++;
                Program.MainLog.Add(MessageType.MSG, "Accessing Factory {0}, cnt = {1}", factory.NativePointer, factoryAccesCount);
            }
            HwndRenderTargetProperties hwndProps = new HwndRenderTargetProperties();
            RenderTargetProperties rtProps = new RenderTargetProperties();
            hwndProps.Hwnd = cltr.Handle;
            hwndProps.PixelSize = new Size2(cltr.Width, cltr.Height);
            hwndProps.PresentOptions = PresentOptions.Immediately;
            rtProps.Type = Configuration.D2D_RendertargetType;
            mainRenderTarget = new WindowRenderTarget(factory, rtProps, hwndProps);
            Program.MainLog.Add(MessageType.MSG, "Render Target created: {0}", mainRenderTarget.NativePointer);
            //RenderTarget = mainRenderTarget;
            Width = cltr.Width;
            Height = cltr.Height;
            mainRenderTarget.AntialiasMode = AntialiasMode.PerPrimitive;
            mainRenderTarget.DotsPerInch = new Size2F(
                mainRenderTarget.DotsPerInch.Width / scale, mainRenderTarget.DotsPerInch.Height / scale);

            writeFactory = new text.Factory();
            Program.MainLog.Add(MessageType.MSG, "Direct Write Factory created: {0}", writeFactory.NativePointer);
            
            if (brush_gray == null)
                brush_gray = new SolidColorBrush(mainRenderTarget, gdi.Color.Gray.ToColor4());

            renderTarget = mainRenderTarget;
            Program.MainLog.Add(MessageType.MSG, "Renderer initialized");
        }

        public override void Dispose()
        {
            if (Disposed) return;
            Program.MainLog.AddLong(0, MessageType.MSG, "Disposing D2D Renderer", 
                "Render Target: " + mainRenderTarget.NativePointer
                 + "\nFactory: " + factory.NativePointer);
            refTrack.Remove(this);
            Disposed = true;
            mainRenderTarget.Dispose();
            factoryAccesCount--;
            if (factoryAccesCount <= 0)
            {
                Program.MainLog.Add(MessageType.MSG, "Diposing Direct Write Factory {0}", writeFactory.NativePointer);
                writeFactory.Dispose();
                Program.MainLog.Add(MessageType.MSG, "Disposing D2D Factory");
                factory.Dispose();
            }
            foreach (var b in brushes)
                b.Value.Dispose();
            if (refTrack.Count == 0)
                DisposeStatic();
        }

        public static void DisposeStatic()
        {
            brush_gray.Dispose();
        }

        public override void Resize(gdi.Size size)
        {
            if (stateDraw) return;
            mainRenderTarget.Resize(size.ToSize2());
            this.Width = size.Width;
            this.Height = size.Height;
        }

        public override void DrawLine(gdi.Color c, float width, PointF p1, PointF p2, bool capStart = false, bool capEnd = false)
        {
            using (Brush b = new SolidColorBrush(renderTarget, c.ToColor4()))
            {
                CapStyle start = capStart ? CapStyle.Triangle : CapStyle.Flat;
                CapStyle end = capEnd ? CapStyle.Triangle : CapStyle.Flat;
                StrokeStyle style = new StrokeStyle(factory, new StrokeStyleProperties()
                {
                    StartCap = start,
                    EndCap = end
                });
                renderTarget.DrawLine(p1.ToVector(), p2.ToVector(), b, width, style);
                style.Dispose();
            }
        }

        public override void FillEllipse(PBrush c, RectangleF rect)
        {
            using (SolidColorBrush b = new SolidColorBrush(renderTarget, c.GetColor().ToColor4()))
            {
                Ellipse ell = new Ellipse();
                ell.Point = new RawVector2(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                ell.RadiusX = rect.Width / 2;
                ell.RadiusY = rect.Height / 2;
                renderTarget.FillEllipse(ell, b);
            }
        }

        public override void DrawEllipse(PBrush c, float width, RectangleF rect)
        {
            using (Brush b = GetBrush(c))
            {
                Ellipse ell = new Ellipse();
                ell.Point = new RawVector2(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                ell.RadiusX = rect.Width / 2;
                ell.RadiusY = rect.Height / 2;
                renderTarget.DrawEllipse(ell, b, width);
            }
        }

        public override void DrawRoundedLine(PBrush c, float width, PointF p1, PointF p2)
        {
            using (Brush b = GetBrush(c))
            {
                StrokeStyle style = new StrokeStyle(factory, new StrokeStyleProperties()
                {
                    StartCap = CapStyle.Round,
                    EndCap = CapStyle.Round
                });
                renderTarget.DrawLine(p1.ToVector(), p2.ToVector(), b, width, style);
                style.Dispose();
            }
        }

        public override void DrawRoundedRectangle(PBrush c, float width, RectangleF rect)
        {
            using (Brush b = GetBrush(c))
            {
                RoundedRectangle rr = new RoundedRectangle();
                rr.Rect.Left = rect.Left;
                rr.Rect.Right = rect.Right;
                rr.Rect.Top = rect.Top;
                rr.Rect.Bottom = rect.Bottom;
                rr.RadiusX = width / 2;
                rr.RadiusY = width / 2;
                renderTarget.DrawRoundedRectangle(rr, b, width);
            }
        }

        public override void DrawRect(gdi.Color c, float width, RectangleF rect)
        {
            using (Brush b = new SolidColorBrush(renderTarget, c.ToColor4()))
            {
                renderTarget.DrawRectangle(rect.ToRectangleF(), b, width);
            }
        }

        ~GPURenderer1()
        {
            Dispose();
        }

        public override void ResetTransform()
        {
            currentTransform = new Matrix3x3();
            renderTarget.Transform = new RawMatrix3x2();
        }

        public override void Transform(Matrix3x3 m)
        {
            currentTransform.Multiply(m);
            RawMatrix3x2 mat = new RawMatrix3x2(currentTransform[0], currentTransform[1],
                currentTransform[2], currentTransform[3],
                currentTransform[4], currentTransform[5]);
            renderTarget.Transform = mat;
        }

        public override bool Begin()
        {
            if (stateDraw) return false;
            while (tLock) ;
            stateDraw = true;
            mainRenderTarget.BeginDraw();
            return true;
        }

        public override bool Begin(gdi.Color color)
        {
            if (stateDraw) return false;
            while (tLock) ;
            stateDraw = true;
            mainRenderTarget.BeginDraw();
            mainRenderTarget.Clear(color.ToColor4());
            return true;
        }

        public override void End()
        {
            if (!stateDraw) return;
            mainRenderTarget.EndDraw();
            stateDraw = false;
        }

        public override void End(RectangleF rect)
        {
            if (!stateDraw) return;
            mainRenderTarget.EndDraw();
            stateDraw = false;
        }

        public override void FillRectangle(gdi.Color c, RectangleF rect)
        {
            using (Brush b = new SolidColorBrush(renderTarget, c.ToColor4()))
            {
                renderTarget.FillRectangle(rect.ToRectangleF(), b);
            }
        }

        public override void EditPage()
        {
            //tLock = true;
            //while (stateDraw) ;
        }

        public override void EndEditPage()
        {
            //tLock = false;
        }

        public override void DrawImage(Image img, RectangleF rect)
        {
            try
            {
                if (img.D2DBitmap == null || img.D2DBitmap.IsDisposed)
                {
                    img.LoadD2D(mainRenderTarget);
                }
                var intMode = img.TextureFiltering ? BitmapInterpolationMode.Linear : BitmapInterpolationMode.NearestNeighbor;
                renderTarget.DrawBitmap(img.D2DBitmap, rect.ToRectangleF(), 1, intMode);
            }
            catch (Exception e)
            {
                Program.MainLog.Add(e);
            }
        }

        public override void DrawDashPolygon(PointF[] pts)
        {
            if (pts.Length == 0) return;
            PathGeometry geo = new PathGeometry(factory);
            var sink = geo.Open();
            sink.BeginFigure(pts[0].ToVector(), FigureBegin.Filled);
            for (int i = 0; i < pts.Length; i++)
                sink.AddLine(pts[i].ToVector());
            sink.EndFigure(FigureEnd.Open);
            sink.Close();
            StrokeStyle style = new StrokeStyle(factory, new StrokeStyleProperties()
            {
                DashStyle = DashStyle.Dash
            });
            renderTarget.DrawGeometry(geo, brush_gray, 4, style);
            renderTarget.DrawLine(pts[0].ToVector(), pts[pts.Length - 1].ToVector(), brush_gray, 2, style);
            style.Dispose();
            sink.Dispose();
            geo.Dispose();
        }

        public override void FillPolygon(PBrush b, PointF[] pts)
        {
            if (pts.Length == 0) return;
            using (Brush brush = GetBrush(b))
            {
                PathGeometry geo = new PathGeometry(factory);
                var sink = geo.Open();
                sink.BeginFigure(pts[0].ToVector(), FigureBegin.Filled);
                for (int i = 0; i < pts.Length; i++)
                    sink.AddLine(pts[i].ToVector());
                sink.EndFigure(FigureEnd.Closed);
                sink.Close();
                renderTarget.FillGeometry(geo, brush);
                sink.Dispose();
                geo.Dispose();
            }
        }

        public override void DrawText(string str, PBrush brush, RectangleF rect, float size)
        {
            float sizept = Util.MmToPoint(size);
            float px = Util.MmToPoint(rect.X);
            float py = Util.MmToPoint(rect.Y);
            float pw = Util.MmToPoint(rect.Width);
            float ph = Util.MmToPoint(rect.Height);
            RawRectangleF rectpt = new RawRectangleF(px, py, pw, ph);

            text.TextFormat format = new text.TextFormat(writeFactory, "Arial", sizept);
            using (Brush b = GetBrush(brush))
            {
                renderTarget.DrawText(str, format, rectpt, b, DrawTextOptions.None, MeasuringMode.GdiNatural);
            }
        }

        public static explicit operator RenderTarget(GPURenderer1 r)
        {
            return r.mainRenderTarget;
        }

        public override RenderBitmap CreateRenderTarget()
        {
            return new RenderBitmap1(renderTarget);
        }

        public override void SetRenderTarget(RenderBitmap bmp)
        {
            if (bmp == null || !(bmp is RenderBitmap1))
                renderTarget = mainRenderTarget;
            else
                renderTarget = ((RenderBitmap1)bmp).GetRenderTarget();
        }

        public override void DrawRenderBitmap(RenderBitmap bmp)
        {
            if (!(bmp is RenderBitmap1))
                return;
            renderTarget.DrawBitmap(((RenderBitmap1)bmp).GetBitmap(), 1, BitmapInterpolationMode.Linear);
        }

        public override float GetScaleFactor()
        {
            return Util.GetScaleFactor();
        }

        public override void DrawText(string text, PointF pos, float size, gdi.Color c)
        {
            float sizept = Util.MmToPoint(size);
            float px = Util.MmToPoint(pos.X);
            float py = Util.MmToPoint(pos.Y);
            float pw = Util.MmToPoint(1000);
            float ph = Util.MmToPoint(1000);
            RawRectangleF rectpt = new RawRectangleF(px, py, pw, ph);

            using (SolidColorBrush brush = new SolidColorBrush(renderTarget, c.ToColor4()))
            {
                text.TextFormat format = new text.TextFormat(writeFactory, "Arial", sizept);
                renderTarget.DrawText(text, format, rectpt, brush);
                format.Dispose();
            }
        }

        public override void BeginCircles(PBrush brush)
        {
            circleBrush = GetBrush(brush);
        }

        public override void Circle(float x, float y, float r)
        {
            if (x < -10000 || x > 10000 || y < -10000 || y > 10000) return;
            Ellipse ell = new Ellipse(new RawVector2(x, y), r, r);
            renderTarget.FillEllipse(ell, circleBrush);
        }

        public override void EndCircle()
        {
            circleBrush?.Dispose();
            circleBrush = null;
        }

        public Brush GetBrush(PBrush pBrush)
        {
            return new SolidColorBrush(renderTarget, pBrush.GetColor().ToColor4());
        }

        public override void BeginLines(gdi.Color c, float width)
        {
            RawColor4 color = new RawColor4(c.R / 255f, c.G / 255f, c.B / 255, c.A / 255f);
            lineBrush = new SolidColorBrush(renderTarget, color);
            lineWidth = width;
        }

        public override void BatchedLine(PointF p1, PointF p2)
        {
            renderTarget.DrawLine(new RawVector2(p1.X, p1.Y), new RawVector2(p2.X, p2.Y), lineBrush);
        }

        public override void EndLines()
        {
            lineBrush?.Dispose();
        }

        public override void BeginRects(PBrush brush)
        {
            circleBrush = GetBrush(brush);
        }

        public override void Rect(RectangleF rect)
        {
            RawRectangleF rrect = new RawRectangleF(rect.Left, rect.Top, rect.Right, rect.Bottom);
            renderTarget.FillRectangle(rrect, circleBrush);
        }

        public override void EndRects()
        {
            circleBrush?.Dispose();
            circleBrush = null;
        }

        public override Matrix3x3 GetCurrentTransform()
        {
            return currentTransform;
        }

        public override void DrawText(string str, gdi.Color color, float x, float y, string fontFamily, float size, TextAlign align)
        {
            using (SolidColorBrush b = new SolidColorBrush(renderTarget,
                new RawColor4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f)))
            {

                using (text.TextFormat format = new text.TextFormat(writeFactory, fontFamily, Util.MmToPoint(size)))
                {
                    switch (align)
                    {
                        case TextAlign.Left: format.TextAlignment = text.TextAlignment.Leading; break;
                        case TextAlign.Center: format.TextAlignment = text.TextAlignment.Center; break;
                        case TextAlign.Right: format.TextAlignment = text.TextAlignment.Trailing; break;
                    }
                    using (text.TextLayout layout = new text.TextLayout(writeFactory, str, format, 1000, 1000, Util.GetScaleFactor(), true))
                    {
                        layout.MaxWidth = layout.Metrics.Width;
                        renderTarget.DrawTextLayout(new RawVector2(x, y), layout, b);
                    }
                }
            }
        }

        public override void DrawStroke(PBrush brush, IList<LPoint> points, float wScale)
        {
            if (points == null)
                return;
            var gdic = brush.GetColor();
            RawColor4 color = new RawColor4(gdic.R / 255f, gdic.G/ 255f, gdic.B / 255f, gdic.A / 255f);
            using (SolidColorBrush b = new SolidColorBrush(renderTarget, color))
            {
                using (StrokeStyle style = new StrokeStyle(factory, new StrokeStyleProperties()
                {
                    EndCap = CapStyle.Round,
                    StartCap = CapStyle.Round
                })) {
                    if (points.Count == 0)
                        return;
                    for(int i = 1; i < points.Count; i++)
                    {
                        LPoint pt = points[i];
                        LPoint lastPoint = points[i - 1];
                        if (pt == null || lastPoint == null)
                            return;
                        float width = pt.Rad * 2;
                        var p1 = new RawVector2(lastPoint.X, lastPoint.Y);
                        var p2 = new RawVector2(pt.X, pt.Y);
                        renderTarget.DrawLine(p1, p2, b, width + wScale, style);
                    }
                }
            }
        }
    }
}