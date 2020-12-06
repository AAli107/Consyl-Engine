﻿// If you want to draw an image/texture on screen, you need to place it in the same directory as the game's executable file
// Currently supports the following file formats: BMP, GIF, EXIF, JPG, PNG and TIFF

using System.Drawing;

namespace Consyl_Engine.EngineContents
{
    class Texture
    {
        private string fileName; // Stores the Texture's file name
        Bitmap img; // loads the image in a variable

        public Texture(string _fileName) // constructor for inputting the Texture's file name
        {
            fileName = _fileName;
            InitGraphics();
        }
        
        private void InitGraphics() // Initializes the Bitmap class variable
        {
            img = new Bitmap(fileName);
        }

        public void DrawImage(int x, int y) // Will draw the loaded texture file 
        {
            // Loops between pixels
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j); // saves the color value of the current pixel in a variable
                    
                    float AvgColor = (pixel.R + pixel.G + pixel.B) / 3; // Takes all the color data Red, green and blue to be a single average number

                    int shade = (int)(AvgColor / (255.0f / (float)gfx.shadeCharArray.Length)); // converts the average color into a number inside the range of the gfx.shadeCharArray

                    // Locks the shade value to be exactly between 0 and the shadeCharArray's length - 1
                    if (shade < 0)
                    {
                        shade = 0;
                    }
                    else if (shade > gfx.shadeCharArray.Length - 1)
                    {
                        shade = gfx.shadeCharArray.Length - 1;
                    }

                    gfx.DrawPixel(i + x, j + y, gfx.shadeCharArray[shade]); // Draws the image
                }
            }
        }

        public char[] GetAllShadeColor() // Gets all the Consyl-Shaded Color from the loaded Bitmap
        {
            char[] Data = new char[img.Width * img.Height];

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j); // saves the color value of the current pixel in a variable

                    float AvgColor = (pixel.R + pixel.G + pixel.B) / 3; // Takes all the color data Red, green and blue to be a single average number

                    int shade = (int)(AvgColor / (255.0f / (float)gfx.shadeCharArray.Length)); // converts the average color into a number inside the range of the gfx.shadeCharArray

                    // Locks the shade value to be exactly between 0 and the shadeCharArray's length - 1
                    if (shade < 0)
                    {
                        shade = 0;
                    }
                    else if (shade > gfx.shadeCharArray.Length - 1)
                    {
                        shade = gfx.shadeCharArray.Length - 1;
                    }

                    Data[img.Width * j + i] = gfx.shadeCharArray[shade];
                }
            }
            return Data;
        }

        public char GetShadeColorAtPixel(int x, int y) // Gets the Consyl-Shaded color from a chosen pixel based on x and y
        {
            Color pixel = img.GetPixel(x, y); // saves the color value of the current pixel in a variable

            float AvgColor = (pixel.R + pixel.G + pixel.B) / 3; // Takes all the color data Red, green and blue to be a single average number

            int shade = (int)(AvgColor / (255.0f / (float)gfx.shadeCharArray.Length)); // converts the average color into a number inside the range of the gfx.shadeCharArray

            // Locks the shade value to be exactly between 0 and the shadeCharArray's length - 1
            if (shade < 0)
            {
                shade = 0;
            }
            else if (shade > gfx.shadeCharArray.Length - 1)
            {
                shade = gfx.shadeCharArray.Length - 1;
            }

            return gfx.shadeCharArray[shade];
        }

        public Color[] GetAllColor() // Gets the color of all pixels from the loaded Bitmap
        {
            Color[] Data = new Color[img.Width * img.Height];

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Data[img.Width * j + i] = img.GetPixel(i, j); // Returns an array of colors from the loaded Bitmap
                }
            }
            return Data;
        }

        public Color GetColorAtPixel(int x, int y) // Gets the Color at a chosen pixel directly from the Bitmap
        {
            return img.GetPixel(x, y);
        }
    }
}
