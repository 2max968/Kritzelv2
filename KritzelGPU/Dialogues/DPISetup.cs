using Kritzel.PointerInputLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class DPISetup : Form
    {
        static Bitmap euro = null;
        PointerManager pm;
        FingerTransform oldFt = null;
        float scale;

        public DPISetup()
        {
            InitializeComponent();

            if (euro == null)
                euro = ResManager.LoadIcon("euro.jpg", 512, 512);

            pm = new PointerManager(this);
            scale = Util.GetRealScreenDPIFactor();
            this.ResizeEnd += DPISetup_ResizeEnd;
            refreshImg();
        }

        private void DPISetup_ResizeEnd(object sender, EventArgs e)
        {
            refreshImg();
        }

        protected override void WndProc(ref Message m)
        {
            pm.HandleWndProc(ref m);
            base.WndProc(ref m);
        }

        private void tmInput_Tick(object sender, EventArgs e)
        {
            List<Touch> touches = pm.Touches.Values.ToList();
            if(touches.Count == 2)
            {
                FingerTransform ft = new FingerTransform(touches[0].X, touches[0].Y,
                    touches[1].X, touches[1].Y);

                if(oldFt != null)
                {
                    float dScale = 1 + (ft.Distance - oldFt.Distance) / oldFt.Distance;
                    Console.WriteLine(dScale);
                    scale *= dScale;
                    refreshImg();
                }
                oldFt = ft;
            }
            else
            {
                oldFt = null;
            }
        }

        void refreshImg()
        {
            float mmWidth = 23.25f;
            float pxWidth = Util.MmToPoint(mmWidth);

            this.Text = "" + scale;
            Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            float size = pxWidth * scale;
            g.DrawImage(euro, (this.ClientSize.Width - size) / 2, (this.ClientSize.Height - size) / 2, size, size);
            g.FillRectangle(Brushes.White, new RectangleF(
                Util.GetGUISize() / 2, this.ClientSize.Height - (int)(Util.GetGUISize() * 1.5),
                this.ClientSize.Width - Util.GetGUISize(), Util.GetGUISize()));
            g.Dispose();
            Image tmp = BackgroundImage;
            BackgroundImage = bmp;
            tmp?.Dispose();
        }
    }
}
