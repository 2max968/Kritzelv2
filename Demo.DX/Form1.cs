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

namespace Demo.DX
{
    public partial class Form1 : Form
    {
        [DllImport("Kritzel.D2D.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int k2dMain(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();

            this.Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            k2dMain(this.Handle);
        }
    }
}
