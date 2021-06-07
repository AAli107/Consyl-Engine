using System;
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
                    renderLine += xArray[i] + " ";
                }
                renderedImage += renderLine + "\n";
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

        public static void DrawRectangleOutline(int x, int y, int width, int height, char pixelLook) // Draws an ASCII rectangle outline on screen
        {
            for (int y0 = y; y0 < height + y; y0++)
            {
                for (int x0 = x; x0 < width + x; x0++)
                {
                    if (y0 == y || y0 == height + (y - 1) || x0 == x || x0 == width + (x - 1))
                    {
                        DrawPixel(x0, y0, pixelLook);
                    }
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
                    err -= dy;
                    x0 += sx;
                }

                if (e2 < dy)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        public static void DrawCircle(int centerX, int centerY, float radius, char pixelLook, bool outline = false) // Draws a Circle on screen
        {
            if (outline)
            {   // Draws the outline version of the circle
                for (double i = 0.0; i < 360; i += 0.1)
                {
                    double angle = i * Math.PI / 180;

                    DrawPixel((int)(radius * Math.Cos(angle)) + centerX, (int)(radius * Math.Sin(angle)) + centerY, pixelLook);
                }
            }
            else
            {   // Draws the filled version of the circle
                for (double i = 0.0; i < 360; i += 0.1)
                {
                    for (int r = 0; r < radius + 1; r++)
                    {
                        double angle = i * Math.PI / 180;

                        DrawPixel((int)(r * Math.Cos(angle)) + centerX, (int)(r * Math.Sin(angle)) + centerY, pixelLook);
                    }
                }
            }
        }

        public static void DrawPolygon(Vector2 p1, Vector2 p2, Vector2 p3, char pixelLook, bool outline = false) // Draws a polygon on screen, shape is based on the coordinates given.
        {
            if (outline)
            {   // Draws the polygon as outline
                DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y, pixelLook);
                DrawLine((int)p2.X, (int)p2.Y, (int)p3.X, (int)p3.Y, pixelLook);
                DrawLine((int)p3.X, (int)p3.Y, (int)p1.X, (int)p1.Y, pixelLook);
            }
            else
            {   // Draws the polygon with fillings (tasty)
                List<Vector2> p = new List<Vector2>();
                p.Add(p1);
                p.Add(p2);
                p.Add(p3);

                for (int y = 0; y < drawHeight; y++)
                {
                    for (int x = 0; x < drawWidth; x++)
                    {
                        int j = p.Count - 1;
                        bool c = false;
                        for (int i = 0; i < p.Count; j = i++)
                            c ^= p[i].Y > y ^ p[j].Y > y && x < (p[j].X - p[i].X) * (y - p[i].Y) / (p[j].Y - p[i].Y) + p[i].X;

                        if (c)
                            DrawPixel(x, y, pixelLook); // Will draw pixel only if the coordinates given from the xy for loop is inside the polygon bounds
                    }
                }
            }
        }

        public static void DrawQuad(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, char pixelLook, bool outline = false) // Draws a quadrilateral on screen, shape is based on the coordinates given.
        {
            if (outline)
            {   // Draws the Quadrilateral as outline
                DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y, pixelLook);
                DrawLine((int)p2.X, (int)p2.Y, (int)p3.X, (int)p3.Y, pixelLook);
                DrawLine((int)p3.X, (int)p3.Y, (int)p4.X, (int)p4.Y, pixelLook);
                DrawLine((int)p4.X, (int)p4.Y, (int)p1.X, (int)p1.Y, pixelLook);
            }
            else
            {
                // Draws the Quadrilateral with fillings (tasty)
                List<Vector2> p = new List<Vector2>();
                p.Add(p1);
                p.Add(p2);
                p.Add(p3);
                p.Add(p4);

                for (int y = 0; y < drawHeight; y++)
                {
                    for (int x = 0; x < drawWidth; x++)
                    {
                        int j = p.Count - 1;
                        bool c = false;
                        for (int i = 0; i < p.Count; j = i++)
                            c ^= p[i].Y > y ^ p[j].Y > y && x < (p[j].X - p[i].X) * (y - p[i].Y) / (p[j].Y - p[i].Y) + p[i].X;

                        if (c)
                            DrawPixel(x, y, pixelLook); // Will draw pixel only if the coordinates given from the xy for loop is inside the Quadrilateral bounds
                    }
                }
            }
        }

        public class GameUI
        {
            public static void DrawText(int x, int y, string text) // Draws text in a position on screen
            {
                for (int i = 0; i < text.Length; i++)
                {
                    DrawPixel(x + i, y, text[i]);
                }
            }

            public static void DrawProgressBar(int x, int y, int width, int height, char fillLook, char emptyLook, float percent, bool horizontal = true) // Draws a progress bar on the screen
            {
                DrawRectangle(x, y, width, height, emptyLook); // Draws the Progress bar background

                // Limits the percentage fill between 0 to 1 so that the filled part of the progress bar doesn't get bigger than the background
                if (percent > 1.0f)
                {
                    percent = 1.0f;
                }
                if (percent < 0.0f)
                {
                    percent = 0.0f;
                }

                if (horizontal) // Draws the fill background based on if the user wants vertical or horizontal bars
                {
                    DrawRectangle(x, y, (int)(width * percent), height, fillLook);
                }
                else
                {
                    DrawRectangle(x, y - (int)(height * percent) + height, width, (int)(height * percent), fillLook);
                }
            }

            public static void DrawCircularProgressBar(int centerX, int centerY, float radius, char fillLook, char emptyLook, float percent) // Draws a Filled Circle
            {
                DrawCircle(centerX, centerY, radius, emptyLook); // Draws the Progress bar background

                // Limits the percentage fill between 0 to 1 so that the filled part of the progress bar doesn't get bigger than the background
                if (percent > 1.0f)
                {
                    percent = 1.0f;
                }
                if (percent < 0.0f)
                {
                    percent = 0.0f;
                }

                // Draws the fill background
                for (double i = 0.0; i > (int)(-360 * percent); i -= 0.1)
                {
                    for (int r = 0; r < radius + 1; r++)
                    {
                        double angle = (i * Math.PI / 180);

                        DrawPixel((int)(r * Math.Cos(angle - 1.5708)) + centerX, (int)(r * Math.Sin(angle - 1.5708)) + centerY, fillLook);
                    }
                }
            }
        }
    }
}