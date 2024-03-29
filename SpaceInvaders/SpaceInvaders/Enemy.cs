﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Enemy Class corresponds to an ingame enemy object
    /// </summary>
    class Enemy : GameObject
    {
        // Constants related to what's done in this script
        private const int SPRITE_HEIGTH = 3;

        /// <summary>
        /// The coordinate for the ship
        /// </summary>
        private Vector2 coordinates;
        public Vector2 Coordinates => coordinates;

        /// <summary>
        /// If the enemy is on the first sprite
        /// </summary>
        public bool FirstSprite { get; set; }

        /// <summary>
        /// The current type of movement the enemy has
        /// </summary>
        public MoveType MovementType { get; set; }

        /// <summary>
        /// If the enemy is to go down
        /// </summary>
        public bool IncreaseY { get; set; }

        /// <summary>
        /// If the enemy will move this frame
        /// </summary>
        public bool CanMove { get; set; }

        // The enemy first sprite
        private string[] sprite1;

        // The enemy second sprite
        private string[] sprite2;

        // The enemy current sprite
        private string[] currentSprite;

        // The color of the enemy
        private ConsoleColor myColor;

        /// <summary>
        /// Constructor for the enemy class
        /// </summary>
        /// <param name="x">The starting x position</param>
        /// <param name="y">The starting y position</param>
        /// <param name="color">The color of the enemy</param>
        /// <param name="enemyType">The type of the enemy</param>
        public Enemy(int x, int y, ConsoleColor color, EnemyType enemyType)
        {
            // Set the enemy starting position
            coordinates = new Vector2(x, y);

            // Set the enemy color
            myColor = color;

            // Set the correct sprites
            SetSprites(enemyType);
        }

        /// <summary>
        /// Sets all the necessary sprites for the enemy
        /// </summary>
        /// <param name="enemyType">The type of enemy</param>
        private void SetSprites(EnemyType enemyType)
        {
            // Check the enemy type to use the correct sprite
            switch(enemyType)
            {
                // Enemy One
                case EnemyType.ONE:
                    sprite1 = Sprites.enemy1String;
                    sprite2 = Sprites.enemy2String;
                    break;
                // Enemy Two
                case EnemyType.TWO:
                    sprite1 = Sprites.enemy3String;
                    sprite2 = Sprites.enemy4String;
                    break;
                // Enemy Three
                case EnemyType.THREE:
                    sprite1 = Sprites.enemy5String;
                    sprite2 = Sprites.enemy6String;
                    break;
            }
        }

        /// <summary>
        /// The Update method
        /// </summary>
        public override void Update()
        {
            // If the enemy can move
            if (CanMove)
            {
                // Move
                Move();
            }

            // Writes the enemy sprite to the buffer
            WriteToBuffer();
        }

        /// <summary>
        /// Moves the enemies up
        /// </summary>
        public void DecreaseY()
        {
            // Move the enemy up
            coordinates = new Vector2(Coordinates.X, Coordinates.Y - 1);
        }

        /// <summary>
        /// Moves the enemies down
        /// </summary>
        public void YIncreasse()
        {
            // Move the enemy up
            coordinates = new Vector2(Coordinates.X, Coordinates.Y + 1);
        }

        /// <summary>
        /// Deletes the enemy from the buffer
        /// </summary>
        public void Delete()
        {
            // Go through all the strings in the sprite
            for (int i = 0; i < SPRITE_HEIGTH; i++)
            {
                // Write to the current buffer an empty string
                BufferEditor.Delete(coordinates.X, coordinates.Y + i, "       ");
            }
        }

        /// <summary>
        /// Writes the enemy sprite to the buffer
        /// </summary>
        private void WriteToBuffer()
        {
            // Select the correct sprite to render
            currentSprite = FirstSprite ? sprite1 : sprite2;

            // Go through the array of strings
            for (int i = 0; i < currentSprite.Length; i++)
            {
                // Write a string to the buffer
                BufferEditor.WriteWithColor(0, coordinates.Y + i, " ", myColor);

                // Write each string to the buffer
                BufferEditor.Delete(coordinates.X, coordinates.Y + i, currentSprite[i]);
            }
        }

        /// <summary>
        /// Moves the enemy based on it's current direction
        /// </summary>
        private void Move()
        {
            // If the enemy is to move down
            if (IncreaseY)
            {
                // Clear the area above the enemy
                BufferEditor.WriteWithColor(0, coordinates.Y, " ", ConsoleColor.Black);
                BufferEditor.Delete(coordinates.X, coordinates.Y, "       ");

                // Moves the enemy down
                coordinates.Y++;

                // Reset the `IncreaseY` back to false
                IncreaseY = false;
            } 
            // Else
            else
            {
                // If the movement type is left
                if (MovementType == MoveType.LEFT)
                {
                    // Move the enemy left
                    coordinates.X--;
                }
                // Else if the movement type is right
                else if (MovementType == MoveType.RIGHT)
                {
                    // Move the enemy right
                    coordinates.X++;
                }
            }
        }
    }
}
