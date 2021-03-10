#define OPENGL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dxgi = SharpDX.DXGI;
using d2d = SharpDX.Direct2D1;
#if (OPENGL)
using Kritzel.GLRenderer;
#endif
using gdi = System.Drawing;

namespace Kritzel.Main.Renderer
{
    public abstract class RenderBitmap : IDisposable
    {
        public bool IsDisposed { get; protected set; } = false;
        public abstract void Dispose();
        public abstract void Begin();
        public abstract void End();
    }
    
    public class RenderBitmap1 : RenderBitmap
    {
        d2d.BitmapRenderTarget renderTarget;

        public RenderBitmap1(d2d.RenderTarget parentRenderTarget)
        {
            renderTarget = new d2d.BitmapRenderTarget(parentRenderTarget, d2d.CompatibleRenderTargetOptions.None);
        }

        public d2d.RenderTarget GetRenderTarget()
        {
            return renderTarget;
        }

        public d2d.Bitmap GetBitmap()
        {
            return renderTarget.Bitmap;
        }

        public override void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            renderTarget.Dispose();
        }

        public override void Begin()
        {
            renderTarget.BeginDraw();
            renderTarget.Clear(gdi.Color.Transparent.ToColor4());
        }

        public override void End()
        {
            renderTarget.EndDraw();
        }
    }

    public class RenderBitmap2 : RenderBitmap
    {
        gdi.Bitmap buffer;
        gdi.Graphics g;

        public RenderBitmap2(gdi.Size size)
        {
            buffer = new gdi.Bitmap(size.Width, size.Height);
            g = gdi.Graphics.FromImage(buffer);
            g.SmoothingMode = gdi.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = gdi.Drawing2D.InterpolationMode.HighQualityBilinear;
        }

        public override void Dispose()
        {
            buffer?.Dispose();
            g?.Dispose();
            IsDisposed = true;
        }

        public override void Begin()
        {

        }

        public override void End()
        {

        }

        public gdi.Graphics GetGraphics()
        {
            return g;
        }

        public gdi.Bitmap GetBuffer()
        {
            return buffer;
        }
    }

    public class RenderBitmap3 : RenderBitmap
    {
        public FrameBufferObject FBO;

        public RenderBitmap3(int w, int h)
        {
            FBO = new FrameBufferObject(w, h, 4);
        }

        public override void Begin()
        {
            FBO.Bind();
            Opengl32.glClear(GLConsts.GL_COLOR_BUFFER_BIT);
        }

        public override void Dispose()
        {
            FBO.Dispose();
        }

        public override void End()
        {
            FBO.Blit();
        }
    }
}
