using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues.Settings
{
    public partial class SettingsPagePalettes : UserControl, ISettingsPage
    {
        public SettingsPagePalettes()
        {
            InitializeComponent();

            btnColorUp.Image = ResManager.LoadIcon("simpleArrowUp.svg", 16);
            btnColorDown.Image = ResManager.LoadIcon("simpleArrowDown.svg", 16);
            btnColorDelete.Image = ResManager.LoadIcon("actions/delete.svg", 16);
        }

        public bool CheckValues()
        {
            return true;
        }

        public void LoadSettings()
        {
            lvColors.Items.Clear();
            string[] pens = Configuration.PenColors.Split(',');
            foreach (string pen in pens)
            {
                ListViewItem itm = new ListViewItem(pen);
                itm.Tag = ColorTranslator.FromHtml(pen);
                lvColors.Items.Add(itm);
            }
        }

        public void PostSave()
        {
            
        }

        public void SaveSettings()
        {
            List<string> colors = new List<string>();
            foreach(ListViewItem itm in lvColors.Items)
            {
                if(itm.Tag is Color)
                {
                    colors.Add(ColorTranslator.ToHtml((Color)itm.Tag));
                }
            }
            Configuration.PenColors = string.Join(",", colors.ToArray());
        }

        private void lvColors_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if(e.Item.Tag is Color)
            {
                Rectangle bounds = e.Bounds;
                bounds.X += 16;
                bounds.Width -= 32;
                if (e.Item.Focused)
                    bounds = e.Bounds;
                using (Brush b = new SolidBrush((Color)e.Item.Tag))
                    e.Graphics.FillRectangle(b, bounds);
                if (e.Item.Focused)
                    e.Graphics.DrawRectangle(SystemPens.Highlight, e.Bounds);
            }
        }

        private void btnColorUp_Click(object sender, EventArgs e)
        {
            if (lvColors.FocusedItem != null && lvColors.FocusedItem.Index > 0)
            {
                int index = lvColors.FocusedItem.Index - 1;
                ListViewItem itm = lvColors.FocusedItem;
                lvColors.Items.Remove(itm);
                lvColors.Items.Insert(index, itm);
                itm.Focused = true;
            }
        }

        private void btnColorDown_Click(object sender, EventArgs e)
        {
            if (lvColors.FocusedItem != null && lvColors.FocusedItem.Index < lvColors.Items.Count-1)
            {
                int index = lvColors.FocusedItem.Index + 1;
                ListViewItem itm = lvColors.FocusedItem;
                lvColors.Items.Remove(itm);
                lvColors.Items.Insert(index, itm);
                itm.Focused = true;
            }
        }

        private void btnColorDelete_Click(object sender, EventArgs e)
        {
            if (lvColors.FocusedItem != null)
            {
                lvColors.Items.Remove(lvColors.FocusedItem);
            }
        }

        private void btnColorAdd_Click(object sender, EventArgs e)
        {
            Form frame = new Form();
            var cd = new GUIElements.ColorDialog();
            cd.AddCloseHandler(delegate ()
            {
                frame.Close();
            });
            frame.ClientSize = cd.Size;
            frame.Controls.Add(cd);
            frame.FormBorderStyle = FormBorderStyle.FixedSingle;
            frame.MinimizeBox = frame.MaximizeBox = frame.ShowInTaskbar = frame.ShowIcon = false;
            frame.StartPosition = FormStartPosition.CenterParent;
            cd.Bounds = new Rectangle(0, 0, cd.Width, cd.Height);
            frame.ShowDialog();
            if(cd.Result == DialogResult.OK)
            {
                ListViewItem itm = new ListViewItem(ColorTranslator.ToHtml(cd.SelectedColor));
                itm.Tag = cd.SelectedColor;
                lvColors.Items.Add(itm);
            }
        }
    }
}
