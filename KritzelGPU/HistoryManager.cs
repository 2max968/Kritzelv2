using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using frm = System.Windows.Forms;

namespace Kritzel.Main
{
    public class HistoryManager
    {
        static readonly Encoding E = Encoding.UTF8;

        public struct HistoryEntry
        {
            public KPage Page;
            public byte[] Data;
        }

        public class History
        {
            public int Current = -1;
            public List<HistoryEntry> Versions = new List<HistoryEntry>();
        }

        static Dictionary<KPage, History> history = new Dictionary<KPage, History>();
        static Dictionary<KPage, int> versions = new Dictionary<KPage, int>();
        static frm.Control btnUndo = null;
        static frm.Control btnRedo = null;

        public static void StoreState(KPage page)
        {
            if (!history.ContainsKey(page))
                history.Add(page, new History());

            var vers = history[page].Versions;
            int start = history[page].Current + 1;
            if (start < vers.Count)
                vers.RemoveRange(start, vers.Count - start);

            HistoryEntry entry = new HistoryEntry()
            {
                Page = page,
                Data = E.GetBytes(page.SaveToString())
            };
            history[page].Versions.Add(entry);
            history[page].Current++;
            SetButtonVisibility(page);
        }

        public static bool Undo(KPage page)
        {
            if (!history.ContainsKey(page)) return false;
            if (history[page].Current <= 0) return false;

            page.LoadFromString(E.GetString(history[page].Versions[--history[page].Current].Data), Program.MainLog);
            SetButtonVisibility(page);
            return true;
        }

        public static bool Redo(KPage page)
        {
            if (!history.ContainsKey(page)) return false;
            if (history[page].Current >= history[page].Versions.Count - 1) return false;

            page.LoadFromString(E.GetString(history[page].Versions[++history[page].Current].Data), Program.MainLog);
            SetButtonVisibility(page);
            return true;
        }

        public static void RegisterHistoryButtons(frm.Control btnUndo, frm.Control btnRedo)
        {
            HistoryManager.btnUndo = btnUndo;
            HistoryManager.btnRedo = btnRedo;
        }

        public static void SetButtonVisibility(KPage page)
        {
            if (!history.ContainsKey(page)) return;
            if(btnUndo != null) btnUndo.Enabled = history[page].Current > 0;
            if(btnRedo != null) btnRedo.Enabled = history[page].Current < history[page].Versions.Count - 1;
        }
    }
}
