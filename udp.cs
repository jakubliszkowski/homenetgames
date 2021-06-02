using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace WW1MMF
{
    struct Telemetry
    {
        public float Roll, Pitch, Heave, Yaw, Sway, Surge;
        public const int Size = 6 * 4;
    }

    class Program
    {
        static void Main(string[] args)
        {
            string IP = "127.0.0.1";
            const int port = 50000;

            UdpClient client = new UdpClient(port);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            IntPtr buffer = Marshal.AllocHGlobal(Telemetry.Size);

            try
            {
                while (true)
                {
                    byte[] data = client.Receive(ref endPoint);
                    Marshal.Copy(data, 0, buffer, Telemetry.Size);
                    Telemetry t = (Telemetry)Marshal.PtrToStructure(buffer, typeof(Telemetry));

                    Console.WriteLine($"Roll {t.Roll} Pitch {t.Pitch} Heave {t.Heave} Yaw {t.Yaw} Sway {t.Sway} Surge {t.Surge}");
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Marshal.FreeHGlobal(buffer);
        }
    }
}
