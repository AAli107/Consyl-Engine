using Consyl_Engine.EngineContents;
using System;
using System.Numerics;
using EZInput;

namespace Consyl_Engine
{
    class GameCode // Here is where you make most of your game's code!
    {
        // Insert Variables here! \\
        // \/\/\/\/\/\/\/\/\/\/\/\/ \\
        static bool rand = Utilities.Rand.RandBool();
        static bool canPress = true;
        // /\/\/\/\/\/\/\/\/\/\/\/\ \\

        public static void OnGameStart() // Gets Executed when game starts running/when the game begins
        {

        }

        public static void OnGameUpdate() // Gets Executed every frame as long as the game is running
        {
            if (Keyboard.IsKeyPressed(Key.Space))
            {
                if (canPress)
                {
                    rand = Utilities.Rand.RandBool();
                    canPress = false;
                }
            }
            else
            {
                canPress = true;
            }
        }

        public static void OnGraphicsUpdate() // Will be used to draw graphics related items per frame
        {
            if (rand)
            {
                gfx.GameUI.DrawText(10, 10, "true");
            }
            else
            {
                gfx.GameUI.DrawText(10, 10, "false");
            }
        }

        public static void OnGameEnd() // Gets Executed when Engine.gameRunning = false which is basically when the game ends
        {
            
        }
    }
}
