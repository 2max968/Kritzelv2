using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace ImageImporter
{
    public partial class CamForm : Form
    {
        VideoCapture cap;
        int currentInd = 0;

        public CamForm()
        {
            InitializeComponent();

            cap = new VideoCapture();
            cap.Open(currentInd);

            tmCapture.Start();
        }

        private void tmCapture_Tick(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            if (cap.Read(frame) && !frame.Empty())
            {
                LightInverter.InvertValue(frame, out Mat frame2);
                Bitmap bmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame2);
                frame.Dispose();
                frame2.Dispose();
                pbCam.Image?.Dispose();
                pbCam.Image = bmp;
            }
            else
            {
                currentInd = 0;
                cap.Open(currentInd);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            currentInd++;
            cap.Open(currentInd);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            cap.Read(frame);
            Bitmap bmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
            frame.Dispose();
            Stream stdout = Console.OpenStandardOutput();
            bmp.Save(stdout, ImageFormat.Png);
            bmp.Dispose();
            Environment.Exit(0);
        }
    }
}
