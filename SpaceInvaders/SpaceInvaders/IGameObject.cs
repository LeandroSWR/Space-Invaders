using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Interface for all game objects
    /// </summary>
    interface IGameObject
    {
        /// <summary>
        /// The coordinates for the game object
        /// </summary>
        public Vector2 Coordinates { get; }

        /// <summary>
        /// The update method of the game object
        /// </summary>
        public void Update();
    }
}
