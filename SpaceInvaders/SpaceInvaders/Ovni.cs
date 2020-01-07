using System;

namespace SpaceInvaders
{
    /// <summary>
    /// Controls the Ovni object
    /// </summary>
    class Ovni : GameObject
    {
        /// <summary>
        /// Enum for the current state of the ovni
        /// </summary>
        enum State
        {
            WAITING,
            FLYING
        }

        // Constanst necessary for this script
        private const int OVNI_HEIGHT = 2;
        private const int INIT_X = 0;
        private const int INIT_Y = 6;
        private const int WATING_TIME = 1000;
        private const int MOVE_SPEED = 2;
        private const int RIGHT_BOUDARY = 90;

        // Timer to wait for spawn
        private Timer waitTimer;

        // Timer to wait to move
        private Timer moveTimer;

        // The current state of the Ovni
        private State currentState;

        /// <summary>
        /// The coordinates for the ovni
        /// </summary>
        private Vector2 coordinates;
        public Vector2 Coordinates => coordinates;

        // The sprite of the ovni
        private string[] sprite;

        /// <summary>
        /// Constructor for the ovni class
        /// </summary>
        public Ovni()
        {
            // Set the ovni starting coordinates
            coordinates = new Vector2(INIT_X, INIT_Y);

            // Initialize the wait timer
            waitTimer = new Timer(WATING_TIME);

            // Initialize the move timer
            moveTimer = new Timer(MOVE_SPEED);

            // Set the Ovni sprite
            sprite = new string[2]
            {
                "   ▄▄▄ |",
                " ▀▀▀▀▀▀▀"
            };
        }

        /// <summary>
        /// The Update method
        /// </summary>
        public override void Update()
        {
            // If the current state is wating
            if (currentState == State.WAITING)
            {
                // Call the wating method
                Wating();
            }
            // Else...
            else
            {
                // Call the flying method
                Flying();

                // Call the Write method
                Write();
            }
        }

        /// <summary>
        /// Writes the ovni to the buffer
        /// </summary>
        public void Write()
        {
            // If the current state is Flying
            if (currentState == State.FLYING)
            {
                // Loop the height of the ovni sprite
                for (int i = 0; i < OVNI_HEIGHT; i++)
                {
                    // Set the color
                    BufferEditor.WriteWithColor(0, coordinates.Y + i, " ", ConsoleColor.DarkMagenta);

                    // Write the ovni to the buffer
                    BufferEditor.Write(coordinates.X, coordinates.Y + i, sprite[i]);
                }
            }
        }

        /// <summary>
        /// Returns true of false if the ovni was hit by a bullet
        /// </summary>
        /// <param name="bulletCoordinate">The bullet's coordinate</param>
        /// <returns>True of false if the ovni was hit by a bullet</returns>
        public bool IsDestroyed(Vector2 bulletCoordinate) =>
            bulletCoordinate.X > coordinates.X &&
            bulletCoordinate.X < coordinates.X + 8 &&
            bulletCoordinate.Y == coordinates.Y + 1;

        /// <summary>
        /// Returns true or false if the ovni is or not flying
        /// </summary>
        /// <returns></returns>
        public bool IsFlying() => currentState == State.FLYING;

        /// <summary>
        /// Waits for a certain amount of time to spawn the ovni
        /// </summary>
        private void Wating()
        {
            // If the wait timer has stopped counting
            if (!waitTimer.IsCounting())
            {
                // Set the current state to flying
                currentState = State.FLYING;
            }
        }

        /// <summary>
        /// Move the Ovni forward
        /// </summary>
        private void Flying()
        {
            // If the move timer is not counting
            if (!moveTimer.IsCounting())
            {
                // Increase the Ovni's X
                coordinates.X++;

                // If the Ovni has reached the boundary
                if (coordinates.X > RIGHT_BOUDARY)
                {
                    // Delete it
                    Delete();

                    // Set the coordinates to 0
                    coordinates.X = 0;

                    // Set it's state to wating
                    currentState = State.WAITING;
                }
            }
        }

        /// <summary>
        /// Deletes the Ovni from the buffer
        /// </summary>
        private void Delete()
        {
            // Delete the ovni from the buffer
            BufferEditor.Delete(coordinates.X, coordinates.Y, "         ");
            BufferEditor.Delete(coordinates.X, coordinates.Y + 1, "         ");
        }
    }
}
