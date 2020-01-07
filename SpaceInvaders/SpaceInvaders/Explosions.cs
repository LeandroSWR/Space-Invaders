using System.Collections.Generic;

namespace SpaceInvaders
{
    /// <summary>
    /// Controlls all the explosions in the game
    /// </summary>
    class Explosions : GameObject
    {
        // List of explosions
        private List<Explosion> explosions;

        /// <summary>
        /// Constructor for the Explosions class
        /// </summary>
        public Explosions()
        {
            // Initialize the list of explosions
            explosions = new List<Explosion>();
        }

        /// <summary>
        /// The Update method
        /// </summary>
        public override void Update()
        {
            // Go through all the explosions on the list
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                // If the explosion has finished the animation
                if (explosions[i].Explode())
                {
                    // Delete the explosion
                    explosions[i].Delete();

                    // Remove it from the list
                    explosions.Remove(explosions[i]);
                }
            }
        }

        /// <summary>
        /// Adds an explosion to the list
        /// </summary>
        /// <param name="coordinate">The coordinates for the explosion</param>
        /// <param name="explosionType">The type of explosion</param>
        public void Add(Vector2 coordinate, ExplosionType explosionType)
        {
            // Adds a new explosion to the list
            explosions.Add(new Explosion(coordinate, explosionType));
        }
    }
}
