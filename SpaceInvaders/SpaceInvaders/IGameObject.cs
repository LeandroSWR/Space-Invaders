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
        public void Update();
    }
}
