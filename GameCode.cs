using Consyl_Engine.EngineContents;
using System;
using System.Collections.Generic;
using EZInput;

namespace Consyl_Engine
{
    class GameCode // Here is where you make most of your game's code!
    {
         // Insert Variables here! \\
        // \/\/\/\/\/\/\/\/\/\/\/\/ \\
        static Audio music = new Audio("Bruh Sound Effect #2.mp3");
        static bool allowMusic = true;
        // /\/\/\/\/\/\/\/\/\/\/\/\ \\

        public static void OnGameStart() // Gets Executed when game starts running/when the game begins
        {

        }

        public static void OnGameUpdate() // Gets Executed every frame as long as the game is running
        {
            if (Keyboard.IsKeyPressed(Key.S) & allowMusic)
            {
                music.PlaySound();
                allowMusic = false;
            }
        }

        public static void OnGameEnd() // Gets Executed when Engine.gameRunning = false which is basically when the game ends
        {
            
        }
    }
}
