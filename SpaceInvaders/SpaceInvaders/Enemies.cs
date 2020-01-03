using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// All enemies do the same so this class manages all of them
    /// </summary>
    class Enemies : IGameObject
    {
        // Constants related to what's done in this script
        private const int ANIMATION_INIT_SPEED = 12;
        private const int MOVE_INIT_SPEED = 7;
        private const int LEFT_BOUNDARY = 3;
        private const int RIGHT_BOUNDARY = 90;
        private const int BOTTOM_BOUDARY = 54;
        private const int MOVE_DOWN_SPEED = 5;
        private const int MOVE_DOWN_STEPS = 30;
        private const int NUMBER_OF_COLUMNS = 7;

        // The current movement direction of all enemies
        private MoveType currentMove;

        // A list with all the enemies
        private List<Enemy> enemies;

        // A timer for the animation
        private Timer animationTimer;

        // A timer for moving down
        private Timer moveDownTimer;

        // A timer for the movement
        private Timer moveTimer;

        // If the enemy is on the first sprite
        private bool firstSprite;

        // If the enemy is to go down
        private bool increaseY;

        // If the enemy will move this frame
        private bool moveEnemy;

        public Enemies()
        {
            // Initialize the list
            enemies = new List<Enemy>();

            // Instantiate a new timer for the animation speed
            animationTimer = new Timer(ANIMATION_INIT_SPEED);

            // Instantiate a new timer for the movement speed
            moveTimer = new Timer(MOVE_INIT_SPEED);

            // Instantiate a new timer for the move down speed
            moveDownTimer = new Timer(MOVE_DOWN_SPEED);

            // When the game starts the enemie is on the first sprite
            firstSprite = true;

            // Set the starting movement direction for the enemies
            currentMove = MoveType.RIGHT;

            // At the start the y wont increasse
            increaseY = false;

            AddEnemies();
        }

        public void Update()
        {
            // If the animation timer has finished counting
            if (!animationTimer.IsCounting())
            {
                // Change the sprite to animate the enemy
                firstSprite = !firstSprite;
            }

            // Update the movement for the enemies
            Move();

            // Updates all enemies
            UpdateAllEnemies();
        }

        /// <summary>
        /// Updates all enemies
        /// </summary>
        private void UpdateAllEnemies()
        {
            // Go through every enemy on the list
            for (int i = 0; i < enemies.Count; i++)
            {
                // Update the sprite to use
                enemies[i].FirstSprite = firstSprite;

                // Update the movement type
                enemies[i].MovementType = currentMove;

                // Update if it's to increase the Y value
                enemies[i].IncreaseY = increaseY;

                // Tell the enemy he can move
                enemies[i].CanMove = moveEnemy;

                // Call the update method on this enemy
                enemies[i].Update();
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
                for (int i = 0; i < enemies.Count; i++)
                {
                    // If the enemies are moving left and 
                    // the current enemy X coordinate is lower than the left boundary
                    if (currentMove == MoveType.LEFT && enemies[i].Coordinates.X < LEFT_BOUNDARY)
                    {
                        // Set the movement direction to right
                        currentMove = MoveType.RIGHT;
                        
                        // Set the increaseY to true
                        increaseY = true;
                    }
                    // Else if the enemies are moving right and 
                    // the current enemy X coordinate is greater than right boundary
                    else if (currentMove == MoveType.RIGHT && enemies[i].Coordinates.X > RIGHT_BOUNDARY)
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
        private void AddEnemies()
        {
            for (int i = 0; i < NUMBER_OF_COLUMNS; i++)
            {
                enemies.Add(new Enemy(i * 8, 8, ConsoleColor.Green, EnemyType.ONE));
                enemies.Add(new Enemy(i * 8, 12, ConsoleColor.Green, EnemyType.ONE));
                enemies.Add(new Enemy(i * 8, 16, ConsoleColor.Cyan, EnemyType.TWO));
                enemies.Add(new Enemy(i * 8, 20, ConsoleColor.Cyan, EnemyType.TWO));
                enemies.Add(new Enemy(i * 8, 24, ConsoleColor.Magenta, EnemyType.THREE));
                enemies.Add(new Enemy(i * 8, 28, ConsoleColor.Magenta, EnemyType.THREE));
            }
        }
    }
}
