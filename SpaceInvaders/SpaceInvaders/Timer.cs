using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// This class is used to create a generig timer for several diferent purposes
    /// </summary>
    class Timer
    {
        // What limit we can count to
        private int limit;

        // The counter
        private int counter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit">How much to count to</param>
        public Timer(int limit)
        {
            // Set the limit to be equal to the passed value
            this.limit = limit;

            // Set the counter to start at 0
            counter = 0;
        }

        /// <summary>
        /// Returns a true or false base on if the timer is still counting or not
        /// </summary>
        /// <returns>true or false base on if the timer is still counting or not</returns>
        public bool IsCounting()
        {
            // Increase counter by 1 each frame
            counter++;

            // If the counter is greater or equal to the set limit
            if (counter >= limit)
            {
                // Set the counter to 0
                counter = 0;

                // Return false
                return false;
            }

            // Return true
            return true;
        }
    }
}
