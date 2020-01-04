using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Barrier
    {
        private const int BARRIER_HEIGHT = 4;
        private const int BARRIER_WIDTH = 8;

        private string[] sprite;
        private int[][] barrierGrid;

        /// <summary>
        /// The coordinate for the ship
        /// </summary>
        private Vector2 coordinates;
        public Vector2 Coordinates => coordinates;

        public Barrier(int x, int y)
        {
            sprite = new string[4]
            {
                "  ▄▄▄▄  ",
                "▄██████▄",
                "██▀▀▀▀██",
                "▀      ▀"
            };

            coordinates = new Vector2(x, y);

            barrierGrid = new int[BARRIER_WIDTH][];

            for (int i = 0; i < BARRIER_HEIGHT; i++)
            {
                barrierGrid[i] = new int[BARRIER_WIDTH];

                for (int j = 0; j < BARRIER_WIDTH; j++)
                {
                    barrierGrid[i][j] = sprite[i][j] == ' ' ? 0 : 1;
                }
            }
        }

        /// <summary>
        /// Set's the color for the barriers
        /// </summary>
        public void SetColor()
        {
            for (int i = 0; i < BARRIER_HEIGHT; i++)
            {
                BufferEditor.SetColor(ConsoleColor.Green);
                BufferEditor.Write(0, coordinates.Y + i - 1, " ");
                BufferEditor.Write(99, coordinates.Y + i - 1, " ");
            }
        }

        /// <summary>
        /// Writes the barriers to the buffer
        /// </summary>
        public void Write()
        {
            // Set the color to green
            BufferEditor.SetColor(ConsoleColor.Green);

            for (int i = 0; i < BARRIER_HEIGHT; i++)
            {
                BufferEditor.Delete(coordinates.X, coordinates.Y + i - 1, sprite[i]);
            }
        }

        /// <summary>
        /// Returns true if the bullet is on the same position as an undestroyed piece of barrier
        /// </summary>
        /// <param name="bulletCoordinate">The bullet coordinate</param>
        /// <returns>True if the bullet is on the same position as an undestroyed piece of barrier</returns>
        public bool IsNotDestroyed(Vector2 bulletCoordinate) =>
            bulletCoordinate.X >= coordinates.X &&
            bulletCoordinate.X < coordinates.X + BARRIER_WIDTH &&
            (barrierGrid[bulletCoordinate.Y - coordinates.Y + 1][bulletCoordinate.X - coordinates.X] == 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bulletCoordinate">The bullet coordinate</param>
        public void SetDestroyed(Vector2 bulletCoordinate)
        {
            barrierGrid[bulletCoordinate.Y - coordinates.Y + 1][bulletCoordinate.X - coordinates.X] = 0;
        }
    }
}
