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
	public partial class MainForm : Form
	{
		Graphics gr;
		Bitmap bitmap;
		Quaternion loc = new Quaternion(-3, 0, 0, -3); // координаты камеры
		Quaternion vel = new Quaternion(0); // скрость камеры
		double k = 1086; // масштаб
		double a = 2.5; // глубина
		int t = 0;
		Quaternion rot1 = new Quaternion(1);
		Quaternion rot2 = new Quaternion(1);
		public MainForm()
		{
			InitializeComponent();
			bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			gr = Graphics.FromImage(bitmap);
			gr.Clear(Color.White);
			pictureBox1.Image = bitmap;
			timer1.Start();
		}
		
		private void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D) vel.Im = 1;
			if (e.KeyCode == Keys.A) vel.Im = -1;
			if (e.KeyCode == Keys.Space) vel.Jm = 1;
			if (e.KeyCode == Keys.ShiftKey) vel.Jm = -1;
			if (e.KeyCode == Keys.W) vel.Km = 1;
			if (e.KeyCode == Keys.S) vel.Km = -1;
			if (e.KeyCode == Keys.R) vel.Re = 1;
			if (e.KeyCode == Keys.F) vel.Re = -1;
			if (e.KeyCode == Keys.T) k *= 1.01;
			if (e.KeyCode == Keys.G) k /= 1.01;
			//if (e.KeyCode == Keys.T) a += 0.01;
			//if (e.KeyCode == Keys.G) a -= 0.01;
			if (e.KeyCode == Keys.Z) rot1.RotateW(new Quaternion(1, 0, 0), 0.01);
			if (e.KeyCode == Keys.X) rot1.RotateW(new Quaternion(0, 1, 0), 0.01);
			if (e.KeyCode == Keys.C) rot1.RotateW(new Quaternion(0, 0, 1), 0.01);
			if (e.KeyCode == Keys.V) rot2.RotateW(new Quaternion(1, 0, 0), 0.01);
			if (e.KeyCode == Keys.B) rot2.RotateW(new Quaternion(0, 1, 0), 0.01);
			if (e.KeyCode == Keys.N) rot2.RotateW(new Quaternion(1, 0, 0), 0.01);
		}

		private void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D) vel.Im = 0;
			if (e.KeyCode == Keys.A) vel.Im = 0;
			if (e.KeyCode == Keys.Space) vel.Jm = 0;
			if (e.KeyCode == Keys.ShiftKey) vel.Jm = 0;
			if (e.KeyCode == Keys.W) vel.Km = 0;
			if (e.KeyCode == Keys.S) vel.Km = 0;
			if (e.KeyCode == Keys.R) vel.Re = 0;
			if (e.KeyCode == Keys.F) vel.Re = 0;
		}

		private void MainFormSizeChanged(object sender, EventArgs e)
		{
			if (pictureBox1.Height > 0 && pictureBox1.Height > 0)
			{
				bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
				gr = Graphics.FromImage(bitmap);
			}
		}
		
		private void Timer1Tick(object sender, EventArgs e)
		{
			loc.Im += vel.Im / 25;
			loc.Jm += vel.Jm / 25;
			loc.Km += vel.Km / 25;
			loc.Re += vel.Re / 25;
			gr.Clear(Color.White);
			try
			{
				if (loc.Km * loc.Km + loc.Re * loc.Re >= 0)
				{
				/*if ((1 - z) > 0)                */ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1,  1)), draw_calculation(new Quaternion( 1, -1,  1,  1)));
				/*if ((1 - z) > 0)                */ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1,  1)), draw_calculation(new Quaternion( 1,  1, -1,  1)));
				/*if ((1 - z) > 0)                */ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1, -1,  1)), draw_calculation(new Quaternion( 1, -1,  1,  1)));
				/*if ((1 - z) > 0)                */ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1, -1,  1)), draw_calculation(new Quaternion( 1,  1, -1,  1)));
				
				/*if ((-1 - z) > 0)               */ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1, -1)), draw_calculation(new Quaternion( 1, -1,  1, -1)));
				/*if ((-1 - z) > 0)               */ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1, -1)), draw_calculation(new Quaternion( 1,  1, -1, -1)));
				/*if ((-1 - z) > 0)               */ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1, -1, -1)), draw_calculation(new Quaternion( 1, -1,  1, -1)));
				/*if ((-1 - z) > 0)               */ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1, -1, -1)), draw_calculation(new Quaternion( 1,  1, -1, -1)));
				
				/*if ((1 - z) > 0 && (-1 - z) > 0)*/ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1, -1)), draw_calculation(new Quaternion( 1,  1,  1,  1)));
				/*if ((1 - z) > 0 && (-1 - z) > 0)*/ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1, -1, -1)), draw_calculation(new Quaternion( 1,  1, -1,  1)));
				/*if ((1 - z) > 0 && (-1 - z) > 0)*/ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1,  1, -1)), draw_calculation(new Quaternion( 1, -1,  1,  1)));
				/*if ((1 - z) > 0 && (-1 - z) > 0)*/ gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1, -1, -1)), draw_calculation(new Quaternion( 1, -1, -1,  1)));
				
				
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1,  1,  1,  1)), draw_calculation(new Quaternion(-1, -1,  1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1,  1,  1,  1)), draw_calculation(new Quaternion(-1,  1, -1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1, -1,  1)), draw_calculation(new Quaternion(-1, -1,  1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1, -1,  1)), draw_calculation(new Quaternion(-1,  1, -1,  1)));
				
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1,  1,  1, -1)), draw_calculation(new Quaternion(-1, -1,  1, -1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1,  1,  1, -1)), draw_calculation(new Quaternion(-1,  1, -1, -1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1, -1, -1)), draw_calculation(new Quaternion(-1, -1,  1, -1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1, -1, -1)), draw_calculation(new Quaternion(-1,  1, -1, -1)));
				
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1,  1,  1, -1)), draw_calculation(new Quaternion(-1,  1,  1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1,  1, -1, -1)), draw_calculation(new Quaternion(-1,  1, -1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1,  1, -1)), draw_calculation(new Quaternion(-1, -1,  1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1, -1, -1)), draw_calculation(new Quaternion(-1, -1, -1,  1)));
				
				
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1,  1)), draw_calculation(new Quaternion(-1,  1,  1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1, -1)), draw_calculation(new Quaternion(-1,  1,  1, -1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1, -1,  1)), draw_calculation(new Quaternion(-1,  1, -1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1, -1, -1)), draw_calculation(new Quaternion(-1,  1, -1, -1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1,  1,  1)), draw_calculation(new Quaternion(-1, -1,  1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1,  1, -1)), draw_calculation(new Quaternion(-1, -1,  1, -1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1, -1,  1)), draw_calculation(new Quaternion(-1, -1, -1,  1)));
				gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1, -1, -1)), draw_calculation(new Quaternion(-1, -1, -1, -1)));
				}

				gr.DrawEllipse(new Pen(Color.Red), (float)(pictureBox1.Width / 2 - k), (float)(pictureBox1.Height / 2 - k), (float)(2 * k), (float)(2 * k));
			} catch { }
			pictureBox1.Image = bitmap;

			label1.Text = "x: " + loc.Im + "\ny: " + loc.Jm + "\nz: " + loc.Km + "\nw: " + loc.Re + "\nмасштаб: " + k + "\nглубина: " + a + "\nвремя: " + t;
			t++;
		}
		
		Point draw_calculation(Quaternion h)
		{
			//Quaternion h0 = h.RotateW(new Quaternion(Math.Sin(0.01 * t), Math.Sin(0.01 * t + 2 * Math.PI / 3), Math.Sin(0.01 * t + 4 * Math.PI / 3)), 1);
			Quaternion h0 = h.Rotate(new Quaternion(1, 0, 0), 0.01 * t).RotateW(new Quaternion(0, 1, 0), 0.01 * t);
			h0 = h0.Rotate(rot1, 1).RotateW(rot2, 1);
			return new Point((int)( (h0.Im - loc.Im) / Rd(h0, loc) * k) + pictureBox1.Width  / 2,
			                 (int)(-(h0.Jm - loc.Jm) / Rd(h0, loc) * k) + pictureBox1.Height / 2);
		}
		
		double Rd(Quaternion h1, Quaternion h2)
		{
			return Math.Sqrt((h1.Im - h2.Im) * (h1.Im - h2.Im) + (h1.Jm - h2.Jm) * (h1.Jm - h2.Jm) + (h1.Km - h2.Km) * (h1.Km - h2.Km) * a * a + (h1.Re - h2.Re) * (h1.Re - h2.Re) * a * a);
		}
	}
}
