using Consyl_Engine.EngineContents;
using System;
using System.Numerics;
using System.Threading;

namespace Consyl_Engine
{
    class Engine
    {
        #region Variables
        // Essential Engine Variables
        public static string gameTitle = "Consyl Game"; // The title of the game
        public static bool gameRunning = true; // Determines whether the game is running or not (Game will close when false)
        public static bool gamePaused = false; // The GameCode.OnGameUpdate() function will not execute only if it's false

        // ASCII Graphics-related variables
        public static bool drawASCIIRender = true; // If True, the game draws in ASCII
        public static Vector2 resolution = new Vector2(115, 60); // Drawing Resolution in ASCII
        public static float framerate = 60.0f; // ASCII Rendering max framerate

        // Variables that controls the Initial Colors of the Background and text
        static readonly ConsoleColor BgColor = ConsoleColor.Black; // Initial Background Color
        static readonly ConsoleColor FgColor = ConsoleColor.White; // Initial Text Color
        #endregion

        #region EngineCode
        static void Main(string[] args)
        {
            Console.Title = gameTitle; // Set the Game's title

            // Sets the Colors of the background and refreshes the screen
            Console.BackgroundColor = BgColor;
            Console.ForegroundColor = FgColor;
            Console.Clear();

            GameCode.OnGameStart(); // Calls OnGameStart() from GameCode when the game runs

            // Calls OnGameUpdate() from GameCode constantly as long as the gameRunning is true
            while (gameRunning)
            {
                if (!gamePaused)
                {
                    GameCode.OnGameUpdate(); // Updates the game
                }
                if (drawASCIIRender) // Also it updates the ASCII graphics if drawASCIIRender is equal to true
                {
                    Console.CursorVisible = false; // Hides cursor, I place it on every frame because refreshing the screen makes it visible again
                    GameCode.OnGraphicsUpdate();
                    gfx.DrawASCII();
                }

                // Wait for amount of milliseconds and refresh the screen
                Thread.Sleep((int)((1.0f / framerate) * 1000.0f));
                gfx.ClearScreen();
            }

            // Automatically turns off the rest of the program and executes GameCode.OnGameEnd() function if gameRunning = false
            GameCode.OnGameEnd();
            Console.Clear();
            drawASCIIRender = false;
        }
        #endregion
    }
}