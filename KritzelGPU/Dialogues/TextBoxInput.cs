using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class TextBoxInput : Form
    {
        static List<TextBoxInput> opened = new List<TextBoxInput>();
        Forms.TextBox tb;
        InkControl ink;

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr parent);

        public TextBoxInput(Forms.TextBox tb, InkControl parent)
        {
            InitializeComponent();
            Translator.Translate(this);
            this.tb = tb;
            tbContent.Text = tb.Text;
            this.ink = parent;
            SetParent(Handle, parent.Handle);

            this.Text = Language.GetText("Dialog.textBox");
            this.Icon = Icon.FromHandle(Forms.TextBox.BitmapTB.GetHicon());

            foreach (FontFamily ff in FontFamily.Families)
            {
                cbFontFamilies.Items.Add(ff.Name);
            }
            cbFontFamilies.Text = tb.FontFamily;
            numFontSize.Value = (decimal)tb.FontSize;
            foreach (string name in Enum.GetNames(typeof(Renderer.BaseRenderer.TextAlign)))
            {
                cbAlignment.Items.Add(name);
            }

            CloseAll();
            if (!opened.Contains(this))
                opened.Add(this);
            this.FormClosing += TextBoxInput_FormClosing;
        }

        private void TextBoxInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (opened.Contains(this))
                opened.Remove(this);
            HistoryManager.StoreState(ink.Page);
        }

        private void tbContent_TextChanged(object sender, EventArgs e)
        {
            if (ink == null) return;
            tb.Text = tbContent.Text;
            ink.Refresh();
        }

        private void numFontSize_ValueChanged(object sender, EventArgs e)
        {
            if (ink == null) return;
            tb.FontSize = (float)numFontSize.Value;
            tb.Text = tb.Text;
            ink.Refresh();
        }

        private void cbAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse(cbAlignment.SelectedItem.ToString(), out Renderer.BaseRenderer.TextAlign align))
                tb.Align = align;
            tb.Text = tb.Text;
            ink.Refresh();
        }

        private void cbFontFamilies_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb.FontFamily = cbFontFamilies.SelectedItem.ToString();
            tb.Text = tb.Text;
            ink.Refresh();
        }

        public static void CloseAll()
        {
            while (opened.Count > 0)
                opened[0].Close();
        }

        private void TextBoxInput_Load(object sender, EventArgs e)
        {

        }
    }
}
