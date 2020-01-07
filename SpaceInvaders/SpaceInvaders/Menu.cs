using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Menu
    {
        // Constant of the Y value to render the selection button
        private const int Y_SELECTION = 33;

        // Constant of the X value to render the selection button for the play
        private const int PLAY_X_SELECTION = 27;

        // Constant of the X value to render the selection button for the quit
        private const int QUIT_X_SELECTION = 67;

        // If the play button is selected
        private bool playSelected;

        // If the quit button is selected
        private bool quitSelected;

        // Holds a game instance
        private Game game;

        /// <summary>
        /// Constructor for the menu class
        /// </summary>
        public Menu() 
        {
            // The play button starts selected
            playSelected = true;

            // The quit button starts unselected
            quitSelected = false;
        }

        /// <summary>
        /// Renders all the sprites in the menu
        /// </summary>
        public void RenderMenu()
        {
            // Display the enemies in the menu
            // Enemy 1
            for (int i = 0; i < Sprites.enemy1String.Length; i++)
            {
                BufferEditor.WriteWithColor(0, 10 + i, " ", ConsoleColor.Green);
                BufferEditor.Write(6, 10 + i, Sprites.enemy1String[i]);
                BufferEditor.Write(85, 10 + i, Sprites.enemy1String[i]);
            }
            // Enemy 2
            for (int i = 0; i < Sprites.enemy2String.Length; i++)
            {
                BufferEditor.WriteWithColor(0, 17 + i, " ", ConsoleColor.Cyan);
                BufferEditor.Write(6, 17 + i, Sprites.enemy2String[i]);
                BufferEditor.Write(85, 17 + i, Sprites.enemy2String[i]);
            }
            // Enemy 3
            for (int i = 0; i < Sprites.enemy3String.Length; i++)
            {
                BufferEditor.WriteWithColor(0, 24 + i, " ", ConsoleColor.Magenta);
                BufferEditor.Write(6, 24 + i, Sprites.enemy3String[i]);
                BufferEditor.Write(85, 24 + i, Sprites.enemy3String[i]);
            }
            // Enemy 4
            for (int i = 0; i < Sprites.enemy4String.Length; i++)
            {
                BufferEditor.WriteWithColor(0, 31 + i, " ", ConsoleColor.Blue);
                BufferEditor.Write(6, 31 + i, Sprites.enemy4String[i]);
                BufferEditor.Write(85, 31 + i, Sprites.enemy4String[i]);
            }
            // Enemy 5
            for (int i = 0; i < Sprites.enemy5String.Length; i++)
            {
                BufferEditor.WriteWithColor(0, 37 + i, " ", ConsoleColor.DarkGreen);
                BufferEditor.Write(6, 37 + i, Sprites.enemy5String[i]);
                BufferEditor.Write(85, 37 + i, Sprites.enemy5String[i]);
            }

            // Display Title
            for (int i = 0; i < Sprites.spaceString.Length; i++)
            {
                BufferEditor.WriteWithColor(0, 21 + i, " ", ConsoleColor.Yellow);
                BufferEditor.Write(21, 21 + i, Sprites.spaceString[i]);
                BufferEditor.Write(45, 21 + i, Sprites.invadersString[i]);
            }

            // Display Buttons
            for (int i = 0; i < Sprites.playString.Length; i++)
            {
                BufferEditor.WriteWithColor(0, 28 + i, " ", ConsoleColor.White);
                BufferEditor.Write(22, 28 + i, Sprites.playString[i]);
                BufferEditor.Write(63, 28 + i, Sprites.quitString[i]);
            }

            // Display Button Selector
            for (int i = 0; i < Sprites.selectionString.Length; i++)
            {
                BufferEditor.WriteWithColor(0, 33 + i, " ", ConsoleColor.White);
                // If we have the play button selected
                if (playSelected)
                {
                    // Draw the ship bellow the play button
                    BufferEditor.Write(PLAY_X_SELECTION, Y_SELECTION + i, Sprites.selectionString[i]);
                    BufferEditor.Write(QUIT_X_SELECTION, Y_SELECTION + i, "      ");
                } 
                // If we have the quit button selected
                else if (quitSelected)
                {
                    // Draw the ship bellow the quit button
                    BufferEditor.Write(QUIT_X_SELECTION, Y_SELECTION + i, Sprites.selectionString[i]);
                    BufferEditor.Write(PLAY_X_SELECTION, Y_SELECTION + i, "      ");
                }
            }

            // Tells the double buffer to render the frame
            BufferEditor.DisplayRender();

            // Gets the input from the user
            GetInput();
        }

        /// <summary>
        /// Check the input from the user
        /// </summary>
        private void GetInput()
        {
            // Check what's the user input
            switch(Console.ReadKey(true).Key)
            {
                // If the user pressed the left arrow key
                case ConsoleKey.LeftArrow:
                    // If the quit button is selected
                    if (quitSelected)
                    {
                        // Change what button is selected
                        playSelected = true;
                        quitSelected = false;
                    }
                    break;
                // If the user pressed the right arrow key
                case ConsoleKey.RightArrow:
                    // If the play button is selected
                    if (playSelected)
                    {
                        // Change what button is selected
                        playSelected = false;
                        quitSelected = true;
                    }
                    break;
                // If the user pressed enter
                case ConsoleKey.Enter:
                    // If the play button is selected
                    if (playSelected)
                    {
                        // Instantiate a new Game
                        game = new Game();

                        // Start the game
                        game.Loop();
                    }
                    else
                    {
                        // Exits the application
                        Environment.Exit(0);
                    }
                    break;
            }

            // Render the menu
            RenderMenu();
        }
    }
}
