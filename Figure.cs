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
		public List<Face> lines;
		
		public Figure(string type, Quaternion location)
		{
			this.location = new Quaternion(location);
			points = new List<Quaternion>();
			lines = new List<Face>();
			
			if(type == "Cube")
			{
				name = "Cube";
				for(int i = 0; i < 8; i++) points.Add(new Quaternion(((i/8)%2 - 0.5)*0, ((i/4)%2 - 0.5)*2, ((i/2)%2 - 0.5)*2, (i%2 - 0.5)*2));
				foreach (Quaternion h1 in points) foreach (Quaternion h2 in points)
				{
					bool i = true;
					foreach (Face l in lines) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && h1.Sub(h2).Abs() == 2) lines.Add(new Face(h1, h2));
				}
			}
			else
			{
				name = "Cube";
				for(int i = 0; i < 8; i++) points.Add(new Quaternion(((i/8)%2 - 0.5)*0, ((i/4)%2 - 0.5)*2, ((i/2)%2 - 0.5)*2, (i%2 - 0.5)*2));
				foreach (Quaternion h1 in points) foreach (Quaternion h2 in points)
				{
					bool i = true;
					foreach (Face l in lines) if(l.h1 == h2 && l.h2 == h1) i = false;
					if(i && h1.Sub(h2).Abs() == 2) lines.Add(new Face(h1, h2));
				}
			}
		}
	}
}
