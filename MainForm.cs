using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace растеризатор
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		Graphics gr;
        Bitmap bitmap;
        double x = 0, y = 0, z = -5; // координаты камеры
        double vx = 0, vy = 0, vz = 0; // скрость камеры
        double k = 1086; // масштаб
        double a = 2.5; // глубина
        
		public MainForm()
		{
			InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            gr.Clear(Color.White);
            pictureBox1.Image = bitmap;
            timer1.Start();
		}
		
		void MainFormSizeChanged(object sender, EventArgs e)
		{
			if (pictureBox1.Height > 0 && pictureBox1.Height > 0)
            {
                bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                gr = Graphics.FromImage(bitmap);
            }
		}
		
		void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D) vx = 1;
            if (e.KeyCode == Keys.A) vx = -1;
            if (e.KeyCode == Keys.Space) vy = 1;
            if (e.KeyCode == Keys.ShiftKey) vy = -1;
            if (e.KeyCode == Keys.W) vz = 1;
            if (e.KeyCode == Keys.S) vz = -1;
            if (e.KeyCode == Keys.R) k *= 1.01;
            if (e.KeyCode == Keys.F) k /= 1.01;
            //if (e.KeyCode == Keys.T) a += 0.01;
            //if (e.KeyCode == Keys.G) a -= 0.01;
		}
		void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D) vx = 0;
            if (e.KeyCode == Keys.A) vx = 0;
            if (e.KeyCode == Keys.Space) vy = 0;
            if (e.KeyCode == Keys.ShiftKey) vy = 0;
            if (e.KeyCode == Keys.W) vz = 0;
            if (e.KeyCode == Keys.S) vz = 0;
		}
		
		void Timer1Tick(object sender, EventArgs e)
		{
			x += vx / 25;
            y += vy / 25;
            z += vz / 25;
            gr.Clear(Color.White);
            try
            {
                if ((1 - z) > 0)                 gr.DrawLine(new Pen(Color.Black), (float)(( 1 - x) / R( 1,  1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R( 1,  1,  1, x, y, z) * k) + pictureBox1.Height / 2, (float)((-1 - x) / R(-1,  1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R(-1,  1,  1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((1 - z) > 0)                 gr.DrawLine(new Pen(Color.Black), (float)(( 1 - x) / R( 1,  1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R( 1,  1,  1, x, y, z) * k) + pictureBox1.Height / 2, (float)(( 1 - x) / R( 1, -1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R( 1, -1,  1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((1 - z) > 0)                 gr.DrawLine(new Pen(Color.Black), (float)((-1 - x) / R(-1, -1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R(-1, -1,  1, x, y, z) * k) + pictureBox1.Height / 2, (float)((-1 - x) / R(-1,  1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R(-1,  1,  1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((1 - z) > 0)                 gr.DrawLine(new Pen(Color.Black), (float)((-1 - x) / R(-1, -1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R(-1, -1,  1, x, y, z) * k) + pictureBox1.Height / 2, (float)(( 1 - x) / R( 1, -1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R( 1, -1,  1, x, y, z) * k) + pictureBox1.Height / 2);
                
                if ((-1 - z) > 0)                gr.DrawLine(new Pen(Color.Black), (float)(( 1 - x) / R( 1,  1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R( 1,  1, -1, x, y, z) * k) + pictureBox1.Height / 2, (float)((-1 - x) / R(-1,  1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R(-1,  1, -1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((-1 - z) > 0)                gr.DrawLine(new Pen(Color.Black), (float)(( 1 - x) / R( 1,  1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R( 1,  1, -1, x, y, z) * k) + pictureBox1.Height / 2, (float)(( 1 - x) / R( 1, -1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R( 1, -1, -1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((-1 - z) > 0)                gr.DrawLine(new Pen(Color.Black), (float)((-1 - x) / R(-1, -1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R(-1, -1, -1, x, y, z) * k) + pictureBox1.Height / 2, (float)((-1 - x) / R(-1,  1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R(-1,  1, -1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((-1 - z) > 0)                gr.DrawLine(new Pen(Color.Black), (float)((-1 - x) / R(-1, -1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R(-1, -1, -1, x, y, z) * k) + pictureBox1.Height / 2, (float)(( 1 - x) / R( 1, -1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R( 1, -1, -1, x, y, z) * k) + pictureBox1.Height / 2);
                
                if ((1 - z) > 0 && (-1 - z) > 0) gr.DrawLine(new Pen(Color.Black), (float)(( 1 - x) / R( 1,  1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R( 1,  1, -1, x, y, z) * k) + pictureBox1.Height / 2, (float)(( 1 - x) / R( 1,  1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R( 1,  1,  1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((1 - z) > 0 && (-1 - z) > 0) gr.DrawLine(new Pen(Color.Black), (float)(( 1 - x) / R( 1, -1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R( 1, -1, -1, x, y, z) * k) + pictureBox1.Height / 2, (float)(( 1 - x) / R( 1, -1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R( 1, -1,  1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((1 - z) > 0 && (-1 - z) > 0) gr.DrawLine(new Pen(Color.Black), (float)((-1 - x) / R(-1,  1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R(-1,  1, -1, x, y, z) * k) + pictureBox1.Height / 2, (float)((-1 - x) / R(-1,  1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-( 1 - y) / R(-1,  1,  1, x, y, z) * k) + pictureBox1.Height / 2);
                if ((1 - z) > 0 && (-1 - z) > 0) gr.DrawLine(new Pen(Color.Black), (float)((-1 - x) / R(-1, -1, -1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R(-1, -1, -1, x, y, z) * k) + pictureBox1.Height / 2, (float)((-1 - x) / R(-1, -1,  1, x, y, z) * k) + pictureBox1.Width / 2, (float)(-(-1 - y) / R(-1, -1,  1, x, y, z) * k) + pictureBox1.Height / 2);

                gr.DrawEllipse(new Pen(Color.Red), (float)(pictureBox1.Width / 2 - k), (float)(pictureBox1.Height / 2 - k), (float)(2 * k), (float)(2 * k));
            } catch { }
            pictureBox1.Image = bitmap;

            label1.Text = "x: " + x + "\ny: " + y + "\nz: " + z + "\nмасштаб: " + k + "\nглубина: " + a;
		}
		
		double R(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2) * a * a) ;
        }

        float draw_calculation()
        {

            return 0;
        }
	}
}


