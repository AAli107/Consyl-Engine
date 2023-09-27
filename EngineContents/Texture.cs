// If you want to draw an image/texture on screen, you need to place it in the same directory as the game's executable file
// Currently supports the following file formats: BMP, GIF, EXIF, JPG, PNG and TIFF

using System.Drawing;
using System.Numerics;
using System;

namespace Consyl_Engine.EngineContents
{
    class Texture
    {
        private string filename; // Stores the Texture's file name
        private Bitmap img; // Bitmap class that would store information about the texture
        private Vector2 imageResolution;
        private int[][] pixelShadeValues;

        /// <summary>
        /// Constructor for initializing the Texture class with a file name/path
        /// </summary>
        /// <param name="_filename"></param>
        public Texture(string _filename)
        {
            SetTextureFile(_filename);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Texture()
        {
            SetTextureFile("");
        }

        /// <summary>
        /// Changes Texture path
        /// </summary>
        /// <param name="_filename"></param>
        public void SetTextureFile(string _filename)
        {
            if (_filename == filename) return; // If set to the same file, it would stop executing this method

            filename = _filename; // Sets the file name/path
            try
            {
                img = new Bitmap(filename); // Assigning the Bitmap class into a variable with the fileName
                imageResolution = new Vector2(img.Width, img.Height); // Saves the Resolution of the image

                // Reads all the pixels and store them before-hand so that it won't have to do it every frame
                pixelShadeValues = new int[img.Width][];
                for (int i = 0; i < img.Width; i++)
                {
                    int[] line = new int[img.Height];
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color pixel = img.GetPixel(i, j); // saves the color value of the current pixel in a variable

                        int shade = (int)(((pixel.R + pixel.G + pixel.B) / 3) / (255.0f / (float)gfx.shadeCharArray.Length)); // converts the average color into a number inside the range of the gfx.shadeCharArray

                        // Locks the shade value to be exactly between 0 and the shadeCharArray's length - 1
                        if (shade < 0)
                            shade = 0;
                        else if (shade > gfx.shadeCharArray.Length - 1)
                            shade = gfx.shadeCharArray.Length - 1;

                        line[j] = shade;
                    }
                    pixelShadeValues[i] = line;
                }
            }
            catch (ArgumentException)
            {
                img = null;
                // Will make an empty texture
                pixelShadeValues = new int[1][];
                for (int i = 0; i < 1; i++)
                {
                    int[] line = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        line[j] = gfx.shadeCharArray[0]; ;
                    }
                    pixelShadeValues[i] = line;
                }
            }
        }

        /// <summary>
        /// Will draw the loaded texture file
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="blackTransparent"></param>
        /// <param name="scale"></param>
        /// <param name="isStatic"></param>
        public void DrawImage(int x, int y, bool blackTransparent = false, float scale = 1, bool isStatic = false)
        {
            // Loops between pixels
            if (img != null)
            {
                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        int shade = pixelShadeValues[i][j];

                        // Draws the cropped image
                        if (!(blackTransparent && shade == 0))
                            gfx.DrawRectangle((int)(i * scale) + x, (int)(j * scale) + y, (int)MathF.Ceiling(scale), (int)MathF.Ceiling(scale), gfx.shadeCharArray[shade], false, isStatic); // Draws the image
                    }

                }
            }
            else
            {
                gfx.GameUI.DrawText(x, y, "incorrect texture path.");
            }
        }

        /// <summary>
        /// Will draw the loaded texture file cropped based on the offset and size Vectors.
        /// </summary>
        /// <param name="imageLoc"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="blackTransparent"></param>
        /// <param name="scale"></param>
        /// <param name="isStatic"></param>
        public void DrawCroppedImage(Vector2 imageLoc, Vector2 offset, Vector2 size, bool blackTransparent = false, float scale = 1, bool isStatic = false)
        {
            if (img != null)
            {
                // Loops between pixels
                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        int shade = pixelShadeValues[i][j];

                        // Draws the cropped Image only within the offset and size
                        if (!(blackTransparent && shade == 0) && (i > offset.X && i < size.X + offset.X) && (j > offset.Y && j < size.Y + offset.Y))
                            gfx.DrawRectangle(((int)(i * scale) - (int)offset.X) + (int)imageLoc.X, ((int)(j * scale) - (int)offset.Y) + (int)imageLoc.Y, (int)MathF.Ceiling(scale), (int)MathF.Ceiling(scale), gfx.shadeCharArray[shade], false, isStatic); // Draws the image
                    }
                }
            }
            else
            {
                gfx.GameUI.DrawText((int)imageLoc.X, (int)imageLoc.Y, "incorrect texture path.");
            }
        }

        /// <summary>
        /// Gets all the Consyl-Shaded Color from the loaded Bitmap
        /// </summary>
        /// <returns></returns>
        public char[] GetAllShadeColor()
        {
            if (img != null)
            {
                char[] Data = new char[img.Width * img.Height];

                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color pixel = img.GetPixel(i, j); // saves the color value of the current pixel in a variable

                        int shade = (int)(((pixel.R + pixel.G + pixel.B) / 3) / (255.0f / (float)gfx.shadeCharArray.Length)); // converts the average color into a number inside the range of the gfx.shadeCharArray

                        // Locks the shade value to be exactly between 0 and the shadeCharArray's length - 1
                        if (shade < 0)
                            shade = 0;
                        else if (shade > gfx.shadeCharArray.Length - 1)
                            shade = gfx.shadeCharArray.Length - 1;

                        Data[img.Width * j + i] = gfx.shadeCharArray[shade];
                    }
                }
                return Data;
            }
            return null;
        }

        /// <summary>
        /// Gets the Consyl-Shaded color from a chosen pixel based on x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public char GetShadeColorAtPixel(int x, int y)
        {
            if (img != null)
            {
                Color pixel = img.GetPixel(x, y); // saves the color value of the current pixel in a variable

                int shade = (int)(((pixel.R + pixel.G + pixel.B) / 3) / (255.0f / (float)gfx.shadeCharArray.Length)); // converts the average color into a number inside the range of the gfx.shadeCharArray

                // Locks the shade value to be exactly between 0 and the shadeCharArray's length - 1
                if (shade < 0)
                    shade = 0;
                else if (shade > gfx.shadeCharArray.Length - 1)
                    shade = gfx.shadeCharArray.Length - 1;

                return gfx.shadeCharArray[shade];
            }
            return 'N';
        }

        /// <summary>
        /// Gets the color of all pixels from the loaded Bitmap
        /// </summary>
        /// <returns></returns>
        public Color[] GetAllColor()
        {
            if (img != null)
            {
                Color[] Data = new Color[img.Width * img.Height];

                for (int i = 0; i < img.Width; i++)
                    for (int j = 0; j < img.Height; j++)
                        Data[img.Width * j + i] = img.GetPixel(i, j); // Returns an array of colors from the loaded Bitmap

                return Data;
            }
            return null;
        }

        /// <summary>
        /// Gets the Color at a chosen pixel directly from the Bitmap
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetColorAtPixel(int x, int y)
        {
            if (img != null)
            {
                return img.GetPixel(x, y);
            }
            return new Color();
        }

        public Vector2 GetResolution()
        {
            return imageResolution;
        }
    }
}
