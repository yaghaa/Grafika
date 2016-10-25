namespace Grafika_Zadanie2
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelShapes = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxShape = new System.Windows.Forms.ComboBox();
            this.textBoxEnd = new System.Windows.Forms.TextBox();
            this.textBoxStart = new System.Windows.Forms.TextBox();
            this.cbImageResolution = new System.Windows.Forms.ComboBox();
            this.bImportImage = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(322, 345);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelShapes);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBoxShape);
            this.groupBox1.Controls.Add(this.textBoxEnd);
            this.groupBox1.Controls.Add(this.textBoxStart);
            this.groupBox1.Controls.Add(this.cbImageResolution);
            this.groupBox1.Controls.Add(this.bImportImage);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(322, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 445);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // panelShapes
            // 
            this.panelShapes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShapes.AutoScroll = true;
            this.panelShapes.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelShapes.Location = new System.Drawing.Point(41, 172);
            this.panelShapes.Name = "panelShapes";
            this.panelShapes.Size = new System.Drawing.Size(44, 241);
            this.panelShapes.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(41, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Usuń";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxShape
            // 
            this.comboBoxShape.FormattingEnabled = true;
            this.comboBoxShape.Location = new System.Drawing.Point(41, 136);
            this.comboBoxShape.Name = "comboBoxShape";
            this.comboBoxShape.Size = new System.Drawing.Size(121, 21);
            this.comboBoxShape.TabIndex = 5;
            // 
            // textBoxEnd
            // 
            this.textBoxEnd.Location = new System.Drawing.Point(41, 110);
            this.textBoxEnd.Name = "textBoxEnd";
            this.textBoxEnd.ReadOnly = true;
            this.textBoxEnd.Size = new System.Drawing.Size(100, 20);
            this.textBoxEnd.TabIndex = 3;
            // 
            // textBoxStart
            // 
            this.textBoxStart.Location = new System.Drawing.Point(41, 84);
            this.textBoxStart.Name = "textBoxStart";
            this.textBoxStart.ReadOnly = true;
            this.textBoxStart.Size = new System.Drawing.Size(100, 20);
            this.textBoxStart.TabIndex = 2;
            // 
            // cbImageResolution
            // 
            this.cbImageResolution.FormattingEnabled = true;
            this.cbImageResolution.Location = new System.Drawing.Point(41, 57);
            this.cbImageResolution.Name = "cbImageResolution";
            this.cbImageResolution.Size = new System.Drawing.Size(121, 21);
            this.cbImageResolution.TabIndex = 1;
            // 
            // bImportImage
            // 
            this.bImportImage.Location = new System.Drawing.Point(66, 19);
            this.bImportImage.Name = "bImportImage";
            this.bImportImage.Size = new System.Drawing.Size(75, 23);
            this.bImportImage.TabIndex = 0;
            this.bImportImage.Text = "Dodaj obraz";
            this.bImportImage.UseVisualStyleBackColor = true;
            this.bImportImage.Click += new System.EventHandler(this.bImportImage_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 345);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 445);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bImportImage;
        private System.Windows.Forms.ComboBox cbImageResolution;
        private System.Windows.Forms.TextBox textBoxEnd;
        private System.Windows.Forms.TextBox textBoxStart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxShape;
        private System.Windows.Forms.Panel panelShapes;
    }
}

