namespace SpaceInvaders
{
    /// <summary>
    /// Displays an explosion where an object was destroyed
    /// </summary>
    class Explosion
    {
        // Constant to define the sprite height
        private const int SPRITE_HEIGHT = 3;

        /// <summary>
        /// The coordinate for the ship
        /// </summary>
        private Vector2 coordinates;
        public Vector2 Coordinates => coordinates;

        // Create a new Timer
        private Timer timer;

        // Save the type of explosion
        private ExplosionType type;

        // All the sprites for the explosions
        private string[] sprite1;
        private string[] sprite2;
        private string[] sprite3;
        private string[] sprite4;
        private string[] sprite5;

        // The current animation frame
        private int animation;

        /// <summary>
        /// Constructor for the Explosion class
        /// </summary>
        /// <param name="coordinate">The coordinate for the explosion</param>
        /// <param name="explosionType">The type of explosion</param>
        public Explosion(Vector2 coordinate, ExplosionType explosionType)
        {
            // Instantiate a new timer
            timer = new Timer(2);

            // Set the coordinates
            coordinates = coordinate;

            // Decrease the X coordinate by 1
            // Makes the explosion sprite fit better
            coordinates.X--;

            // Set the animation frame to 0
            animation = 0;

            // Set the type of explosion
            type = explosionType;

            // Set the explosion sprites
            SetSprites();
        }
        
        /// <summary>
        /// Writes the sprite of the explosion to the buffer
        /// </summary>
        /// <returns>If the explosion has finished the animation</returns>
        public bool Explode()
        {
            // Create a bool variable
            bool retVal = false;

            // Create a new String array for the sprite
            string[] sprite = null;

            // If the timer is not counting
            if (!timer.IsCounting())
            {
                // Switch the animation frame
                switch (animation)
                {
                    // If it's the frame 0
                    case 0:
                        // Set the sprite to 1
                        sprite = sprite1;
                        break;
                    // If it's the frame 1
                    case 1:
                        // Set the sprite to 2
                        sprite = sprite2;
                        break;
                    // If it's the frame 2
                    case 2:
                        // Set the sprite to 3
                        sprite = sprite3;
                        break;
                    // If it's the frame 3
                    case 3:
                        // If it's a small explosion the animation lasts less time
                        if (type == ExplosionType.SMALL)
                        {
                            // Set the sprite to 5
                            sprite = sprite5;

                            // Set the return value to true
                            retVal = true;
                        }
                        // Else...
                        else
                        {
                            // Set the sprite to 4
                            sprite = sprite4;
                        }
                        break;
                    // If it's the frame 4
                    case 4:
                        // Set the sprite to 5
                        sprite = sprite5;

                        // Set the return value to true
                        retVal = true;
                        break;
                }

                // Increase the animation frame
                animation++;

                // Go through every string on the sprite
                for (int i = 0; i < SPRITE_HEIGHT; i++)
                {
                    // Write it to the buffer
                    BufferEditor.Write(coordinates.X, coordinates.Y + i, sprite[i]);
                }
            }

            // Return the return value
            return retVal;
        }

        /// <summary>
        /// Deletes the sprite from the buffer
        /// </summary>
        public void Delete()
        {
            // Go through every string on the sprite
            for (int i = 0; i < sprite5.Length; i++)
            {
                // Delete it from the buffer
                BufferEditor.Delete(coordinates.X, coordinates.Y + i, sprite5[i]);
            }
        }

        /// <summary>
        /// Sets the sprites for the explosion
        /// </summary>
        private void SetSprites()
        {
            sprite1 = new string[3]
            {
                "         ",
                "    ▄    ",
                "         "
            };
            sprite2 = new string[3]
            {
                "         ",
                "   ▀▄▀   ",
                "   ▀ ▀   "
            };
            sprite3 = new string[3]
            {
                "  ▄   ▄  ",
                " ▄ ▀ ▀ ▄ ",
                "  ▄▀ ▀▄  "
            };
            sprite4 = new string[3]
            {
                " ▄     ▄ ",
                "▄ ▀   ▀ ▄",
                " ▄▀   ▀▄ "
            };
            sprite5 = new string[3]
            {
                "         ",
                "         ",
                "         "
            };
        }
    }
}
