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
        	color = new Pen(Color.White);
        }
        
        public Face(List<Quaternion> points)
        {
        	type = false;
        	this.points = points;
        	color = new Pen(Color.Gray);
        }
    }
}