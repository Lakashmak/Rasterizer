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
		public double[] location;
		public List<double[]> points;
		public List<int[]> lines;
		
		public Figure()
		{
			//location = new double[3](); for(int i = 0; i < 3; i++) location[i] = 0;
			points = new List<double[]>();
			lines = new List<int[]>();
			
			name = "Cube";
			for (int i = 0; i < 8; i++) 
			{
				//points.Add(new double[3]());
				points[i][0] = (i/1)%2 - 0.5;
				points[i][1] = (i/2)%2 - 0.5;
				points[i][2] = (i/4)%2 - 0.5;
			}
			
			for(int i = 0; i < 8; i++)
			{
				for(int j = i+1; j < 8; j++)
				{
					int a = 0;
					if(points[j][0] - points[i][0] == 1) a++;
					if(points[j][1] - points[i][1] == 1) a++;
					if(points[j][2] - points[i][2] == 1) a++;
					if(a == 1) lines.Add(new int[2]);
					lines[lines.Count - 1][0] = i;
					lines[lines.Count - 1][1] = j;
				}
			}
		}
		
		
	}
}
