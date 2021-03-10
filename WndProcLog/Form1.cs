using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndProcLog
{
    public partial class Form1 : Form
    {
        WndMsgs msgs;

        public Form1()
        {
            InitializeComponent();

            msgs = new WndMsgs();
        }

        protected override void WndProc(ref Message m)
        {
            string name = "0x" + m.Msg.ToString("x");
            foreach (KeyValuePair<string, int> kvp in msgs.Messages)
            {
                if(kvp.Value == m.Msg)
                {
                    name = kvp.Key;
                    break;
                }
            }
            listBox1.Items.Add(name);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            base.WndProc(ref m);
        }
    }
}
