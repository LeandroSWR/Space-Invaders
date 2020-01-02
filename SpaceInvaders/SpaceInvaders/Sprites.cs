using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Sprites
    {
        public static readonly string[] spaceString = new string[3] {
            "█▀▀ █▀█ █▀█ █▀▀ █▀▀",
            "▀▀█ █▀▀ █▀█ █   █▀▀",
            "▀▀▀ ▀   ▀ ▀ ▀▀▀ ▀▀▀"
        };

        public static readonly string[] invadersString = new string[3] {
            "█ █▄ █ █▌ ▐█ █▀█ █▀▄ █▀▀ █▀█ █▀▀",
            "█ █ ▀█  ▌ ▐  █▀█ █ █ █▀▀ █▀▌ ▀▀█",
            "▀ ▀  ▀   ▀   ▀ ▀ ▀▀  ▀▀▀ ▀ ▀ ▀▀▀"
        };

        public static readonly string[] playString = new string[3] {
            "█▀█ █   █▀█ █   █",
            "█▀▀ █   █▀█  ▀▄▀ ",
            "▀   ▀▀▀ ▀ ▀   ▀  "
        };

        public static readonly string[] quitString = new string[3] {
            "█▀█ █ █ █ ▀█▀",
            "▀▀█ █ █ █  █ ",
            "  ▀ ▀▀▀ ▀  ▀ "
        };

        public static readonly string[] selectionString = new string[3] {
            "   ▄   ",
            "  ▄█▄  ",
            " ▀█▀█▀ "
        };

        public static readonly string[] enemy1String = new string[3] {
            "▀▄ ▄▀",
            "█▀█▀█",
            "▐▀▀▀▌"
        };

        public static readonly string[] enemy2String = new string[3] {
            " ▐ ▌ ",
            "█▀█▀█",
            "▀▀▀▀▀"
        };

        public static readonly string[] enemy3String = new string[3] {
            " ▄█▄ ",
            "█▄█▄█",
            "║╚ ╝║"
        };

        public static readonly string[] enemy4String = new string[3] {
            " ▄█▄ ",
            "█▄█▄█",
            "╝║ ║╚"
        };

        public static readonly string[] enemy5String = new string[3] {
            " ║ ║ ",
            "▄▀█▀▄",
            "▀▀█▀▀"
        };
    }
}
