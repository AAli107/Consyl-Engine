using Consyl_Engine.EngineContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Diagnostics;

namespace Consyl_Engine
{
    class Engine
    {
        #region Variables
        // Essential Engine Variables
        public static string gameTitle = "Consyl Game"; // The title of the game
        public static bool gameRunning = true; // Determines whether the game is running or not (Game will close when false)
        public static bool gamePaused = false; // The GameCode.OnGameUpdate() function and other updates will not execute only if it's false

        /// <summary>
        /// The amount in seconds it took to render the last frame (read only)
        /// </summary>
        public static float deltaTime
        {get { return deltaT; }}

        /// <summary>
        /// The frames per second the game is running at
        /// </summary>
        public static float currentFPS
        {get { return 1 / deltaT; }}

        // ASCII Graphics-related variables
        public static bool drawASCIIRender = true; // If True, the game draws in ASCII
        public static Vector2 resolution = new Vector2(115, 60); // Drawing Resolution in ASCII
        public static float maxFramerate = 60.0f; // ASCII Rendering max framerate
        public static Vector2 worldSize = new Vector2(115, 60); // Size of the 2D world
        public static Camera mainCamera = new Camera(new Vector2(0,0)); // The main camera the player will see through

        // Variables that controls the Initial Colors of the Background and text
        private static readonly ConsoleColor BgColor = ConsoleColor.Black; // Initial Background Color
        private static readonly ConsoleColor FgColor = ConsoleColor.White; // Initial Text Color

        // Variables that you shouldn't modify or change directly
        public static List<GameObject> gameObjects = new List<GameObject>();
        #endregion

        #region ReadOnlyVariables
        private static float deltaT; // The amount in seconds it takes to render a frame
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

            Stopwatch time = new Stopwatch();

            // Calls OnGameUpdate() from GameCode constantly as long as the gameRunning is true
            while (gameRunning)
            {
                time.Start(); // starts a timer to measure the delta time of the current frame

                if (!gamePaused)
                {
                    foreach (var gameObject in gameObjects) // Updates all the existing Game Objects
                        if (gameObject != null)
                            gameObject.Update();

                    GameCode.OnGameUpdate(); // Updates the game
                    GameObjectCollisionUpdate(); // Does collision updates
                }
                if (drawASCIIRender) // Also it updates the ASCII graphics if drawASCIIRender is equal to true
                {
                    if (mainCamera == null) // If the mainCamera has been set to null, then it recreates it again before drawing
                        mainCamera = new Camera(new Vector2(0, 0));
                    
                    Console.CursorVisible = false; // Hides cursor, I place it on every frame because refreshing the screen makes it visible again

                    foreach (var gameObject in gameObjects) // Renders the graphics of all the existing Game Objects
                        if (gameObject != null)
                            gameObject.DrawUpdate();

                    GameCode.OnGraphicsUpdate();
                    gfx.DrawASCII();
                }

                // Wait for amount of milliseconds and refresh the screen
                Thread.Sleep((int)((1 / maxFramerate) * 1000));
                gfx.ClearScreen();

                // Stops timer and sets deltaT to the time it took to render a frame
                time.Stop();
                deltaT = (float)time.Elapsed.TotalSeconds;
                time.Reset();
            }

            // Automatically turns off the rest of the program and executes GameCode.OnGameEnd() function if gameRunning = false
            GameCode.OnGameEnd();
            Console.Clear();
            drawASCIIRender = false;
        }
        #endregion

