using System;
using System.Numerics;

namespace Consyl_Engine.EngineContents
{
    public static class Utilities
    {
        /// <summary>
        /// class that stores functions that returns rng-related values
        /// </summary>
        public static class Rand
        {
            static readonly Random rng = new Random(); // Variable that stores the Random Class

            /// <summary>
            /// Returns a random int in range (inclusive)
            /// </summary>
            /// <param name="min"></param>
            /// <param name="max"></param>
            /// <returns></returns>
            public static int RangeInt(int min, int max)
            {
                return rng.Next(min, max + 1);
            }

            /// <summary>
            /// Returns a random float in range
            /// </summary>
            /// <param name="min"></param>
            /// <param name="max"></param>
            /// <returns></returns>
            public static float RangeFloat(float min, float max)
            {
                return (float)rng.NextDouble() * (max - min) + min;
            }

            /// <summary>
            /// Returns a random bool, true or false
            /// </summary>
            /// <returns></returns>
            public static bool RandBool()
            {
                return rng.Next(0, 2) == 1;
            }

            /// <summary>
            /// returns true randomly based on the decimal chance it would do it
            /// </summary>
            /// <param name="chance"></param>
            /// <returns></returns>
            public static bool RandBoolByChance(float chance = 0.5f)
            {
                return chance >= RangeFloat(0.0f, 1.0f);
            }

            /// <summary>
            /// Returns a random int between 0 to max
            /// </summary>
            /// <param name="max"></param>
            /// <returns></returns>
            public static int RandInt(int max)
            {
                return rng.Next(max + 1);
            }
        }

        /// <summary>
        /// class that stores functions that are related to Vector2
        /// </summary>
        public static class Vec2D
        {
            /// <summary>
            /// Calculates the 2D distance between two points/vectors
            /// </summary>
            /// <param name="point1"></param>
            /// <param name="point2"></param>
            /// <returns></returns>
            public static float Distance2D(Vector2 point1, Vector2 point2)
            {
                return MathF.Sqrt(MathF.Pow(point2.X - point1.X, 2) + MathF.Pow(point2.Y - point1.Y, 2));
            }

            /// <summary>
            /// Returns the center between two 2D vectors
            /// </summary>
            /// <param name="point1"></param>
            /// <param name="point2"></param>
            /// <returns></returns>
            public static Vector2 Midpoint2D(Vector2 point1, Vector2 point2)
            {
                return (point1 + point2) / 2;
            }

            /// <summary>
            /// Returns the position of a point after being rotated by a given degrees around a given point
            /// </summary>
            /// <param name="pointToRotate"></param>
            /// <param name="centerPoint"></param>
            /// <param name="degreesAngle"></param>
            /// <returns></returns>
            public static Vector2 RotateAroundPoint(Vector2 pointToRotate, Vector2 centerPoint, float degreesAngle)
            {   
                float radianAngle = Numbers.DegreeToRad(degreesAngle); // Converts Degrees angle to Radian angle
                float cosTheta = MathF.Cos(radianAngle);
                float sinTheta = MathF.Sin(radianAngle);

                return new Vector2((cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X), (sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y));
            }
        }

        /// <summary>
        /// class that stores functions that are related to Vector3
        /// </summary>
        public static class Vec3D
        {
            /// <summary>
            /// Calculates the 3D distance between two points/vectors
            /// </summary>
            /// <param name="point1"></param>
            /// <param name="point2"></param>
            /// <returns></returns>
            public static float Distance3D(Vector3 point1, Vector3 point2)
            {
                return MathF.Sqrt(MathF.Pow(point2.X - point1.X, 2) + MathF.Pow(point2.Y - point1.Y, 2) + MathF.Pow(point2.Z - point1.Z, 2));
            }

            /// <summary>
            /// Returns the center between two 3D vectors
            /// </summary>
            /// <param name="point1"></param>
            /// <param name="point2"></param>
            /// <returns></returns>
            public static Vector3 Midpoint3D(Vector3 point1, Vector3 point2)
            {
                return (point1 + point2) / 2;
            }

