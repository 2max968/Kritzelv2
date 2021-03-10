using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WndProcLog
{
    public class WndMsgs
    {
        public Dictionary<string, int> Messages = new Dictionary<string, int>();

        public WndMsgs()
        {
            string csv = Properties.Resources.WndMsg;
            string[] lines = csv.Split('\n');
            foreach (string line in lines)
            {
                if (line == "") continue;
                string[] words = line.Split(';');
                int id = int.Parse(words[0]);
                string name = words[1];
                if(!Messages.ContainsKey(name))
                    Messages.Add(name, id);
            }
        }
    }
}
