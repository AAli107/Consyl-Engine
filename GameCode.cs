using Consyl_Engine.EngineContents;
using System;
using System.Collections.Generic;
using System.Numerics;
using EZInput;

namespace Consyl_Engine
{
    class GameCode // Here is where you make most of your game's code!
    {
        // Insert static Variables here \\
        // \/\/\/\/\/\/\/\/\/\/\/\/\/\/\/ \\
        static Save save;
        public static object[] objs = { "Hello World!", 'G', 12, 23.5f, 22.66, new Vector2(120, 213) };
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ \\

        public static void OnGameStart() // Gets Executed when game starts running/when the game begins
        {
            save = new Save("file.txt");

            save.SaveToFile(objs, true);
        }

        public static void OnGameUpdate() // Gets Executed every frame as long as the game is running
        {
            object[] objr = save.ReadFileContents();
            for (int i = 0; i < objr.Length; i++)
            {
                gfx.GameUI.DrawText(0, i, objr[i].ToString());
            }

            float vec =  float.Parse(objr[3].ToString());

            gfx.GameUI.DrawText(10, 30, vec.ToString());
        }

        public static void OnGraphicsUpdate() // Will be used to draw graphics related items per frame
        {
            
        }

        public static void OnGameEnd() // Gets Executed when Engine.gameRunning = false which is basically when the game ends
        {
            
        }
    }
}
