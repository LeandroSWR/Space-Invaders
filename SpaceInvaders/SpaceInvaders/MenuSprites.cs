using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class MenuSprites
    {
        public readonly string[] spaceString = new string[3] {
            "█▀▀ █▀█ █▀█ █▀▀ █▀▀",
            "▀▀█ █▀▀ █▀█ █   █▀▀",
            "▀▀▀ ▀   ▀ ▀ ▀▀▀ ▀▀▀"
        };

        public readonly string[] invadersString = new string[3] {
            "█ █▄ █ █▌ ▐█ █▀█ █▀▄ █▀▀ █▀█ █▀▀",
            "█ █ ▀█  ▌ ▐  █▀█ █ █ █▀▀ █▀▌ ▀▀█",
            "▀ ▀  ▀   ▀   ▀ ▀ ▀▀  ▀▀▀ ▀ ▀ ▀▀▀"
        };

        public readonly string[] playString = new string[3] {
            "█▀█ █   █▀█ █   █",
            "█▀▀ █   █▀█  ▀▄▀ ",
            "▀   ▀▀▀ ▀ ▀   ▀  "
        };

        public readonly string[] quitString = new string[3] {
            "█▀█ █ █ █ ▀█▀",
            "▀▀█ █ █ █  █ ",
            "  ▀ ▀▀▀ ▀  ▀ "
        };

        public readonly string[] selectionString = new string[3] {
            "   ▄   ",
            "  ▄█▄  ",
            " ▀█▀█▀ "
        };

        public readonly string[] enemy1String = new string[3] {
            "▀▄ ▄▀",
            "█▀█▀█",
            "▐▀▀▀▌"
        };

        public readonly string[] enemy2String = new string[3] {
            " ▐ ▌ ",
            "█▀█▀█",
            "▀▀▀▀▀"
        };

        public readonly string[] enemy3String = new string[3] {
            " ▄█▄ ",
            "█▄█▄█",
            "║╚ ╝║"
        };

        public readonly string[] enemy4String = new string[3] {
            " ▄█▄ ",
            "█▄█▄█",
            "╝║ ║╚"
        };

        public readonly string[] enemy5String = new string[3] {
            " ║ ║ ",
            "▄▀█▀▄",
            "▀▀█▀▀"
        };
    }
}
