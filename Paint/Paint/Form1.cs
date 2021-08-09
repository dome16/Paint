using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Paint
{
    //Wenn am Panel neue sachen gemacht werden ganzes namespace auskommentieren 
    //im Form1.cs [Design] Zeile 32 auf ... ändern
    // this.panel1 = new System.Windows.Forms.Panel();
    public class TPanel : Panel
    {
        public TPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
    public partial class Form1 : Form
    {
        //Variablen
        int x = -1;
        int y = -1;
        bool bewegung = false;
        Pen pen;
        Bitmap surface;
        Graphics graph;
        Image File;


        public Form1()
        {         
            InitializeComponent();
            surface = new Bitmap(panel1.Height, panel1.Width);
            panel1.BackgroundImage = surface;
            panel1.BackgroundImageLayout = ImageLayout.None;
            graph = Graphics.FromImage(surface);
            graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            bewegung = true;
            x = e.X;
            y = e.Y;
            panel1.Cursor = Cursors.Cross;
            panel1.Invalidate();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bewegung && x!=-1 && y!=-1)
            {
                graph.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
                panel1.Invalidate();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            bewegung = false;
            x = -1;
            y = -1;
            panel1.Cursor = Cursors.Default;
            panel1.Invalidate();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            //surface.Save(s,System.Drawing.Imaging.ImageFormat.Png);
            //s = "drawing"+i;
            //i++;
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "JPG(*.JPG)|*.jpg";

            if (s.ShowDialog() == DialogResult.OK)
            {
                surface.Save(s.FileName);
            }

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            pen.Color = p.BackColor;
            pen.Width = 5;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            pen.Color = p.BackColor;
            pen.Width = 50;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*.JPG)|*.jpg";

            if (f.ShowDialog() == DialogResult.OK)
            {
                Bitmap loadfile = new Bitmap(f.FileName);
                surface = loadfile;
               
            }
        }
    }
}
