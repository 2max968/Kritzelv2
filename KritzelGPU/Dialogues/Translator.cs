using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public class Translator
    {
        public static void Translate(Control control)
        {
            Translate(control, Language.CurrentLanguage);
        }

        public static void Translate(Control control, Language language)
        {
            control.Text = language.Get(control.Text);
            foreach (Control c in control.Controls)
            {
                Translate(c, language);
            }
            if (control is TabControl)
            {
                var tc = (TabControl)control;
                foreach (TabPage page in tc.TabPages)
                {
                    Translate(page, language);
                }
            }
        }
    }
}
