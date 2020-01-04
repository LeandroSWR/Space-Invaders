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

            // Loops...
            do
            {
                /// Call a global update method ///
                for (int i = 0; i < objectsCollection.Count; i++)
                {
                    (objectsCollection[i] as IGameObject).Update();
                }

                // Displays the header
                DisplayHeader();

                // Check for hits
                EnemyDestroyedCheck();
                
                // Check if the ship was hit
                if (ShipDestroyedCheck())
                {
                    ShipDestroyed();
                    gameOver = lifes == 0;
                }

                /// Render the frame ///
                BufferEditor.DisplayRender();

                // Delay the thread by a certain ammout of time so the game can be precieved
                Thread.Sleep(FAME_TIME);

                // While the game is not over
            } while (!gameOver);
        }

        /// <summary>
        /// Check if the ship was destroyed
        /// </summary>
        private bool ShipDestroyedCheck() =>
            (objectsCollection[1] as Enemies).
            CheckShipHit((objectsCollection[0] as Ship).Coordinates);

        /// <summary>
        /// Check if an enemy was destroyed
        /// </summary>
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

        private void ShipDestroyed()
        {
            lifes--;

            LifeLost();

            NumberManager.WriteLifes(lifes);
        }

        private void LifeLost()
        {
            int timer = (lifes == 0) ? 50 : 150;

            Timer counter = new Timer(timer);
            Timer blinkCounter = new Timer(8);
            bool displayLife = true;

            (objectsCollection[0] as Ship).LifeLost = true;
            (objectsCollection[1] as Enemies).ResetMoveUp();
            (objectsCollection[1] as Enemies).shipDestroyed = true;

            while (counter.IsCounting())
            {
                for (int i = 0; i < objectsCollection.Count; i++)
                {
                    (objectsCollection[i] as IGameObject).Update();
                }

                // Delay the thread by a certain ammout of time so the game can be precieved
                Thread.Sleep(FAME_TIME);
                
                if (!blinkCounter.IsCounting())
                {
                    if (displayLife)
                    {
                        displayLife = false;
                        NumberManager.WriteLifes(lifes + 1);
                        BufferEditor.DisplayRender();
                        NumberManager.WriteLifes(lifes + 1);
                    }
                    else
                    {
                        displayLife = true;
                        NumberManager.DeleteLifes();
                    }
                }

                /// Render the frame ///
                BufferEditor.DisplayRender();
            }

            (objectsCollection[0] as Ship).Init();
            (objectsCollection[0] as Ship).LifeLost = false;
            (objectsCollection[1] as Enemies).shipDestroyed = false;

            NumberManager.WriteLifes(lifes);
        }

        private void DisplayHeader()
        {
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

            NumberManager.WriteScore(score);
            NumberManager.WriteLevel(level);
            NumberManager.WriteLifes(lifes);
        }
    }
}
