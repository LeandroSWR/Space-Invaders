using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SpaceInvaders
{
    class Game
    {
        // Constant that holds the frame time
        private const int FAME_TIME = 20;
        private const int START_LIFES = 3;

        // The current score
        private int score;

        // The current number of lifes
        private int lifes;

        // The current level
        private int level;

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

            // Initialize the lifes at 3
            lifes = START_LIFES;

            // Initialize the level at 1
            level = 1;
        }

        /// <summary>
        /// Game Loop Class
        /// </summary>
        public void Loop()
        {
            // Clears the console
            Console.Clear();

            // Add a ship to the objects collection
            objectsCollection.Add(new Ship());

            // Add the enemies to the objects collection
            objectsCollection.Add(new Enemies());

            // Clear the buffer from the menu render
            BufferEditor.ClearBuffer();

            // Displays the initial header
            DisplayHeader();

            // Loops...
            do
            {
                /// Call a global update method ///

                for (int i = 0; i < objectsCollection.Count; i++)
                {
                    (objectsCollection[i] as IGameObject).Update();
                }

                // Update the numbers on the header
                UpdateHeader();

                // Check for hits
                EnemyDestroyedCheck();

                /// Render the frame ///
                BufferEditor.DisplayRender();

                // Delay the thread by a certain ammout of time so the game can be precieved
                Thread.Sleep(FAME_TIME);

                // While the game is not over
            } while (!gameOver);
        }

        private void EnemyDestroyedCheck()
        {
            List<Bullet> bullets;
            List<Bullet> bulletsToDelete;

            bullets = (objectsCollection[0] as Ship).ShipBullets.BulletsList;
            bulletsToDelete = new List<Bullet>(3);

            if (bullets.Count > 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if ((objectsCollection[1] as Enemies).CheckHit(bullets[i].Coordinates))
                    {
                        bulletsToDelete.Add(bullets[i]);
                        score += 100;
                    }
                }

                (objectsCollection[0] as Ship).ShipBullets.DeleteBullets(bulletsToDelete);
            }
        }

        private void DisplayHeader()
        {
            // Ascii
            // ▄ ▀ █ ▐ ▌ ∞

            Console.SetCursorPosition(0, 0);
            BufferEditor.SetColor(ConsoleColor.Red);

            for (int i = 0; i < 100; i++)
            {
                BufferEditor.Write(i, 0, "-");
                BufferEditor.Write(i, 5, "-");
            }

            BufferEditor.SetColor(ConsoleColor.Blue);

            BufferEditor.Write(1, 1, "Score:");
            BufferEditor.Write(30, 1, "Level:");
            BufferEditor.Write(47, 1, "Lifes:");
        }

        private void UpdateHeader()
        {
            NumberManager.WriteScore(score);
            NumberManager.WriteLevel(level);
            NumberManager.WriteLifes(lifes);
        }
    }
}
