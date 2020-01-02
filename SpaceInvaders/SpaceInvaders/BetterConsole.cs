using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class BetterConsole
    {
        static readonly BufferedStream str;

            static BetterConsole()
            {
                Console.OutputEncoding = Encoding.Unicode;  // crucial

                // avoid special "ShadowBuffer" for hard-coded size 0x14000 in 'BufferedStream' 
                str = new BufferedStream(Console.OpenStandardOutput(), 0x15000);
            }

            public static void WriteLine(String s) => Write(s + "\r\n");

            public static void Write(String s)
            {
                // avoid endless 'GetByteCount' dithering in 'Encoding.Unicode.GetBytes(s)'
                var rgb = new byte[s.Length << 1];
                Encoding.Unicode.GetBytes(s, 0, s.Length, rgb, 0);

                lock (str)   // (optional, can omit if appropriate)
                    str.Write(rgb, 0, rgb.Length);
            }

            public static void Flush() { lock (str) str.Flush(); }
    }
}
