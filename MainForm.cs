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
		Quaternion loc = new Quaternion(0, 0, 0, -5); // координаты камеры
		Quaternion vel = new Quaternion(0); // скрость камеры
		double k = 1086; // масштаб
		double a = 2.5; // глубина
		int t = 0;
		Quaternion rot = new Quaternion(0);
		Quaternion rot1 = new Quaternion(1);
		Quaternion rot2 = new Quaternion(1);
		Figure Cube = new Figure("Cube", new Quaternion(0, 0, 0));
		string text = "";
		Point lostE;
		public MainForm()
		{
			InitializeComponent();
			bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			gr = Graphics.FromImage(bitmap);
			gr.Clear(Color.White);
			pictureBox1.Image = bitmap;
			timer1.Start();
			
			this.MouseWheel += new MouseEventHandler(this.Form1_MouseWhel);
			
			//for(int i = 0; i < 16; i++)
			//{
			//	Quaternion h1 = new Quaternion(((i/8)%2 - 0.5)*2, ((i/4)%2 - 0.5)*2, ((i/2)%2 - 0.5)*2, (i%2 - 0.5)*2);
			//	bool j = false; if (i == 0) j = true;
			//	foreach (Quaternion h2 in points)
			//	{
			//		if(h1.Sub(h2).Abs() == 2*Math.Sqrt(1)) j = true;
			//	}
			//	if(j) points.Add(h1);
			//}
			//foreach (Quaternion e in points) text += e.Re + " " + e.Im + " " + e.Jm + " " + e.Km + "\n";
		}
		
		private void Form1_MouseWhel(object sender, MouseEventArgs e)
        {
			
        }
		
		void MainFormMouseMove(object sender, MouseEventArgs e)
		{
			Double r = 1000000;
			rot.Im += (e.X - lostE.X) / r;
			rot.Jm += (e.Y - lostE.Y) / r;
			lostE = e.Location;
		}
		
		void PictureBox1MouseMove(object sender, MouseEventArgs e)
		{
			rot.Im += e.X - lostE.X;
			rot.Jm += e.Y - lostE.Y;
			lostE = e.Location;
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
			
			//foreach (Quaternion p in Cube.points) 
			for (int i = 0; i < Cube.points.Count; i++) Cube.points[i] = Cube.points[i].Rotate(new Quaternion(Math.Sin(0.01 * t), Math.Sin(0.01 * t + 2 * Math.PI / 3), Math.Sin(0.01 * t + 4 * Math.PI / 3)), 0.01);
			Cube.Update();
			Draw();
			//text = "";
			//foreach (Quaternion p in Cube.points)
			//{
			//	Quaternion h0 = p.Sum(Cube.location).Rotate(new Quaternion(Math.Sin(0.01 * t), Math.Sin(0.01 * t + 2 * Math.PI / 3), Math.Sin(0.01 * t + 4 * Math.PI / 3)), 1);
			//	//Quaternion h0 = p.Sum(Cube.location).Rotate(new Quaternion(1, 0, 0), 0.01 * t * 0).RotateW(new Quaternion(0, 1, 0), 0.01 * t * 0);
			//	//h0 = h0.Rotate(rot1, 1).RotateW(rot2, 1);
			//
			//	text += h0.Abs() + " ( " + h0.Im + "; " + h0.Jm + "; "+ h0.Km + "; "+ h0.Re + ")\n";
			//}
			
			label1.Text = "x: " + loc.Im + "\ny: " + loc.Jm + "\nz: " + loc.Km + "\nw: " + loc.Re + "\nмасштаб: " + k + "\nглубина: " + a + "\nвремя: " + t + "\n\n" + text;
			t++;
		}
		
		void Draw()
		{
			gr.Clear(Color.White);
			try
			{
				if (loc.Km * loc.Km + loc.Re * loc.Re >= 0)
				{
					//foreach (Quaternion h1 in points) foreach (Quaternion h2 in points)
					//{
					//	if(h1.Sub(h2).Abs() == 2*Math.Sqrt(1)) gr.DrawLine(new Pen(Color.White), draw_calculation(h1), draw_calculation(h2));
					//	if(h1.Sub(h2).Abs() == 2*Math.Sqrt(2)) gr.DrawLine(new Pen(Color.Lime), draw_calculation(h1), draw_calculation(h2));
					//	if(h1.Sub(h2).Abs() == 2*Math.Sqrt(3)) gr.DrawLine(new Pen(Color.Red), draw_calculation(h1), draw_calculation(h2));
					//	if(h1.Sub(h2).Abs() == 2*Math.Sqrt(4)) gr.DrawLine(new Pen(Color.Blue), draw_calculation(h1), draw_calculation(h2));
					//}
					
					List<Face> l1 = new List<Face>();
					List<Face> l2 = new List<Face>();
					foreach (Face f in Cube.faces) l1.Add(f);
					while (l1.Count > 0)
					{
						Face i = l1[0];
						foreach (Face f in l1)
						{
							if(f.type)
							{
								Quaternion loc1 = f.h1.Sum(f.h2).Div(2).Sum(Cube.location);
								Quaternion loc2 = new Quaternion(0); if(i.type) loc2 = i.h1.Sum(i.h2).Div(2).Sum(Cube.location); else
								{
									int j = 0;
									foreach (Quaternion p in i.points) {loc2 = loc2.Sum(p); j++;}
									loc2 = loc2.Div(j).Sum(Cube.location);
								}
								if(loc1.Sub(loc).Abs() >= loc2.Sub(loc).Abs()) i = f;
							}
							if(!f.type)
							{
								Quaternion loc1 = new Quaternion(0);
								{
									int j = 0;
									foreach (Quaternion p in f.points){loc1 = loc1.Sum(p); j++;}
									loc1 = loc1.Div(j).Sum(Cube.location);
								}
								Quaternion loc2 = new Quaternion(0); if(i.type) loc2 = i.h1.Sum(i.h2).Div(2).Sum(Cube.location); else
								{
									int j = 0;
									foreach (Quaternion p in i.points) {loc2 = loc2.Sum(p); j++;}
									loc2 = loc2.Div(j).Sum(Cube.location);
								}
								if(loc1.Sub(loc).Abs() >= loc2.Sub(loc).Abs()) i = f;
							}
						}
						l2.Add(i);
						l1.Remove(i);
					}
					foreach (Face f in l2)
					{
						if(f.type) gr.DrawLine(f.color, draw_calculation(f.h1.Sum(Cube.location)), draw_calculation(f.h2.Sum(Cube.location)));
						if(!f.type)
						{
							List<Quaternion> list = new List<Quaternion>(f.points);
							List<Point> po = new List<Point>();
							foreach (Quaternion el in list)
							{
								po.Add(draw_calculation(el.Sum(Cube.location)));
							}
							gr.FillPolygon(f.color.Brush, po.ToArray());
						}
					}
				}
				gr.DrawEllipse(new Pen(Color.Red), (float)(pictureBox1.Width / 2 - k), (float)(pictureBox1.Height / 2 - k), (float)(2 * k), (float)(2 * k));
			} catch { }
			pictureBox1.Image = bitmap;
		}
		
		Point draw_calculation(Quaternion h)
		{
			Quaternion h0 = h.Sub(loc).Rotate(new Quaternion(0, -rot.Im, 0), 0.01).Rotate(new Quaternion(rot.Jm, 0, 0), 0.001).Sum(loc);
			//Quaternion h0 = h.Rotate(new Quaternion(1, 0, 0), 0.01 * t);//.RotateW(new Quaternion(0, 1, 0), 0.01 * t * 0);
			//h0 = h0.Rotate(rot1, 1).RotateW(rot2, 1);
			return new Point((int)( (h0.Im - loc.Im) / Rd(h0, loc) * k) + pictureBox1.Width  / 2,
			                 (int)(-(h0.Jm - loc.Jm) / Rd(h0, loc) * k) + pictureBox1.Height / 2);
		}
		
		double Rd(Quaternion h1, Quaternion h2)
		{
			return Math.Sqrt((h1.Im - h2.Im) * (h1.Im - h2.Im) + (h1.Jm - h2.Jm) * (h1.Jm - h2.Jm) + (h1.Km - h2.Km) * (h1.Km - h2.Km) * a * a + (h1.Re - h2.Re) * (h1.Re - h2.Re) * a * a);
		}
	}
}
