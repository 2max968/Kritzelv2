using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main
{
    public class MessageLog : IEnumerable<MessageEntry>
    {
        public List<MessageEntry> Entries = new List<MessageEntry>();
        public MessageEntry this[int ind]
        {
            get
            {
                return Entries[ind];
            }
            set
            {
                Entries[ind] = value;
            }
        }
        Stream logStream = null;
        MessageLog parent = null;
        static string templHead = null, templEntry = null, templEntryAdd = null;
        uint htmlId = 0;

        public MessageLog()
        {

        }

        public void SetOutputFile(FileInfo logFile)
        {
            if (templHead == null)
            {
                templHead = ResManager.GetText("templates/LogHeader.htm");
                templEntry = ResManager.GetText("templates/LogEntry.htm");
                templEntryAdd = ResManager.GetText("templates/LogEntryAdditional.htm");
            }

            logStream = logFile.OpenWrite();
            string date = DateTime.Now.ToString("dd.MM.yyyy - HH:mm:ss");
            logStream.Write(templHead.Replace("%DATE%", date));

            for(int i = 0; i < Entries.Count;i++)
            {
                writeEntryToFile(Entries[i]);
            }
        }

        public MessageLog(MessageLog parent)
        {
            this.parent = parent;
        }

        ~MessageLog()
        {
            Close();
        }

        public void Close()
        {
            logStream?.Close();
        }

        public void Add(int level, MessageType type, string formatter, params object[] args)
        {
            var entry = new MessageEntry(type, formatter, args);
            writeEntryToFile(entry, level);
            Entries.Add(entry);
            if(parent != null)
            {
                parent.Add(level + 1, type, formatter, args);
            }
        }

        public void AddLong(int level, MessageType type, string text, string additionalData)
        {
            var entry = new MessageEntry(type, text);
            writeEntryToFile(entry, level, additionalData);
            Entries.Add(entry);
            if (parent != null)
            {
                parent.Add(level + 1, type, text);
            }
        }

        public void Add(MessageType type, string formatter, params object[] args)
        {
            Add(0, type, formatter, args);
        }

        public void Add(Exception ex)
        {
            AddLong(0, MessageType.ERROR, 
                ex.GetType().FullName + ": " + ex.Message, 
                ex.StackTrace);
        }

        void writeEntryToFile(MessageEntry entry, int level = 0, string additional = null)
        {

            if (logStream != null && templEntryAdd != null)
            {
                if (additional != null)
                {
                    string oText = templEntryAdd.Replace("%TYPE%", entry.Type.ToString().ToLower())
                        .Replace("%TEXT%", getHtmlPrefix(entry.Type, level) + entry.Message)
                        .Replace("%ADDITIONAL%", additional)
                        .Replace("%ID%", "container" + htmlId++);
                    logStream.Write(oText);
                    logStream.Flush();
                }
                else
                {
                    string oText = templEntry.Replace("%TYPE%", entry.Type.ToString().ToLower())
                        .Replace("%TEXT%", getHtmlPrefix(entry.Type, level) + entry.Message);
                    logStream.Write(oText);
                    logStream.Flush();
                }
            }
        }

        public IEnumerator<MessageEntry> GetEnumerator()
        {
            return Entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Entries.GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(MessageEntry message in this)
            {
                sb.AppendLine(message.ToString());
            }
            return sb.ToString();
        }

        string getHtmlPrefix(MessageType type, int level)
        {
            string text = "";
            for (int i = 0; i < level; i++)
            {
                text += "&nbsp;&nbsp;";
            }
            switch (type)
            {
                case MessageType.MSG: text += "[MSG]&nbsp;&nbsp;"; break;
                case MessageType.WARN: text += "[WARN]&nbsp;"; break;
                case MessageType.ERROR: text += "[ERR]&nbsp;&nbsp;"; break;
            }
            return text;
        }
    }

    public class MessageEntry
    {
        public MessageType Type;
        public string Message;

        public MessageEntry(MessageType type, string formatter, params object[] args)
        {
            this.Type = type;
            this.Message = string.Format(formatter, args);
        }

        public override string ToString()
        {
            return string.Format("[{0}]\t{1}", this.Type.ToString(), this.Message);
        }
    }

    public enum MessageType
    {
        MSG,
        WARN,
        ERROR
    }
}
