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
		Quaternion loc = new Quaternion(0, 10, 0, 5); // координаты камеры
		Quaternion vel = new Quaternion(0); // скрость камеры
		Quaternion rot = new Quaternion(0); // поворот камеры
		double a; // радиус поля зрения
		double k; // масштаб
		int t = 0;
		Quaternion rot1 = new Quaternion(1);
		Quaternion rot2 = new Quaternion(1);
		List<Figure> Figures = new List<Figure>();
		string text = "";
		Point lostE;
		bool cursorVisible = false;
		bool speed = false;
		int id = 0;
		public MainForm()
		{
			InitializeComponent();
			this.MouseWheel += new MouseEventHandler(this.Form1_MouseWhel);
			a = 20;
			k = 500*a;
			bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			gr = Graphics.FromImage(bitmap);
			gr.Clear(Color.White);
			pictureBox1.Image = bitmap;
			timer1.Start();
			
			
			Figures.Add(new Figure(Figures.Count, "Tesseract", new Quaternion(0, 0, 0)));
			Figures.Add(new Figure(Figures.Count, "Hexadecahoron", new Quaternion(-5, 0, 0)));
			
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(9, -1, -2)));
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(9, -1, 0)));
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(9, 1, -2)));
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(9, 1, 0)));
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(11, -1, -2)));
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(11, -1, 0)));
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(11, 1, -2)));
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(11, 1, 0)));
			
			Figures.Add(new Figure(Figures.Count, "Tetrahedron", new Quaternion(0, 0, 5)));
			Figures.Add(new Figure(Figures.Count, "Octahedron", new Quaternion(5, 0, 5)));
			Figures.Add(new Figure(Figures.Count, "Cube", new Quaternion(10, 0, 5)));
			
			Figures.Add(new Figure(Figures.Count, "Stellated octahedron", new Quaternion(0, 5, 5)));
		}
		
		private void Form1_MouseWhel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 0) k *= 1.05;
			else k /= 1.05;
		}
		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			if (!cursorVisible) Cursor.Hide();
			cursorVisible = true;
			if (cursorVisible) Cursor.Position = new Point(this.Location.X + 8 + pictureBox1.Location.X + pictureBox1.Width / 2, this.Location.Y + 31 + pictureBox1.Location.Y + pictureBox1.Height / 2);
			lostE = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
		}

		void PictureBox1MouseMove(object sender, MouseEventArgs e)
		{
			if (cursorVisible)
			{
				rot.Im += (lostE.X - e.X) * 0.0025;
				rot.Jm += (lostE.Y - e.Y) * 0.0025;
				//text = (lostE.X) + " " + (lostE.Y) + "\n" + (e.X) + " " + (e.Y) + "\n" + (lostE.X - e.X) + " " + (lostE.Y - e.Y);
			}
			if (cursorVisible) Cursor.Position = new Point(this.Location.X + 8 + pictureBox1.Location.X + pictureBox1.Width / 2, this.Location.Y + 31 + pictureBox1.Location.Y + pictureBox1.Height / 2);
		}
		
		private void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (cursorVisible) Cursor.Show();
				cursorVisible = false;
			}
			if (e.KeyCode == Keys.D) { vel.Km = Math.Sin(rot.Im); vel.Im = Math.Cos(rot.Im); }
			if (e.KeyCode == Keys.A) { vel.Km = -Math.Sin(rot.Im); vel.Im = -Math.Cos(rot.Im); }
			if (e.KeyCode == Keys.Space) vel.Jm = 1;
			if (e.KeyCode == Keys.ShiftKey) vel.Jm = -1;
			if (e.KeyCode == Keys.W) { vel.Km = Math.Cos(rot.Im); vel.Im = -Math.Sin(rot.Im);}
			if (e.KeyCode == Keys.S) { vel.Km = -Math.Cos(rot.Im); vel.Im = Math.Sin(rot.Im); }
			if (e.KeyCode == Keys.R) vel.Re = 1;
			if (e.KeyCode == Keys.F) vel.Re = -1;
			if (e.KeyCode == Keys.T) a += 0.05;
			if (e.KeyCode == Keys.G) a -= 0.05;
			if (e.KeyCode == Keys.Y) { a += 0.05; k *= a/(a - 0.05); }
			if (e.KeyCode == Keys.H) { a -= 0.05; k /= (a + 0.05)/a; }
			if (e.KeyCode == Keys.Z) for (int i = 0; i < Figures[id].points.Count; i++) Figures[id].points[i] = Figures[id].points[i].Rotate (new Quaternion(1, 0, 0), 0.05);
			if (e.KeyCode == Keys.X) for (int i = 0; i < Figures[id].points.Count; i++) Figures[id].points[i] = Figures[id].points[i].Rotate (new Quaternion(0, 1, 0), 0.05);
			if (e.KeyCode == Keys.C) for (int i = 0; i < Figures[id].points.Count; i++) Figures[id].points[i] = Figures[id].points[i].Rotate (new Quaternion(0, 0, 1), 0.05);
			if (e.KeyCode == Keys.V) for (int i = 0; i < Figures[id].points.Count; i++) Figures[id].points[i] = Figures[id].points[i].RotateW(new Quaternion(1, 0, 0), 0.05);
			if (e.KeyCode == Keys.B) for (int i = 0; i < Figures[id].points.Count; i++) Figures[id].points[i] = Figures[id].points[i].RotateW(new Quaternion(0, 1, 0), 0.05);
			if (e.KeyCode == Keys.N) for (int i = 0; i < Figures[id].points.Count; i++) Figures[id].points[i] = Figures[id].points[i].RotateW(new Quaternion(0, 0, 1), 0.05);
			if (e.KeyCode == Keys.ControlKey) speed = true;
		}

		private void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D) { vel.Im = 0; vel.Km = 0; }
			if (e.KeyCode == Keys.A) { vel.Im = 0; vel.Km = 0; }
			if (e.KeyCode == Keys.Space) vel.Jm = 0;
			if (e.KeyCode == Keys.ShiftKey) vel.Jm = 0;
			if (e.KeyCode == Keys.W) { vel.Im = 0; vel.Km = 0; }
			if (e.KeyCode == Keys.S) { vel.Im = 0; vel.Km = 0; }
			if (e.KeyCode == Keys.R) vel.Re = 0;
			if (e.KeyCode == Keys.F) vel.Re = 0;
			if (e.KeyCode == Keys.ControlKey) speed = false;
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
			if(numericUpDown1.Focused)
			{
				numericUpDown1.Enabled = false;
				numericUpDown1.Enabled = true;
			}
			if(speed)
			{
				loc.Im += vel.Im / 25 * Convert.ToDouble(numericUpDown1.Value);
				loc.Jm += vel.Jm / 25 * Convert.ToDouble(numericUpDown1.Value);
				loc.Km += vel.Km / 25 * Convert.ToDouble(numericUpDown1.Value);
				loc.Re += vel.Re / 25 * Convert.ToDouble(numericUpDown1.Value);
			}
			else
			{
				loc.Im += vel.Im / 25;
				loc.Jm += vel.Jm / 25;
				loc.Km += vel.Km / 25;
				loc.Re += vel.Re / 25;
			}
			
			for (int f = 0; f < Figures.Count; f++) if(Figures[f].location.Sub(loc).Abs() <= Figures[id].location.Sub(loc).Abs()) id = f;
			text = "                  " + id;
			
			Draw();
			
			//for (int i = 0; i < Figures[0].points.Count; i++) Figures[0].points[i] = Figures[0].points[i].Rotate(new Quaternion(Math.Sin(0.01 * t), Math.Sin(0.01 * t + 2 * Math.PI / 3), Math.Sin(0.01 * t + 4 * Math.PI / 3)), 0.01 * 0);
			
			//text = "";
			//{
			//	Quaternion h1 = new Quaternion(0, 1, 2, 3);
			//	Quaternion h2 = new Quaternion(0, 4, 5, 6);
			//	double l = 30;
			//	for (double d = 0; d < l; d++)
			//	{
			//		Quaternion q1 = h1.Mul((l-d)/l).Sum(h2.Mul(d/l));
			//		Quaternion q2 = h1.Mul((l-d-1)/l).Sum(h2.Mul((d+1)/l));
			//		text += "(" + q1.Re + " " + q1.Im + " " + q1.Jm + " " + q1.Km + ") (" + q2.Re + " " + q2.Im + " " + q2.Jm + " " + q2.Km + ")\n";
			//	}
			//}
			
			label1.Text = "x: " + loc.Im + "\ny: " + loc.Jm + "\nz: " + loc.Km + "\nw: " + loc.Re + "\nглубина: " + k/a + "\nмасштаб: " + k + "\nэффект рыбьего глаза: " + 1/a + "\nвремя: " + t + "\n\n" + text;
			t++;
		}
		
		void Draw()
		{
			gr.Clear(Color.Black);
			try
			{
				gr.FillEllipse(new Pen(Color.FromArgb(127, 191, 255)).Brush, (float)(pictureBox1.Width / 2 - k), (float)(pictureBox1.Height / 2 - k), (float)(2 * k), (float)(2 * k));

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
					foreach (Figure Cube in Figures) foreach (Face f in Cube.faces) l1.Add(f);
					while (l1.Count > 0)
					{
						Face i = l1[0];
						foreach (Face f in l1)
						{
							if(f.type)
							{
								Quaternion loc1 = Figures[f.id].points[f.h1].Sum(Figures[f.id].points[f.h2]).Div(2).Sum(Figures[f.id].location);
								Quaternion loc2 = new Quaternion(0); if(i.type) loc2 = Figures[i.id].points[i.h1].Sum(Figures[i.id].points[i.h2]).Div(2).Sum(Figures[i.id].location); else
								{
									int j = 0;
									foreach (int p in i.points) {loc2 = loc2.Sum(Figures[i.id].points[p]); j++;}
									loc2 = loc2.Div(j).Sum(Figures[i.id].location);
								}
								if(loc1.Sub(loc).Abs() >= loc2.Sub(loc).Abs()) i = f;
							}
							if(!f.type)
							{
								Quaternion loc1 = new Quaternion(0);
								{
									int j = 0;
									foreach (int p in f.points){loc1 = loc1.Sum(Figures[f.id].points[p]); j++;}
									loc1 = loc1.Div(j).Sum(Figures[f.id].location);
								}
								Quaternion loc2 = new Quaternion(0); if(i.type) loc2 = Figures[i.id].points[i.h1].Sum(Figures[i.id].points[i.h2]).Div(2).Sum(Figures[i.id].location); else
								{
									int j = 0;
									foreach (int p in i.points) {loc2 = loc2.Sum(Figures[i.id].points[p]); j++;}
									loc2 = loc2.Div(j).Sum(Figures[i.id].location);
								}
								if(loc1.Sub(loc).Abs() >= loc2.Sub(loc).Abs()) i = f;
							}
						}
						l2.Add(i);
						l1.Remove(i);
					}
					foreach (Face f in l2)
					{
						double ogr = 0.6;
						double l = 1;
						if(f.type) for (double d = 0; d < l; d++)
							if(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul(d/l)).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Km - loc.Km > ogr
							   //&& Math.Abs(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul(d/l)).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Re - loc.Re) < ogr
							   || Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d-1)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul((d+1)/l)).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Km - loc.Km > ogr
							   //&& Math.Abs(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d-1)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul((d+1)/l)).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Re - loc.Re) < ogr
							   //|| Math.Abs(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul(d/l)).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Km - loc.Km) > ogr
							   //&& Math.Abs(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul(d/l)).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Re - loc.Re) > ogr
							   //|| Math.Abs(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d-1)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul((d+1)/l)).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Km - loc.Km) > ogr
							   //&& Math.Abs(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d-1)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul((d+1)/l)).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Re - loc.Re) > ogr
							  ) gr.DrawLine(f.color, draw_calculation(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul(d/l))), draw_calculation(Figures[f.id].points[f.h1].Sum(Figures[f.id].location).Mul((l-d-1)/l).Sum(Figures[f.id].points[f.h2].Sum(Figures[f.id].location).Mul((d+1)/l))));
						if(!f.type)
						{
							bool pr = false;
							List<int> list = new List<int>(f.points);
							List<Point> po = new List<Point>();
							foreach (int el in list)
							{
								if (Figures[f.id].points[el].Sum(Figures[f.id].location).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Km - loc.Km > ogr
								    //&& Math.Abs(Figures[f.id].points[el].Sum(Figures[f.id].location).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Re - loc.Re) < ogr
								    //|| Math.Abs(Figures[f.id].points[el].Sum(Figures[f.id].location).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Km - loc.Km) > ogr
								    //&& Math.Abs(Figures[f.id].points[el].Sum(Figures[f.id].location).Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc).Re - loc.Re) > ogr
								   ) pr = true;
								po.Add(draw_calculation(Figures[f.id].points[el].Sum(Figures[f.id].location)));
							}
							if(pr) gr.FillPolygon(f.color.Brush, po.ToArray());
						}
					}
				}
			} catch { }
			pictureBox1.Image = bitmap;
		}
		
		Point draw_calculation(Quaternion h)
		{
			Quaternion h0 = h.Sub(loc).Rotate(new Quaternion(0, rot.Im, 0), 1).Rotate(new Quaternion(rot.Jm, 0, 0), 1).Sum(loc);
			//Quaternion h0 = h.Rotate(new Quaternion(1, 0, 0), 0.01 * t);//.RotateW(new Quaternion(0, 1, 0), 0.01 * t * 0);
			//h0 = h0.Rotate(rot1, 1).RotateW(rot2, 1);
			return new Point((int)( (h0.Im - loc.Im) / Rd(h0, loc) * k) + pictureBox1.Width  / 2,
			                 (int)(-(h0.Jm - loc.Jm) / Rd(h0, loc) * k) + pictureBox1.Height / 2);
		}
		
		double Rd(Quaternion h1, Quaternion h2)
		{
			if(Math.Abs(h1.Re - h2.Re) < 0.2 || true)
			{
				if (Math.Sign(h1.Km - h2.Km) == 1) return Math.Sqrt((h1.Im - h2.Im) * (h1.Im - h2.Im) + (h1.Jm - h2.Jm) * (h1.Jm - h2.Jm) + Math.Sign(h1.Km - h2.Km) * (h1.Km - h2.Km) * (h1.Km - h2.Km) * a * a + (h1.Re - h2.Re) * (h1.Re - h2.Re) * a * a);
				else return Math.Sqrt((h1.Im - h2.Im) * (h1.Im - h2.Im) + (h1.Jm - h2.Jm) * (h1.Jm - h2.Jm) + (h1.Re - h2.Re) * (h1.Re - h2.Re) * a * a);
			}
			else return Math.Sqrt((h1.Im - h2.Im) * (h1.Im - h2.Im) + (h1.Jm - h2.Jm) * (h1.Jm - h2.Jm) + (h1.Km - h2.Km) * (h1.Km - h2.Km) * a * a + (h1.Re - h2.Re) * (h1.Re - h2.Re) * a * a);
		}
	}
}
