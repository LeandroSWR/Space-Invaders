using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SpaceInvaders
{
    class Game
    {
        // Constant that holds the frame time
        private const int FAME_TIME = 40;
        private const int BASE_DELAY = 25;
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
        List<GameObject> objectsCollection;

        // Create a list of bullets to delete
        List<Bullet> bulletsToDelete;

        // Create a new list of bullets
        List<Bullet> bullets;

        // Create a new Timer
        Timer counter;

        // Create a new blinkCounter
        Timer blinkCounter;

        /// <summary>
        /// Constructor for the Game class
        /// </summary>
        public Game()
        {
            // Initialize the collection
            objectsCollection = new List<GameObject>();

            bulletsToDelete = new List<Bullet>(20);

            bullets = new List<Bullet>(20);

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
            // Add a ship to the objects collection
            objectsCollection.Add(new Ship());

            // Add the enemies to the objects collection
            objectsCollection.Add(new Enemies());

            // Add the barriers to the objects collection
            objectsCollection.Add(new Barriers());

            // Clear the buffer from the menu render
            BufferEditor.ClearBuffer();

            /// Call a global Start method ///
            for (int i = 0; i < objectsCollection.Count; i++)
            {
                objectsCollection[i].Start();
            }

            long start;

            int timeToWait;

            // Calls the get ready animation before the game starts
            GetReady();

            // Loops...
            do
            {
                start = DateTime.Now.Ticks / 10000;

                /// Call a global update method ///
                for (int i = 0; i < objectsCollection.Count; i++)
                {
                    objectsCollection[i].Update();
                }

                // Displays the header
                DisplayHeader();

                // Check for hits
                EnemyDestroyedCheck();
                BarrierHitCheck();

                // Check if the ship was hit
                if (ShipDestroyedCheck())
                {
                    LifeLost();
                    gameOver = lifes == 0;
                }

                long begin = DateTime.Now.Ticks / 10000;

                /// Render the frame ///
                BufferEditor.DisplayRender();

                timeToWait = (int)(start + FAME_TIME - DateTime.Now.Ticks / 10000);

                // Delay the thread by a certain ammout of time so the game can be precieved
                Thread.Sleep(timeToWait < 0 ? 0 : timeToWait);

                // While the game is not over
            } while (!gameOver);
        }

        private void GetReady()
        {
            Timer counter = new Timer(160);
            Timer blinker = new Timer(9);
            bool displayText = true;

            while (counter.IsCounting())
            {
                if (!blinker.IsCounting())
                {
                    displayText = !displayText;

                    if (displayText)
                    {
                        BufferEditor.WriteWithColor(0, 50, " ", ConsoleColor.Yellow);
                        BufferEditor.Delete(45, 50, "Get Ready!");
                    }
                    else
                    {
                        BufferEditor.Delete(45, 50, "          ");
                    }
                }

                (objectsCollection[1] as Enemies).MoveDown();

                /// Render the frame ///
                BufferEditor.DisplayRender();

                Thread.Sleep(BASE_DELAY);
            }

            BufferEditor.Write(45, 50, "          ");
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
            // Clear the bullets to delete list
            bulletsToDelete.Clear();

            // Set bullets to be equal to the bullets list from the ship
            bullets = (objectsCollection[0] as Ship).ShipBullets.BulletsList;

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
        /// Check if a bullet has hit a barrier
        /// </summary>
        private void BarrierHitCheck()
        {
            // Create a new list of bullets to delete
            bulletsToDelete.Clear();

            // Create a new vector2 to hold the bullet coordinate
            Vector2 bulletCoordinate;

            // Go through every bullet on the ship's bullet list
            foreach (Bullet bullet in (objectsCollection[0] as Ship).ShipBullets.BulletsList)
            {
                // Save it's coordinate to the vector2
                bulletCoordinate = bullet.Coordinates;

                // If that bullet is in range of a barrier
                if (bulletCoordinate.Y > 49 && bulletCoordinate.Y < 56)
                {
                    // Check if it hit the barrier
                    if ((objectsCollection[2] as Barriers).DeleteBarrierPart(bulletCoordinate))
                    {
                        // Add the bullet to the bullets to delete list
                        bulletsToDelete.Add(bullet);

                        // Remove the bullet from the screen
                        bullet.Delete();
                    }
                }
            }

            // Delete all the bullets that hit the barrier
            (objectsCollection[0] as Ship).ShipBullets.DeleteBullets(bulletsToDelete);

            // Clear the list
            bulletsToDelete.Clear();


            // Go through every bullet on the Enemies bullet list
            foreach (Bullet bullet in (objectsCollection[1] as Enemies).EnemyBullets.BulletsList)
            {
                // Save it's coordinate to the vector2
                bulletCoordinate = bullet.Coordinates;

                // If that bullet is in range of a barrier
                if (bulletCoordinate.Y > 50 & bulletCoordinate.Y < 55)
                {
                    // Check if it hit the barrier
                    if ((objectsCollection[2] as Barriers).DeleteBarrierPart(bulletCoordinate))
                    {
                        // Add the bullet to the bullets to delete list
                        bulletsToDelete.Add(bullet);

                        // Remove the bullet from the screen
                        bullet.Delete();
                    }
                }
            }

            // Delete all the bullets that hit the barrier
            (objectsCollection[1] as Enemies).EnemyBullets.DeleteBullets(bulletsToDelete);
        }

        /// <summary>
        /// The player lost a life
        /// </summary>
        private void LifeLost()
        {
            // Int with the time ammount the blinking will last
            int timer = (lifes == 0) ? 50 : 150;

            // Create a new timer
            counter = new Timer(timer);

            // How long it takes for each blink
            blinkCounter = new Timer(8);

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
                Thread.Sleep(BASE_DELAY);
                
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

                // Check for hits
                EnemyDestroyedCheck();
                BarrierHitCheck();

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
            BufferEditor.WriteWithColor(0, 0, " ", ConsoleColor.Red);
            BufferEditor.WriteWithColor(0, 5, " ", ConsoleColor.Red);

            // Loop 100 times
            for (int i = 0; i < 100; i++)
            {
                // Write to the buffer
                BufferEditor.Write(i, 0, "-");
                BufferEditor.Write(i, 5, "-");
            }

            // Change the color to blue
            BufferEditor.WriteWithColor(0, 1, " ", ConsoleColor.Blue);

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
