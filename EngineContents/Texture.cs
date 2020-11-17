// If you want to draw an image/texture on screen, you need to place it in the same directory as the game's executable file
// Currently supports the following file formats: BMP, GIF, EXIF, JPG, PNG and TIFF

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Consyl_Engine.EngineContents
{
    class Texture
    {
        static string fileName; // Stores the Texture's file name

        public Texture(string _fileName) // constructor for inputting the Texture's file name
        {
            fileName = _fileName;
        }     

        public void DrawImage(int x, int y) // Will draw the loaded texture file 
        {
            Bitmap img = new Bitmap(fileName);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    float AvgColor = (pixel.R + pixel.G + pixel.B) / 3;
                    int shade = (int)(AvgColor / 4.047619047619047619047619047619f);
                    if (shade < 0)
                    {
                        shade = 0;
                    }
                    else if (shade > 62)
                    {
                        shade = 62;
                    }
                    gfx.DrawPixel(i + x, j + y, gfx.shadeCharArray[shade]);
                }
            }
        }
    }
}
