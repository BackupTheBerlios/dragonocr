using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;

using dragonfly.OCR;

namespace OCR_Image_Manipulation
{
    /// <summary>
    /// klasse
    /// </summary>
    public partial class Form1 : Form
    {
        OpticalCharacterRecognitation imgrec;
        Detector dr;
        /// <summary>
        /// konstruktor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imageBox.Load(ofd.FileName);
            }
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPG Files|*.jpg|BMP Files|*.bmp|PNG Files|*.png";
            sfd.AddExtension = true;
            sfd.FilterIndex = 3;
            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(imageBox.Image);
                bmp.Save(sfd.FileName);
            }
        }

        private void kontratmaximierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        
        private void erstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(1024, 768);
            Random r = new Random(Environment.TickCount);
            int wert = 0;
            Color c;

            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    wert = r.Next(0,765)/3;
                    c = Color.FromArgb(wert, wert, wert);
                    b.SetPixel(x, y, c);
                }
            }
            imageBox.Image = (Image)b;
        }
        

        private void rauschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
   
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.imageBox.Image = Image.FromFile(@"C:\OCR\OCR-A.png");
            
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            imgrec = new OpticalCharacterRecognitation(imageBox.Image);
            imgrec.ContrastRefValue = Convert.ToByte(hexwert.Text, 16);
            imgrec.ImageNoiseRefValue = Convert.ToSingle(blackfactor.Text);
            imgrec.ImageNoiseSphere = Convert.ToSByte(width.Text);

            imgrec.ImproveImage();

            imageBox.Image = (Image)imgrec.Image;
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            dr = new Detector(imgrec.Image);
            dr.getWords();
        }

    

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            foreach (Bitmap b in dr.Lines)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "JPG Files|*.jpg|BMP Files|*.bmp|PNG Files|*.png";
                sfd.AddExtension = true;
                sfd.FilterIndex = 3;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    
                    b.Save(sfd.FileName);
                }
            }
            MessageBox.Show("test");
            foreach (Bitmap b in dr.Words)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "JPG Files|*.jpg|BMP Files|*.bmp|PNG Files|*.png";
                sfd.AddExtension = true;
                sfd.FilterIndex = 3;

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    b.Save(sfd.FileName);
                }
            }
        }

    }
}