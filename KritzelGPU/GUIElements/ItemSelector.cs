using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.GUIElements
{
    [System.ComponentModel.DesignerCategory("")]
    public class ItemSelector : Control
    {
        int selectedIndex = 0;
        string[] items = new string[0];

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; _update(); }
        }
        public string[] Items
        {
            get { return items; }
            set { items = value; _update(); }
        }

        SolidBrush brushBgr;
        SolidBrush brushFg;
        SolidBrush brushSel;

        public ItemSelector()
        {
            brushBgr = new SolidBrush(Style.Default.MenuBackground);
            brushFg = new SolidBrush(Color.Black);
            brushSel = new SolidBrush(Style.Default.Selection);

            this.Disposed += ItemSelector_Disposed;
        }

        private void ItemSelector_Disposed(object sender, EventArgs e)
        {
            brushBgr.Dispose();
            brushFg.Dispose();
            brushSel.Dispose();
        }

        protected override void OnClick(EventArgs e)
        {
            int itmHeight = Height / Items.Length;
            Point mousePos = this.PointToClient(Cursor.Position);
            int clickedIndex = mousePos.Y * Items.Length / Height;
            SelectedIndex = clickedIndex;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (Items.Length == 0)
            {
                e.Graphics.Clear(Color.White);
                e.Graphics.DrawString("...", this.Font, Brushes.Black, new PointF(10, 10));
            }
            else
            {
                int itmHeight = Height / Items.Length;
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Near;
                for (int i = 0; i < Items.Length; i++)
                {
                    Rectangle itmRect = new Rectangle(0, i * itmHeight, Width, itmHeight);
                    e.Graphics.FillRectangle(brushBgr, itmRect);
                    e.Graphics.DrawString(Items[i], this.Font, brushFg, 
                        new Rectangle(itmRect.X + Util.GetGUISize(), itmRect.Y, itmRect.Width, itmRect.Height), 
                        format);
                    Rectangle selectorRect = new Rectangle(itmRect.X, itmRect.Y, Util.GetGUISize(), Util.GetGUISize());
                    selectorRect.Extend(Util.GetGUISize() / 4);
                    e.Graphics.DrawEllipse(Pens.Black, selectorRect);
                    selectorRect.Extend(Util.GetGUISize() / 8);
                    if (SelectedIndex == i) e.Graphics.FillEllipse(brushSel, selectorRect);
                }
            }
        }

        void _update()
        {
            using (Graphics g = CreateGraphics())
            {
                PaintEventArgs args = new PaintEventArgs(g, new Rectangle(0, 0, Width, Height));
                OnPaint(args);
            }
        }
    }
}
