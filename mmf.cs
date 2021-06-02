using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace WW1MMF
{
    struct Telemetry
    {
        public float Roll, Pitch, Heave, Yaw, Sway, Surge;
    }

    class Program
    {
        static void Main(string[] args)
        {
            using(var mmf = MemoryMappedFile.OpenExisting("ww1"))
            {
                using(var accessor = mmf.CreateViewAccessor())
                {
                    while(true)
                    {
                        accessor.Read(0, out Telemetry t);
                        Console.WriteLine($"Roll {t.Roll} Pitch {t.Pitch} Heave {t.Heave} Yaw {t.Yaw} Sway {t.Sway} Surge {t.Surge}");
                        Thread.Sleep(20);
                    }
                }
            }
        }
    }
}
