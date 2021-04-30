using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class CastDialog : Form
    {
        InkControl ink;

        public CastDialog(InkControl ink)
        {
            InitializeComponent();
            this.ink = ink;
            this.Text = Language.GetText("File.cast");
            Dialogues.Translator.Translate(this);

            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            lblCurrentIP.Text = Language.GetText("Cast.http.cip") + " " + localIP;
            lblCurrentIP.Tag = localIP;
            tbWebId.Text = Util.GetUsername() + "-cast";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ushort port;
            if(!ushort.TryParse(tbHttpPort.Text, out port))
            {
                MsgBox.ShowOk("Port '" + tbHttpPort.Text + "' not valid");
                return;
            }
            HTTPCast.StartCasting(port, ink, "1234");
            this.Close();
        }

        private void lblCurrentIP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string uri = "http://" + (lblCurrentIP.Tag as string);
            Process.Start(uri);
        }

        private async void btnWebStart_Click(object sender, EventArgs e)
        {
            tbWebHostname.Enabled = false;
            tbWebId.Enabled = false;
            btnWebStart.Enabled = false;
            tbWebHostname.Text = WebCast.GetHost(tbWebHostname.Text);
            int connectionActive = await WebCast.StartCasting(tbWebHostname.Text, tbWebId.Text, ink);
            if(connectionActive == 0)
            {
                this.Close();
            }
            else if(connectionActive == 1)
            {
                MsgBox.ShowOk("Cast.web.rejected");
            }
            else
            {
                MsgBox.ShowOk("Cast.web.connectionError");
            }
            tbWebHostname.Enabled = true;
            tbWebId.Enabled = true;
            btnWebStart.Enabled = true;
        }
    }
}
