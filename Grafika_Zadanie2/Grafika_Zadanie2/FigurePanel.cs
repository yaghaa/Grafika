using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Zadanie2
{
    public partial class FigurePanel : UserControl
    {
        public Figure Figure { get; set; }
        public bool Checked { get; set; }

        public FigurePanel(Figure figure)
        {
            InitializeComponent();
            Figure = figure;
            Checked = true;
        }

        public PictureBox PictureBox
        {
            get { return pictureBoxFigure; }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ControlClicked();
        }

        private void ControlClicked()
        {
            Checked = !Checked;
            this.BackColor = (Checked) ? Color.Red : Color.Aquamarine;
            Parent.Refresh();

            if (Checked)
            {
                if (Figure is MyRectangle)
                    Figure.MainForm.SetRectanglePoints((MyRectangle) Figure);

                if (Figure is MyEllipse)
                    Figure.MainForm.SetEllipsePoints((MyEllipse) Figure);

                if (Figure is MyPolygon)
                    Figure.MainForm.SetPolygonPoints((MyPolygon) Figure);
            }
            else
            {
                if (Figure is MyPolygon)
                    Figure.MainForm.PolygonPointsClear();
            }
                
        }

        private void FigurePanel_Load(object sender, EventArgs e)
        {
            ControlClicked();
        }
    }
}
