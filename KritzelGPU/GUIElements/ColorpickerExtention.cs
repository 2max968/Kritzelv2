using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.GUIElements
{
    public partial class ColorpickerExtention : UserControl, IClosable
    {
        event CloseDelegate Close;
        List<Color> colors;
        ColorPicker parent;

        public ColorpickerExtention(ColorPicker parent, Color? selectedColor)
        {
            InitializeComponent();

            this.parent = parent;
            colors = parent.GetColors();
            int wNum = parent.GetCapacity();
            int hNum = (colors.Count - 1) / wNum + 1;
            this.Location = new Point(parent.Location.X + Util.GetGUISize(), parent.Location.Y);
            this.Width = wNum * Util.GetGUISize();
            this.Height = hNum * Util.GetGUISize();

            for (int i = 0; i < colors.Count; i++)
            {
                int x = (i % wNum) * Util.GetGUISize();
                int y = (i / wNum) * Util.GetGUISize();
                Button btn = new Button();
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = (colors[i] == selectedColor) 
                    ? Style.Default.Selection : Style.Default.MenuBackground;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackgroundImage = parent.createIcon(colors[i]);
                btn.Bounds = new Rectangle(x, y, Util.GetGUISize(), Util.GetGUISize());
                btn.Click += Btn_Click;
                btn.Tag = colors[i];
                Controls.Add(btn);
            }

            this.BackColor = Style.Default.MenuBackground;
        }

        public void AddCloseHandler(CloseDelegate handler)
        {
            Close += handler;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if(sender is Button)
            {
                if(((Button)sender).Tag is Color)
                {
                    Color c = (Color)((Button)sender).Tag;
                    parent.SetColorExtern(c);
                }
            }
            Close?.Invoke();
        }
    }
}
