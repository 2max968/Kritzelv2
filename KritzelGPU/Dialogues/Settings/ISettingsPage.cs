using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues.Settings
{
    public interface ISettingsPage
    {
        void LoadSettings();
        void SaveSettings();
        void PostSave();
        bool CheckValues();
    }
}
