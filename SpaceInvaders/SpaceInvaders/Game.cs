using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SpaceInvaders
{
    class Game
    {
        // Constant that holds the frame time
        private const int FAME_TIME = 17;

        // Bool knows if the game is over
        private bool gameOver;

        // Collection with all the game objects
        List<IGameObject> objectsCollection;

        /// <summary>
        /// Constructor for the Game class
        /// </summary>
        public Game()
        {
            // Initialize the collection
            objectsCollection = new List<IGameObject>();

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
                
                for (int i  = 0; i < objectsCollection.Count; i++)
                {
                    (objectsCollection[i] as IGameObject).Update();
                }

                /// Render the frame ///
                BufferEditor.DisplayRender();

                // Delay the thread by a certain ammout of time so the game can be precieved
                Thread.Sleep((int)(start / 10000 + FAME_TIME - DateTime.Now.Ticks / 10000));

                // While the game is not over
            } while (!gameOver);
        }
    }
}
