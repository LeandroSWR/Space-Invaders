using System;
using System.Threading;

namespace SpaceInvaders
{
    class Game
    {
        // Constant that holds the frame time
        private const int FAME_TIME = 17;

        // Bool knows if the game is over
        private bool gameOver;

        /// <summary>
        /// Constructor for the Game class
        /// </summary>
        public Game()
        {
            // Game over is false when the game starts
            gameOver = false;
        }

        /// <summary>
        /// Game Loop Class
        /// </summary>
        public void Loop()
        {
            // Loops...
            do
            {
                // Create a new long based on the current tick
                long start = DateTime.Now.Ticks;


                /// Call a global update method ///
                

                /// Render the frame ///


                // Delay the thread by a certain ammout of time so the game can be precieved
                Thread.Sleep((int)(start / 10000 + FAME_TIME - DateTime.Now.Ticks / 10000));

                // While the game is not over
            } while (!gameOver);
        }
    }
}
