using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kritzel.Main
{
    public class DebugInterface
    {
        static TcpListener listener;
        static TcpClient client;
        static Thread debugThread;
        static bool running = false;
        
        public static void Start()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Loopback, 1337);
            listener = new TcpListener(ipep);
            listener.Start();
            running = true;
            debugThread = new Thread(thread);
            debugThread.Start();
        }

        public static void Stop()
        {
            running = false;
            listener.Stop();
            client.Close();
            debugThread.Join();
        }

        static void thread()
        {
            while (running)
            {
                try
                {
                    client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    stream.Write("Kritzel Debug Interface\n\r>>");
                    byte[] inpBuffer = new byte[1024];
                    int l = stream.Read(inpBuffer, 0, inpBuffer.Length);
                    string input = Encoding.ASCII.GetString(inpBuffer, 0, l).Trim().ToLower();
                    if(input == "timing")
                    {
                        stream.Write("5s\n\r");
                    }
                    stream.Close();
                    client.Close();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
