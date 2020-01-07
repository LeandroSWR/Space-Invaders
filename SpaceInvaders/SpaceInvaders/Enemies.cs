using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// All enemies do the same so this class manages all of them
    /// </summary>
    class Enemies : GameObject
    {
        // Constants related to what's done in this script
        private const int ANIMATION_INIT_SPEED = 12;
        private const int NUMBER_OF_COLUMNS = 7;
        private const int BULLET_MOVE_SPEED = 2;
        private const int MOVE_DOWN_STEPS = 30;
        private const int INIT_NUM_BULLETS = 3;
        private const int MAX_NUM_BULLETS = 10;
        private const int BOTTOM_BOUDARY = 54;
        private const int RIGHT_BOUNDARY = 90;
        private const int LOWER_BOUNDARY = 58;
        private const int MOVE_DOWN_SPEED = 5;
        private const int MOVE_INIT_SPEED = 7;
        private const int MOVE_UP_STEPS = 15;
        private const int MOVE_UP_SPEED = 5;
        private const int TOP_START_ROW = 6;
        private const int LEFT_BOUNDARY = 3;
        private const int Y_MIN = 12;
        
        // The current movement direction of all enemies
        private MoveType currentMove;

        // A list with all the enemies
        public List<Enemy> EnemyList { get; private set; }

        // A timer for the animation
        private Timer animationTimer;

        // A timer for moving down
        private Timer moveDownTimer;

        // A timer for the movement
        private Timer moveTimer;

        // A timer for the movement up steps
        private Timer moveUpSteps;

        // A timer for the movement up speed
        private Timer moveUpSpeed;

        // Instance of Bullets
        public Bullets EnemyBullets { get; private set; }

        // Instantiate a new Random
        Random rnd = new Random();

        // The step ammount to move the enemy down
        private int moveDownSteps;

        private int numOfBullets;

        // If the enemy is on the first sprite
        private bool firstSprite;

        // If the enemy is to go down
        private bool increaseY;

        // If the enemy will move this frame
        private bool moveEnemy;

        // Check if the ship died
        public bool shipDestroyed;

        /// <summary>
        /// The enemies constructor 
        /// </summary>
        /// <param name="level">The current level</param>
        public Enemies(int level)
        {
            // Set the max number of bullets for the enemies
            numOfBullets = INIT_NUM_BULLETS;

            // Increase the number of bullets every 4 levels up to a maximum of 10 bullets
            Timer bulletCounter = new Timer(4);

            // Loop the level amount of times
            for (int i = 0; i < level; i++)
            {
                // If the bullet counter has stopped and the num of bullets is less than the max
                if (!bulletCounter.IsCounting() && (numOfBullets < MAX_NUM_BULLETS))
                {
                    // Increase the number of bullets by 2
                    numOfBullets += 2;
                }
            }

            // Initialize the list
            EnemyList = new List<Enemy>();

            // Instantiate a new timer for the animation speed
            animationTimer = new Timer(ANIMATION_INIT_SPEED);

            // Instantiate a new timer for the movement speed
            moveTimer = new Timer(MOVE_INIT_SPEED);

            // Instantiate a new timer for the move down speed
            moveDownTimer = new Timer(MOVE_DOWN_SPEED);

            // Instantiate a new timer for the move up steps
            moveUpSteps = new Timer(MOVE_UP_STEPS);

            // Instantiate a new timer for the move up speed
            moveUpSpeed = new Timer(MOVE_UP_SPEED);

            // Instatiate the ship bullets
            EnemyBullets = new Bullets(LOWER_BOUNDARY, numOfBullets, BULLET_MOVE_SPEED);

            // When the game starts the enemie is on the first sprite
            firstSprite = true;

            // Set the starting movement direction for the enemies
            currentMove = MoveType.RIGHT;

            // At the start the y wont increasse
            increaseY = false;

            AddEnemies(level);
        }

        public override void Update()
        {
            // If the animation timer has finished counting
            if (!animationTimer.IsCounting())
            {
                // Change the sprite to animate the enemy
                firstSprite = !firstSprite;
            }

            // Update all enemy bullets
            EnemyBullets.UpdateBullets();

            // If the ship is not destroyed
            if (!shipDestroyed)
            {
                // Try to shoot
                Shoot();

                // Update the movement for the enemies
                Move();
            } 
            // Else...
            else
            {
                // Move the enemies up
                MoveUp();
            }
            
            // Updates all enemies
            UpdateAllEnemies();

        }

        /// <summary>
        /// Moves the enemies up
        /// </summary>
        public void MoveUp()
        {
            // If the moveSpeed timer is not counting and the move steps is counting
            if (!moveUpSpeed.IsCounting() && moveUpSteps.IsCounting())
            {
                // Create a new int `min`
                int min = 60;

                // Create a new coordinate
                Vector2 coordinate;

                // Go through every enemy in the enemies list
                for (int i = 0; i < EnemyList.Count; i++)
                {
                    // Save it's coordinates
                    coordinate = EnemyList[i].Coordinates;

                    // If the min is bigger than the coordinate Y
                    if (min > coordinate.Y)
                    {
                        // Set the min to be equal to the coordinate Y
                        min = coordinate.Y;
                    }
                }

                // If min is bigger than the minimum Y value
                if (min > Y_MIN)
                {
                    // Go through every enemy on the enemies list
                    for (int i = 0; i < EnemyList.Count; i++)
                    {
                        // Save it's coordinates
                        coordinate = EnemyList[i].Coordinates;

                        // Delete the under part of the enemy
                        BufferEditor.Delete(coordinate.X, coordinate.Y + 2, "       ");

                        // Decrease the enemy Y value
                        EnemyList[i].DecreaseY();
                    }
                }
            }
        }

        /// <summary>
        /// Moves all enemies down
        /// </summary>
        public void MoveDown()
        {
            // If the animation timer has finished counting
            if (!animationTimer.IsCounting())
            {
                // Change the sprite to animate the enemy
                firstSprite = !firstSprite;
            }

            // If the moveDownTimer is not counting
            if (!moveDownTimer.IsCounting())
            {
                //Increase the steps by 1
                moveDownSteps++;

                // If the moveDowSteps is greater than the `MOVE_DOWN_STEPS` returns
                if (moveDownSteps > MOVE_DOWN_STEPS) return;

                // Create a new vector2
                Vector2 coordinate;

                // Goes through every enemy in the Enemy List
                foreach (Enemy enemy in EnemyList)
                {
                    // Save the enemy coordinate to a more convinient variable
                    coordinate = enemy.Coordinates;

                    // Set the enemy to not move
                    enemy.CanMove = false;

                    // Update the enemy sprite
                    enemy.FirstSprite = firstSprite;

                    // Increase the enemy Y
                    enemy.YIncreasse();

                    // If the enemy Y coordinate is greater than the `TOP_START_ROW`
                    if (coordinate.Y > TOP_START_ROW)
                    {
                        // Delete the top line of the enemy
                        BufferEditor.Delete(coordinate.X, coordinate.Y, "       ");

                        // Update the enemy
                        enemy.Update();
                    }
                }
            }
        }

        /// <summary>
        /// Reset the move up timers
        /// </summary>
        public void ResetMoveUp()
        {
            // Reset the moveUpSteps timer
            moveUpSteps = new Timer(MOVE_UP_STEPS);
            
            // Reset the moveUpSpeed;
            moveUpSpeed = new Timer(MOVE_UP_SPEED);
        }

        /// <summary>
        /// A random enemy is choosen to shoot a bullet
        /// </summary>
        private void Shoot()
        {
            // Get a random index from the list of enemies
            int index = rnd.Next(0, EnemyList.Count);

            // Get the coordinates of the enemy
            int x = EnemyList[index].Coordinates.X + 3;
            int y = EnemyList[index].Coordinates.Y + 3;

            // Add a new bullet
            EnemyBullets.Add(x, y, MoveType.DOWN);
        }

        /// <summary>
        /// Checks if an enemy was hit by a bullet
        /// </summary>
        /// <param name="bulletCoordinate">The coordinate of the bullet</param>
        /// <returns>If there was a hit</returns>
        public bool CheckHit(Vector2 bulletCoordinate, out Vector2 coordinate)
        {
            coordinate = new Vector2(0, 0);

            // Go through all existing enemies
            for (int i = EnemyList.Count - 1; i >= 0; i--)
            {
                // Save the coordinate on a specific variable
                coordinate = EnemyList[i].Coordinates;

                // If the bullet hit an enemy
                if (bulletCoordinate.X >= coordinate.X + 1 &&
                    bulletCoordinate.X < coordinate.X + 6 &&
                    bulletCoordinate.Y >= coordinate.Y &&
                    bulletCoordinate.Y < coordinate.Y + 2)
                {
                    // Delete the enemy
                    EnemyList[i].Delete();

                    // Remove the enemy from the list
                    EnemyList.Remove(EnemyList[i]);

                    // Return true
                    return true;
                }
            }

            // Return false
            return false;
        }

        /// <summary>
        /// Checks if any of the enemy bullets has it the ship
        /// </summary>
        /// <param name="shipCoordinate">The ship's coordinate</param>
        /// <returns>If the ship was hit</returns>
        public bool CheckShipHit(Vector2 shipCoordinate)
        {
            // Saves the ship location
            Vector2 coordinate;

            // Go through all existing enemy bullets
            for (int i = EnemyBullets.BulletsList.Count - 1; i >= 0; i--)
            {
                // Save the coordinate on a specific variable
                coordinate = EnemyBullets.BulletsList[i].Coordinates;

                // Check if there was a collision between the ship and the bullet
                if ((coordinate.X > shipCoordinate.X + 2 &&
                    coordinate.X < shipCoordinate.X + 4 &&
                    coordinate.Y == shipCoordinate.Y) ||
                    (coordinate.X > shipCoordinate.X + 1 &&
                    coordinate.X < shipCoordinate.X + 5 &&
                    coordinate.Y == shipCoordinate.Y + 1) ||
                    (coordinate.X > shipCoordinate.X &&
                    coordinate.X < shipCoordinate.X + 6 &&
                    coordinate.Y == shipCoordinate.Y + 2))
                {
                    // Delete the bullet
                    EnemyBullets.BulletsList[i].Delete();

                    // Remove the bullet from the list
                    EnemyBullets.BulletsList.Remove(EnemyBullets.BulletsList[i]);

                    // Return true
                    return true;
                }
            }

            // Return false
            return false;
        }

        /// <summary>
        /// Updates all enemies
        /// </summary>
        private void UpdateAllEnemies()
        {
            // Go through every enemy on the list
            for (int i = 0; i < EnemyList.Count; i++)
            {
                // Update the sprite to use
                EnemyList[i].FirstSprite = firstSprite;

                // Update the movement type
                EnemyList[i].MovementType = currentMove;

                // Update if it's to increase the Y value
                EnemyList[i].IncreaseY = increaseY;

                // Tell the enemy he can move
                EnemyList[i].CanMove = moveEnemy;

                if (shipDestroyed)
                {
                    EnemyList[i].CanMove = false;
                }

                // Call the update method on this enemy
                EnemyList[i].Update();
            }

            // Reset the `increaseY` to false
            increaseY = false;

            // Reset the move enemy back to false
            moveEnemy = false;
        }

        /// <summary>
        /// Updates the movement type for all the enemies
        /// </summary>
        private void Move()
        {
            // If the move timer has finished counting
            if (!moveTimer.IsCounting())
            {
                // The enemy will move this frame
                moveEnemy = true;

                // Find the closest enemy to the right and the left
                for (int i = 0; i < EnemyList.Count; i++)
                {
                    // If the enemies are moving left and 
                    // the current enemy X coordinate is lower than the left boundary
                    if (currentMove == MoveType.LEFT && EnemyList[i].Coordinates.X < LEFT_BOUNDARY)
                    {
                        // Set the movement direction to right
                        currentMove = MoveType.RIGHT;
                        
                        // Set the increaseY to true
                        increaseY = true;
                    }
                    // Else if the enemies are moving right and 
                    // the current enemy X coordinate is greater than right boundary
                    else if (currentMove == MoveType.RIGHT && EnemyList[i].Coordinates.X > RIGHT_BOUNDARY)
                    {
                        // Set the movement direction to left
                        currentMove = MoveType.LEFT;

                        // Set the increaseY to true
                        increaseY = true;
                    }
                }
            }
        }

        /// <summary>
        /// Adds enemies to the list of enemies
        /// </summary>
        private void AddEnemies(int level)
        {
            // Create a new int for the number of columns
            int numColumns = NUMBER_OF_COLUMNS;

            // Create a new int for how many more enemies to add
            int moreEnemies = 0;

            // Create a counter
            int counter = 0;

            // Increase the number of row every 4 levels up to a maximum of 11
            for (int i = 0; i < level; i++)
            {
                // Increase the counter by 1
                counter++;

                // If the counter is equal to 4
                if (counter == 4)
                {
                    // Set the counter to 0
                    counter = 0;

                    // If ``moreEnemies is less than 4`
                    if (moreEnemies < 4)
                    {
                        // Increase the number of enemies to add
                        moreEnemies++;
                    }
                }
            }

            // Increase the number of enemies by `moreEnemies`
            numColumns += moreEnemies;

            // Loops the number of `numColumns`
            for (int i = 0; i < numColumns; i++)
            {
                // Add all the starting enemies
                EnemyList.Add(new Enemy(i * 8, -20, ConsoleColor.Green, EnemyType.ONE));
                EnemyList.Add(new Enemy(i * 8, -16, ConsoleColor.Green, EnemyType.ONE));
                EnemyList.Add(new Enemy(i * 8, -12, ConsoleColor.Cyan, EnemyType.TWO));
                EnemyList.Add(new Enemy(i * 8, -8, ConsoleColor.Cyan, EnemyType.TWO));
                EnemyList.Add(new Enemy(i * 8, -4, ConsoleColor.Magenta, EnemyType.THREE));
                EnemyList.Add(new Enemy(i * 8, 0, ConsoleColor.Magenta, EnemyType.THREE));

                // If the level is greater than 4
                if (level > 4)
                {
                    // Adds an extra line of enemies
                    EnemyList.Add(new Enemy(i * 8, 4, ConsoleColor.Blue, EnemyType.ONE));

                    // If the level is greate than 9
                    if (level > 9)
                    {
                        // Adds an extra line of enemies
                        EnemyList.Add(new Enemy(i * 8, 8, ConsoleColor.Blue, EnemyType.TWO));

                        // If the level is greate than 14
                        if (level > 14)
                        {
                            // Adds an extra line of enemies
                            EnemyList.Add(new Enemy(i * 8, 12, ConsoleColor.DarkGreen, EnemyType.THREE));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if an enemy has reached the bottom of the screen
        /// </summary>
        public bool HasReachedBottom()
        {
            // Create a new bool for the return value
            bool retVal = false;

            // Go through every enemy on the enemy list
            for (int i = EnemyList.Count - 1; i >= 0; i--)
            {
                // If it has reached the bottom
                if (EnemyList[i].Coordinates.Y > BOTTOM_BOUDARY)
                {
                    // Delete that enemy
                    EnemyList[i].Delete();

                    // Remove it from the list
                    EnemyList.Remove(EnemyList[i]);

                    // Sets the return value to true
                    retVal = true;
                }
            }

            // Return `reVal`
            return retVal;
        }
    }
}
