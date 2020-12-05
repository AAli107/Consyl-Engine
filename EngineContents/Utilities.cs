﻿using System;
using System.Numerics;

namespace Consyl_Engine.EngineContents
{
    class Utilities
    {
        public class Rand
        {
            static readonly Random rng = new Random(); // Variable that stores the Random Class

            public static int RangeInt(int min, int max) // Returns a random int in range (inclusive)
            {
                return rng.Next(min, max + 1);
            }

            public static float RangeFloat(float min, float max) // Returns a random float in range
            {
                return (float)rng.NextDouble() * (max - min) + min;
            }

            public static bool RandBool() // Returns a random bool, true or false
            {
                int n = rng.Next(0, 2);
                if (n == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool RandBoolByChance(float chance = 0.5f)
            {
                float randNum = RangeFloat(0.0f, 1.0f);
                return chance >= randNum;
            }

            public static int RandInt(int max) // Returns a random int between 0 to max
            {
                return rng.Next(max + 1);
            }
        } // class that stores functions that returns rng-related values

        public class Vec2D
        {
            public static float Distance2D(Vector2 point1, Vector2 point2) // Calculates the 2D distance between two points/vectors
            {
                return MathF.Sqrt(MathF.Pow(point2.X - point1.X, 2) + MathF.Pow(point2.Y - point1.Y, 2));
            }

            public static Vector2 Midpoint2D(Vector2 point1, Vector2 point2) // Returns the center between two vectors
            {
                return (point1 + point2) / 2;
            }
        } // class that stores functions that are related to Vector2
    }
}
