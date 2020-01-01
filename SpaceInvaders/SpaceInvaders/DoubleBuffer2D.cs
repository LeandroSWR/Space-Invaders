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

        private void Clear()
        {
            Array.Clear(next, 0, XDim * YDim - 1);
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
            for (int i = 0; i < YDim; i++)
            {
                for (int a = 0; a < XDim; a++)
                {
                    Console.ForegroundColor = current[a, i].pixelColor;
                    Console.Write(current[a, i].pixelChar);
                }
                Console.WriteLine();
            }
        }
    }
}