            /// <summary>
            /// Converts 3D Vectors into 2D vectors (Can be used to render simple 3D graphics)
            /// </summary>
            /// <param name="vec3D"></param>
            /// <param name="depth"></param>
            /// <returns></returns>
            public static Vector2 Vec3DToVec2D(Vector3 vec3D, float depth = 100)
            {
                return new Vector2((vec3D.X * (depth / vec3D.Z)) + (gfx.drawWidth / 2), (vec3D.Y * (depth / vec3D.Z)) + (gfx.drawHeight / 2));
            }
        }

        /// <summary>
        /// class that stores functions that are related to numbers
        /// </summary>
        public static class Numbers
        {
            /// <summary>
            /// calculates the distance between two single numbers
            /// </summary>
            /// <param name="num1"></param>
            /// <param name="num2"></param>
            /// <returns></returns>
            public static float Distance1D(float num1, float num2)
            {
                return MathF.Sqrt(MathF.Pow(num1 - num2, 2));
            }

            /// <summary>
            /// Will return the average of numbers inside an array
            /// </summary>
            /// <param name="floatArray"></param>
            /// <returns></returns>
            public static float AverageNum(float[] floatArray)
            {
                float value = 0.0f;
                for (int i = 0; i < floatArray.Length; i++)
                    value += floatArray[i];

                return value / floatArray.Length;
            }

            /// <summary>
            /// Converts degrees to radians
            /// </summary>
            /// <param name="degree"></param>
            /// <returns></returns>
            public static float DegreeToRad(float degree)
            {
                return (degree * MathF.PI) / 180.0f;
            }

            /// <summary>
            /// Converts radian to degrees
            /// </summary>
            /// <param name="radian"></param>
            /// <returns></returns>
            public static float RadToDegree(float radian)
            {
                return (radian * 180.0f) / MathF.PI;
            }

            /// <summary>
            /// Fixes the value number between the minimum and maximum
            /// </summary>
            /// <param name="val"></param>
            /// <param name="min"></param>
            /// <param name="max"></param>
            /// <returns></returns>
            public static float ClampN(float val, float min, float max)
            {
                return val > max ? max : val < min ? min : val;
            }

            /// <summary>
            /// Returns the largest number in an array of floats
            /// </summary>
            /// <param name="floatArray"></param>
            /// <returns></returns>
            public static float MaxVal(float[] floatArray)
            {
                float max = 0;
                for (int i = 0; i < floatArray.Length; i++)
                    if (floatArray[i] > max)
                    {
                        max = floatArray[i];
                    }

                return max;
            }

            /// <summary>
            /// Returns the smallest number in an array of floats
            /// </summary>
            /// <param name="floatArray"></param>
            /// <returns></returns>
            public static float MinVal(float[] floatArray)
            {
                float min = floatArray[0];
                for (int i = 0; i < floatArray.Length; i++)
                    if (floatArray[i] < min)
                    {
                        min = floatArray[i];
                    }

                return min;
            }
        }

        /// <summary>
        /// class that stores miscellaneous enumerations for general use
        /// </summary>
        public static class Enums
        {
            /// <summary>
            /// Enumeration for most common damage types
            /// </summary>
            public enum DamageType // You could add or remove damage types if necessary
            {
                None,
                Generic,
                Piercing,
                Falling,
                Burning,
                Magic,
                Drowning,
                Suffocation,
                Freezing,
                Bleeding,
                Explosion,
                Projectile,
                Lightning,
                Poisoning,
                Arrow,
                Prickling,
                Stabbing,
                Kinetic,
                Shooting,
                Curse,
                Acid,
                Rotting
            }

            /// <summary>
            /// Enumeration for the directions in 2D space
            /// </summary>
            public enum CardinalDirection
            {
                North,
                NorthEast,
                East,
                SouthEast,
                South,
                SouthWest,
                West,
                NorthWest
            }
        }
    }
}
