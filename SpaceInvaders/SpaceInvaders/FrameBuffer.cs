using System;
using System.Text;
using System.Linq;

namespace SpaceInvaders
{
    class DoubleBuffer2D
    {
        public Pixel[,] current;
        private Pixel[,] next;
        private Pixel[,] aux;

        public int XDim => next.GetLength(0);
        public int YDim => next.GetLength(1);

        char[] charsCurrent;

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
                    charsCurrent[i] = ' ';
                }

                Console.WriteLine(charsCurrent);
            }
        }

        public DoubleBuffer2D(int x, int y)
        {
            current = new Pixel[x, y];
            next = new Pixel[x, y];

            charsCurrent = new char[x];

            Clear();
        }
        
        public void Swap()
        {
            aux = current;
            current = next;
            next = aux;
        }

        public void Display()
        {
            Console.SetCursorPosition(0, 0);

            ConsoleColor currentForeground = ConsoleColor.Black;

            for (int y = 0; y < YDim; y++)
            {
                for (int x = 0; x < XDim; x++)
                {
                    charsCurrent[x] = current[x, y].pixelChar;
                }

                if (!current[0, y].pixelColor.Equals(currentForeground))
                {
                    currentForeground = current[0, y].pixelColor;
                    Console.ForegroundColor = currentForeground;

                }

                Console.WriteLine(charsCurrent);
            }
        }
    }
}
