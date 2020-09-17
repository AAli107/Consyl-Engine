using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Consyl_Engine.EngineContents
{
    class gfx
    {
        // Resolution width and height of the ASCII screen
        public static int drawWidth = (int)Engine.resolution.X;
        public static int drawHeight = (int)Engine.resolution.Y;

        // An Array that stores ASCII chars that can be used for shading
        public static char[] shadeCharArray = { ' ','.',',','-','^','*',':',';','I','l','!','i','>','<','~','+','_','?',']','[','}','{',')','(','|','/','t','f',
            'j','r','x','n','u','v','c','z','X','Y','U','J','C','L','Q','O','Z','m','w','q','p','d','b','k','h','a','o','#','M','W','&','%','B','@','$' };

        static char[] textImage = new char[drawWidth * drawHeight]; // Stores the Data of the ASCII pixels
        static ConsoleColor[] ColorImage = new ConsoleColor[drawWidth * drawHeight]; // Stores the Color Data of the ASCII pixels
        
        public static void DrawASCII()
        {
            // Draws ASCII Render on screen
            for (int y = 0; y < drawHeight; y++)
            {
                // Converts 1D array to 2D array
                List<char> xArray = new List<char>(drawWidth);
                List<ConsoleColor> cArray = new List<ConsoleColor>(drawWidth);
                for (int x = 0; x < drawWidth; x++)
                {
                    xArray.Add(textImage[drawWidth * y + x]);
                    cArray.Add(ColorImage[drawWidth * y + x]);
                }

                for (int i = 0; i < xArray.Count; i++) // stores a line of the screen
                {
                    Console.ForegroundColor = cArray[i];
                    Console.Write(xArray[i] + " ");
                }
                Console.WriteLine("");
            }
        }

        public static void ClearScreen()
        {
            // Resets the Cursor position to redraw the next frame
            Console.CursorLeft = 0;
            Console.CursorTop = 0;

            // Clears the Image data from textImage
            for (int i = 0; i < textImage.Length; i++)
            {
                textImage[i] = shadeCharArray[1];
                ColorImage[i] = ConsoleColor.White;
            }
        }

        public static void DrawPixel(int x, int y, char pixelLook, ConsoleColor color) // Draws ASCII pixel on screen
        {
            if (x >= 0 && x < drawWidth && y >= 0 && y < drawHeight)
            {
                textImage[drawWidth * y + x] = pixelLook;
                ColorImage[drawWidth * y + x] = color;
            }
        }

        public static void DrawRectangle(int x, int y, int width, int height, char pixelLook, ConsoleColor color) // Draws an ASCII rectangle on screen
        {
            for (int y0 = y; y0 < height + y; y0++)
            {
                for (int x0 = x; x0 < width + x; x0++)
                {
                    DrawPixel(x0, y0, pixelLook, color);
                }
            }
        }
        public static void DrawText(int x, int y, string text, ConsoleColor color) // Draws text in a position on screen
        {
            char[] charArray = new char[text.Length];
            charArray = text.ToCharArray();

            for (int i = 0; i < text.Length; i++)
            {
                DrawPixel(x + i, y, charArray[i], color);
            }
        }

        public static void DrawLine(int x0, int y0, int x1, int y1, char pixelLook, ConsoleColor color) // Draw lines between two points
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;

            for (; ; )
            {
                DrawPixel(x0, y0, pixelLook, color);

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

        public static void DrawFilledCircle(int centerX, int centerY, float radius, char pixelLook, ConsoleColor color) // Draws a Filled Circle
        {
            for (double i = 0.0; i < 360; i += 0.1)
            {
                for (int r = 0; r < radius; r++)
                {
                    double angle = i * System.Math.PI / 180;
                    int x = (int)(r * System.Math.Cos(angle));
                    int y = (int)(r * System.Math.Sin(angle));

                    DrawPixel(x + centerX, y + centerY, pixelLook, color);
                }
            }
        }

        public static void DrawCircle(int centerX, int centerY, float radius, char pixelLook, ConsoleColor color) // Draws a Circle
        {
            for (double i = 0.0; i < 360; i += 0.1)
            {
                    double angle = i * System.Math.PI / 180;
                    int x = (int)(radius * System.Math.Cos(angle));
                    int y = (int)(radius * System.Math.Sin(angle));

                    DrawPixel(x + centerX, y + centerY, pixelLook, color);
            }
        }

        public static void DrawTri(int x, int y, int size, char pixelLook, ConsoleColor color) // Draws a right angle triangle
        {
            for (int i = x; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    DrawPixel(x + j, y + i, pixelLook, color);
                }
            }
        }
    }
}
