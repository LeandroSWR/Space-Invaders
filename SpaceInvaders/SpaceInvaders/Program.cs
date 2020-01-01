using System;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hides the console cursor to not be visible
            Console.CursorVisible = false;

            // Set the window size for the console
            Console.SetWindowSize(100, 61);

            // Clears the console
            Console.Clear();

            // Instantiate a new Menu
            Menu menu = new Menu();

            // Render the menu
            menu.RenderMenu();
        }
    }
}
