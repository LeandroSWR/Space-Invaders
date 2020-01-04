using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Class to hold and manage all the barriers in the game
    /// </summary>
    class Barriers : GameObject
    {
        // Necessary constants for this script
        private const int NUM_OF_BARRIERS = 4;
        private const int POSITION_OFFSET = 20;
        private const int START_X = 16;
        private const int Y_POS = 51;

        // List with all the barriers
        private List<Barrier> barriers;

        /// <summary>
        /// Barriers class constructor
        /// </summary>
        public Barriers()
        {
            // Initialize the list of barriers
            barriers = new List<Barrier>();

            // Create new barriers and add them to the list
            for (int i = 0; i < NUM_OF_BARRIERS; i++)
            {
                barriers.Add(new Barrier(START_X + i * POSITION_OFFSET, Y_POS));
            }
        }

        /// <summary>
        /// Is called at the start of the game
        /// </summary>
        public override void Start()
        {
            // Write the barriers to the buffer
            WriteBarriers();
        }

        /// <summary>
        /// The update method
        /// </summary>
        public override void Update()
        {
            // Go through every barrier on the list and set it's color to green
            for (int i = 0; i < barriers.Count; i++)
            {
                barriers[i].SetColor();
            }
        }

        /// <summary>
        /// Writes the barriers to the buffer
        /// </summary>
        private void WriteBarriers()
        {
            // Go through every barrier on the list and write it to the buffer
            for(int i = 0; i < barriers.Count; i++)
            {
                barriers[i].Write();
            }
        }

        /// <summary>
        /// Deletes a part of the barrier
        /// </summary>
        /// <param name="bulletCoordinate">The coordinate of the bullet</param>
        /// <returns></returns>
        public bool DeleteBarrierPart(Vector2 bulletCoordinate)
        {
            bool retVal = false;

            for (int i = 0; i < barriers.Count; i++)
            {
                if (barriers[i].IsNotDestroyed(bulletCoordinate))
                {
                    barriers[i].SetDestroyed(bulletCoordinate);
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}
