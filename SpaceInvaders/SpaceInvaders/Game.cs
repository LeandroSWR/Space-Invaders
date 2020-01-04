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
                    LifeLost();
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
            // Create a new list of bullets
            List<Bullet> bullets;

            // Create a new list of bullets to delete
            List<Bullet> bulletsToDelete;

            // Set bullets to be equal to the bullets list from the ship
            bullets = (objectsCollection[0] as Ship).ShipBullets.BulletsList;

            // Instantiate the bullets to delete list
            bulletsToDelete = new List<Bullet>(3);

            // If there's bullets on the scene
            if (bullets.Count > 0)
            {
                // Go through every bullet
                for (int i = 0; i < bullets.Count; i++)
                {
                    // If the bullet has hit an enemy
                    if ((objectsCollection[1] as Enemies).CheckHit(bullets[i].Coordinates))
                    {
                        // Add the bullet to the bullets to delete
                        bulletsToDelete.Add(bullets[i]);

                        // Increase the score
                        score += 100;
                    }
                }

                // Delete the bullets
                (objectsCollection[0] as Ship).ShipBullets.DeleteBullets(bulletsToDelete);
            }
        }

        /// <summary>
        /// The player lost a life
        /// </summary>
        private void LifeLost()
        {
            // Int with the time ammount the blinking will last
            int timer = (lifes == 0) ? 50 : 150;

            // Create a new timer
            Timer counter = new Timer(timer);

            // How long it takes for each blink
            Timer blinkCounter = new Timer(8);

            // If we wan't to display the lifes
            bool displayLife = true;

            // Decrease the ammount of lifes
            lifes--;

            // Set the life lost to true
            (objectsCollection[0] as Ship).LifeLost = true;

            // Reset the Enemies Move Up
            (objectsCollection[1] as Enemies).ResetMoveUp();

            // Set the ship destroyed true
            (objectsCollection[1] as Enemies).shipDestroyed = true;

            // While the counter is counting
            while (counter.IsCounting())
            {
                // Go through all objects in the collection
                for (int i = 0; i < objectsCollection.Count; i++)
                {
                    // Update them
                    (objectsCollection[i] as IGameObject).Update();
                }

                // Delay the thread by a certain ammout of time so the game can be precieved
                Thread.Sleep(FAME_TIME);
                
                // If the blink counter has stopped counting
                if (!blinkCounter.IsCounting())
                {
                    // If display life is true
                    if (displayLife)
                    {
                        // Set it to false
                        displayLife = false;

                        // Display the lifes
                        NumberManager.WriteLifes(lifes + 1);
                        BufferEditor.DisplayRender();
                        NumberManager.WriteLifes(lifes + 1);
                    }
                    // Else
                    else
                    {
                        // Set it to true
                        displayLife = true;

                        // Delete the lifes
                        NumberManager.DeleteLifes();
                    }
                }

                /// Render the frame ///
                BufferEditor.DisplayRender();
            }

            // Reset the ship values
            (objectsCollection[0] as Ship).Init();

            // Set the life lost to false
            (objectsCollection[0] as Ship).LifeLost = false;

            // Set the ship destroyed to false
            (objectsCollection[1] as Enemies).shipDestroyed = false;

            // Display the lifes
            NumberManager.WriteLifes(lifes);
        }

        /// <summary>
        /// Displays the header
        /// </summary>
        private void DisplayHeader()
        {
            // Set the color to red
            BufferEditor.SetColor(ConsoleColor.Red);

            // Loop 100 times
            for (int i = 0; i < 100; i++)
            {
                // Write to the buffer
                BufferEditor.Write(i, 0, "-");
                BufferEditor.Write(i, 5, "-");
            }

            // Change the color to blue
            BufferEditor.SetColor(ConsoleColor.Blue);

            // Write to the buffer
            BufferEditor.Write(1, 1, "Score:");
            BufferEditor.Write(30, 1, "Level:");
            BufferEditor.Write(47, 1, "Lifes:");

            // Write the numbers to the buffer
            NumberManager.WriteScore(score);
            NumberManager.WriteLevel(level);
            NumberManager.WriteLifes(lifes);
        }
    }
}