        #region GameObjectCollisionUpdate
        /// <summary>
        /// This method is called right after the OnGameUpdate code is executed
        /// </summary>
        static void GameObjectCollisionUpdate()
        {
            foreach (GameObject obj in gameObjects)
            {
                foreach (GameObject obj2 in gameObjects)
                {
                    if (gameObjects.Count > 1) // Will test for collision  only if the amount of GameObjects are 2 or greater
                    {
                        if (obj != obj2) // Checks if the GameObject is not itself, so that it won't test collision with itself
                        {
                            if (obj.collisionEnabled && obj2.collisionEnabled) // Checks if the collision is enabled in both GameObjects
                            {
                                // Stores the collision box sides of obj in variables
                                int objTopLoc = (int)(obj.location.Y + obj.collisionOffset.Y);
                                int objLeftLoc = (int)(obj.location.X + obj.collisionOffset.X);
                                int objBottomLoc = (int)(obj.location.Y + obj.collisionOffset.Y + obj.height);
                                int objRightLoc = (int)(obj.location.X + obj.collisionOffset.X + obj.width);

                                // Stores the collision box sides of obj2 in variables
                                int obj2TopLoc = (int)(obj2.location.Y + obj2.collisionOffset.Y);
                                int obj2LeftLoc = (int)(obj2.location.X + obj2.collisionOffset.X);
                                int obj2BottomLoc = (int)(obj2.location.Y + obj2.collisionOffset.Y + obj2.height);
                                int obj2RightLoc = (int)(obj2.location.X + obj2.collisionOffset.X + obj2.width);

                                // Finds out the center point of obj's hitbox
                                Vector2 objCenter = Utilities.Vec2D.Midpoint2D(obj.location + obj.collisionOffset, obj.location + obj.collisionOffset + new Vector2(obj.width, obj.height));

                                // Finds out the center point of obj2's hitbox
                                Vector2 obj2Center = Utilities.Vec2D.Midpoint2D(obj2.location + obj2.collisionOffset, obj2.location + obj2.collisionOffset + new Vector2(obj2.width, obj2.height));

                                // Calculates the normal between the two GameObjects
                                Vector2 normal = Vector2.Normalize(obj2Center - objCenter);

                                // Rounds the normal so that collision works properly
                                normal.X = MathF.Round(normal.X);
                                normal.Y = MathF.Round(normal.Y);

                                // Collision test
                                if (objRightLoc > obj2LeftLoc && objLeftLoc < obj2RightLoc && objTopLoc < obj2BottomLoc && obj2TopLoc < objBottomLoc)
                                {
                                    if (obj.isPushable) // Pushes GameObject if isPushable is true
                                        obj.location -= normal;
                                    else if (obj.detectOverlap) // Will detect overlap if detectOverlap is true
                                        obj.isOverlapping = true;

                                    if (obj2.isPushable) // Pushes GameObject if isPushable is true
                                    {
                                        obj2.location += normal;
                                        obj2.speed = obj.speed / 2;
                                    }
                                    else if (obj2.detectOverlap) // Will detect overlap if detectOverlap is true
                                        obj2.isOverlapping = true;
                                }
                                else // If not colliding, the GameObjects' isOverlapping variable will be false
                                {
                                    obj.isOverlapping = false;
                                    obj2.isOverlapping = false;
                                }
                            }
                        }
                    }
                }
                if (obj != null)
                {
                    if (obj.collideWithBounds) // Checks if the object is colliding with edge of the world and prevent the object from leaving the world bounds
                    {
                        if (obj.location.X + obj.collisionOffset.X + obj.width >= worldSize.X)
                        {
                            obj.location.X = worldSize.X - (obj.collisionOffset.X + obj.width);
                            if (obj.speed.X > 0.0f)
                                obj.speed.X = 0.0f;
                        }
                        else if (obj.location.X + obj.collisionOffset.X < 0.0f)
                        {
                            obj.location.X = -obj.collisionOffset.X;
                            if (obj.speed.X < 0.0f)
                                obj.speed.X = 0.0f;
                        }

                        if (obj.location.Y + obj.collisionOffset.Y + obj.height >= worldSize.Y)
                        {
                            obj.location.Y = worldSize.Y - (obj.collisionOffset.Y + obj.height);
                            if (obj.speed.Y > 0.0f)
                                obj.speed.Y = 0.0f;
                        }
                        else if (obj.location.Y + obj.collisionOffset.Y < 0.0f)
                        {
                            obj.location.Y = -obj.collisionOffset.Y;
                            if (obj.speed.Y < 0.0f)
                                obj.speed.Y = 0.0f;
                        }
                    }
                }
            }
        }
        #endregion

