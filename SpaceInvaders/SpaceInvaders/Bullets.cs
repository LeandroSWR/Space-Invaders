using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Bullets
    {
        // How fast the beam goes (per second)
        private const int MOVE_SPEED = 1;

        // A constant to hold the values of the Y boundry
        private const int UPPER_BOUNDARY = 7;


        // List with the current beams on the scene
        public List<Bullet> BulletsList { get; private set; }
        
        // The current number of beams
        private int numOfBullets;

        // The max number of beams of the scene
        private int maxNumOfBullets;

        private int endRow;

        private Timer moveTimer;

        /// <summary>
        /// `ShipBeams` constructor class
        /// </summary>
        public Bullets()
        {
            // Set the maximum number of beams
            maxNumOfBullets = 3;

            endRow = UPPER_BOUNDARY;

            moveTimer = new Timer(MOVE_SPEED);

            // Initialize a new `List` of `ShipBeam`
            BulletsList = new List<Bullet>(maxNumOfBullets);

            // Set the starting number of beams
            numOfBullets = 0;
        }

        public void UpdateBullets()
        {
            if (moveTimer.IsCounting()) return;

            // Go through all the beams in the beams list
            for (int i = 0; i < BulletsList.Count; i++)
            {
                // Move the beams
                BulletsList[i].Update();

                // If the beam has reached the top part of the level
                if (BulletsList[i].Coordinates.Y == endRow)
                {
                    // Decrease the current number of beams
                    numOfBullets--;

                    // Delete the bullet
                    BulletsList[i].Delete();

                    // Add it to the remove list
                    BulletsList.Remove(BulletsList[i]);
                }
            }
        }

        public void DeleteBulets(List<Bullet> bullets)
        {
            foreach (Bullet bullet in bullets)
            {
                BulletsList.Remove(bullet);
                numOfBullets--;
            }
        }

        public void Add(int x, int y, MoveType movement)
        {
            if (numOfBullets < maxNumOfBullets)
            {
                maxNumOfBullets++;

                BulletsList.Add(new Bullet(x, y, movement));
            }
        }
    }
}
