using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class DebugSetup : Form
    {
        Type editType = null;

        public DebugSetup()
        {
            InitializeComponent();

            Type type = typeof(Configuration);
            Configuration.SaveConfig();
            FileStream stream = File.OpenRead(Configuration.FILENAME);
            StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                int sep = line.IndexOf(Configuration.SEPARATOR);
                if (sep > 0 && !line.StartsWith("#"))
                {
                    string key = line.Substring(0, sep);
                    string value = line.Substring(sep + 1);
                    ListViewItem itm = new ListViewItem(new string[] { value, key });
                    try
                    {
                        itm.Tag = type.GetField(key).FieldType;
                    }
                    catch(Exception)
                    {
                        itm.Tag = null;
                    }
                    listView1.Items.Add(itm);
                }
                else
                {
                    ListViewItem itm = new ListViewItem(new string[] { line, "Comment" });
                    itm.ForeColor = Color.Green;
                    itm.Tag = false;
                    listView1.Items.Add(itm);
                }
            }
            reader.Dispose();
            stream.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listView1.FocusedItem != null)
            {
                editType = listView1.FocusedItem.Tag as Type;
                listView1.FocusedItem.BeginEdit();
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == "%default")
            {
                return;
            }

            if (editType != null)
            {
                if(editType.IsEnum)
                {
                    if (!editType.GetEnumNames().Contains(e.Label))
                        e.CancelEdit = true;
                }
                else if(editType == typeof(bool))
                {
                    if (e.Label == "0") setText(e, "False");
                    else if (e.Label == "1") setText(e, "True");
                    else if (e.Label == "false") setText(e, "False");
                    else if (e.Label == "true") setText(e, "True");
                    else if (!(new string[] { "True", "False" }).Contains(e.Label))
                        e.CancelEdit = true;
                }
            }
            editType = null;
        }

        void setText(LabelEditEventArgs e, string text)
        {
            e.CancelEdit = true;
            listView1.Items[e.Item].Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach(ListViewItem itm in listView1.Items)
            {
                if(itm.Tag is bool)
                {
                    sb.AppendLine(itm.Text);
                }
                else
                {
                    string value = itm.Text;
                    if(value != "%default")
                        sb.AppendFormat("{0}{1}{2}\n",
                            itm.SubItems[1].Text, Configuration.SEPARATOR, itm.Text);
                }
            }
            File.WriteAllText(Configuration.FILENAME, sb.ToString());
            Configuration.RestoreDefaults();
            Configuration.LoadConfig();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(listView1.FocusedItem != null)
                    listView1.FocusedItem.Text = "%default";
            }
        }
    }
}