        #region EngineMethods
        /// <summary>
        /// A method to create a GameObject
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_CollisionEnabled"></param>
        /// <param name="_collisionWidth"></param>
        /// <param name="_collisionHeight"></param>
        /// <param name="_detectOverlap"></param>
        /// <param name="_image"></param>
        /// <param name="_isPushable"></param>
        /// <param name="_colOffsetX"></param>
        /// <param name="_colOffsetY"></param>
        /// <param name="_collideWithBounds"></param>
        /// <param name="_drawDebugCollision"></param>
        /// <returns></returns>
        static public int CreateGameObject(int _x, int _y, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap, Texture _image, bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false)
        {
            int newObjID = -1;

            bool set = false;
            bool stillEqual = false;

            while (!set)
            {
                int num = Utilities.Rand.RandInt(2000000000);
                if (gameObjects.Count > 0)
                {
                    for (int i = 0; i < gameObjects.Count; i++)
                        if (gameObjects[i].objID != num)
                            set = true;
                }
                else
                    set = true;

                newObjID = num;
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].objID == newObjID)
                {
                    stillEqual = true;
                    break;
                }
                else
                    stillEqual = false;
            }
            if (!stillEqual)
            {
                GameObject gameObject = new GameObject(_x, _y, _CollisionEnabled, _collisionWidth, _collisionHeight, _detectOverlap, _image, _isPushable, _colOffsetX, _colOffsetY, _collideWithBounds, _drawDebugCollision, newObjID);
                gameObjects.Add(gameObject);

                return newObjID;
            }
            return -1;
        }

        /// <summary>
        /// A method to create a GameObject without applying the textures
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_CollisionEnabled"></param>
        /// <param name="_collisionWidth"></param>
        /// <param name="_collisionHeight"></param>
        /// <param name="_detectOverlap"></param>
        /// <param name="_isPushable"></param>
        /// <param name="_colOffsetX"></param>
        /// <param name="_colOffsetY"></param>
        /// <param name="_collideWithBounds"></param>
        /// <param name="_drawDebugCollision"></param>
        /// <returns></returns>
        static public int CreateGameObjectNoTex(int _x, int _y, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap, bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false)
        {
            int newObjID = -1;

            bool set = false;
            bool stillEqual = false;

            while (!set)
            {
                int num = Utilities.Rand.RandInt(2000000000);
                if (gameObjects.Count > 0)
                {
                    for (int i = 0; i < gameObjects.Count; i++)
                        if (gameObjects[i].objID != num)
                            set = true;
                }
                else
                    set = true;

                newObjID = num;
            }

            for (int i = 0; i < gameObjects.Count; i++)
                if (gameObjects[i].objID == newObjID)
                {
                    stillEqual = true;
                    break;
                }
                else
                    stillEqual = false;

            if (!stillEqual)
            {
                GameObject gameObject = new GameObject(_x, _y, _CollisionEnabled, _collisionWidth, _collisionHeight, _detectOverlap, _isPushable, _colOffsetX, _colOffsetY, _collideWithBounds, _drawDebugCollision, newObjID);
                gameObjects.Add(gameObject);

                return newObjID;
            }
            return -1;
        }

        /// <summary>
        /// A method that returns the GameObject when you input it's ID
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        static public GameObject GetGameObjectByID(int objectID)
        {
            return gameObjects.FirstOrDefault(obj => obj.objID == objectID);
        }

        /// <summary>
        /// A method that deletes a spawned GameObject
        /// </summary>
        /// <param name="objectID"></param>
        static public void DestroyGameObject(int objectID)
        {
            if (objectID >= 0)
                gameObjects.Remove(GetGameObjectByID(objectID));
        }

        /// <summary>
        /// A method that checks whether a GameObject with specified ID exists.
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        static public bool DoesGameObjectExist(int objectID)
        {
            for (int i = 0; i < gameObjects.Count; i++)
                if (gameObjects[i].objID == objectID)
                    return true;

            return false;
        }

        /// <summary>
        /// Allows you to get the background color
        /// </summary>
        /// <returns></returns>
        static public ConsoleColor GetBgColor()
        {
            return BgColor;
        }

        /// <summary>
        /// Allows you to get the foreground color
        /// </summary>
        /// <returns></returns>
        static public ConsoleColor GetFgColor()
        {
            return FgColor;
        }

        /// <summary>
        /// Returns whether two GameObjects are in the same location
        /// </summary>
        /// <param name="gameObjID1"></param>
        /// <param name="gameObjID2"></param>
        /// <returns></returns>
        static public bool GameObjectEqualsLocation(int gameObjID1, int gameObjID2)
        {
            return GetGameObjectByID(gameObjID1).location == GetGameObjectByID(gameObjID2).location;
        }
        #endregion
    }
}