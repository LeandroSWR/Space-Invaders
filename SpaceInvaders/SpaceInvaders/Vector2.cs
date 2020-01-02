using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    struct Vector2
    {
        /// <summary>
        /// Entity X Coordinates
        /// </summary>
        public float X { get; set; }
        
        /// <summary>
        /// Entity Y Coordinates
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Entity coordinates constructor
        /// </summary>
        /// <param name="x">The x coordinates</param>
        /// <param name="y">The y coordinates</param>
        public Vector2(float x, float y)
        {
            // Initialize the `X` coordinates with the given value
            X = x;

            // Initialize the `Y` coordinates with the given value
            Y = y;
        }
    }
}
