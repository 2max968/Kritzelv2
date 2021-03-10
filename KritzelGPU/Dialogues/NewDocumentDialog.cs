using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kritzel.Main.GUIElements;

namespace Kritzel.Main.Dialogues
{
    public partial class NewDocumentDialog : UserControl, IClosable
    {
        public event CloseDelegate Close;
        Dictionary<string, PageFormat> formats;
        InkControl control;
        KDocument doc;
        MainWindow window;

        public NewDocumentDialog(InkControl control, KDocument doc, MainWindow window)
        {
            InitializeComponent();

            this.control = control;
            this.doc = doc;
            this.window = window;

            this.BackColor = Style.Default.MenuBackground;
            this.ForeColor = Style.Default.MenuForeground;
            this.Width = 5 * Util.GetGUISize();
            this.Dock = DockStyle.Left;

            formats = PageFormat.GetFormats();
            foreach (var f in formats)
            {
                cbPageFormat.Items.Add(f.Key);
            }
            foreach(ColorFilter filter in Enum.GetValues(typeof(ColorFilter)))
            {
                cbFilters.Items.Add(Language.GetText($"Newdoc.filter.{filter}"));
            }
            cbFilters.Text = Language.GetText($"Newdoc.filter.{ColorFilter.Normal}");

            if (formats.ContainsKey(Configuration.DefaultFormat))
                cbPageFormat.Text = Configuration.DefaultFormat;
            else
                cbPageFormat.Text = cbPageFormat.Items[0].ToString();

            Translator.Translate(this);
        }

        public void AddCloseHandler(CloseDelegate handler)
        {
            Close += handler;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            PageFormat pf = formats[cbPageFormat.Text];

            KDocument doc = new KDocument();
            KPage page = new KPage(doc);
            page.Format = pf;
            doc.DefaultFormat = cbPageFormat.Text;
            doc.Pages.Add(page);
            doc.SetCurrentStateAsSaved();
            try
            {
                page.Filter = (ColorFilter)Enum.GetValues(typeof(ColorFilter)).GetValue(cbFilters.SelectedIndex);
            }
            catch (Exception) { }
            window.SetDocument(doc);
            Close?.Invoke();
        }
    }
}
