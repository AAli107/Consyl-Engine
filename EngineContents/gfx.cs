﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace Consyl_Engine.EngineContents
{
    class gfx
    {
        // Resolution width and height of the ASCII screen
        public static int drawWidth = (int)Engine.resolution.X;
        public static int drawHeight = (int)Engine.resolution.Y;

        // An Array that stores ASCII chars that can be used for shading
        public static readonly char[] shadeCharArray = { ' ','.',',','-','^','*',':',';','I','l','!','i','>','<','~','+','_','?',']','[','}','{',')','(','|','/','t','f',
            'j','r','x','n','u','v','c','z','X','Y','U','J','C','L','Q','O','Z','m','w','q','p','d','b','k','h','a','o','#','M','W','&','%','B','@','$' };

        static char[] textImage = new char[drawWidth * drawHeight]; // Stores the Data of the ASCII pixels

        public static void DrawASCII() // Renders the Image on Screen
        {
            // Draws ASCII Render on screen
            string renderedImage = "";
            for (int y = 0; y < drawHeight; y++)
            {
                // Converts 1D array to 2D array
                List<char> xArray = new List<char>(drawWidth);
                for (int x = 0; x < drawWidth; x++)
                {
                    xArray.Add(textImage[drawWidth * y + x]);
                }

                string renderLine = "";
                for (int i = 0; i < xArray.Count; i++) // stores a line of the screen
                {
                    renderLine = renderLine + xArray[i] + " ";
                }
                renderedImage = renderedImage + renderLine + "\n";
            }
            Console.WriteLine(renderedImage);
        }

        public static void ClearScreen()
        {
            // Resets the Cursor position to redraw the next frame
            Console.CursorLeft = 0;
            Console.CursorTop = 0;

            // Clears the Image data from textImage
            for (int i = 0; i < textImage.Length; i++)
            {
                textImage[i] = shadeCharArray[0];
            }
        }

        public static char ReadPixelAt(int x, int y) // Give a pixel coordinate and it will return the pixel's char it has on screen
        {
            return textImage[drawWidth * y + x];
        }

        public static void DrawPixel(int x, int y, char pixelLook) // Draws ASCII pixel on screen
        {
            if (x >= 0 && x < drawWidth && y >= 0 && y < drawHeight)
            {
                textImage[drawWidth * y + x] = pixelLook;
            }
        }

        public static void DrawRectangle(int x, int y, int width, int height, char pixelLook) // Draws an ASCII rectangle on screen
        {
            for (int y0 = y; y0 < height + y; y0++)
            {
                for (int x0 = x; x0 < width + x; x0++)
                {
                    DrawPixel(x0, y0, pixelLook);
                }
            }
        }

        public static void DrawLine(int x0, int y0, int x1, int y1, char pixelLook) // Draw lines between two points
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;

            for (; ; )
            {
                DrawPixel(x0, y0, pixelLook);

                if (x0 == x1 && y0 == y1)
                    break;

                e2 = err;

                if (e2 > -dx)
                {
                    err -= dy; x0 += sx;
                }

                if (e2 < dy)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        public static void DrawFilledCircle(int centerX, int centerY, float radius, char pixelLook) // Draws a Filled Circle
        {
            for (double i = 0.0; i < 360; i += 0.1)
            {
                for (int r = 0; r < radius + 1; r++)
                {
                    double angle = i * Math.PI / 180;
                    int x = (int)(r * Math.Cos(angle));
                    int y = (int)(r * Math.Sin(angle));

                    DrawPixel(x + centerX, y + centerY, pixelLook);
                }
            }
        }

        public static void DrawCircle(int centerX, int centerY, float radius, char pixelLook) // Draws a Circle
        {
            for (double i = 0.0; i < 360; i += 0.1)
            {
                double angle = i * Math.PI / 180;
                int x = (int)(radius * Math.Cos(angle));
                int y = (int)(radius * Math.Sin(angle));

                DrawPixel(x + centerX, y + centerY, pixelLook);
            }
        }

        public static void DrawTri(int x, int y, int size, char pixelLook) // Draws a right angle triangle
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    DrawPixel(x + j, y + i - 1, pixelLook);
                }
            }
        }

        public static void DrawPolygon(Vector2 p1, Vector2 p2, Vector2 p3, char pixelLook) // Draws a polygon outline on screen
        {
            DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y, pixelLook);
            DrawLine((int)p2.X, (int)p2.Y, (int)p3.X, (int)p3.Y, pixelLook);
            DrawLine((int)p3.X, (int)p3.Y, (int)p1.X, (int)p1.Y, pixelLook);
        }

        public static void DrawQuad(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, char pixelLook) // Draws a quadrilateral outine on screen
        {
            DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y, pixelLook);
            DrawLine((int)p2.X, (int)p2.Y, (int)p3.X, (int)p3.Y, pixelLook);
            DrawLine((int)p3.X, (int)p3.Y, (int)p4.X, (int)p4.Y, pixelLook);
            DrawLine((int)p4.X, (int)p4.Y, (int)p1.X, (int)p1.Y, pixelLook);
        }

        class GameUI
        {
            public static void DrawText(int x, int y, string text) // Draws text in a position on screen
            {
                for (int i = 0; i < text.Length; i++)
                {
                    DrawPixel(x + i, y, text[i]);
                }
            }
        }
    }
}