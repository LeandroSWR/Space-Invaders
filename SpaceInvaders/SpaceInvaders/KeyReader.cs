using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SpaceInvaders
{
    class KeyReader
    {
        // BlockingCollection to hold all the key presses
        private BlockingCollection<ConsoleKey> collection;

        // Holds the current key press
        private ConsoleKey currentKey;
        public ConsoleKey CurrentKey {
            get {
                // Tries to get a key from the BlokingCollection
                collection.TryTake(out currentKey);
                // Returns said key
                return currentKey;
            }
        }

        /// <summary>
        /// Constructor for the class KeyReader
        /// </summary>
        public KeyReader()
        {
            // Instantiate a new collection of `ConsoleKey`
            collection = new BlockingCollection<ConsoleKey>();

            // Instantiate a new thread for the keys to be read on
            Thread thread = new Thread(ReadKeys);

            // Start the thread
            thread.Start();
        }

        /// <summary>
        /// Reads the keys input from the user
        /// </summary>
        public void ReadKeys()
        {
            // Hold the last pressed key
            ConsoleKey key;

            // Loops...
            do
            {
                // Set the key to be equal to the user input
                key = Console.ReadKey(true).Key;

                // Add it to the collection
                collection.Add(key);

                // While the pressed key is not Escape
            } while (key != ConsoleKey.Escape);

        }
    }
}
