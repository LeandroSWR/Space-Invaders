using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Bullet
    {
        private const int BULLET_SPRITE_HEIGHT = 3;

        // The bullet prite
        private char bullet;

        // How much to move the bullet
        private int increment;

        /// <summary>
        /// The coordinate for the bullet
        /// </summary>
        private Vector2 coordinates;
        public Vector2 Coordinates => coordinates;

        /// <summary>
        /// Constructor for the bullet class
        /// </summary>
        /// <param name="x">The starting x position</param>
        /// <param name="y">The starting y position</param>
        /// <param name="movement">The type of movement the bullet will have (Up/Down)</param>
        public Bullet(int x, int y, MoveType movement) 
        {
            // Set the bullet coordinates to the starting position
            coordinates = new Vector2(x, y);

            // Set the type of increment based on the move type
            increment = movement == MoveType.UP ? -1 : 1;

            // Set the beam sprite
            bullet = '║';
        }

        /// <summary>
        /// Write the bullet to the buffer
        /// </summary>
        private void Write()
        {
            BufferEditor.Write(coordinates.X, coordinates.Y, bullet.ToString());
        }

        /// <summary>
        /// Delete the bullet from the buffer
        /// </summary>
        public void Delete()
        {
            // Write the bullet to the buffer
            BufferEditor.Delete(coordinates.X, coordinates.Y, " ");
            BufferEditor.Write(coordinates.X, coordinates.Y, " ");
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
