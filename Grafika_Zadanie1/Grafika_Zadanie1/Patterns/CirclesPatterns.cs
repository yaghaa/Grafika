using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Grafika_Zadanie1.Patterns
{
    public class CirclesPatterns
    {
        public Image CreateCirlces(Bitmap bitmap = null)
        {
            Bitmap image;

            // Image resolution
            int x_res, y_res;

            // Ring center coordinates
            int x_c;

            // Loop variables - indices of the current row and column
            int i, j;

            // Get required image resolution from command line arguments
            x_res = 500;
            y_res = 500;

            // Initialize an empty image, use pixel format
            // with RGB packed in the integer data type
            image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);
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
                firstx = x_c - bok/2;
                for (var k = 0; firstx > bok; k++)
                {
                    firstx -= bok;
                }

                firstx -= bok/2;

                y = (int) (firstx + (yCount - 1)*bok);
                x = (int) (firstx + (xCount - 1)*bok);

                for (j = 0; j < x_res; j++)
                {
                    var d = Math.Sqrt((i - y)*(i - y) + (j - x)*(j - x));

                    // Find the ring index

                    if (j > bok*xCount - bok/2)
                    {
                        xCount++;
                        x = (int) (firstx + (xCount - 1)*bok);
                    }

                    if (d < bok/2)
                        image.SetPixel(j, i, Color.Black);
                    else
                        image.SetPixel(j, i, bitmap != null ? bitmap.GetPixel(j, i) : Color.White);
                }
                if (i > bok*yCount - bok/2)
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


        public Image CreateCirlcesWithShields(Bitmap bitmap)
        {
            Bitmap image;

            // Image resolution
            int x_res, y_res;

            // Ring center coordinates
            int x_c;

            // Loop variables - indices of the current row and column
            int i, j;

            // Get required image resolution from command line arguments
            x_res = 500;
            y_res = 500;

            // Initialize an empty image, use pixel format
            // with RGB packed in the integer data type
            image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);
            x_c = x_res/2;

            var xCount = 1;
            var yCount = 1;
            double bok = 50;
            int ringSize = 4;
            // Process the image, pixel by pixel
            for (i = 0; i < y_res; i++)
            {
                var x = 0;
                var y = 0;
                double firstx = 0;
                firstx = x_c - bok/2;
                for (var k = 0; firstx > bok; k++)
                {
                    firstx -= bok;
                }

                firstx -= bok/2;

                y = (int) (firstx + (yCount - 1)*bok);
                x = (int) (firstx + (xCount - 1)*bok);

                for (j = 0; j < x_res; j++)
                {
                    var d = Math.Sqrt((i - y)*(i - y) + (j - x)*(j - x));

                    // Find the ring index

                    if (j > bok*xCount - bok/2)
                    {
                        xCount++;
                        x = (int) (firstx + (xCount - 1)*bok);
                    }

                    var r = (int)d / ringSize;
                    var left = d % ringSize;
                    if (r % 2 == 0)
                        image.SetPixel(j, i, Color.Black);
                    else
                        image.SetPixel(j, i, Color.White);

                }
                if (i > bok*yCount - bok/2)
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