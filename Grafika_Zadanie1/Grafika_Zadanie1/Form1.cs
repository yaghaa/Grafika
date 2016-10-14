using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Grafika_Zadanie1.Patterns;

namespace Grafika_Zadanie1
{
  public partial class Form1 : Form
  {
    public Bitmap MyImage { get; set; }
    private DartBoard _dartBoard = new DartBoard();
    private CratePatterns _cratePatterns = new CratePatterns();
    private Color _color1 = new Color();
    private Color _color2 = new Color();
    private Color _color3 = new Color();
    private Color _color4 = new Color();
    private Color _color5 = new Color();
    private Color _color6 = new Color();

    public Form1()
    {
      InitializeComponent();
      _color1 = _color3 = _color5 = Color.Black;
      _color2 = _color3 = _color5 = Color.Bisque;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var image = _dartBoard.DartBoardWithBlur(50, 10);
      panel1.BackgroundImage = image;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      var x = 10;
      int crateLineWidth = 10;
      int xAxisDistance = 40;
      int yAxisDistance = 50;
      int outParse1 = 0;
      int outParse2 = 0;
      int outParse3 = 0;

      if (Int32.TryParse(textBox4.Text, out outParse1) && textBox4.Text != String.Empty)
      {
        crateLineWidth = Int32.Parse(textBox4.Text);
      }
      else
      {
        MessageBox.Show("Podaj wartość liczbową całkowitą SZEROKOŚCI", "Błąd");
      }

      if (Int32.TryParse(textBox3.Text, out outParse2) && textBox3.Text != String.Empty)
      {
        xAxisDistance = Int32.Parse(textBox3.Text);
      }
      else
      {
        MessageBox.Show("Podaj wartość liczbową całkowitą ODLEGŁOŚCI X", "Błąd");
      }

      if (Int32.TryParse(textBox5.Text, out outParse3) && textBox5.Text != String.Empty)
      {
        yAxisDistance = Int32.Parse(textBox5.Text);
      }
      else
      {
        MessageBox.Show("Podaj wartość liczbową całkowitą ODLEGŁOŚCI Y", "Błąd");
      }

      var image = _cratePatterns.Crate(_color1, _color2, crateLineWidth, xAxisDistance, yAxisDistance,MyImage);
      panel1.BackgroundImage = image;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      var x = 10;
      int squareSize = 20;
      int resolution = 250000;
      int outParse1 = 0;
      int outParse2 = 0;

      if (Int32.TryParse(textBox8.Text, out outParse1) && textBox8.Text != String.Empty)
      {
        squareSize = Int32.Parse(textBox8.Text);
      }
      else
      {
        MessageBox.Show("Podaj wartość liczbową całkowitą WIELKOŚĆ", "Błąd");
      }

      if (Int32.TryParse(textBox7.Text, out outParse2) && textBox7.Text != String.Empty)
      {
        resolution = Int32.Parse(textBox7.Text);
      }
      else
      {
        MessageBox.Show("Podaj wartość liczbową całkowitą ROZDZIELCZOŚĆ", "Błąd");
      }

      var image = _cratePatterns.ChessBoard(_color3, _color4, squareSize, resolution, MyImage);
      panel1.BackgroundImage = image;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      var x = 10;
      int squareSize = 200;
      int resolution = 250000;
      int outParse1 = 0;
      int outParse2 = 0;

      if (Int32.TryParse(textBox9.Text, out outParse1) && textBox9.Text != String.Empty)
      {
        squareSize = Int32.Parse(textBox9.Text);
      }
      else
      {
        MessageBox.Show("Podaj wartość liczbową całkowitą WIELKOŚĆ", "Błąd");
      }

      if (Int32.TryParse(textBox6.Text, out outParse2) && textBox6.Text != String.Empty)
      {
        resolution = Int32.Parse(textBox6.Text);
      }
      else
      {
        MessageBox.Show("Podaj wartość liczbową całkowitą ROZDZIELCZOŚĆ", "Błąd");
      }

      var image = _cratePatterns.ChessBoardRotate(_color5, _color6, squareSize, resolution);
      panel1.BackgroundImage = image;
    }

