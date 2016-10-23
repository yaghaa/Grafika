using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Zadanie2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cbImageResolution.DataSource = new [] {new Resolution(300, 300),new Resolution(500,500),new Resolution(700,700), new Resolution(600,400)};
            cbImageResolution.SelectedIndex = 0;
        }

        public void PaintComponent(Graphics g)
        {
            LinearGradientBrush gradient = new LinearGradientBrush( new Point(70, 70), new Point(150, 150), Color.Red, Color.Blue);
            g.FillEllipse(gradient,70, 70, 100, 100);
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PaintComponent(e.Graphics);
        }

        private void bImportImage_Click(object sender, EventArgs e)
        {
            var resolution = (Resolution) cbImageResolution.SelectedItem;
            this.Size = new Size(resolution.X + 200,resolution.Y+100);

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                //dlg.Filter = "bmp files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var resizedImage = ResizeImage(new Bitmap(dlg.FileName), resolution.X, resolution.Y);
                    pictureBox1.Image = resizedImage;
                }
            }
        }


        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }

    public class Resolution
    {
        public Resolution(int x,int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return X + "x" + Y;
        }
    }
}
