using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Menu
    {
        private int slectionY = 33;

        private bool playSelected;
        private bool quitSelected;

        private MenuSprites mySprites;

        public Menu() 
        {
            mySprites = new MenuSprites();
        }
    }
}
