using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Forms;

namespace Grafika_Zadanie1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var demo = new Demo0();
            var image = demo.Circles(50, 10);
            panel1.BackgroundImage = image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var demo = new Demo0();
            var image = demo.Crate(10, 40,50, Color.Black, Color.Aqua);
            panel1.BackgroundImage = image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var demo = new Demo0();
            var image = demo.ChessBoard(Color.Black, Color.Aqua, 20,250000);

            panel1.BackgroundImage = image;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var demo = new Demo0();
            var image = demo.ChessBoardRotate(Color.Black, Color.Aqua, 200, 250000);

            panel1.BackgroundImage = image;
        }
    }

    public class Demo0
    {
        private bool goingUp = true;
        
        public Image Circles(int circleBlurSize, int blurSize)
        {
            Bitmap image;

            // Image resolution  
            int x_res, y_res;

            // Ring center coordinates   
            int x_c, y_c;

            // Predefined black and white RGB representations   
            // packed as integers  
            int black, white;

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
            x_c = x_res/2;
            y_c = y_res/2;


            // Process the image, pixel by pixel  
            for (i = 0; i < y_res; i++)
                for (j = 0; j < x_res; j++)
                {
                    double d;
                    int r;

                    // Calculate distance to the image center   
                    d = Math.Sqrt((i - y_c)*(i - y_c) + (j - x_c)*(j - x_c));

                    // Find the ring index  
                    r = (int) d/w;

                    // Make decision on the pixel color   
                    // based on the ring index  
                    var left = d%w;
                    if (r%2 == 0)
                        // Even ring - set black color  i
                        if (left < blurSize/2)
                        {
                            var color = GetGrayShade(blurSize,true, (int)left,circleBlurSize);
                            image.SetPixel(j, i, color);
                        }
                        else if (left > circleBlurSize - blurSize/2)
                        {
                            var color = GetGrayShade(blurSize, true, circleBlurSize-(int)left, circleBlurSize);
                            image.SetPixel(j, i, color);
                        }
                        else
                        {
                            image.SetPixel(j, i, Color.Black);
                        }

                    else
                    // Odd ring - set white color  
                        if (left < blurSize/2)
                        {
                        var color = GetGrayShade(blurSize, false, (int)left, circleBlurSize);
                        image.SetPixel(j, i, color);
                        }
                        else if (left > circleBlurSize - blurSize/2)
                        {
                        var color = GetGrayShade(blurSize, false, circleBlurSize-(int)left, circleBlurSize);
                        image.SetPixel(j, i, color);
                        }
                        else
                        {
                            image.SetPixel(j, i, Color.White);
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
                if (left < circleSize/2)
                {
                    var p = 255/blurSize;
                    var fromZero = blurSize/2 - left;
                    color = 0 + p*fromZero;
                }
                else
                {
                    var p = 255 / blurSize;
                    var fromZero =   circleSize - left - blurSize / 2;
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
            return Color.FromArgb(color,color,color);
        }

        public Image Crate(int lineSize, int xAxisDistance, int yAxisDistance,Color lineColore, Color bgColor)
        {
            
            Bitmap image;
            
            // Image resolution  
            int x_res, y_res;

            int x_c, y_c;

            // Loop variables - indices of the current row and column  
            int i, j;

            // Fixed ring width  


            // Get required image resolution from command line arguments  
            x_res = 700;
            y_res = 700;

            // Initialize an empty image, use pixel format   
            // with RGB packed in the integer data type  
            image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);

            // Find coordinates of the image center   
            x_c = x_res/2;
            y_c = y_res/2;


 
            for (i = 0; i < y_res; i++)
                for (j = 0; j < x_res; j++)
                {
                    double d;
                    double g;
                    int r;
                    int l;

                    if (j > x_c)
                    {
                        d = j - x_c;
                    }
                    else
                    {
                        d = x_c - j;
                    }
                    if (i > y_c)
                    {
                        g = i - y_c;
                    }
                    else
                    {
                        g = y_c - i;
                    }

                    

                    
                    var leftX = d% xAxisDistance;
                    var leftY = g% yAxisDistance;
                    if ((leftX > xAxisDistance/2 + lineSize/2 || leftX < xAxisDistance/2 - lineSize/2) && (leftY > yAxisDistance/2+lineSize/2 || leftY < yAxisDistance/2-lineSize/2))
                        image.SetPixel(j, i, bgColor);
                    else
                        image.SetPixel(j, i, lineColore);
                        
                }

            // Save the created image in a graphics file  
            try
            {
                image.Save("crate.bmp", ImageFormat.Bmp);
            }
            catch (IOException e)
            {
            }
            return image;
        }

        // This method assembles RGB color intensities into single   
        // packed integer. Arguments must be in <0..255> range  
        private static int int2RGB(int red, int green, int blue)
        {
            // Make sure that color intensities are in 0..255 range   
            red = red & 0x000000FF;
            green = green & 0x000000FF;
            blue = blue & 0x000000FF;

            // Assemble packed RGB using bit shift operations 
            return (red << 16) + (green << 8) + blue;
        }

        public Image ChessBoard(Color firstColor, Color secondColor, int sideSize, int resolution)
        {
            Bitmap image;
            bool useFirstColor = true;
            int yCount = 1;
            int xCount = 1;
            
            var x_res = (int)Math.Sqrt(resolution);
            var y_res = x_res;
            image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);
            
            for (var i = 0; i < y_res; i++)
            {


                for (var j = 0; j < x_res; j++)
                {
                    if (j > xCount*sideSize)
                    {
                        useFirstColor = !useFirstColor;
                        xCount++;
                    }

                    if (i > yCount*sideSize)
                    {
                        useFirstColor = !useFirstColor;
                        yCount++;
                    }

                    if (useFirstColor)
                        image.SetPixel(j, i, firstColor);
                    else
                        image.SetPixel(j, i, secondColor);

                }
                xCount = 1;
            }

            // Save the created image in a graphics file  
            try
            {
                image.Save("crate.bmp", ImageFormat.Bmp);
            }
            catch (IOException e)
            {
            }
            return image;

        }

        public Image ChessBoardRotate(Color firstColor, Color secondColor, int sideSize, int resolution)
        {
            Bitmap image;
            bool useFirstColor = true;
            int yCount = 1;
            int xCount = 1;

            var x_res = (int)Math.Sqrt(resolution);
            var y_res = x_res;
            image = new Bitmap(x_res, y_res, PixelFormat.Format32bppRgb);

            for (var i = 0; i < y_res; i++)
            {
                for (var j = 0; j < x_res; j++)
                {
                    if (j > xCount * sideSize)
                    {
                        xCount++;
                    }

                    if (i > yCount * sideSize)
                    {
                        yCount++;
                    }
                    var x_c = (xCount*sideSize) - sideSize/2;
                    var y_c = (yCount*sideSize) - sideSize/2;
                    
                    if ((j < x_c && i < y_c))
                         {
                        var x = ((xCount - 1)*sideSize);
                        var y = ((yCount - 1) * sideSize);
                        var distanceFromSquareCenter = Math.Sqrt((i - y_c) * (i - y_c) + (j - x_c) * (j - x_c));
                        var distanceFormLeftUpperCorner = Math.Sqrt((i - y) * (i - y) + (j - x) * (j - x));
                        if (distanceFormLeftUpperCorner > distanceFromSquareCenter)
                        {
                            image.SetPixel(j, i, firstColor);
                        }
                        else
                            image.SetPixel(j, i, secondColor);
                    }else if ((j > x_c && i < y_c))
                    {
                        var x = ((xCount ) * sideSize);
                        var y = ((yCount-1 ) * sideSize);
                        var distanceFromSquareCenter = Math.Sqrt((i - y_c) * (i - y_c) + (j - x_c) * (j - x_c));
                        var distanceFormRightUpperCorner = Math.Sqrt((i - y) * (i - y) + (j - x) * (j - x));
                        if (distanceFormRightUpperCorner > distanceFromSquareCenter)
                        {
                            image.SetPixel(j, i, firstColor);
                        }
                        else
                            image.SetPixel(j, i, secondColor);
                    }

                    else if ((j > x_c && i > y_c))
                    {
                        var x = ((xCount) * sideSize);
                        var y = ((yCount) * sideSize);
                        var distanceFromSquareCenter = Math.Sqrt((i - y_c) * (i - y_c) + (j - x_c) * (j - x_c));
                        var distanceFormRightLowerCorner = Math.Sqrt((i - y) * (i - y) + (j - x) * (j - x));
                        if (distanceFormRightLowerCorner > distanceFromSquareCenter)
                        {
                            image.SetPixel(j, i, firstColor);
                        }
                        else
                            image.SetPixel(j, i, secondColor);
                    }

                    else if ((j < x_c && i > y_c))
                    {
                        var x = ((xCount-1) * sideSize);
                        var y = ((yCount) * sideSize);
                        var distanceFromSquareCenter = Math.Sqrt((i - y_c) * (i - y_c) + (j - x_c) * (j - x_c));
                        var distanceFormLeftLowerCorner = Math.Sqrt((i - y) * (i - y) + (j - x) * (j - x));
                        if (distanceFormLeftLowerCorner >  distanceFromSquareCenter)
                        {
                            image.SetPixel(j, i, firstColor);
                        }
                        else
                            image.SetPixel(j, i, secondColor);
                    }




                }
                xCount = 1;
            }

            // Save the created image in a graphics file  
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