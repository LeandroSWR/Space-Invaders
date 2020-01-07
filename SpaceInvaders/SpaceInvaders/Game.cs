using System;
using System.Collections.Generic;
using System.Threading;

namespace SpaceInvaders
{
    /// <summary>
    /// Game Class responsible for the game loop and several game functions
    /// </summary>
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

        // Instance of the ship
        Ship ship;

        // Instance of the enemies
        Enemies enemies;

        // Instance of the barriers
        Barriers barriers;

        // Instance of the ovni
        Ovni ovni;

        /// <summary>
        /// Constructor for the Game class
        /// </summary>
        public Game()
        {
            // Initialize the lifes at 3
            lifes = START_LIFES;

            // Initialize the level at 1
            level = 1;

            // Initialize the collection
            objectsCollection = new List<GameObject>();

            // Initialize the list of bullets to delete
            bulletsToDelete = new List<Bullet>(20);

            // Initialize the list of bullets
            bullets = new List<Bullet>(20);

            // Instantiate new enemies
            enemies = new Enemies(level);

            // Instantiate new Barriers
            barriers = new Barriers();

            // Instantiate a new Ovni
            ovni = new Ovni();

            // Game over is false when the game starts
            gameOver = false;
        }

        /// <summary>
        /// Game Loop Class
        /// </summary>
        public void Loop()
        {
            // Intantiate a new ship
            ship = new Ship(level);

            // Add a ship to the objects collection
            objectsCollection.Add(ship);

            // Add the enemies to the objects collection
            objectsCollection.Add(enemies);

            // Add the barriers to the objects collection
            objectsCollection.Add(barriers);

            objectsCollection.Add(ovni);

            // Clear the buffer from the menu render
            BufferEditor.ClearBuffer();

            /// Call a global Start method ///
            for (int i = 0; i < objectsCollection.Count; i++)
            {
                objectsCollection[i].Start();
            }

            // Create a new long start
            long start;

            // Create a new long timeToWait
            int timeToWait;

            // Calls the get ready animation before the game starts
            GetReady();

            // Loops...
            do
            {
                // The time in ticks at the start of the frame
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
                OvniDestroyedCheck();
                BarrierHitCheck();

                // If there's no enemies left
                if (enemies.EnemyList.Count == 0)
                {
                    // Call the level completed method
                    LevelCompleted();
                }

                // Check if the ship was hit
                if (ShipDestroyedCheck())
                {
                    // Call the life lost method
                    LifeLost();
                    gameOver = lifes == 0;
                }

                /// Render the frame ///
                BufferEditor.DisplayRender();

                // Get the delay needed
                timeToWait = (int)(start + FAME_TIME - DateTime.Now.Ticks / 10000);

                // Delay the thread by a certain ammout of time so the game can be precieved
                Thread.Sleep(timeToWait < 0 ? 0 : timeToWait);

                // While the game is not over
            } while (!gameOver);
        }

        /// <summary>
        /// Get Ready method to animate the begining of the game or level
        /// </summary>
        private void GetReady()
        {
            // Create a new timer counter
            Timer counter = new Timer(160);

            // Create a new timer to blink the text
            Timer blinker = new Timer(9);

            // Create a new bool to display the text
            bool displayText = true;

            // While the counter is counting
            while (counter.IsCounting())
            {
                // Updates the header
                DisplayHeader();

                // Sets the `displayText` to true or false
                displayText = !blinker.IsCounting() ? !displayText : displayText;

                // If the displayText is true
                if (displayText)
                {
                    // Set a color and write to the buffer
                    BufferEditor.WriteWithColor(0, 50, " ", ConsoleColor.Yellow);
                    BufferEditor.Write(45, 50, "Get Ready!");
                }
                // Else...
                else
                {
                    // Delete from the buffer
                    BufferEditor.Delete(45, 50, "          ");
                }

                // Move the enemies down
                enemies.MoveDown();

                /// Render the frame ///
                BufferEditor.DisplayRender();

                // Delay the loop
                Thread.Sleep(BASE_DELAY);
            }

            // Delete the text again
            BufferEditor.Write(45, 50, "          ");
        }

        /// <summary>
        /// Check if the ship was destroyed
        /// </summary>
        private bool ShipDestroyedCheck() =>
            enemies.CheckShipHit(ship.Coordinates) || enemies.HasReachedBottom();

        /// <summary>
        /// Check if an enemy was destroyed
        /// </summary>
        private void EnemyDestroyedCheck()
        {
            // Clear the bullets to delete list
            bulletsToDelete.Clear();

            // Set bullets to be equal to the bullets list from the ship
            bullets = ship.ShipBullets.BulletsList;

            // If there's bullets on the scene
            if (bullets.Count > 0)
            {
                // Go through every bullet
                for (int i = 0; i < bullets.Count; i++)
                {
                    // If the bullet has hit an enemy
                    if (enemies.CheckHit(bullets[i].Coordinates))
                    {
                        // Add the bullet to the bullets to delete
                        bulletsToDelete.Add(bullets[i]);

                        // Increase the score
                        score += 100;
                    }
                }

                // Delete the bullets
                ship.ShipBullets.DeleteBullets(bulletsToDelete);
            }
        }

