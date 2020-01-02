using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SpaceInvaders
{
    class DoubleBuffer2D
    {
        private Pixel[,] current;
        private Pixel[,] next;

        public int XDim => next.GetLength(0);
        public int YDim => next.GetLength(1);

        StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);

        public Pixel this[int x, int y] {
            get => current[x, y];
            set => next[x, y] = value;
        }

        public void Clear()
        {
            Array.Clear(next, 0, XDim * YDim - 1);
            Array.Clear(current, 0, XDim * YDim - 1);

            for (int i = 0; i < YDim; i++)
            {
                for (int j = 0; j < XDim; j++)
                {
                    next[j, i].pixelChar = ' ';
                    current[j, i].pixelChar = ' ';
                }
            }
        }

        public DoubleBuffer2D(int x, int y)
        {
            current = new Pixel[x, y];
            next = new Pixel[x, y];

            Clear();
        }
        
        public void Swap()
        {
            Pixel[,] aux = current;
            current = next;
            next = aux;
        }

        public void Display()
        {
            Console.SetCursorPosition(0, 0);

            Console.BackgroundColor = ConsoleColor.Black;

            ConsoleColor currentForeground = ConsoleColor.Black;

            string s = "";

            for (int y = 0; y < YDim; y++)
            {
                for (int x = 0; x < XDim; x++)
                {
                    if (!current[x, y].pixelColor.Equals(ConsoleColor.Black) &&
                        !current[x, y].pixelColor.Equals(currentForeground))
                    {
                        currentForeground = current[x, y].pixelColor;
                        Console.ForegroundColor = current[x, y].pixelColor;
                    }

                    s += current[x, y].pixelChar;
                }

                if (s.Count(c => c == ' ') < 100)
                {
                    Console.WriteLine(s);
                } else
                {
                    Console.SetCursorPosition(0, y + 1);
                }
                
                s = "";
            }
        }
    }
}
