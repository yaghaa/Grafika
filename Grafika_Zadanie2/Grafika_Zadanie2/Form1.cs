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
using System.Xml.Serialization;
using Grafika_Zadanie2.Properties;

namespace Grafika_Zadanie2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cbImageResolution.DataSource = new [] {new Resolution(300, 300),new Resolution(500,500),new Resolution(700,700), new Resolution(600,400)};
            cbImageResolution.SelectedIndex = 2;
            comboBoxShape.DataSource = new[]
            {
                "Prostokąt", "Elipsa", "Wielokąt"
            };
            comboBoxShape.SelectedIndex = -1;
        }

        int mouseStartX, mouseStartY, mouseEndX, mouseEndY;

        public List<Figure> Figures = new List<Figure>();

        public void PaintComponent(Graphics g)
        {
            LinearGradientBrush gradient = new LinearGradientBrush( new Point(70, 70), new Point(150, 150), Color.Red, Color.Blue);
            g.FillEllipse(gradient,70, 70, 100, 100);
        }


        //private void pictureBox1_Paint(object sender, PaintEventArgs e)
        //{
        //    PaintComponent(e.Graphics);
        //}

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

        private void pbImage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseStartX = e.X;
            mouseStartY = e.Y;
            textBoxStart.Text = "X: " + mouseStartX + " Y: " + mouseStartY;
            textBoxStart.Refresh();
            pictureBox1.Refresh();
        }

        private void pbImage_MouseUp(object sender, MouseEventArgs e)
        {
            mouseEndX = e.X;
            mouseEndY = e.Y;
            textBoxEnd.Text = "X: " + mouseEndX + " Y: " + mouseEndY;
            textBoxEnd.Refresh();
            pictureBox1.Refresh();
            var myRectangle = new MyRectangle(this)
            {
                StartPoint = new Point(mouseStartX, mouseStartY),
                EndPoint = new Point(mouseEndX, mouseEndY),
            };
            if(comboBoxShape.SelectedIndex != -1)
                AddToFigureList(myRectangle);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (mouseStartX != default(int) && mouseStartY !=default(int) || comboBoxShape.SelectedIndex != -1)
            {
                // We first cast the "Image" property of the pbImage picture box control
                // into a Bitmap object.
                var pbImageBitmap = (Bitmap)(pictureBox1.Image);
                // Obtain a Graphics object from the Bitmap object.
                var graphics = Graphics.FromImage((Image)pbImageBitmap);

                var whitePen = new Pen(Color.White, 1) {DashStyle = DashStyle.Dash};

                // Show the coordinates of the mouse click on the label, label1.
                if(comboBoxShape.SelectedIndex == 0)
                    DrawRectangle(e,whitePen);

                // Refresh the picture box control in order that
                // our graphics operation can be rendered.
                pictureBox1.Refresh();

                // Calling Dispose() is like calling the destructor of the respective object.
                // Dispose() clears all resources associated with the object, but the object still remains in memory
                // until the system garbage-collects it.
                graphics.Dispose();
            }
        }

        private void DrawRectangle(PaintEventArgs e,Pen p)
        {
            var rect = new Rectangle(mouseStartX, mouseStartY, mouseEndX - mouseStartX, mouseEndY - mouseStartY);

            // Draw the rectangle, starting with the given coordinates, on the picture box.
            e.Graphics.DrawRectangle(p, rect);
        }
        private void AddToFigureList(Figure figure)
        { 
            Figures.Add(figure);
            ReloadPanel();
            panelShapes.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (FigurePanel panel in panelShapes.Controls)
            {
                if (panel.Checked)
                {
                    ChangeColor(panel.Figure);
                  Figures.Remove(panel.Figure);
                }
            }
            ReloadPanel();
        }

        private void ChangeColor(Figure panelFigure)
        {
            var resolution = (Resolution)cbImageResolution.SelectedItem;
            var image = CreateHelperImage(panelFigure,resolution);

            for (var i = 0; i < resolution.Y; i++)
                for (var j = 0; j < resolution.X; j++)
                {
                    var color =image.GetPixel(j, i);
                    if (image.GetPixel(j,i) == Color.FromArgb(255,0,0,0))
                        ((Bitmap)pictureBox1.Image).SetPixel(j, i,Color.White);
                }
            pictureBox1.Refresh();

        }

        private Bitmap CreateHelperImage(Figure panelFigure, Resolution resolution)
        {
            var bmp = new Bitmap(resolution.X, resolution.Y, PixelFormat.Format24bppRgb);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, resolution.X, resolution.Y);
                graphics.FillRectangle(Brushes.Black, ((MyRectangle)panelFigure).StartPoint.X, ((MyRectangle)panelFigure).StartPoint.Y, ((MyRectangle)panelFigure).EndPoint.X - ((MyRectangle)panelFigure).StartPoint.X, ((MyRectangle)panelFigure).EndPoint.Y - ((MyRectangle)panelFigure).StartPoint.Y);
            }
            return bmp;
        }

        private void ReloadPanel()
        {
            panelShapes.Controls.Clear();
            foreach (var figure in Figures)
                {
                    var f = new FigurePanel(figure);
                    f.PictureBox.Image = figure.FigureImage;
                    f.Dock = DockStyle.Top;
                    panelShapes.Controls.Add(f);
                    f.PictureBox.Refresh();
            }
        }

        public void SetRectanglePoints(MyRectangle rectangle)
        {
            mouseStartX = rectangle.StartPoint.X;
            mouseStartY = rectangle.StartPoint.Y;
            mouseEndX = rectangle.EndPoint.X;
            mouseEndY = rectangle.EndPoint.Y;
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

    
    public abstract class Figure
    {
        public Image FigureImage { get; set; }
        public Form1 MainForm;
    }

    public class MyRectangle : Figure
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public MyRectangle(Form1 form)
        {
            FigureImage = Form1.ResizeImage(Resources.rectangle, 30, 30);
            MainForm = form;
        }
    }
}
