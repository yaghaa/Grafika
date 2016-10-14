using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Grafika_Zadanie1.Patterns
{
  public class DartBoard
  {
    public Image CreateDartBoardWithBlur(int circleBlurSize, int blurSize, Bitmap bitmap)
    {
      Bitmap image;

      // Image resolution
      int x_res, y_res;

      // Ring center coordinates
      int x_c, y_c;

      // Loop variables - indices of the current row and column
      int i, j;

      // Fixed ring width
      var w = circleBlurSize;

      // Get required image resolution from command line arguments
      x_res = 500;
      y_res = 500;

      // Initialize an empty image, use pixel format
      // with RGB packed in the integer data type
      image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);

      // Find coordinates of the image center
      x_c = x_res / 2;
      y_c = y_res / 2;

      // Process the image, pixel by pixel
      for (i = 0; i < y_res; i++)
        for (j = 0; j < x_res; j++)
        {
          double d;
          int r;

          // Calculate distance to the image center
          d = Math.Sqrt((i - y_c) * (i - y_c) + (j - x_c) * (j - x_c));

          // Find the ring index
          r = (int)d / w;

          // Make decision on the pixel color
          // based on the ring index
          var left = d % w;
          if (r % 2 == 0)
            // Even ring - set black color  i
            if (left < blurSize / 2)
            {
              var color = GetGrayShade(blurSize, true, (int)left, circleBlurSize);
              image.SetPixel(j, i, color);
            }
            else if (left > circleBlurSize - blurSize / 2)
            {
              var color = GetGrayShade(blurSize, true, circleBlurSize - (int)left, circleBlurSize);
              image.SetPixel(j, i, color);
            }
            else
            {
              image.SetPixel(j, i, Color.Black);
            }
          else
              // Odd ring - set white color
              if (left < blurSize / 2)
          {
            var color = GetGrayShade(blurSize, false, (int)left, circleBlurSize);
            image.SetPixel(j, i, color);
          }
          else if (left > circleBlurSize - blurSize / 2)
          {
            var color = GetGrayShade(blurSize, false, circleBlurSize - (int)left, circleBlurSize);
            image.SetPixel(j, i, color);
          }
          else
          {
            image.SetPixel(j, i, bitmap!=null?bitmap.GetPixel(j,i):Color.White);
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

    private Color GetGrayShade(int blurSize, bool up, int left, int circleSize)
    {
      int color = 0;
      if (up)
      {
        if (left < circleSize / 2)
        {
          var p = 255 / blurSize;
          var fromZero = blurSize / 2 - left;
          color = 0 + p * fromZero;
        }
        else
        {
          var p = 255 / blurSize;
          var fromZero = circleSize - left - blurSize / 2;
          color = 0 + p * fromZero;
        }
      }
      else
      {
        if (left < circleSize / 2)
        {
          var p = 255 / blurSize;
          var fromZero = blurSize / 2 - left;
          color = 255 - p * fromZero;
        }
        else
        {
          var p = 255 / blurSize;
          var fromZero = circleSize - left - blurSize / 2;
          color = 255 - p * fromZero;
        }
      }
      return Color.FromArgb(color, color, color);
    }
  }
}