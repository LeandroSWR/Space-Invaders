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

        // Class with all the sprites to be used in the menu
        private MenuSprites mySprites;

        /// <summary>
        /// Constructor for the menu class
        /// </summary>
        public Menu() 
        {
            // Instantiate the sprites
            mySprites = new MenuSprites();

            // The play button starts selected
            playSelected = true;

            // The quit button starts unselected
            quitSelected = false;
        }

        public void RenderMenu()
        {
            // Display the enemies in the menu
            // Enemy 1
            for (int i = 0; i < mySprites.enemy1String.Length; i++)
            {
                BufferEditor.SetColor(ConsoleColor.Green);
                BufferEditor.Write(7, 10 + i, mySprites.enemy1String[i]);
                BufferEditor.Write(86, 10 + i, mySprites.enemy1String[i]);
            }
            // Enemy 2
            for (int i = 0; i < mySprites.enemy2String.Length; i++)
            {
                BufferEditor.SetColor(ConsoleColor.Cyan);
                BufferEditor.Write(7, 17 + i, mySprites.enemy2String[i]);
                BufferEditor.Write(86, 17 + i, mySprites.enemy2String[i]);
            }
            // Enemy 3
            for (int i = 0; i < mySprites.enemy3String.Length; i++)
            {
                BufferEditor.SetColor(ConsoleColor.Magenta);
                BufferEditor.Write(7, 24 + i, mySprites.enemy3String[i]);
                BufferEditor.Write(86, 24 + i, mySprites.enemy3String[i]);
            }
            // Enemy 4
            for (int i = 0; i < mySprites.enemy4String.Length; i++)
            {
                BufferEditor.SetColor(ConsoleColor.Blue);
                BufferEditor.Write(7, 31 + i, mySprites.enemy4String[i]);
                BufferEditor.Write(86, 31 + i, mySprites.enemy4String[i]);
            }
            // Enemy 5
            for (int i = 0; i < mySprites.enemy5String.Length; i++)
            {
                BufferEditor.SetColor(ConsoleColor.DarkGreen);
                BufferEditor.Write(7, 37 + i, mySprites.enemy5String[i]);
                BufferEditor.Write(86, 37 + i, mySprites.enemy5String[i]);
            }

            BufferEditor.DisplayRender();
        }
    }
}
