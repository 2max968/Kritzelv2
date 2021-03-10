using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;
using System.Runtime.InteropServices;
using Ionic.Zip;

namespace Kritzel.Main
{
    public class ResManager
    {
        public const string PACKNAME = "Kritzel.pck";
#if CONF_SINGLEDIRECTORY
        public static readonly string APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static readonly string CONFDIR = $@"{APPDATA}\Kritzel\";
        public static readonly string LOGDIR = $"{CONFDIR}logs\\";
        public static readonly string TMPDIR = $"{Path.GetTempPath()}Kritzel\\";
#else
        public static readonly string CONFDIR = $@".\config\";
        public static readonly string LOGDIR = $".\\logs\\";
        public static readonly string TMPDIR = $"tmp\\";
#endif

        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        static ZipFile pack = null;

        public static void Init()
        {
            if (File.Exists(PACKNAME))
            {
                Program.MainLog.Add(MessageType.MSG, "Loading Ressource Pack '{0}'", PACKNAME);
                pack = ZipFile.Read(PACKNAME);
                Program.MainLog.Add(1, MessageType.MSG, "Ressource Pack loaded");
            }
            else
                Program.MainLog.Add(MessageType.WARN, "Ressource Pack '{0}' not found", PACKNAME);

            if(!Directory.Exists(CONFDIR))
            {
                Directory.CreateDirectory(CONFDIR);
            }
        }

        public static Bitmap LoadIcon(string name, int size)
        {
            return LoadIcon(name, size, size);
        }

        public static Bitmap LoadIcon(string name, int width, int height)
        {
            Stream stream;
            if(name == "null")
            {
                Bitmap bmp = new Bitmap(width, height);
                return bmp;
            }
            else if ((stream = GetStream("img/" + name)) != null)
            {
                if (name.EndsWith(".svg"))
                {
                    if(!Configuration.RenderSVG)
                    {
                        return GetErrorBmp(width, height);
                    }
                    StreamReader sr = new StreamReader(stream);
                    string svgText = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                    stream.Close();
                    stream.Dispose();
                    var doc = SvgDocument.FromSvg<SvgDocument>(svgText);
                    try
                    {
                        doc.ShapeRendering = SvgShapeRendering.Auto;
                        Bitmap bmp = doc.Draw(width, height);
                        return bmp;
                    }
                    catch
                    {
                        
                    }
                }
                else
                {
                    Bitmap bmp = new Bitmap(stream);
                    Bitmap target = new Bitmap(bmp, width, height);
                    bmp.Dispose();
                    stream.Close();
                    stream.Dispose();
                    return target;
                }
            }
            return GetErrorBmp(width, height);
        }

        public static string GetText(string path)
        {
            Stream stream = GetStream(path);
            if (stream == null) return null;
            string text;
            using (StreamReader reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            stream.Close();
            return text;
        }

        public static Bitmap GetErrorBmp(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);
                g.FillRectangle(Brushes.Fuchsia, new RectangleF(0, 0, width/2, height/2));
                g.FillRectangle(Brushes.Fuchsia, 
                    new RectangleF(width / 2, height / 2, width / 2, height / 2));
            }
            return bmp;
        }

        public static Stream GetStream(string name, string dir)
        {
            string lText = "Loading name={0}, dir={1} ";
            Console.Write("Loading name={0}, dir={1} ", name, dir);
            if(File.Exists("res/" + dir + "/" + name))
            {
                Console.WriteLine("from Filesystem");
                lText += "from Filesystem";
                Program.MainLog.Add(MessageType.MSG, lText, name, dir);
                return File.OpenRead("res/" + dir + "/" + name);
            }
            if (pack != null)
            {
                Console.WriteLine("from Package");
                lText += "from Package";
                Program.MainLog.Add(MessageType.MSG, lText, name, dir);
                var entries = pack.SelectEntries($"name={name}", $"res/{dir}");
                if (entries.Count > 0)
                {
                    return entries.First().OpenReader();
                }
            }
            Program.MainLog.Add(MessageType.ERROR, "File not found name={0}, dir={1}", name, dir);
            return null;
        }

        public static Stream GetStream(string path)
        {
            int sep1 = path.LastIndexOf('/');
            int sep2 = path.LastIndexOf('\\');
            int sep = Math.Max(sep1, sep2);
            if(sep > -1)
            {
                string dir = path.Substring(0, sep);
                string name = path.Substring(sep + 1);
                return GetStream(name, dir);
            }
            else
            {
                return GetStream(path.TrimStart('/', '\\'), "");
            }
        }

        public static List<string> ListFiles(string dir, string pattern)
        {
            string path = "res/" + dir;
            string log = "";
            List<string> files = new List<string>();
            if (pack != null)
            {
                foreach (ZipEntry entry in pack.SelectEntries($"name={pattern}", $"res/{dir}"))
                {
                    string name = entry.FileName.Substring("res/".Length);
                    files.Add(name);
                    log += name + " in Package\n";
                }
            }
            if (Directory.Exists(path))
            {
                string[] filesDir = Directory.GetFiles(path, pattern);
                foreach(string file in filesDir)
                {
                    string name = file.Substring("res/".Length).Replace('\\', '/');
                    if (!files.Contains(name))
                    {
                        files.Add(name);
                        log += name + " in Filesystem\n";
                    }
                }
            }
            Program.MainLog.AddLong(0, MessageType.MSG, "List Files dir=" + path + ", pattern=" + pattern, log);
            return files;
        }

        public static string GetLogfileName()
        {
            if (!Directory.Exists(LOGDIR))
                Directory.CreateDirectory(LOGDIR);
            return LOGDIR + "KritzelLog" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".htm";
        }

        public static void DeleteOldLogs(int numLogsToKeep)
        {
            DirectoryInfo logdir = new DirectoryInfo(LOGDIR);
            if (!logdir.Exists) return;
            FileInfo[] files = logdir.GetFiles("*.htm");
            if (files.Length < numLogsToKeep) return;
            Array.Sort(files, delegate (FileInfo a, FileInfo b)
            {
                return -a.CreationTime.CompareTo(b.CreationTime);
            });
            for(int i = numLogsToKeep; i < files.Length; i++)
            {
                files[i].Delete();
            }
        }
    }
}
