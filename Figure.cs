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
		public double r = 2;
        public int id;
		Random rnd;

		public Figure(int id, string type, Quaternion location)
		{
			this.id = id;
			rnd = new Random();
			this.location = new Quaternion(location);
			points = new List<Quaternion>();
			faces = new List<Face>();
			name = type;
			if(type == "Stellated octahedron")
			{
				for (int i = 0; i < 8; i++) points.Add(new Quaternion(((i / 8) % 2 - 0.5) * 0, ((i / 4) % 2 - 0.5) * r, ((i / 2) % 2 - 0.5) * r, (i % 2 - 0.5) * r));
				points.Add(new Quaternion(0, 0, -r/2));
				points.Add(new Quaternion(0, -r/2, 0));
				points.Add(new Quaternion(-r/2, 0, 0));
				points.Add(new Quaternion(r/2, 0, 0));
				points.Add(new Quaternion(0, r/2, 0));
				points.Add(new Quaternion(0, 0, r/2));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && points[h1].Sub(points[h2]).Abs() == Math.Sqrt(2)*r/2) faces.Add(new Face(id, h1, h2));
				}
				int alp = 191;
				List<int> p;
				List<Color> cl = new List<Color>(); 
				for(int i = 0; i < 8; i++) cl.Add(Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256)));
				p = new List<int>(); p.Add(0); p.Add( 8); p.Add( 9); faces.Add(new Face(id, p, cl[0]));
				p = new List<int>(); p.Add(0); p.Add( 8); p.Add(10); faces.Add(new Face(id, p, cl[1]));
				p = new List<int>(); p.Add(0); p.Add( 9); p.Add(10); faces.Add(new Face(id, p, cl[2]));
				p = new List<int>(); p.Add(1); p.Add(13); p.Add( 9); faces.Add(new Face(id, p, cl[3]));
				p = new List<int>(); p.Add(1); p.Add(13); p.Add(10); faces.Add(new Face(id, p, cl[4]));
				p = new List<int>(); p.Add(1); p.Add( 9); p.Add(10); faces.Add(new Face(id, p, cl[5]));
				p = new List<int>(); p.Add(2); p.Add( 8); p.Add(12); faces.Add(new Face(id, p, cl[6]));
				p = new List<int>(); p.Add(2); p.Add( 8); p.Add(10); faces.Add(new Face(id, p, cl[5]));
				p = new List<int>(); p.Add(2); p.Add(12); p.Add(10); faces.Add(new Face(id, p, cl[4]));
				p = new List<int>(); p.Add(3); p.Add(13); p.Add(12); faces.Add(new Face(id, p, cl[7]));
				p = new List<int>(); p.Add(3); p.Add(13); p.Add(10); faces.Add(new Face(id, p, cl[2]));
				p = new List<int>(); p.Add(3); p.Add(12); p.Add(10); faces.Add(new Face(id, p, cl[1]));
				p = new List<int>(); p.Add(4); p.Add( 8); p.Add( 9); faces.Add(new Face(id, p, cl[5]));
				p = new List<int>(); p.Add(4); p.Add( 8); p.Add(11); faces.Add(new Face(id, p, cl[6]));
				p = new List<int>(); p.Add(4); p.Add( 9); p.Add(11); faces.Add(new Face(id, p, cl[3]));
				p = new List<int>(); p.Add(5); p.Add(13); p.Add( 9); faces.Add(new Face(id, p, cl[2]));
				p = new List<int>(); p.Add(5); p.Add(13); p.Add(11); faces.Add(new Face(id, p, cl[7]));
				p = new List<int>(); p.Add(5); p.Add( 9); p.Add(11); faces.Add(new Face(id, p, cl[0]));
				p = new List<int>(); p.Add(6); p.Add( 8); p.Add(12); faces.Add(new Face(id, p, cl[1]));
				p = new List<int>(); p.Add(6); p.Add( 8); p.Add(11); faces.Add(new Face(id, p, cl[0]));
				p = new List<int>(); p.Add(6); p.Add(12); p.Add(11); faces.Add(new Face(id, p, cl[7]));
				p = new List<int>(); p.Add(7); p.Add(13); p.Add(12); faces.Add(new Face(id, p, cl[4]));
				p = new List<int>(); p.Add(7); p.Add(13); p.Add(11); faces.Add(new Face(id, p, cl[3]));
				p = new List<int>(); p.Add(7); p.Add(12); p.Add(11); faces.Add(new Face(id, p, cl[6]));
			} else
			if(type == "Tetrahedron")
			{
				points.Add(new Quaternion(-r/2, -r/2, -r/2));
				points.Add(new Quaternion(-r/2, r/2, r/2));
				points.Add(new Quaternion(r/2, -r/2, r/2));
				points.Add(new Quaternion(r/2, r/2, -r/2));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && points[h1].Sub(points[h2]).Abs() == Math.Sqrt(2)*r) faces.Add(new Face(id, h1, h2));
				}
				int alp = 191;
				List<int> p;
				for (int i = 0; i < points.Count; i++) for (int j = i+1; j < points.Count; j++) for (int k = j+1; k < points.Count; k++)
				{ p = new List<int>(); p.Add(i); p.Add(j); p.Add(k); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256)))); }
			} else
			if(type == "Octahedron")
			{
				points.Add(new Quaternion(0, 0, -r/2));
				points.Add(new Quaternion(0, -r/2, 0));
				points.Add(new Quaternion(-r/2, 0, 0));
				points.Add(new Quaternion(r/2, 0, 0));
				points.Add(new Quaternion(0, r/2, 0));
				points.Add(new Quaternion(0, 0, r/2));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && points[h1].Sub(points[h2]).Abs() == Math.Sqrt(2)*r/2) faces.Add(new Face(id, h1, h2));
				}
				int alp = 191;
				List<int> p;
				for (int i = 0; i < points.Count; i++) for (int j = i+1; j < points.Count; j++) for (int k = j+1; k < points.Count; k++)
				if(i != j && i != 5-j && i != k && i != 5-k && k != j && k != 5-j)
				{ p = new List<int>(); p.Add(i); p.Add(j); p.Add(k); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256)))); }
				//p = new List<int>(); p.Add(0); p.Add(1); p.Add(2); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				//p = new List<int>(); p.Add(0); p.Add(1); p.Add(3); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				//p = new List<int>(); p.Add(0); p.Add(2); p.Add(4); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				//p = new List<int>(); p.Add(0); p.Add(3); p.Add(4); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				//p = new List<int>(); p.Add(1); p.Add(2); p.Add(5); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				//p = new List<int>(); p.Add(1); p.Add(3); p.Add(5); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				//p = new List<int>(); p.Add(2); p.Add(4); p.Add(5); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				//p = new List<int>(); p.Add(3); p.Add(4); p.Add(5); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
			} else
			if(type == "Icosahedron")
			{
				points.Add(new Quaternion(-r/2.0, -(Math.Sqrt(5)-1.0)*r/4.0, 0));
				points.Add(new Quaternion(-r/2.0, (Math.Sqrt(5)-1.0)*r/4.0, 0));
				points.Add(new Quaternion(-(Math.Sqrt(5)-1.0)*r/4.0, 0, -r/2.0));
				points.Add(new Quaternion(-(Math.Sqrt(5)-1.0)*r/4.0, 0, r/2.0));
				points.Add(new Quaternion(0, -r/2.0, -(Math.Sqrt(5)-1.0)*r/4.0));
				points.Add(new Quaternion(0, -r/2.0, (Math.Sqrt(5)-1.0)*r/4.0));
				points.Add(new Quaternion(0, r/2.0, -(Math.Sqrt(5)-1.0)*r/4.0));
				points.Add(new Quaternion(0, r/2.0, (Math.Sqrt(5)-1.0)*r/4.0));
				points.Add(new Quaternion((Math.Sqrt(5)-1.0)*r/4.0, 0, -r/2.0));
				points.Add(new Quaternion((Math.Sqrt(5)-1.0)*r/4.0, 0, r/2.0));
				points.Add(new Quaternion(r/2.0, -(Math.Sqrt(5)-1.0)*r/4.0, 0));
				points.Add(new Quaternion(r/2.0, (Math.Sqrt(5)-1.0)*r/4.0, 0));
				//points.Add(new Quaternion(-(Math.Sqrt(5)-1.0)*r/4.0, -r/2.0, 0));
				//points.Add(new Quaternion((Math.Sqrt(5)-1.0)*r/4.0, -r/2.0, 0));
				//points.Add(new Quaternion(-r/2.0, 0, -(Math.Sqrt(5)-1.0)*r/4.0));
				//points.Add(new Quaternion(r/2.0, 0, -(Math.Sqrt(5)-1.0)*r/4.0));
				//points.Add(new Quaternion(0, -(Math.Sqrt(5)-1.0)*r/4.0, -r/2.0));
				//points.Add(new Quaternion(0, (Math.Sqrt(5)-1.0)*r/4.0, -r/2.0));
				//points.Add(new Quaternion(0, -(Math.Sqrt(5)-1.0)*r/4.0, r/2.0));
				//points.Add(new Quaternion(0, (Math.Sqrt(5)-1.0)*r/4.0, r/2.0));
				//points.Add(new Quaternion(-r/2.0, 0, (Math.Sqrt(5)-1.0)*r/4.0));
				//points.Add(new Quaternion(r/2.0, 0, (Math.Sqrt(5)-1.0)*r/4.0));
				//points.Add(new Quaternion(-(Math.Sqrt(5)-1.0)*r/4.0, r/2.0, 0));
				//points.Add(new Quaternion((Math.Sqrt(5)-1.0)*r/4.0, r/2.0, 0));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && Math.Abs(points[h1].Sub(points[h2]).Abs() - Math.Sqrt(6.0-2.0*Math.Sqrt(5.0))*r/2.0) < 0.001) faces.Add(new Face(id, h1, h2));
				}
				int alp = 191;
				List<int> p;
				for (int i = 0; i < points.Count; i++) for (int j = i+1; j < points.Count; j++) for (int k = j+1; k < points.Count; k++)
					if(Math.Abs(points[i].Sub(points[j]).Abs() - Math.Sqrt(6.0-2.0*Math.Sqrt(5.0))*r/2.0) < 0.001 && 
					   Math.Abs(points[j].Sub(points[k]).Abs() - Math.Sqrt(6.0-2.0*Math.Sqrt(5.0))*r/2.0) < 0.001 && 
					   Math.Abs(points[k].Sub(points[i]).Abs() - Math.Sqrt(6.0-2.0*Math.Sqrt(5.0))*r/2.0) < 0.001)
				{ p = new List<int>(); p.Add(i); p.Add(j); p.Add(k); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256)))); }
			} else
			if(type == "Large icosahedron")
			{
				points.Add(new Quaternion(-r/2.0, -(Math.Sqrt(5)-1.0)*r/4.0, 0));
				points.Add(new Quaternion(-r/2.0, (Math.Sqrt(5)-1.0)*r/4.0, 0));
				points.Add(new Quaternion(-(Math.Sqrt(5)-1.0)*r/4.0, 0, -r/2.0));
				points.Add(new Quaternion(-(Math.Sqrt(5)-1.0)*r/4.0, 0, r/2.0));
				points.Add(new Quaternion(0, -r/2.0, -(Math.Sqrt(5)-1.0)*r/4.0));
				points.Add(new Quaternion(0, -r/2.0, (Math.Sqrt(5)-1.0)*r/4.0));
				points.Add(new Quaternion(0, r/2.0, -(Math.Sqrt(5)-1.0)*r/4.0));
				points.Add(new Quaternion(0, r/2.0, (Math.Sqrt(5)-1.0)*r/4.0));
				points.Add(new Quaternion((Math.Sqrt(5)-1.0)*r/4.0, 0, -r/2.0));
				points.Add(new Quaternion((Math.Sqrt(5)-1.0)*r/4.0, 0, r/2.0));
				points.Add(new Quaternion(r/2.0, -(Math.Sqrt(5)-1.0)*r/4.0, 0));
				points.Add(new Quaternion(r/2.0, (Math.Sqrt(5)-1.0)*r/4.0, 0));
				//points.Add(new Quaternion(-(Math.Sqrt(5)-1.0)*r/4.0, -r/2.0, 0));
				//points.Add(new Quaternion((Math.Sqrt(5)-1.0)*r/4.0, -r/2.0, 0));
				//points.Add(new Quaternion(-r/2.0, 0, -(Math.Sqrt(5)-1.0)*r/4.0));
				//points.Add(new Quaternion(r/2.0, 0, -(Math.Sqrt(5)-1.0)*r/4.0));
				//points.Add(new Quaternion(0, -(Math.Sqrt(5)-1.0)*r/4.0, -r/2.0));
				//points.Add(new Quaternion(0, (Math.Sqrt(5)-1.0)*r/4.0, -r/2.0));
				//points.Add(new Quaternion(0, -(Math.Sqrt(5)-1.0)*r/4.0, r/2.0));
				//points.Add(new Quaternion(0, (Math.Sqrt(5)-1.0)*r/4.0, r/2.0));
				//points.Add(new Quaternion(-r/2.0, 0, (Math.Sqrt(5)-1.0)*r/4.0));
				//points.Add(new Quaternion(r/2.0, 0, (Math.Sqrt(5)-1.0)*r/4.0));
				//points.Add(new Quaternion(-(Math.Sqrt(5)-1.0)*r/4.0, r/2.0, 0));
				//points.Add(new Quaternion((Math.Sqrt(5)-1.0)*r/4.0, r/2.0, 0));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && Math.Abs(points[h1].Sub(points[h2]).Abs() - r) < 0.001) faces.Add(new Face(id, h1, h2));
				}
				int alp = 191;
				List<int> p;
				for (int i = 0; i < points.Count; i++) for (int j = i+1; j < points.Count; j++) for (int k = j+1; k < points.Count; k++)
					if(Math.Abs(points[i].Sub(points[j]).Abs() - r) < 0.001 && 
					   Math.Abs(points[j].Sub(points[k]).Abs() - r) < 0.001 && 
					   Math.Abs(points[k].Sub(points[i]).Abs() - r) < 0.001)
				{ p = new List<int>(); p.Add(i); p.Add(j); p.Add(k); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256)))); }
			} else
			if(type == "Tesseract")
			{
				for(int i = 0; i < 16; i++) points.Add(new Quaternion(((i/8)%2 - 0.5)*r, ((i/4)%2 - 0.5)*r, ((i/2)%2 - 0.5)*r, (i%2 - 0.5)*r));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && points[h1].Sub(points[h2]).Abs() == r) faces.Add(new Face(id, h1, h2));
				}
				int alp = 63;
				List<int> p;
				p = new List<int>(); p.Add(0); p.Add(1); p.Add(3); p.Add(2); faces.Add(new Face(id, p, Color.FromArgb(alp,   0, 255, 255)));
				p = new List<int>(); p.Add(4); p.Add(5); p.Add(7); p.Add(6); faces.Add(new Face(id, p, Color.FromArgb(alp, 255,   0,   0)));
				p = new List<int>(); p.Add(0); p.Add(1); p.Add(5); p.Add(4); faces.Add(new Face(id, p, Color.FromArgb(alp, 255,   0, 255)));
				p = new List<int>(); p.Add(1); p.Add(3); p.Add(7); p.Add(5); faces.Add(new Face(id, p, Color.FromArgb(alp,   0,   0, 255)));
				p = new List<int>(); p.Add(3); p.Add(2); p.Add(6); p.Add(7); faces.Add(new Face(id, p, Color.FromArgb(alp,   0, 255,   0)));
				p = new List<int>(); p.Add(2); p.Add(0); p.Add(4); p.Add(6); faces.Add(new Face(id, p, Color.FromArgb(alp, 255, 255,   0)));
				p = new List<int>(); p.Add( 8); p.Add( 9); p.Add(11); p.Add(10); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(12); p.Add(13); p.Add(15); p.Add(14); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add( 8); p.Add( 9); p.Add(13); p.Add(12); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add( 9); p.Add(11); p.Add(15); p.Add(13); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(11); p.Add(10); p.Add(14); p.Add(15); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(10); p.Add( 8); p.Add(12); p.Add(14); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(0); p.Add(1); p.Add( 9); p.Add( 8); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(0); p.Add(2); p.Add(10); p.Add( 8); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(0); p.Add(4); p.Add(12); p.Add( 8); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(1); p.Add(3); p.Add(11); p.Add( 9); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(1); p.Add(5); p.Add(13); p.Add( 9); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(2); p.Add(3); p.Add(11); p.Add(10); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(2); p.Add(6); p.Add(14); p.Add(10); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(3); p.Add(7); p.Add(15); p.Add(11); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(4); p.Add(5); p.Add(13); p.Add(12); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(4); p.Add(6); p.Add(14); p.Add(12); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(5); p.Add(7); p.Add(15); p.Add(13); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
				p = new List<int>(); p.Add(6); p.Add(7); p.Add(15); p.Add(14); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256))));
			} else
				if(type == "Pentahoron")
			{
				points.Add(new Quaternion(-Math.Sqrt(1.0/5.0)*r/2, -r/2, -r/2, -r/2));
				points.Add(new Quaternion(-Math.Sqrt(1.0/5.0)*r/2, -r/2,  r/2,  r/2));
				points.Add(new Quaternion(-Math.Sqrt(1.0/5.0)*r/2,  r/2, -r/2,  r/2));
				points.Add(new Quaternion(-Math.Sqrt(1.0/5.0)*r/2,  r/2,  r/2, -r/2));
				points.Add(new Quaternion(Math.Sqrt(16.0/5.0)*r/2, 0, 0, 0));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && points[h1].Sub(points[h2]).Abs() == Math.Sqrt(2)*r) faces.Add(new Face(id, h1, h2));
				}
				int alp = 63;
				List<int> p;
				for (int i = 0; i < points.Count; i++) for (int j = i+1; j < points.Count; j++) for (int k = j+1; k < points.Count; k++)
				{ p = new List<int>(); p.Add(i); p.Add(j); p.Add(k); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256)))); }
			} else
			if(type == "Hexadecahoron")
			{
				points.Add(new Quaternion(0, 0, 0, -r/2));
				points.Add(new Quaternion(0, 0, -r/2, 0));
				points.Add(new Quaternion(0, -r/2, 0, 0));
				points.Add(new Quaternion(-r/2, 0, 0, 0));
				points.Add(new Quaternion(r/2, 0, 0, 0));
				points.Add(new Quaternion(0, r/2, 0, 0));
				points.Add(new Quaternion(0, 0, r/2, 0));
				points.Add(new Quaternion(0, 0, 0, r/2));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && points[h1].Sub(points[h2]).Abs() == Math.Sqrt(2)*r/2) faces.Add(new Face(id, h1, h2));
				}
				int alp = 63;
				List<int> p;
				for (int i = 0; i < points.Count; i++) for (int j = i+1; j < points.Count; j++) for (int k = j+1; k < points.Count; k++)
				if(i != j && i != 7-j && i != k && i != 7-k && k != j && k != 7-j)
				{ p = new List<int>(); p.Add(i); p.Add(j); p.Add(k); faces.Add(new Face(id, p, Color.FromArgb(alp, rnd.Next(256), rnd.Next(256), rnd.Next(256)))); }
			}
			else
			{
				name = "Cube";
				for (int i = 0; i < 8; i++) points.Add(new Quaternion(((i / 8) % 2 - 0.5) * 0, ((i / 4) % 2 - 0.5) * r, ((i / 2) % 2 - 0.5) * r, (i % 2 - 0.5) * r));
				for (int h1 = 0; h1 < points.Count; h1++) for (int h2 = 0; h2 < points.Count; h2++)
				{
					bool i = true;
					foreach (Face l in faces) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && points[h1].Sub(points[h2]).Abs() == r) faces.Add(new Face(id, h1, h2));
				}
				int alp = 191;
				List<int> p;
				p = new List<int>(); p.Add(0); p.Add(1); p.Add(3); p.Add(2); faces.Add(new Face(id, p, Color.FromArgb(alp,   0, 255, 255)));
				p = new List<int>(); p.Add(4); p.Add(5); p.Add(7); p.Add(6); faces.Add(new Face(id, p, Color.FromArgb(alp, 255,   0,   0)));
				p = new List<int>(); p.Add(0); p.Add(1); p.Add(5); p.Add(4); faces.Add(new Face(id, p, Color.FromArgb(alp, 255,   0, 255)));
				p = new List<int>(); p.Add(1); p.Add(3); p.Add(7); p.Add(5); faces.Add(new Face(id, p, Color.FromArgb(alp,   0,   0, 255)));
				p = new List<int>(); p.Add(3); p.Add(2); p.Add(6); p.Add(7); faces.Add(new Face(id, p, Color.FromArgb(alp,   0, 255,   0)));
				p = new List<int>(); p.Add(2); p.Add(0); p.Add(4); p.Add(6); faces.Add(new Face(id, p, Color.FromArgb(alp, 255, 255,   0)));
			}
		}
		
		void IdUpdate(int id)
		{
			this.id = id;
			foreach (Face f in faces) f.id = id;
		}
	}
}
