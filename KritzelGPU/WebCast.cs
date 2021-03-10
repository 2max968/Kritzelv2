using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http.Headers;

namespace Kritzel.Main
{
    public class WebCast
    {
        static string host;
        static string castId;
        static WebClient client;
        static InkControl control;
        static bool uploadFinished = true;
        static Exception error = null;

        public static bool IsCasting { get; private set; } = false;

        public static string GetHost(string host)
        {
            if (!host.StartsWith("http://") && !host.StartsWith("https://"))
                host = "https://" + host;
            if (!host.EndsWith("/"))
                host += "/";
            return host;
        }

        public static async Task<int> StartCasting(string host, string castID, InkControl control)
        {
            WebCast.host = host;
            WebCast.castId = castID;
            WebCast.control = control;

            client = new WebClient();
            client.UploadDataCompleted += (object s, UploadDataCompletedEventArgs e) =>
            {
                error = e.Error;
                uploadFinished = true;
            };
            //client.BaseAddress = new Uri($"{host}");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/bmp"));
            try
            {
                string uri = $"{host}Create/{castID}";
                Program.MainLog.Add(MessageType.MSG, "Connecting to '{0}'", uri);
                //var response = await client.GetAsync(uri);
                //string text = await response.Content.ReadAsStringAsync();
                string text = client.DownloadString(new Uri(uri));
                Program.MainLog.Add(MessageType.MSG, $"Response from Server: '{text}'");
                if (!text.StartsWith("OK"))
                    return 1;
            }
            catch(Exception ex)
            {
                Program.MainLog.Add(ex);
                return 2;
            }
            IsCasting = true;
            BackgroundLoop();
            return 0;
        }

        public static async Task<bool> UpdateImage(KPage page)
        {
            SizeF pageSize = page.Format.GetPixelSize();
            using (Bitmap bmp = new Bitmap((int)pageSize.Width, (int)pageSize.Height))
            {
                MemoryStream stream = null;
                Task t = new Task(() =>
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        var r = g.GetRenderer();
                        lock (page)
                        {
                            page.DrawBackground(r);
                            page.Draw(r);
                        }
                    }
                    stream = new MemoryStream();
                    bmp.Save(stream, ImageFormat.Bmp);
                });
                t.Start();
                await t;
                uploadFinished = false;
                client.UploadDataAsync(new Uri($"{host}Load/{castId}"), "post", stream.ToArray());
                while(!uploadFinished)
                {
                    await Task.Delay(10);
                }
                stream.Close();
                stream.Dispose();
                if (error == null) return true;
                else return false;
            }
        }

        public static async void BackgroundLoop()
        {
            error = null;
            uint version = uint.MaxValue;
            while(IsCasting)
            {
                if(version != control.Page.Version)
                {
                    version = control.Page.Version;
                    bool success = await UpdateImage(control.Page);
                    if(!success)
                    {
                        IsCasting = false;
                        Dialogues.MsgBox.ShowOk(Language.GetText("Cast.web.close") + "\n" + error.Message);
                    }
                }
                await Task.Delay(500);
            }
        }

        public static void StopCasting()
        {
            IsCasting = false;
        }
    }
}
