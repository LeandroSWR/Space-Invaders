using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Bullets
    {
        // How fast the bullet goes (per second)
        private const int MOVE_SPEED = 1;

        // A constant to hold the values of the Y boundry
        private const int UPPER_BOUNDARY = 7;


        // List with the current bullets on the scene
        public List<Bullet> BulletsList { get; private set; }

        // The current number of bullets
        private int numOfBullets;

        // The max number of bullets of the scene
        private int maxNumOfBullets;

        private int endRow;

        private Timer moveTimer;

        /// <summary>
        /// `ShipBullets` constructor class
        /// </summary>
        public Bullets()
        {
            // Set the maximum number of bullets
            maxNumOfBullets = 3;

            endRow = UPPER_BOUNDARY;

            moveTimer = new Timer(MOVE_SPEED);

            // Initialize a new `List` of `ShipBullets`
            BulletsList = new List<Bullet>(maxNumOfBullets);

            // Set the starting number of bullets
            numOfBullets = 0;
        }

        /// <summary>
        /// Updates all the bullets
        /// </summary>
        public void UpdateBullets()
        {
            if (!moveTimer.IsCounting())
            {
                // Go through all the bullets in the bullets list
                for (int i = 0; i < BulletsList.Count; i++)
                {
                    // Move the bullets
                    BulletsList[i].Update();

                    // If the bullet has reached the top part of the level
                    if (BulletsList[i].Coordinates.Y == endRow)
                    {
                        // Decrease the current number of bullets
                        numOfBullets--;

                        // Delete the bullet
                        BulletsList[i].Delete();

                        // Add it to the remove list
                        BulletsList.Remove(BulletsList[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the bullets passed in
        /// </summary>
        /// <param name="bullets">Bullets to delete</param>
        public void DeleteBullets(List<Bullet> bullets)
        {
            // Go through all the bullets in the list
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                // Decrease the current number of bullets
                numOfBullets--;

                // Delete the bullet
                BulletsList[i].Delete();

                // Add it to the remove list
                BulletsList.Remove(BulletsList[i]);
            }
        }

        /// <summary>
        /// Adds new bullets to the List
        /// </summary>
        /// <param name="x">Bullet X position</param>
        /// <param name="y">Bullet Y position</param>
        /// <param name="movement">The type of movement the bullet has</param>
        public void Add(int x, int y, MoveType movement)
        {
            // If the number of bullets is less than the max
            if (numOfBullets < maxNumOfBullets)
            {
                // Increase the number of bullets
                numOfBullets++;

                // Add a new bullet to the list
                BulletsList.Add(new Bullet(x, y, movement));
            }
        }
    }
}
