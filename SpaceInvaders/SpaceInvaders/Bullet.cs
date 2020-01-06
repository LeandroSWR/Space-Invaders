using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Bullet
    {
        // The bullet prite
        private char bullet;

        // How much to move the bullet
        private int increment;

        /// <summary>
        /// The coordinate for the bullet
        /// </summary>
        private Vector2 coordinates;
        public Vector2 Coordinates => coordinates;

        // Save the move direction of the bullet
        public MoveType moveDirection { get; }

        /// <summary>
        /// Constructor for the bullet class
        /// </summary>
        /// <param name="x">The starting x position</param>
        /// <param name="y">The starting y position</param>
        /// <param name="movement">The type of movement the bullet will have (Up/Down)</param>
        public Bullet(int x, int y, MoveType movement) 
        {
            // Set the move direction
            moveDirection = movement;

            // Set the bullet coordinates to the starting position
            coordinates = new Vector2(x, y);

            // Set the type of increment based on the move type
            increment = movement == MoveType.UP ? -1 : 1;

            // Set the bullet sprite
            bullet = '║';
        }

        /// <summary>
        /// Write the bullet to the buffer
        /// </summary>
        private void Write()
        {
            // Write to the buffer
            BufferEditor.Delete(coordinates.X, coordinates.Y, bullet.ToString());

            BufferEditor.WriteWithColor(0, coordinates.Y, " ", ConsoleColor.White);
        }

        /// <summary>
        /// Delete the bullet from the buffer
        /// </summary>
        public void Delete()
        {
            // Write the bullet to the buffer
            BufferEditor.Delete(coordinates.X, coordinates.Y, " ");
        }

        /// <summary>
        /// Move the bullet
        /// </summary>
        private void Move()
        {
            // Delete the bullet from the buffer
            Delete();

            // Move the bullet
            coordinates.Y += increment;
        }

        /// <summary>
        /// The Update method
        /// </summary>
        public void Update()
        {
            // Move the bullet
            Move();

            // Write to the buffer
            Write();
        }
    }
}
