using System;
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
                if (rng.Next(0, 2) == 1)
                    return true;

                return false;
            }

            public static bool RandBoolByChance(float chance = 0.5f) // returns true randomly based on the decimal chance it would do it
            {
                return chance >= RangeFloat(0.0f, 1.0f);
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

            public static Vector2 Midpoint2D(Vector2 point1, Vector2 point2) // Returns the center between two 2D vectors
            {
                return (point1 + point2) / 2;
            }

            public static Vector2 RotateAroundPoint(Vector2 pointToRotate, Vector2 centerPoint, float degreesAngle) // Returns the position of a point after being rotated by a given degrees around a given point
            {   
                float radianAngle = Numbers.DegreeToRad(degreesAngle); // Converts Degrees angle to Radian angle
                float cosTheta = MathF.Cos(radianAngle);
                float sinTheta = MathF.Sin(radianAngle);

                return new Vector2((cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X), (sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y));
            }
        } // class that stores functions that are related to Vector2

        public class Vec3D
        {
            public static float Distance3D(Vector3 point1, Vector3 point2) // Calculates the 3D distance between two points/vectors
            {
                return MathF.Sqrt(MathF.Pow(point2.X - point1.X, 2) + MathF.Pow(point2.Y - point1.Y, 2) + MathF.Pow(point2.Z - point1.Z, 2));
            }

            public static Vector3 Midpoint3D(Vector3 point1, Vector3 point2) // Returns the center between two 3D vectors
            {
                return (point1 + point2) / 2;
            }

            public static Vector2 Vec3DToVec2D(Vector3 vec3D, float depth = 100) // Converts 3D Vectors into 2D vectors (Can be used to render simple 3D graphics)
            {
                return new Vector2((vec3D.X * (depth / vec3D.Z)) + (gfx.drawWidth / 2), (vec3D.Y * (depth / vec3D.Z)) + (gfx.drawHeight / 2));
            }
        } // class that stores functions that are related to Vector3

        public class Numbers
        {
            public static float Distance1D(float num1, float num2) // calculates the distance between two single numbers
            {
                return MathF.Sqrt(MathF.Pow(num1 - num2, 2));
            }

            public static float AverageNum(float[] floatArray) // Will return the average of numbers inside an array
            {
                float value = 0.0f;
                for (int i = 0; i < floatArray.Length; i++)
                    value += floatArray[i];

                return value / floatArray.Length;
            }

            public static float DegreeToRad(float degree) // Converts degrees to radians
            {
                return (degree * MathF.PI) / 180.0f;
            }

            public static float RadToDegree(float radian) // Converts radian to degrees
            {
                return (radian * 180.0f) / MathF.PI;
            }
        } // class that stores functions that are related to numbers
    }
}
