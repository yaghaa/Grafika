using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Grafika_Zadanie1.Patterns
{
    public class WarpPattern
    {
        public Image CreateWarpPattern(Bitmap bitmap)
        {
            Bitmap image;

            // Image resolution
            int x_res, y_res;

            // Ring center coordinates
            int x_c, y_c;

            // Loop variables - indices of the current row and column
            int i, j;

            // Get required image resolution from command line arguments
            x_res = 500;
            y_res = 500;

            // Initialize an empty image, use pixel format
            // with RGB packed in the integer data type
            image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);

            // Find coordinates of the image center
            x_c = x_res/2;
            y_c = y_res/2;

            // Process the image, pixel by pixel
            for (i = 0; i < y_res; i++)
                for (j = 0; j < x_res; j++)
                {
                    double d;
                    // Calculate distance to the image center
                    ///d = Math.Sqrt((i - y_c)*(i - y_c) + (j - x_c)*(j - x_c));

                    if (y_c != i)
                    {
                        double a = 0;
                        double b = 1;
                        b = x_c - j;
                        a = y_c - i;
                    
                        double tagAlfa = Math.Abs(a)/Math.Abs(b);
                        var angle = Math.Atan(tagAlfa);
                        var alfa = angle*(180/Math.PI);

                        if (j < x_c && i<y_c)
                        {
                            alfa = 270 + (alfa);
                        }
                        else if( j <x_c && i>y_c)
                        {
                            alfa = 180 + (90-alfa);
                        }
                        else if (j > x_c && i > y_c)
                        {
                            alfa = 90 + (alfa);
                        }
                        else
                        {
                            alfa = 90 - alfa;
                        }

                        if ((alfa > 10 && alfa <= 30)
                            || (alfa > 50 && alfa <= 70)
                            || (alfa > 90 && alfa <= 110)
                            || (alfa > 130 && alfa <= 150)
                            || (alfa > 170 && alfa <= 190)
                            || (alfa > 210 && alfa <= 230)
                            || (alfa > 250 && alfa <= 270)
                            || (alfa > 290 && alfa <= 310)
                            || (alfa > 330 && alfa <= 350)
                            )
                            image.SetPixel(j, i, Color.Black);
                        else
                        {
                            if (j == x_c && i>y_c)
                            {
                                image.SetPixel(j, i, bitmap != null ? bitmap.GetPixel(j, i) : Color.Black);
                            }
                            else
                            {
                                image.SetPixel(j, i, bitmap != null ? bitmap.GetPixel(j, i) : Color.White);
                            }

                        }
                            
                    }
                   
                   
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