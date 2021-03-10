using Kritzel.HTTPServer;
using Kritzel.Main.Dialogues;
using System;
using System.Drawing;

namespace Kritzel.Main
{
    public class HTTPCast
    {
        public static string Password = "";
        static HttpServer server;
        static InkControl ink;

        public static bool IsCasting
        {
            get
            {
                if (server == null) return false;
                return server.Listen;
            }
        }

        public static void StartCasting(int port, InkControl ink, string password)
        {
            if (server != null && server.Active) return;
            HTTPCast.ink = ink;
            Log.SetConsoleHandler();
            server = new HttpServer(port);
            server.RegisterMethod("", mIndex);
            server.RegisterMethod("image", mImage);
            server.RegisterMethod("cast.html", mCast);
            server.RegisterMethod("index.html", mLogin);
            server.RegisterMethod("click", mClick);
            server.RegisterMethod("favicon.ico", mIcon);
            server.RegisterMethod("version", mVersion);
            server.Start();
        }

        public static void StopCasting()
        {
            if(server != null)
            {
                server.Stop();
            }
        }

        static void mIndex(HttpServer server, RequestHandler handler)
        {
            if(Password == "")
                handler.Response = new HttpResponseRedirect(server, "/cast.html");
            else
                handler.Response = new HttpResponseRedirect(server, "/index.html");
        }

        static void mCast(HttpServer server, RequestHandler handler)
        {
            string page = ResManager.GetText("web/Cast.html");
            handler.Response = new HttpResponseString(server, page);
            handler.Response.Head.ContentType = "text/html";
        }

        static void mLogin(HttpServer server, RequestHandler handler)
        {
            string page = ResManager.GetText("web/Index.html");
            handler.Response = new HttpResponseString(server, page);
            handler.Response.Head.ContentType = "text/html";
        }

        static void mImage(HttpServer server, RequestHandler handler)
        {
            bool currentVersion = false;
            if(handler.Head.Cookies != null && handler.Head.Cookies.ContainsKey("version"))
            {
                if (handler.Head.Cookies["version"] == pageInfo(ink.Page))
                    currentVersion = true;
            }
            if (currentVersion)
            {
                handler.Response = new HttpResponseNoAnswer(server);
            }
            else
            {
                SizeF size = ink.Page.Format.GetPixelSize();
                using (Bitmap bmp = new Bitmap((int)size.Width, (int)size.Height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.Clear(Color.White);
                        Renderer.GdiRenderer r = g.GetRenderer();
                        if (ink.Page.BackgroundImage != null)
                            g.DrawImage(ink.Page.BackgroundImage.GdiBitmap, new RectangleF(new PointF(0, 0), ink.Page.Format.GetPixelSize()));
                        ink.Page.Draw(r);
                    }

                    handler.Response = new HttpResponsePNG(server, bmp);
                    handler.Response.Head.Cookies.Add("version", pageInfo(ink.Page));
                    handler.Response.Head.Cookies.Add("w", "" + bmp.Width);
                    handler.Response.Head.Cookies.Add("h", "" + bmp.Height);
                }
            }
        }

        static void mClick(HttpServer server, RequestHandler handler)
        {
            try
            {
                float x = float.Parse(handler.Head.Get["x"]);
                float y = float.Parse(handler.Head.Get["y"]);

                MsgBox.ShowOk("Click at {" + x + ";" + y + "}");
            }
            catch (Exception) { }

            handler.Response = new HttpResponseString(server, "ok");
        }

        static void mIcon(HttpServer server, RequestHandler handler)
        {
            Bitmap bmp = Bitmap.FromHicon(Program.WindowIcon.Handle);
            HttpResponseBitmap resp = new HttpResponseBitmap(server, bmp);
            bmp.Dispose();
            handler.Response = resp;
        }

        static void mVersion(HttpServer server, RequestHandler handler)
        {
            handler.Response = new HttpResponseString(server, "{\"Version\":" + ink.Page.Version + "}");
        }

        static string pageInfo(KPage page)
        {
            return "" + page.Version + "#" + page.Name;
        }
    }
}
