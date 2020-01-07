using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Manages all the number that will be displayed in game
    /// And displays them when necessary
    /// </summary>
    public static class NumberManager
    {
        // Necessary constants to be used in this script
        private const int NUMBER_ROWS = 3;
        private const int SCORE_X_POS = 8;
        private const int LIFES_X_POS = 54;
        private const int LEVELS_X_POS = 37;

        /// <summary>
        /// Sprite for the number 0
        /// </summary>
        private static string[] zero = new string[] {
            "╔╗",
            "║║",
            "╚╝" };

        /// <summary>
        /// Sprite for the number 1
        /// </summary>
        private static string[] one = new string[] {
            " ╗",
            " ║",
            " ╩"};

        /// <summary>
        /// Sprite for the number 2
        /// </summary>
        private static string[] two = new string[] {
            "╔╗",
            "╔╝",
            "╚╝"};

        /// <summary>
        /// Sprite for the number 3
        /// </summary>
        private static string[] three = new string[] {
            "╔╗",
            " ╣",
            "╚╝"};

        /// <summary>
        /// Sprite for the number 4
        /// </summary>
        private static string[] four = new string[] {
            "╗╗",
            "╚╣",
            " ╩"};

        /// <summary>
        /// Sprite for the number 5
        /// </summary>
        private static string[] five = new string[] {
            "╔╗",
            "╚╗",
            "╚╝"};

        /// <summary>
        /// Sprite for the number 6
        /// </summary>
        private static string[] six = new string[] {
            "╔╗",
            "╠╗",
            "╚╝"};

        /// <summary>
        /// Sprite for the number 7
        /// </summary>
        private static string[] seven = new string[] {
            "╔╗",
            " ║",
            " ╩"};

        /// <summary>
        /// Sprite for the number 8
        /// </summary>
        private static string[] eight = new string[] {
            "╔╗",
            "╠╣",
            "╚╝"};

        /// <summary>
        /// Sprite for the number 9
        /// </summary>
        private static string[] nine = new string[] {
            "╔╗",
            "╚╣",
            " ╩"};

        /// <summary>
        /// Creates a new string array of arrays to save all the number sprites
        /// </summary>
        private static string[][] digits = new string[][]
        { zero, one, two, three, four, five, six, seven, eight, nine};

        /// <summary>
        /// Write the score to the buffer
        /// </summary>
        /// <param name="score">The current score the player has</param>
        public static void WriteScore(int score)
        {
            // Write the score to the buffer
            Write(score, 6, SCORE_X_POS);
        }

        /// <summary>
        /// Delete the score on the buffer
        /// </summary>
        public static void DeleteScore()
        {
            // Delete the score from the buffer
            Delete(6, SCORE_X_POS);
        }

        /// <summary>
        /// Write the number of lifes to the buffer
        /// </summary>
        /// <param name="lifes">The current number of lifes the player has</param>
        public static void WriteLifes(int lifes)
        {
            // Write the number of lifes to the buffer
            Write(lifes, 0, LIFES_X_POS);
        }

        /// <summary>
        /// Delete the lifes on the buffer
        /// </summary>
        public static void DeleteLifes()
        {
            // Delete the number of lifes from the buffer
            Delete( 0, LIFES_X_POS);
        }

        /// <summary>
        /// Write the current level the player is in to the buffer
        /// </summary>
        /// <param name="level">The current level the player is in</param>
        public static void WriteLevel(int level)
        {
            // Write the current level the player is in to the buffer
            Write(level, 2, LEVELS_X_POS);
        }

        /// <summary>
        /// Delete the number of levels the buffer
        /// </summary>
        public static void DeleteLevel()
        {
            // Delete the number of levels from the buffer
            Delete(2, LEVELS_X_POS);
        }

        /// <summary>
        /// Writes all numbers to the buffer
        /// </summary>
        /// <param name="value">The value it self (number of lifes, score, etc...)</param>
        /// <param name="format">Used to know how many numbers will represent the value</param>
        /// <param name="x">The X position to write the numbers</param>
        private static void Write(int value, int format, int x)
        {
            // Save the passed value into a string
            string numberstr = value.ToString();

            // Create a new int `xOffset`
            int xOffset = 0;

            // If the length of the string is less than the format
            if (numberstr.Length < format)
            {
                // Loop the ammount of time necessary
                for (int i = 0; i < format - numberstr.Length; i++)
                {
                    // Write the number 0
                    WriteNumber(0, xOffset, x);

                    // Increase the x offset by 2
                    xOffset += 2;
                }
            }

            // Goes through every char on the `numberstr` string
            for (int i = 0; i < numberstr.Length; i++)
            {
                // Cause we know the string only has numbers convert each char back to a int
                // To be written to the buffer
                WriteNumber(int.Parse(numberstr[i].ToString()), xOffset, x);

                // Increase the x offset by 2
                xOffset += 2;
            }
        }

        /// <summary>
        /// Deletes the number from the buffer
        /// </summary>
        /// <param name="format">Used to know how many numbers will represent the value</param>
        /// <param name="x">The X position to write the numbers</param>
        private static void Delete(int format, int x)
        {
            // Create a new int `xOffset`
            int xOffset = 0;
            
            // Loops the ammount of numbers
            for (int i = 0; i <= format; i++)
            {
                // Loops the ammount of rows
                for (int j = 0; j < NUMBER_ROWS; j++)
                {
                    // Deletes the number from the buffer
                    BufferEditor.Delete(x + xOffset, j + 2, "  ");
                }

                // Increase the x offset by 2
                xOffset += 2;
            }
        }

        /// <summary>
        /// Writes the number to the buffer
        /// </summary>
        /// <param name="value">
        /// The value it self (number of lifes, score, etc...)</param>
        /// <param name="xOffset">
        /// Offset the number to the right depending on the number of numbers</param>
        /// <param name="x">The x value where the number will be displayed</param>
        private static void WriteNumber(int value, int xOffset, int x)
        {
            // Loops 3 times...
            for (int i = 0; i < NUMBER_ROWS; i++)
            {
                // Change the color depending on the row
                BufferEditor.WriteWithColor(0, i + 2, " ", i == 0 ?
                    ConsoleColor.White :
                    i == 1 ?
                    ConsoleColor.Yellow :
                    ConsoleColor.DarkYellow);

                // Write each string to the buffer
                BufferEditor.Write(x + xOffset, i + 2, digits[value][i]);
            }
        }
    }
}
