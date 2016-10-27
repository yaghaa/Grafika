using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Grafika_Zadanie2.Properties;
using Newtonsoft.Json;
using Rectangle = System.Drawing.Rectangle;

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
            KeyPreview = true;
        }

        int mouseStartX, mouseStartY, mouseEndX, mouseEndY;
        int ellipseStartX, ellipseStartY, ellipseFirstX, ellipseFirstY, ellipseSecondX, ellipseSecondY;
        private int ellipseMouseClickCount = 0;
        private List<Point> polygonPoints = new List<Point>();

        public List<Figure> Figures = new List<Figure>();

        //GRADIENT ELLIPSE
        public void PaintComponent(Graphics g)
        {
            LinearGradientBrush gradient = new LinearGradientBrush( new Point(70, 70), new Point(150, 150), Color.Red, Color.Blue);
            g.FillEllipse(gradient,70, 70, 100, 100);
        }

        //IMPORT IMAGE
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

        //MOUSE EVENTS
        private void pbImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (comboBoxShape.SelectedIndex == 0)
            {
                RectangleMouseDown(e);
            }
            if (comboBoxShape.SelectedIndex == 1)
            {
                EllipseMouseDown(e);
            }
            if (comboBoxShape.SelectedIndex == 2)
            {
                PolygonMouseDown(e);
            }
            pictureBox1.Refresh();
        }

        private void MouseLeaveEvent(object sender, KeyEventArgs e)
        {
            if (polygonPoints.Count >= 3 && e.Control)
            {
                var myPolygon = new MyPolygon(this)
                {
                    Points = new List<Point>(),
                    IsPolygon = true
                };
                myPolygon.Points = polygonPoints.Select(x => x).ToList();
                AddToFigureList(myPolygon);
                pictureBox1.Refresh();
                polygonPoints.Clear();
            }
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            MouseLeaveEvent(sender, e);
        }


        private void PolygonMouseDown(MouseEventArgs e)
        {
            var control = panelShapes.Controls.Cast<FigurePanel>().ToList();
            if (control.Any(x=> x.Checked && x.Figure.IsPolygon))
                return;

            polygonPoints.Add(new Point
            {
                X = e.X,
                Y = e.Y
            });
        }

        public void PolygonPointsClear()
        {
            polygonPoints.Clear();
        }

        private void EllipseMouseDown(MouseEventArgs e)
        {
            if (ellipseMouseClickCount == 0)
            {
                ellipseStartX = ellipseStartY = ellipseFirstY = ellipseFirstX = ellipseSecondX = ellipseSecondY = default(int);
                ellipseStartX = e.X;
                ellipseStartY = e.Y;
                ellipseMouseClickCount++;
            }
            else if (ellipseMouseClickCount == 1)
            {
                ellipseFirstX = e.X;
                ellipseFirstY = e.Y;
                ellipseMouseClickCount++;
            }
            else if (ellipseMouseClickCount == 2)
            {
                ellipseSecondX = e.X;
                ellipseSecondY = e.Y;
                ellipseMouseClickCount = 0;
                
                var myEllipse = new MyEllipse(this)
                {
                    CenterPoint = new Point(ellipseStartX, ellipseStartY),
                    FirstPoint = new Point(ellipseFirstX, ellipseFirstY),
                    SeckondPoint = new Point(ellipseSecondX, ellipseSecondY),
                    IsEllipse = true
                };
                AddToFigureList(myEllipse);
                
                pictureBox1.Refresh();
            }
            
            pictureBox1.Refresh();
        }

        private void RectangleMouseDown(MouseEventArgs e)
        {
            mouseStartX = mouseStartY = mouseEndX = mouseEndY = default(int);
            mouseStartX = e.X;
            mouseStartY = e.Y;
            textBoxStart.Text = "X: " + mouseStartX + " Y: " + mouseStartY;
            textBoxStart.Refresh();
            pictureBox1.Refresh();
        }

        private void pbImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (comboBoxShape.SelectedIndex == 0)
            {
                mouseEndX = e.X;
                mouseEndY = e.Y;
                textBoxEnd.Text = "X: " + mouseEndX + " Y: " + mouseEndY;
                textBoxEnd.Refresh();
                pictureBox1.Refresh();

                if (comboBoxShape.SelectedIndex == 0)
                {
                    var myRectangle = new MyRectangle(this)
                    {
                        StartPoint = new Point(mouseStartX, mouseStartY),
                        EndPoint = new Point(mouseEndX, mouseEndY),
                        IsRectangle = true
                    };
                    AddToFigureList(myRectangle);
                    
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if ((mouseStartX != default(int) && mouseStartY !=default(int) 
                //&& ellipseStartX != default(int) && ellipseStartY != default(int) && ellipseFirstX != default(int) && ellipseFirstY != default(int) && ellipseSecondX != default(int) && ellipseSecondY != default(int)
                || comboBoxShape.SelectedIndex != -1) && pictureBox1.Image !=null)
            {
                // We first cast the "Image" property of the pbImage picture box control
                // into a Bitmap object.
                var pbImageBitmap = (Bitmap)(pictureBox1.Image);
                // Obtain a Graphics object from the Bitmap object.
                var graphics = Graphics.FromImage((Image)pbImageBitmap);

                var whitePen = new Pen(Color.White, 1) {DashStyle = DashStyle.Dash};

                if(comboBoxShape.SelectedIndex == 0)
                    DrawRectangle(e,whitePen);

                if (comboBoxShape.SelectedIndex == 1)
                    DrawEllipse(e, whitePen);

                if (comboBoxShape.SelectedIndex == 2)
                    DrawPolygon(e, whitePen);

                pictureBox1.Refresh();
                graphics.Dispose();
            }
        }

        private void DrawPolygon(PaintEventArgs e, Pen p)
        {
            if (polygonPoints.Count >= 3)
            {
                e.Graphics.DrawPolygon(p, polygonPoints.ToArray());
            }
            
        }

        private void DrawRectangle(PaintEventArgs e,Pen p)
        {
            var rect = new Rectangle(mouseStartX, mouseStartY, mouseEndX - mouseStartX, mouseEndY - mouseStartY);

            // Draw the rectangle, starting with the given coordinates, on the picture box.
            e.Graphics.DrawRectangle(p, rect);
        }

        private void DrawEllipse(PaintEventArgs e, Pen p)
        {
            var ellipseRect = new Rectangle(ellipseStartX- (ellipseSecondX - ellipseStartX), ellipseFirstY, 2*(ellipseSecondX - ellipseStartX), 2*(ellipseStartY - ellipseFirstY));

            // Draw the rectangle, starting with the given coordinates, on the picture box.
            e.Graphics.DrawEllipse(p, ellipseRect);
        }

        private void AddToFigureList(Figure figure)
        { 
            Figures.Add(figure);
            ReloadPanel();
            panelShapes.Refresh();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (Figures.Count !=0)
            {
                WriteToJsonFile<List<Figure>>("..\\..\\salesmen.txt", Figures);
            }
        }


        private void bUpload_Click(object sender, EventArgs e)
        {

            var rectanglesFromFile = ReadFromJsonFile<List<MyRectangle>>("..\\..\\salesmen.txt").Where(x=> x.IsRectangle);
            var elipsesFromFile = ReadFromJsonFile<List<MyEllipse>>("..\\..\\salesmen.txt").Where(x=> x.IsEllipse);
            var polygonFromFile = ReadFromJsonFile<List<MyPolygon>>("..\\..\\salesmen.txt").Where(x=> x.IsPolygon);
            Figures.Clear();
            Figures = Figures.Concat(elipsesFromFile).ToList();
            Figures = Figures.Concat(rectanglesFromFile).ToList();
            Figures = Figures.Concat(polygonFromFile).ToList();

            Figures.ForEach(x=> x.MainForm = this);
            ReloadPanel();
        }

        
        //DELETE FIGURE FROM LIST
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (FigurePanel panel in panelShapes.Controls)
            {
                if (panel.Checked)
                {
                    if (panel.Figure.IsPolygon)
                    {
                        polygonPoints = ((MyPolygon) panel.Figure).Points.Select(x=> x).ToList();
                    }
                    ChangeColor(panel.Figure);
                  Figures.Remove(panel.Figure);

                }
            }
            polygonPoints.Clear();
            ReloadPanel();
        }

        //DELETE IMAGE SCRAP
        private void ChangeColor(Figure panelFigure)
        {
            var resolution = (Resolution)cbImageResolution.SelectedItem;
            var image = CreateHelperImage(panelFigure,resolution);

            for (var i = 0; i < resolution.Y; i++)
                for (var j = 0; j < resolution.X; j++)
                {
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
                if (panelFigure.IsRectangle)
                {
                    graphics.FillRectangle(Brushes.White, 0, 0, resolution.X, resolution.Y);
                    graphics.FillRectangle(Brushes.Black, ((MyRectangle)panelFigure).StartPoint.X, ((MyRectangle)panelFigure).StartPoint.Y, ((MyRectangle)panelFigure).EndPoint.X - ((MyRectangle)panelFigure).StartPoint.X, ((MyRectangle)panelFigure).EndPoint.Y - ((MyRectangle)panelFigure).StartPoint.Y);

                }
                if (panelFigure.IsEllipse)
                {
                    var e = (MyEllipse) panelFigure;
                    graphics.FillRectangle(Brushes.White, 0, 0, resolution.X, resolution.Y);
                    graphics.FillEllipse(Brushes.Black, (e.CenterPoint.X+e.CenterPoint.X-e.SeckondPoint.X), e.FirstPoint.Y, 2*(e.SeckondPoint.X-e.CenterPoint.X), 2*(e.CenterPoint.Y-e.FirstPoint.Y));

                }
                if (panelFigure.IsPolygon)
                {
                    var e = (MyPolygon)panelFigure;
                    graphics.FillRectangle(Brushes.White, 0, 0, resolution.X, resolution.Y);
                    graphics.FillPolygon(Brushes.Black, polygonPoints.ToArray());

                }
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
            pictureBox1.Refresh();
        }

        public void SetEllipsePoints(MyEllipse ellipse)
        {
            ellipseStartX = ellipse.CenterPoint.X;
            ellipseStartY = ellipse.CenterPoint.Y;
            ellipseFirstX = ellipse.FirstPoint.X;
            ellipseFirstY = ellipse.FirstPoint.Y;
            ellipseSecondX = ellipse.SeckondPoint.X;
            ellipseSecondY = ellipse.SeckondPoint.Y;
            pictureBox1.Refresh();
        }

        public void SetPolygonPoints(MyPolygon polygon)
        {
            polygonPoints = polygon.Points.Select(x=> x).ToList();
            pictureBox1.Refresh();
        }




        //// SAVE TO FILE JSON

        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
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

    [Serializable]
    public class Figure
    {
        [JsonIgnore]
        public Image FigureImage { get; set; }
        [JsonIgnore]
        public Form1 MainForm;

        public bool IsRectangle { get; set; }
        public bool IsEllipse { get; set; }
        public bool IsPolygon { get; set; }
    }

    [Serializable]
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

    [Serializable]
    public class MyEllipse : Figure
    {
        public Point CenterPoint { get; set; }

        public Point FirstPoint { get; set; }

        public Point SeckondPoint { get; set; }

        public MyEllipse(Form1 form)
        {
            FigureImage = Form1.ResizeImage(Resources.ellipse, 30, 30);
            MainForm = form;
        }
    }

    [Serializable]
    public class MyPolygon : Figure
    {
        public List<Point> Points { get; set; }
        
        public MyPolygon(Form1 form)
        {
            FigureImage = Form1.ResizeImage(Resources.polygon, 30, 30);
            MainForm = form;
        }
    }



}
