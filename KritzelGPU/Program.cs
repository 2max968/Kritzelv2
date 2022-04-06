using Kritzel.PointerInputLibrary;
using LineLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool AttachConsole(int processId);
        [DllImport("Shcore.dll")]
        public static extern IntPtr SetProcessDpiAwareness(PROCESS_DPI_AWARENESS dpiAwareness);
        public enum PROCESS_DPI_AWARENESS
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_AWARE = 2
        }

        public static Icon WindowIcon;

        public static bool RestoreData = false;

        public static MessageLog MainLog { get; private set; }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            try
            {
#endif
            SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE);

            Environment.CurrentDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            MainLog = new MessageLog();
            ResManager.Init();
            MainLog.SetOutputFile(new FileInfo(ResManager.GetLogfileName()));
            MainLog.AddLong(0, MessageType.MSG, "Program started",
                "current directory: " + Environment.CurrentDirectory
                 + "\n\nargs:\n" + string.Join("\n", Environment.GetCommandLineArgs())
                 + "\n\nfiles:\n" + string.Join("\n", Directory.GetFiles(".")));
            MainLog.AddLong(0, MessageType.MSG, "Build info", ResManager.GetText("buildinfo.txt"));

            Configuration.StoreAsDefault();
            Configuration.LoadConfig();
            Style.SetStyle();
            ResManager.DeleteOldLogs(Configuration.NumberOfLogs);
            TmpManager.Init();
            Language.Init();
            MainLog.Add(MessageType.MSG, "Set Pointer Gamma");
            PointerManager.Gamma = Configuration.PreassureGamma;
            MainLog.Add(MessageType.MSG, "Load Window Icon");
            WindowIcon = Properties.Resources.KritzelIcon;//Icon.FromHandle(Properties.Resources.KritzelIcon.GetHicon());
            MainLog.Add(MessageType.MSG, "Look for temporary Data");
            //RestoreData = TmpManager.OvertakeFirstUnused();
            MainLog.Add(MessageType.MSG, "Enable Window");
            if(Configuration.EnableVisualStyle) Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
            HTTPCast.StopCasting();
#if !DEBUG
            }
            catch(Exception e)
            {
                MainLog.Add(MessageType.ERROR, "Program crashed");
                MainLog.Add(e);
            }
#endif
        }
    }
}