    private void button6_Click(object sender, EventArgs e)
    {
      ColorDialog MyDialog = new ColorDialog();
      // Keeps the user from selecting a custom color.
      MyDialog.AllowFullOpen = false;
      // Allows the user to get help. (The default is false.)
      MyDialog.ShowHelp = true;
      // Sets the initial color select to the current text color.
      MyDialog.Color = button6.BackColor;

      // Update the text box color if the user clicks OK
      if (MyDialog.ShowDialog() == DialogResult.OK)
      {
        button6.BackColor = MyDialog.Color;
        _color1 = MyDialog.Color;
      }
    }

    private void button7_Click(object sender, EventArgs e)
    {
      ColorDialog MyDialog = new ColorDialog();
      // Keeps the user from selecting a custom color.
      MyDialog.AllowFullOpen = false;
      // Allows the user to get help. (The default is false.)
      MyDialog.ShowHelp = true;
      // Sets the initial color select to the current text color.
      MyDialog.Color = button7.BackColor;

      // Update the text box color if the user clicks OK
      if (MyDialog.ShowDialog() == DialogResult.OK)
      {
        button7.BackColor = MyDialog.Color;
        _color2 = MyDialog.Color;
      }
    }

    private void button9_Click(object sender, EventArgs e)
    {
      ColorDialog MyDialog = new ColorDialog();
      // Keeps the user from selecting a custom color.
      MyDialog.AllowFullOpen = false;
      // Allows the user to get help. (The default is false.)
      MyDialog.ShowHelp = true;
      // Sets the initial color select to the current text color.
      MyDialog.Color = button9.BackColor;

      // Update the text box color if the user clicks OK
      if (MyDialog.ShowDialog() == DialogResult.OK)
      {
        button9.BackColor = MyDialog.Color;
        _color3 = MyDialog.Color;
      }
    }

    private void button8_Click_1(object sender, EventArgs e)
    {
      ColorDialog MyDialog = new ColorDialog();
      // Keeps the user from selecting a custom color.
      MyDialog.AllowFullOpen = false;
      // Allows the user to get help. (The default is false.)
      MyDialog.ShowHelp = true;
      // Sets the initial color select to the current text color.
      MyDialog.Color = button8.BackColor;

      // Update the text box color if the user clicks OK
      if (MyDialog.ShowDialog() == DialogResult.OK)
      {
        button8.BackColor = MyDialog.Color;
        _color4 = MyDialog.Color;
      }
    }

    private void button11_Click(object sender, EventArgs e)
    {
      ColorDialog MyDialog = new ColorDialog();
      // Keeps the user from selecting a custom color.
      MyDialog.AllowFullOpen = false;
      // Allows the user to get help. (The default is false.)
      MyDialog.ShowHelp = true;
      // Sets the initial color select to the current text color.
      MyDialog.Color = button11.BackColor;

      // Update the text box color if the user clicks OK
      if (MyDialog.ShowDialog() == DialogResult.OK)
      {
        button11.BackColor = MyDialog.Color;
        _color5 = MyDialog.Color;
      }
    }

    private void button10_Click(object sender, EventArgs e)
    {
      ColorDialog MyDialog = new ColorDialog();
      // Keeps the user from selecting a custom color.
      MyDialog.AllowFullOpen = false;
      // Allows the user to get help. (The default is false.)
      MyDialog.ShowHelp = true;
      // Sets the initial color select to the current text color.
      MyDialog.Color = button10.BackColor;

      // Update the text box color if the user clicks OK
      if (MyDialog.ShowDialog() == DialogResult.OK)
      {
        button10.BackColor = MyDialog.Color;
        _color6 = MyDialog.Color;
      }
    }

        private void button5_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            MyImage = new Bitmap(Image.FromFile(fileDialog.FileNames[0]));
            
            panel1.BackgroundImage = MyImage;
        }
    }
} 