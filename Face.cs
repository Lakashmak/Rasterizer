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
        public Quaternion h1;
        public Quaternion h2;
        public List<Quaternion> points;
        public Pen color;

        public Face(Quaternion h1, Quaternion h2)
        {
        	type = true;
        	this.h1 = h1;
        	this.h2 = h2;
        	color = new Pen(Color.Black);
        	this.points = new List<Quaternion>();
        }
        
        public Face(Quaternion h1, Quaternion h2, Color cl)
        {
        	type = true;
        	this.h1 = h1;
        	this.h2 = h2;
        	color = new Pen(cl);
        	this.points = new List<Quaternion>();
        }
        
        public Face(List<Quaternion> points)
        {
        	type = false;
        	this.points = points;
        	color = new Pen(Color.Gray);
        	this.h1 = new Quaternion(0);
        	this.h2 = new Quaternion(0);
        }
        
        public Face(List<Quaternion> points, Color cl)
        {
        	type = false;
        	this.points = points;
        	color = new Pen(cl);
        	this.h1 = new Quaternion(0);
        	this.h2 = new Quaternion(0);
        }
    }
}