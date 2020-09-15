using System;
using System.Windows.Input;

namespace Consyl_Engine.EngineContents
{
    class GameInput
    {
        public static char Key() // returns the pressed key in char
        {
            ConsoleKeyInfo keyPress;
            while (Console.KeyAvailable)
            {
                keyPress = Console.ReadKey(true);
                return char.ToUpper(keyPress.KeyChar);
            }
            return default;
        }
    }
}
