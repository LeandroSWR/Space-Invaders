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

        }
    }
}
