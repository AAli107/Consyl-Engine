﻿// Remember that Raycasts can only collide with GameObjects.
// You also need to use the CastLine() method to cast the ray.

using System;
using System.Numerics;

namespace Consyl_Engine.EngineContents
{
    class Raycast2D
    {
        public Vector2 start = new Vector2(0, 0); // Ray Start Location
        public Vector2 end = new Vector2(0, 0); // Ray End Location
        public Vector2 hitLoc = new Vector2(0, 0); // Ray impact location

        public float distance = 0.0f; // Distance between start and hitLoc

        public GameObject hitObject; // GameObject that got hit
        public GameObject[] ignoredObjects = new GameObject[0]; // GameObjects the Raycast should ignore

        public bool drawDebug; // If true, it will draw the Raycast on screen
        public bool hit = false; // Will become true only when the Raycast hits something

        public Raycast2D(Vector2 start, Vector2 end, GameObject[] ignoredObjects, bool drawDebug = false) // Raycast2D constructor
        {
            this.start = start;
            this.end = end;
            this.ignoredObjects = ignoredObjects;
            this.drawDebug = drawDebug;
        }
        public Raycast2D(Vector2 start, Vector2 end, bool drawDebug = false) // Another Raycast2D constructor if you don't want to add any ignored objects
        {
            this.start = start;
            this.end = end;
            this.drawDebug = drawDebug;
        }

        /// <summary>
        /// Cast a Line ray that will hit game objects
        /// </summary>
        /// <returns></returns>
        public bool CastLine()
        {
            float x0 = start.X;
            float y0 = start.Y;
            float x1 = end.X;
            float y1 = end.Y;
            
            float dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            float dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            float err = (dx > dy ? dx : -dy) / 2, e2;

            for (; ; )
            {
                if (drawDebug) gfx.DrawPixel((int)x0, (int)y0, '.');

                if (x0 == x1 && y0 == y1) break;

                e2 = err;
                if (e2 > -dx)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dy)
                {
                    err += dx;
                    y0 += sy;
                }

                if (Engine.gameObjects.Count > 0) // This is where the ray detects collision
                {
                    foreach (GameObject obj in Engine.gameObjects)
                    {
                        if (ignoredObjects.Length <= 0)
                        {
                            float objTopLoc = obj.location.Y + obj.collisionOffset.Y;
                            float objLeftLoc = obj.location.X + obj.collisionOffset.X;
                            float objBottomLoc = obj.location.Y + obj.collisionOffset.Y + obj.height;
                            float objRightLoc = obj.location.X + obj.collisionOffset.X + obj.width;

                            if (x0 > objLeftLoc && x0 < objRightLoc && y0 > objTopLoc && y0 < objBottomLoc)
                            {
                                hitLoc = new Vector2(x0, y0);
                                hit = true;
                                hitObject = obj;
                                goto LoopEnd;
                            }
                        }
                        else
                        {
                            foreach (GameObject ignoredObject in ignoredObjects)
                            {
                                float objTopLoc = obj.location.Y + obj.collisionOffset.Y;
                                float objLeftLoc = obj.location.X + obj.collisionOffset.X;
                                float objBottomLoc = obj.location.Y + obj.collisionOffset.Y + obj.height;
                                float objRightLoc = obj.location.X + obj.collisionOffset.X + obj.width;

                                if (x0 > objLeftLoc && x0 < objRightLoc && y0 > objTopLoc && y0 < objBottomLoc)
                                {
                                    if (obj != ignoredObject)
                                    {
                                        hitLoc = new Vector2(x0, y0);
                                        hit = true;
                                        hitObject = obj;
                                        goto LoopEnd;
                                    }
                                }
                            }
                        }
                    }
                }

                // If the 2D raycast didn't hit anything, hit will be false, hitLoc will be the end location, and hitObject will be null 
                hitLoc = end;
                hit = false;
                hitObject = null;
            }
            LoopEnd:;

            // Get the distance between start and hitLoc
            distance = Utilities.Vec2D.Distance2D(start, hitLoc);

            return hit;
        }
    }
}
