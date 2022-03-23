using Kritzel.GLRenderer;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d2d = SharpDX.Direct2D1;
using SharpDX;
using gdi = System.Drawing;

namespace Kritzel.Main.Renderer
{
    public class Image : IDisposable
    {
        public gdi.Bitmap GdiBitmap { get; private set; }
        public d2d.Bitmap D2DBitmap { get; private set; }
        public bool IsDisposed { get; private set; } = false;
        public int GLTextureID { get; private set; }
        public bool TextureFiltering { get; private set; } = true;

        public Image(gdi.Bitmap bmp)
        {
            this.GdiBitmap = bmp;
            GLTextureID = 0;
            D2DBitmap = null;
        }

        ~Image()
        {
            if (GLTextureID != 0)
                Kritzel.GLRenderer.Util.Leackage(this);
            if (D2DBitmap != null && !D2DBitmap.IsDisposed)
                D2DBitmap?.Dispose();
            D2DBitmap = null;
        }
        
        public static d2d.Bitmap LoadBitmap(d2d.RenderTarget renderTarget, gdi.Bitmap gdiBmp)
        {
            d2d.Bitmap result = null;

            //Lock the gdi resource
            BitmapData drawingBitmapData = gdiBmp.LockBits(
                new gdi.Rectangle(0, 0, gdiBmp.Width, gdiBmp.Height),
                ImageLockMode.ReadOnly, gdi.Imaging.PixelFormat.Format32bppPArgb);

            try
            {
                //Prepare loading the image from gdi resource
                DataStream dataStream = new DataStream(
                    drawingBitmapData.Scan0,
                    drawingBitmapData.Stride * drawingBitmapData.Height,
                    true, false);
                d2d.BitmapProperties properties = new d2d.BitmapProperties();
                properties.PixelFormat = new SharpDX.Direct2D1.PixelFormat(
                    SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                    d2d.AlphaMode.Premultiplied);

                //Load the image from the gdi resource
                result = new d2d.Bitmap(
                    renderTarget,
                    new Size2(gdiBmp.Width, gdiBmp.Height),
                    dataStream, drawingBitmapData.Stride,
                    properties);

                //Unlock the gdi resource
                gdiBmp.UnlockBits(drawingBitmapData);
            }
            catch(Exception e)
            {
                gdiBmp.UnlockBits(drawingBitmapData);
                throw e;
            }

            return result;
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            GdiBitmap.Dispose();
            UnloadGPU();
            if (D2DBitmap != null && !D2DBitmap.IsDisposed)
                D2DBitmap?.Dispose();
            D2DBitmap = null;
        }

        public void UnloadGPU()
        {
            if (GLTextureID != 0)
            {
                int id = GLTextureID;
                Opengl32.glDeleteTextures(1, ref id);
                GLTextureID = 0;
            }
            if(D2DBitmap != null && !D2DBitmap.IsDisposed)
                D2DBitmap?.Dispose();
            //D2DBitmap = null;
        }

        public void LoadGL()
        {
            UnloadGPU();
            int id = Kritzel.GLRenderer.Util.LoadTexture(GdiBitmap);
            GLTextureID = id;
        }

        public void LoadD2D(d2d.RenderTarget renderTarget)
        {
            UnloadGPU();
            D2DBitmap = LoadBitmap(renderTarget, GdiBitmap);
        }
    }
}
