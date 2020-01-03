using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Ship : IGameObject
    {
        // Constants related to what's done in this script
        private const int LEFT_BOUNDARY = 0;
        private const int RIGHT_BOUNDARY = 93;
        private const int MOVE_SPEED = 2;

        /// <summary>
        /// The coordinate for the ship
        /// </summary>
        private Vector2 coordinates;
        public Vector2 Coordinates => coordinates;

        // The sprite for the ship
        private string[] sprite;

        // The current type of movement the ship has
        private MoveType currentMove;

        // Instance of Bullets
        private Bullets shipBullets;

        // A key reader
        private KeyReader keyReader;

        // A timer for the movement of the ship
        private Timer moveTimer;

        /// <summary>
        /// Ship Class constructor
        /// </summary>
        public Ship()
        {
            // Set the ship coordinates to the starting position
            coordinates = new Vector2(47, 55);

            // Instantiate a new Key Reader
            keyReader = new KeyReader();

            // Instatiate the ship bullets
            shipBullets = new Bullets();

            // Instantiate a new Timer
            moveTimer = new Timer(MOVE_SPEED);

            // Set the ship sprite
            sprite = Sprites.selectionString;

            // Set the start move type
            currentMove = MoveType.NONE;
        }

        /// <summary>
        /// The Update method for the ship (Called once every frame)
        /// </summary>
        public void Update()
        {
            // What to write to the buffer
            WriteToBuffer();
            
            // Check for user input
            GetInput();

            // Moves the ship
            Move();

            // Update the bullets
            shipBullets.UpdateBullets();
        }

        /// <summary>
        /// Writes the ship sprite to the buffer
        /// </summary>
        private void WriteToBuffer()
        {
            // Set the color for the ship to be white
            BufferEditor.SetColor(ConsoleColor.White);

            // Go through the ship sprite array
            for (int i = 0; i < sprite.Length; i++)
            {
                // Write each string to the buffer
                BufferEditor.Write(coordinates.X, coordinates.Y + i, sprite[i]);
            }
        }

        /// <summary>
        /// Checks for the user input
        /// </summary>
        private void GetInput()
        {
            // Check what key the user pressed
            switch (keyReader.CurrentKey)
            {
                // If the user pressed the Left Arrow
                case ConsoleKey.LeftArrow:
                    // Change the ship movement to go Left
                    currentMove = MoveType.LEFT;
                    break;
                // If the user pressed the Right Arrow
                case ConsoleKey.RightArrow:
                    // Change the ship movement to go Right
                    currentMove = MoveType.RIGHT;
                    break;
                // If the user pressed the X key
                case ConsoleKey.X:
                    // Stop the ship movement
                    currentMove = MoveType.NONE;
                    break;
                // If the user pressed space bar
                case ConsoleKey.Spacebar:
                    shipBullets.Add(coordinates.X + 3, coordinates.Y - 1, MoveType.UP);
                    break;
            }
        }

        /// <summary>
        /// Moves the ship based on it's current direction
        /// </summary>
        private void Move()
        {
            // If the move timer has stopped counting
            if (!moveTimer.IsCounting())
            {
                // The ship can move
                // If the ship is moving left and hasn't reached the boundary
                if (currentMove == MoveType.LEFT && coordinates.X > LEFT_BOUNDARY)
                {
                    // Move the ship left
                    coordinates.X--;
                }
                // If the ship is moving right and hasn't reached the boundary
                else if (currentMove == MoveType.RIGHT && coordinates.X < RIGHT_BOUNDARY)
                {
                    // Move the ship right
                    coordinates.X++;
                }
            }
        }
    }
}
