using System;

namespace Consyl_Engine.EngineContents
{
    class GameInput
    {
        public static char Key() // returns the pressed key in char
        {
            ConsoleKeyInfo keyPress;
            if (Console.KeyAvailable) // will read the key only if it is pressed (There will be input lag if the frame rate is low)
            {
                keyPress = Console.ReadKey(true);
                return char.ToUpper(keyPress.KeyChar);
            }
            return default;
        }
    }
}
