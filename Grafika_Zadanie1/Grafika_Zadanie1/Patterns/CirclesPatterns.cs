using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Grafika_Zadanie1.Patterns
{
    public class CirclesPatterns
    {
        public Image Cirlces(Bitmap bitmap)
        {
            Bitmap image;

            // Image resolution
            int x_res, y_res;

            // Ring center coordinates
            int x_c, y_c;

            // Loop variables - indices of the current row and column
            int i, j;

            // Get required image resolution from command line arguments
            x_res = 1000;
            y_res = 1000;

            // Initialize an empty image, use pixel format
            // with RGB packed in the integer data type
            image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);

            // Find coordinates of the image center
            x_c = x_res/2;

            var xCount = 1;
            var yCount = 1;
            double bok = 50;
            // Process the image, pixel by pixel
            for (i = 0; i < y_res; i++)
            {
                var x = 0;
                var y = 0;
                double firstx = 0;
                //firstx = x_c - bok/2;
                //for (int k = 0; firstx > bok; k++)
                //{
                //    firstx -= bok;
                //}

                //firstx -= bok/2;

                y = (int) (firstx + (yCount - 1)*bok);
                x = (int) (firstx + (xCount - 1)*bok);

                for (j = 0; j < x_res; j++)
                {
                   if (j ==firstx + bok/2*xCount)
                   {
                       x = (int)(firstx + (xCount) * bok);
                   }

                    if (j > bok*(xCount-1))
                    {
                        xCount++;
                        x = (int) (firstx + (xCount - 1)*bok);
                    }


                    var d = Math.Sqrt((i - y)*(i - y) + (j - x)*(j - x));

                    // Find the ring index


                    if (d < bok/2)
                        image.SetPixel(j, i, Color.Black);
                    else
                        image.SetPixel(j, i, bitmap != null ? bitmap.GetPixel(j, i) : Color.White);
                }
                if (i >bok*yCount)
                {
                    yCount++;
                }
                xCount = 1;
            }

            // Save the created image in a graphics file
            try

            {
                image.Save("circles.bmp", ImageFormat.Bmp);
            }
            catch (IOException e)
            {
            }

            return image;
        }
    }
}