using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gdi = System.Drawing;
using PointF = System.Drawing.PointF;
using RectangleF = System.Drawing.RectangleF;
using gdi2d = System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Kritzel.Main.Renderer
{
    public abstract class GPURenderer : BaseRenderer, IDisposable
    {
        public bool Disposed { get; protected set; } = false;
        public float Width { get; protected set; }
        public float Height { get; protected set; }
        public bool IsHardware { get; protected set; }
        public bool Drawing { get; protected set; } = false;

        public abstract void Resize(gdi.Size size);
        public abstract void ResetTransform();
        public abstract void Transform(Matrix3x3 m);
        public abstract bool Begin();
        public abstract bool Begin(gdi.Color c);
        public abstract void End();
        public abstract void End(RectangleF rect);
        public abstract void FillRectangle(gdi.Color c, gdi.RectangleF rect);
        public abstract void EditPage();
        public abstract void EndEditPage();
        public abstract void DrawDashPolygon(PointF[] pts);
        public abstract RenderBitmap CreateRenderTarget();
        public abstract void SetRenderTarget(RenderBitmap bmp);
        public abstract void DrawRenderBitmap(RenderBitmap bmp);
        public abstract float GetScaleFactor();
        public abstract void DrawText(string text, PointF pos, float size, gdi.Color c);
        public virtual void Init() { }
        public abstract void BeginLines(gdi.Color c, float width);
        public abstract void BatchedLine(PointF p1, PointF p2);
        public abstract void EndLines();
        public abstract void Dispose();
        public abstract Matrix3x3 GetCurrentTransform();

        public static GPURenderer Create(System.Windows.Forms.Control cltr)
        {
            return Create(cltr, Configuration.Renderer);
        }

        public static GPURenderer Create(System.Windows.Forms.Control cltr, RenderMode renderMode)
        {
            if (Process.GetCurrentProcess().ProcessName == "devenv")
                return new GPURenderer2(cltr);
#if SLIMDX
            GPURenderer r = null;
            try
            {
                if (!tryHardware)
                    r = new GPURenderer2(cltr);
                else
                    r = new GPURenderer1(cltr);
            }
            catch(Exception e)
            {
                Console.WriteLine("Cant create Renderer\n" + e.Message);
                r?.Dispose();
                r = new GPURenderer2(cltr);
            }
            return r;
#else
            switch (renderMode)
            {
                case RenderMode.OpenGL:
                    return new GPURenderer3(cltr);
                case RenderMode.Direct2D:
                    return new GPURenderer1(cltr);
                case RenderMode.Software:
                    return new GPURenderer2(cltr);
                default:
                    throw new Exception("Unknown RenderMode " + renderMode.ToString());
            }
#endif
        }
    }
}
