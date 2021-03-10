using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main
{
    public class Configuration
    {
        public static string FILENAME
        {
            get
            {
                return $"{ResManager.CONFDIR}config.{Util.GetUsername()}.ini";
            }
        }
        public const char SEPARATOR = '=';

        public static Dictionary<string, string> restartConfs = new Dictionary<string, string>();
        public static Dictionary<FieldInfo, object> defaults = null;

        [CP] public static RenderMode Renderer = RenderMode.Direct2D;
        [CP] public static float PenSizeMin = 3;
        [CP] public static float PenSizeMax = 11;
        [CP] public static int PenSizeNum = 5;
        [CP] public static float PenCurrentSize = 3;
        [CP] public static int ColorPickerHueSteps = 15;
        [CP] public static int ColorPickerSatSteps = 4;
        [CP] public static int ColorPickerValueSteps = 4;
        [CP] public static bool HandleMouseInput = true;
        [CP] public static float ScreenDPIFactor = -1;
        [CP] public static float PreassureGamma = 1;
        [CP] public static string Language = "";
        [CP] public static float RulerSize = 40;
        [CP] public static bool RefreshOnTransform = false;
        [CP][RS] public static float GUIScaleFactor = 1;
        [CP] public static int AutosaveInterval = 10;
        [CP] public static string DefaultFormat = "A4";
        [CP] public static string PenColors = "Black,#D12324,#E66A0A,#FBC131,#82B92A,#009D63,#A267AB,#585C9C,#B9143C";
        [CP] public static float TransformRotationThreshold = 0.1f;
        [CP] public static float TransformScaleThreshold = 0.2f;
        [CP] public static float TransformTranslateThreshold = 32;
        [CP] public static bool RenderSVG = true;
        [CP] public static bool EnableVisualStyle = true;
        [CP] public static bool CalculateSplinesDuringDrawing = true;
        [CP] public static int NumberOfLogs = 5;
        [CP] public static bool ShowLineBoundingBoxes = false;
        [CP] public static bool ShowBattery = true;
        [CP] public static bool ShowTime = true;
        [CP] public static bool ShowDate = true;
        [CP] public static bool LeftHanded = false;
        [CP][RS] public static bool DarkMode = false;

        public static void LoadConfig()
        {
            MessageLog loadingLog = new MessageLog(Program.MainLog);
            Program.MainLog.Add(MessageType.MSG, "Loading Configurations from '{0}'", FILENAME);
            if(!File.Exists(FILENAME))
            {
                Program.MainLog.Add(MessageType.MSG, "File does not exist, creating file...");
                loadingLog.Add(MessageType.MSG, "File '{0}' doesnt exist", FILENAME);
                SaveConfig();
                return;
            }
            
            FileStream stream = File.OpenRead(FILENAME);
            StreamReader reader = new StreamReader(stream);
            Type type = typeof(Configuration);
            string logText = "";
            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                int sep = line.IndexOf(SEPARATOR);
                if(sep > 0 && !line.StartsWith("#"))
                {
                    string key = line.Substring(0, sep);
                    string value = line.Substring(sep + 1);
                    logText += key + "=" + value + "\n";
                
                    FieldInfo field = type.GetField(key);
                    bool parameterExists = false;
                    if(field != null && field.IsStatic)
                    {
                        IEnumerable<CP> cps = field.GetCustomAttributes<CP>();
                        if(cps.Count() > 0)
                        {
                            parameterExists = true;
                            Type ft = field.FieldType;
                            bool canParse;
                            if(ft == typeof(bool))
                            {
                                bool v;
                                canParse = bool.TryParse(value, out v);
                                field.SetValue(null, v);
                            }
                            else if(ft == typeof(int))
                            {
                                int v;
                                canParse = int.TryParse(value, out v);
                                field.SetValue(null, v);
                            }
                            else if(ft == typeof(float))
                            {
                                float v;
                                canParse = Util.TrySToF(value, out v);
                                field.SetValue(null, v);
                            }
                            else if(ft==typeof(string))
                            {
                                field.SetValue(null, value);
                                canParse = true;
                            }
                            else if(ft.IsEnum)
                            {
                                try
                                {
                                    object v = Enum.Parse(ft, value, true);
                                    field.SetValue(null, v);
                                    canParse = true;
                                }
                                catch (Exception)
                                {
                                    canParse = false;
                                }
                            }
                            else
                            {
                                canParse = true;
                                loadingLog.Add(MessageType.ERROR, "Internal Error: cant parse key '{0}' to type '{1}'", key, ft.FullName);
                            }

                            if(!canParse)
                            {
                                loadingLog.Add(MessageType.ERROR, "Cant parse value '{0}' for key '{0}", value, key);
                            }
                        }
                    }
                    if(!parameterExists)
                    {
                        loadingLog.Add(MessageType.WARN, "The Configuration Parameter '{0}' does not exists", key);
                    }
                }
            }
            Program.MainLog.AddLong(1, MessageType.MSG, "Configuration loaded", logText);
            reader.Close();
            stream.Close();
            stream.Dispose();
        }

        public static void SaveConfig()
        {
            FileStream stream = File.Open(FILENAME, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);

            Type type = typeof(Configuration);
            foreach(FieldInfo field in type.GetFields())
            {
                IEnumerable<CP> cps = field.GetCustomAttributes<CP>();
                if(field.IsStatic && cps.Count() > 0)
                {
                    string key = field.Name;
                    string value = field.GetValue(null).ToString();
                    if (field.FieldType.IsEnum)
                        writer.WriteLine("#Options for {0}: {1}", key, string.Join(", " , field.FieldType.GetEnumNames()));
                    if (field.FieldType == typeof(float))
                        value = Util.FToS((float)field.GetValue(null));
                    writer.WriteLine("{0}{1}{2}", key, SEPARATOR, value);
                }
            }

            writer.Close();
            stream.Close();
            stream.Dispose();

            // TODO: File access
            //FileSecurity sec = File.GetAccessControl(FILENAME);
            //sec.SetAccessRule(new FileSystemAccessRule())
            //File.SetAccessControl(FILENAME, sec);
        }

        public static void SetState()
        {
            Type confClass = typeof(Configuration);
            restartConfs = new Dictionary<string, string>();
            foreach (FieldInfo field in confClass.GetFields())
            {
                IEnumerable<RS> rss = field.GetCustomAttributes<RS>();
                if (field.IsStatic && rss.Count() > 0)
                {
                    string name = field.Name;
                    string key = field.GetValue(null).ToString();
                    if (!restartConfs.ContainsKey(name))
                        restartConfs.Add(name, key);
                }
            }
        }

        public static bool CheckRestart(out string conf)
        {
            Type confClass = typeof(Configuration);
            foreach (FieldInfo field in confClass.GetFields())
            {
                IEnumerable<RS> rss = field.GetCustomAttributes<RS>();
                if (field.IsStatic && rss.Count() > 0)
                {
                    string name = field.Name;
                    string key = field.GetValue(null).ToString();
                    if (!restartConfs.ContainsKey(name))
                    {
                        conf = name;
                        return true;
                    }
                    if(restartConfs[name] != key)
                    {
                        conf = name;
                        return true;
                    }
                }
            }
            conf = null;
            return false;
        }

        public static void StoreAsDefault()
        {
            defaults = new Dictionary<FieldInfo, object>();
            Type confClass = typeof(Configuration);
            foreach (FieldInfo field in confClass.GetFields())
            {
                IEnumerable<CP> cps = field.GetCustomAttributes<CP>();
                if (field.IsStatic && cps.Count() > 0)
                {
                    defaults.Add(field, field.GetValue(null));
                }
            }
        }

        public static void RestoreDefaults()
        {
            if (defaults == null) return;
            foreach(var kvp in defaults)
            {
                kvp.Key.SetValue(null, kvp.Value);
            }
        }
    }

    public class CP : Attribute { }
    public class RS : Attribute { }

    public enum RenderMode
    {
        Software,
        OpenGL,
        Direct2D
    }

    public enum ColorFilter
    {
        Normal,
        InvertLuminosity
    }
}
