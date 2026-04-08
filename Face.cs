using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace растеризатор
{

    public class Face
    {
        public bool type;
        public int h1;
        public int h2;
        public int id;
        public List<int> points;
        public Pen color;

        public Face(int id, int h1, int h2)
        {
        	this.id = id;
        	type = true;
        	this.h1 = h1;
        	this.h2 = h2;
        	color = new Pen(Color.Black);
        	this.points = new List<int>();
        }
        
        public Face(int id, int h1, int h2, Color cl)
        {
        	this.id = id;
        	type = true;
        	this.h1 = h1;
        	this.h2 = h2;
        	color = new Pen(cl);
        	this.points = new List<int>();
        }
        
        public Face(int id, List<int> points)
        {
        	this.id = id;
        	type = false;
        	this.points = points;
        	color = new Pen(Color.Gray);
        	this.h1 = -1;
        	this.h2 = -1;
        }
        
        public Face(int id, List<int> points, Color cl)
        {
        	this.id = id;
        	type = false;
        	this.points = points;
        	color = new Pen(cl);
        	this.h1 = -1;
        	this.h2 = -1;
        }
    }
}