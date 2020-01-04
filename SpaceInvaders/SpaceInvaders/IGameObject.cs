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
        /// The update method of the game object
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// The start method of the game object
        /// </summary>
        public abstract void Start();
    }
}
