using System;

namespace SpaceInvaders
{
    class Ship : GameObject
    {
        // Constants related to what's done in this script
        private const int INIT_NUM_BULLETS = 3;
        private const int MAX_NUM_BULLETS = 8;
        private const int LEFT_BOUNDARY = 0;
        private const int RIGHT_BOUNDARY = 93;
        private const int UPPER_BOUNDARY = 8;
        private const int BULLET_SPEED = 1;
        private const int MOVE_SPEED = 2;
        private const int INIT_X = 47;
        private const int INIT_Y = 55;


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
        public Bullets ShipBullets { get; private set; }

        // A key reader
        private KeyReader keyReader;

        // A timer for the movement of the ship
        private Timer moveTimer;

        // Check if a life was lost
        public bool LifeLost { get; set; }

        // Hold the total number of bullets
        private int numOfBullets;

        /// <summary>
        /// Ship Class constructor
        /// </summary>
        /// <param name="level">The current level</param>
        public Ship(int level)
        {
            // Set the starting number of bullets
            numOfBullets = INIT_NUM_BULLETS;

            // Increase the number of bullets every 4 levels up to a maximum of 8 bullets
            Timer bulletCounter = new Timer(4);

            // Loop the level amount of times
            for (int i = 0; i < level; i++)
            {
                // If the bullet counter has stopped and the num of bullets is less than the max
                if (!bulletCounter.IsCounting() && (numOfBullets < MAX_NUM_BULLETS))
                {
                    numOfBullets += 2;
                }
            }

            // Set the ship coordinates to the starting position
            coordinates = new Vector2(INIT_X, INIT_Y);

            // Instantiate a new Key Reader
            keyReader = new KeyReader();

            // Instatiate the ship bullets
            ShipBullets = new Bullets(UPPER_BOUNDARY, numOfBullets, BULLET_SPEED);

            // Instantiate a new Timer
            moveTimer = new Timer(MOVE_SPEED);

            // Set the ship sprite
            sprite = Sprites.selectionString;

            // Set the start move type
            currentMove = MoveType.NONE;
        }

        /// <summary>
        /// Initializes the ship values
        /// </summary>
        /// <param name="level">The current level</param>
        public void Init(int level)
        {
            // Set the ship coordinates
            coordinates = new Vector2(INIT_X, INIT_Y);

            // Set the type of movement the ship starts with
            currentMove = MoveType.NONE;

            // Set the starting number of bullets
            numOfBullets = INIT_NUM_BULLETS;

            // Increase the number of bulets every 4 levels up to a maximum of 8 bullets
            Timer bulletCounter = new Timer(4);

            // Loop the level amount of times
            for (int i = 0; i < level; i++)
            {
                // If the bullet counter has stopped and the num of bullets is less than the max
                if (!bulletCounter.IsCounting() && (numOfBullets < MAX_NUM_BULLETS))
                {
                    // Increase the number of bullets by 2
                    numOfBullets += 2;
                }
            }

            // Instatiate the ship bullets
            ShipBullets = new Bullets(UPPER_BOUNDARY, numOfBullets, BULLET_SPEED);
        }

        /// <summary>
        /// The Update method for the ship (Called once every frame)
        /// </summary>
        public override void Update()
        {
            if (!LifeLost)
            {
                // What to write to the buffer
                WriteToBuffer();

                // Check for user input
                GetInput();

                // Moves the ship
                Move();
            } else
            {
                Delete();
            }
            
            // Update the bullets
            ShipBullets.UpdateBullets();
        }

        /// <summary>
        /// Writes the ship sprite to the buffer
        /// </summary>
        private void WriteToBuffer()
        {
            // Go through the ship sprite array
            for (int i = 0; i < sprite.Length; i++)
            {
                // Write each string to the buffer
                BufferEditor.Write(coordinates.X, coordinates.Y + i, sprite[i]);

                // Set the color to white
                BufferEditor.WriteWithColor(0, coordinates.Y + i, " ", ConsoleColor.White);
            }
        }

        public void Delete()
        {
            // Go through the ship sprite array
            for (int i = 0; i < sprite.Length; i++)
            {
                // Write each string to the buffer
                BufferEditor.Delete(coordinates.X, coordinates.Y + i, "         ");

                BufferEditor.WriteWithColor(0, coordinates.Y + i, " ", ConsoleColor.Black);
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
                    ShipBullets.Add(coordinates.X + 3, coordinates.Y, MoveType.UP);
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
