using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Kritzel.Main
{
    public class TmpManager
    {
        [DllImport("psapi.dll")]
        public static extern int EnumProcesses(int[] list, int capacity, ref int size);

        public static void Init()
        {
            if (!Directory.Exists(ResManager.TMPDIR))
                Directory.CreateDirectory(ResManager.TMPDIR);

            // Delete empty unused directories
            RemoveUnusedDirs();
        }

        public static DirectoryInfo GetTmpDir(bool checkAndCreate = true)
        {
            var tmp = new DirectoryInfo(ResManager.TMPDIR + Process.GetCurrentProcess().Id);
            if (checkAndCreate && !tmp.Exists)
                tmp.Create();
            return tmp;
        }

        public static void RemoveUnusedDirs()
        {
            int[] list = new int[1024];
            int size = 0;
            EnumProcesses(list, 1024 * sizeof(int), ref size);
            int[] trunclist = new int[size];
            Array.Copy(list, trunclist, trunclist.Length);

            foreach (string dir in Directory.GetDirectories(ResManager.TMPDIR))
            {
                string docName = Path.Combine(dir, "document.kritzel");
                if (File.Exists(docName)) continue;
                string name = new FileInfo(dir).Name;
                int id;
                int.TryParse(name, out id);
                if (!trunclist.Contains(id))
                    Directory.Delete(dir, true);
            }
        }

        public static List<string> ListUnusedDirs()
        {
            int[] list = new int[1024];
            int size = 0;
            EnumProcesses(list, 1024 * sizeof(int), ref size);
            int[] trunclist = new int[size];
            Array.Copy(list, trunclist, trunclist.Length);

            List<string> dirs = new List<string>();
            foreach (string dir in Directory.GetDirectories(ResManager.TMPDIR))
            {
                string name = new FileInfo(dir).Name;
                int id;
                int.TryParse(name, out id);
                if (!trunclist.Contains(id))
                    dirs.Add(dir);
            }
            return dirs;
        }

        public static bool OvertakeFirstUnused()
        {
            var dirs = ListUnusedDirs();
            if (dirs.Count == 0) return false;
            var newName = GetTmpDir().FullName;
            if (Directory.Exists(newName))
                Directory.Delete(newName);
            Directory.Move(dirs[0], newName);
            return true;
        }

        public static string NewFilename(string directory, string prefix, string suffix)
        {
            int num = 0;
            while(true)
            {
                string name = Path.Combine(directory, prefix + num + suffix);
                if(!File.Exists(name))
                {
                    return name;
                }
            }
        }

        public class RecoverableFileInfo
        {
            public int Pid = -1;
            public string Name = null;
            public string Date = null;
            public string Time = null;
            public DirectoryInfo Directory = null;
        }

        public static List<RecoverableFileInfo> GetRecoverableDocuments()
        {
            var list = new List<RecoverableFileInfo>();
            foreach(string dir in ListUnusedDirs())
            {
                string docName = Path.Combine(dir, "document.kritzel");
                if(File.Exists(docName))
                {
                    var info = new RecoverableFileInfo();
                    info.Directory = new DirectoryInfo(dir);
                    using (XmlReader xml = XmlReader.Create(docName))
                    {
                        while(xml.Read())
                        {
                            if (xml.NodeType == XmlNodeType.Element)
                            {
                                try
                                {
                                    string name = xml.Name;
                                    if (name == "Filename") info.Name = xml.ReadElementContentAsString();
                                    if (name == "Date") info.Date = xml.ReadElementContentAsString();
                                    if (name == "Time") info.Time = xml.ReadElementContentAsString();
                                }
                                catch { }
                            }
                        }
                    }
                    list.Add(info);
                }
            }
            return list;
        }
    }
}
