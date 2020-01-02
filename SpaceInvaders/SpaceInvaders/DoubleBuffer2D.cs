using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class DoubleBuffer2D
    {
        private Pixel[,] current;
        private Pixel[,] next;

        public int XDim => next.GetLength(0);
        public int YDim => next.GetLength(1);

        public Pixel this[int x, int y] {
            get => current[x, y];
            set => next[x, y] = value;
        }

        public void Clear()
        {
            Array.Clear(next, 0, XDim * YDim - 1);
            Array.Clear(current, 0, XDim * YDim - 1);
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

            for (int y = 0; y < YDim; y++)
            {
                for (int x = 0; x < XDim; x++)
                {
                    if (current[x, y].pixelChar != default(char))
                    {
                        if (Console.ForegroundColor != current[x, y].pixelColor)
                        {
                            Console.ForegroundColor = current[x, y].pixelColor;
                        }
                        Console.SetCursorPosition(x, y);
                        Console.Write(current[x, y].pixelChar);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
