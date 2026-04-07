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
	public class Figure
	{
		public string name;
		public Quaternion location;
		public List<Quaternion> points;
		public List<Face> faces;
		
		public Figure(string type, Quaternion location)
		{
			this.location = new Quaternion(location);
			points = new List<Quaternion>();
			faces = new List<Face>();
			
			if(type == "Cube")
			{
				name = "Cube";
				double r = 2;
				for(int i = 0; i < 8; i++) points.Add(new Quaternion(((i/8)%2 - 0.5)*0, ((i/4)%2 - 0.5)*r, ((i/2)%2 - 0.5)*r, (i%2 - 0.5)*r));
				foreach (Quaternion h1 in points) foreach (Quaternion h2 in points)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && h1.Sub(h2).Abs() == r) faces.Add(new Face(h1, h2));
				}
				List<Quaternion> p = new List<Quaternion>(); p.Add(points[0]); p.Add(points[1]); p.Add(points[3]); p.Add(points[2]);
				faces.Add(new Face(p, Color.FromArgb(255, 0, 0)));
				p = new List<Quaternion>(); p.Add(points[4]); p.Add(points[5]); p.Add(points[7]); p.Add(points[6]);
				faces.Add(new Face(p, Color.FromArgb(0, 255, 0)));
				p = new List<Quaternion>(); p.Add(points[0]); p.Add(points[1]); p.Add(points[5]); p.Add(points[4]);
				faces.Add(new Face(p, Color.FromArgb(0, 0, 255)));
				p = new List<Quaternion>(); p.Add(points[1]); p.Add(points[3]); p.Add(points[7]); p.Add(points[5]);
				faces.Add(new Face(p, Color.FromArgb(255, 255, 0)));
				p = new List<Quaternion>(); p.Add(points[3]); p.Add(points[2]); p.Add(points[6]); p.Add(points[7]);
				faces.Add(new Face(p, Color.FromArgb(255, 0, 255)));
				p = new List<Quaternion>(); p.Add(points[2]); p.Add(points[0]); p.Add(points[4]); p.Add(points[6]);
				faces.Add(new Face(p, Color.FromArgb(0, 255, 255)));
			}
			else
			{
				name = "Cube";
				for(int i = 0; i < 8; i++) points.Add(new Quaternion(((i/8)%2 - 0.5)*0, ((i/4)%2 - 0.5)*2, ((i/2)%2 - 0.5)*2, (i%2 - 0.5)*2));
				foreach (Quaternion h1 in points) foreach (Quaternion h2 in points)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && h1.Sub(h2).Abs() == 2) faces.Add(new Face(h1, h2));
				}
			}
		}
		
		public void Update()
		{
			faces = new List<Face>();
			
			if(name == "Cube")
			{
				double r = 2;
				foreach (Quaternion h1 in points) foreach (Quaternion h2 in points)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && h1.Sub(h2).Abs() == r) faces.Add(new Face(h1, h2));
				}
				List<Quaternion> p = new List<Quaternion>(); p.Add(points[0]); p.Add(points[1]); p.Add(points[3]); p.Add(points[2]);
				faces.Add(new Face(p, Color.FromArgb(255, 0, 0)));
				p = new List<Quaternion>(); p.Add(points[4]); p.Add(points[5]); p.Add(points[7]); p.Add(points[6]);
				faces.Add(new Face(p, Color.FromArgb(0, 255, 0)));
				p = new List<Quaternion>(); p.Add(points[0]); p.Add(points[1]); p.Add(points[5]); p.Add(points[4]);
				faces.Add(new Face(p, Color.FromArgb(0, 0, 255)));
				p = new List<Quaternion>(); p.Add(points[1]); p.Add(points[3]); p.Add(points[7]); p.Add(points[5]);
				faces.Add(new Face(p, Color.FromArgb(255, 255, 0)));
				p = new List<Quaternion>(); p.Add(points[3]); p.Add(points[2]); p.Add(points[6]); p.Add(points[7]);
				faces.Add(new Face(p, Color.FromArgb(255, 0, 255)));
				p = new List<Quaternion>(); p.Add(points[2]); p.Add(points[0]); p.Add(points[4]); p.Add(points[6]);
				faces.Add(new Face(p, Color.FromArgb(0, 255, 255)));
			}
		}
	}
}
