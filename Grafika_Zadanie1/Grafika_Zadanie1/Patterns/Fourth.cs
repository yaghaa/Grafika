using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Grafika_Zadanie1.Patterns
{
    public class Fourth
    {
        public Image JoinImages(Bitmap image1, Bitmap image2, Bitmap pattern)
        {
            Bitmap image;
            int x_res, y_res;

            // Loop variables - indices of the current row and column
            int i, j;

            // Get required image resolution from command line arguments
            x_res = 500;
            y_res = 500;

            // Initialize an empty image, use pixel format
            // with RGB packed in the integer data type
            image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);

            
            // Process the image, pixel by pixel
            for (i = 0; i < y_res; i++)
                for (j = 0; j < x_res; j++)
                {
                    var r_c = pattern.GetPixel(j, i);
                    var wsp_r = r_c.R/255;
                    var wsp_g = r_c.G/255;
                    var wsp_b = r_c.B/255;

                    var r_wR = wsp_r*image1.GetPixel(j, i).R + (1 - wsp_r)*image2.GetPixel(j, i).R;
                    var r_wG = wsp_g*image1.GetPixel(j, i).G + (1 - wsp_g)*image2.GetPixel(j, i).G;
                    var r_wB = wsp_b*image1.GetPixel(j, i).B + (1 - wsp_b)*image2.GetPixel(j, i).B;

                    image.SetPixel(j, i, Color.FromArgb(r_wR,r_wG,r_wB));
                }
            try
            {
                image.Save("crate.bmp", ImageFormat.Bmp);
            }
            catch (IOException e)
            {
            }
            return image;
            
        }
    }
}