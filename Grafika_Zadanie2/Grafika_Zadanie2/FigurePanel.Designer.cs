namespace Grafika_Zadanie2
{
    partial class FigurePanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxFigure = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFigure)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxFigure
            // 
            this.pictureBoxFigure.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxFigure.Location = new System.Drawing.Point(5, 5);
            this.pictureBoxFigure.Name = "pictureBoxFigure";
            this.pictureBoxFigure.Size = new System.Drawing.Size(30, 30);
            this.pictureBoxFigure.TabIndex = 0;
            this.pictureBoxFigure.TabStop = false;
            this.pictureBoxFigure.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FigurePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aquamarine;
            this.Controls.Add(this.pictureBoxFigure);
            this.Name = "FigurePanel";
            this.Size = new System.Drawing.Size(40, 40);
            this.Load += new System.EventHandler(this.FigurePanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFigure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxFigure;
    }
}
