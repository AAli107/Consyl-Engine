﻿using Consyl_Engine.EngineContents;
using System;
using System.Numerics;
using System.Threading;

namespace Consyl_Engine
{
    class Engine
    {
        // Essential Variables
        public static bool gameRunning = true; // Determines whether the game is running or not (Game will close when false)
        public static bool drawASCIIRender = true; // If True, the game draws in ASCII
        public static Vector2 resolution = new Vector2(115, 60); // Drawing Resolution in ASCII
        public static float framerate = 10000.0f; // ASCII Rendering max framerate
        public static string gameTitle = "Consyl Game"; // The title of the game

        // Variables you shouldn't touch
        public static char key; // variable that stores the keyboard inputs as char

        static void Main(string[] args)
        {
            Console.Title = gameTitle; // Set the Game's title
            
            GameCode.OnGameStart(); // Calls OnGameStart() from GameCode when the game runs
            
            // Calls OnGameUpdate() from GameCode constantly as long as the gameRunning is true
            while (gameRunning)
            {
                key = GameInput.Key(); // Detects Keyboard input and stores it in a variable

                GameCode.OnGameUpdate();
                
                if (drawASCIIRender) // Also it updates the ASCII graphics if drawASCIIRender is equal to true
                {
                    Console.CursorVisible = false; // Hides cursor, I place it on every frame because refreshing the screen makes it visible again
                    gfx.DrawASCII();
                }

                // Wait for amount of milliseconds and refresh the screen
                Thread.Sleep((int)((1.0f / framerate) * 1000.0f));
                gfx.ClearScreen();
            }

            // Automatically turns off the rest of the program and executes GameCode.OnGameEnd() function if gameRunning = false
            if(!gameRunning)
            {
                GameCode.OnGameEnd();

                Console.Clear();
                drawASCIIRender = false;
            }
        }
    }
}