        /// <summary>
        /// Checks if the Ovni was hit by a ship bullet
        /// </summary>
        private void OvniDestroyedCheck()
        {
            // If the ovni is flying
            if (ovni.IsFlying())
            {
                // Clear the bullets to delete list
                bulletsToDelete.Clear();

                // Go through every bullet on the ship's bullet list
                foreach (Bullet bullet in ship.ShipBullets.BulletsList)
                {
                    // If a bullet hit the ovni
                    if (ovni.IsDestroyed(bullet.Coordinates))
                    {
                        // Instantiate a new Ovni
                        ovni = new Ovni();

                        // Increase the score by 1000
                        score += 1000;

                        // Add the bullet to the bullets to delete list
                        bulletsToDelete.Add(bullet);

                        // Break from the foreach
                        break;
                    }
                }

                // If there's bullets in the list
                if (bulletsToDelete.Count > 0)
                {
                    // Delete them
                    ship.ShipBullets.DeleteBullets(bulletsToDelete);
                }
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
            foreach (Bullet bullet in ship.ShipBullets.BulletsList)
            {
                // Save it's coordinate to the vector2
                bulletCoordinate = bullet.Coordinates;

                // If that bullet is in range of a barrier
                if (bulletCoordinate.Y > 49 && bulletCoordinate.Y < 56)
                {
                    // Check if it hit the barrier
                    if (barriers.DeleteBarrierPart(bulletCoordinate))
                    {
                        // Add the bullet to the bullets to delete list
                        bulletsToDelete.Add(bullet);

                        // Remove the bullet from the screen
                        bullet.Delete();
                    }
                }
            }

            // Delete all the bullets that hit the barrier
            ship.ShipBullets.DeleteBullets(bulletsToDelete);

            // Clear the list
            bulletsToDelete.Clear();


            // Go through every bullet on the Enemies bullet list
            foreach (Bullet bullet in enemies.EnemyBullets.BulletsList)
            {
                // Save it's coordinate to the vector2
                bulletCoordinate = bullet.Coordinates;

                // If that bullet is in range of a barrier
                if (bulletCoordinate.Y > 50 & bulletCoordinate.Y < 55)
                {
                    // Check if it hit the barrier
                    if (barriers.DeleteBarrierPart(bulletCoordinate))
                    {
                        // Add the bullet to the bullets to delete list
                        bulletsToDelete.Add(bullet);

                        // Remove the bullet from the screen
                        bullet.Delete();
                    }
                }
            }

            // Delete all the bullets that hit the barrier
            enemies.EnemyBullets.DeleteBullets(bulletsToDelete);
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
            ship.LifeLost = true;

            // Reset the Enemies Move Up
            enemies.ResetMoveUp();

            // Set the ship destroyed true
            enemies.shipDestroyed = true;

            // While the counter is counting
            while (counter.IsCounting())
            {
                // Go through all objects in the collection
                for (int i = 0; i < objectsCollection.Count; i++)
                {
                    // Update them
                    (objectsCollection[i] as GameObject).Update();
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
            ship.Init(level);

            // Set the life lost to false
            ship.LifeLost = false;

            // Set the ship destroyed to false
            enemies.shipDestroyed = false;

            // Display the lifes
            NumberManager.WriteLifes(lifes);
        }

        /// <summary>
        /// Is called when the level is completed to play animations and initialise the next level
        /// </summary>
        private void LevelCompleted()
        {
            // Create a new timer to be a counter for the animations duration
            Timer counter = new Timer(201);

            // Create a new timer for the blinking animation
            Timer blinker = new Timer(9);

            // Create a new bool
            bool displayScore = false;

            // Clears the buffer
            BufferEditor.ClearBuffer();

            // Loops while the counter is counting
            while (counter.IsCounting())
            {
                // Sets the display score to true or false depending on the counter
                displayScore = !blinker.IsCounting() ? !displayScore : displayScore;

                // If displayScore is true
                if (displayScore)
                {
                    // Write an informational text saying that the level was completed
                    BufferEditor.WriteWithColor(0, 22, " ", ConsoleColor.Yellow);
                    BufferEditor.WriteWithColor(0, 24, " ", ConsoleColor.Yellow);
                    BufferEditor.Delete(36, 22, "L E V E L  C O M P L E T E D!");
                    BufferEditor.Delete(38, 24, "Level bonus 1000 points!");
                    NumberManager.WriteLevel(level);
                }
                else
                {
                    // Delete the previously written text
                    BufferEditor.Write(36, 22, "                             ");
                    BufferEditor.Write(38, 24, "                        ");
                    NumberManager.DeleteLevel();
                }

                // Updates the score
                score += 5;
                NumberManager.WriteScore(score);

                // Updates the header
                DisplayHeader();

                // Display the frame
                BufferEditor.DisplayRender();

                // Wait for 20 miliseconds
                Thread.Sleep(20);
            }

            // Removes the text in the middle of the screen
            BufferEditor.Write(36, 22, "                             ");
            BufferEditor.Write(38, 24, "                        ");

            // Increases the level
            level++;

            // Updates the level number
            NumberManager.WriteLevel(level);

            // Delays for 800 miliseconds
            Thread.Sleep(800);

            // Initializes the next level
            InitNextLevel();
        }

        /// <summary>
        /// Initialize the next level
        /// </summary>
        private void InitNextLevel()
        {
            // Reset the ship values
            ship.Init(level);

            // Instantiate new enemies
            enemies = new Enemies(level);

            // Instantiate new barriers
            barriers = new Barriers();

            // Start the barriers
            barriers.Start();

            // Clears the objects collection
            objectsCollection.Clear();

            // Add the ship to the collection
            objectsCollection.Add(ship);

            // Add the enemies to the collection
            objectsCollection.Add(enemies);

            // Add the barriers to the collection
            objectsCollection.Add(barriers);

            // Call the get ready method
            GetReady();
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
