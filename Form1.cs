using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Graphics gr;
        Bitmap bitmap;
        double x = 0, y = 0, z = -5; // координаты камеры
        double vx = 0, vy = 0, vz = 0; // скрость камеры
        double k = 1086; // масштаб
        double a = 2.5; // глубина
        int t = 0;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D) vx = 1;
            if (e.KeyCode == Keys.A) vx = -1;
            if (e.KeyCode == Keys.Space) vy = 1;
            if (e.KeyCode == Keys.ShiftKey) vy = -1;
            if (e.KeyCode == Keys.W) vz = 1;
            if (e.KeyCode == Keys.S) vz = -1;
            if (e.KeyCode == Keys.R) k *= 1.01;
            if (e.KeyCode == Keys.F) k /= 1.01;
            //if (e.KeyCode == Keys.T) a += 0.01;
            //if (e.KeyCode == Keys.G) a -= 0.01;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D) vx = 0;
            if (e.KeyCode == Keys.A) vx = 0;
            if (e.KeyCode == Keys.Space) vy = 0;
            if (e.KeyCode == Keys.ShiftKey) vy = 0;
            if (e.KeyCode == Keys.W) vz = 0;
            if (e.KeyCode == Keys.S) vz = 0;
        }

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            gr.Clear(Color.White);
            pictureBox1.Image = bitmap;
            timer1.Start();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (pictureBox1.Height > 0 && pictureBox1.Height > 0)
            {
                bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                gr = Graphics.FromImage(bitmap);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            x += vx / 25;
            y += vy / 25;
            z += vz / 25;
            gr.Clear(Color.White);
            try
            {
                if ((1 - z) > 0)                 gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1)), draw_calculation(new Quaternion(-1,  1,  1)));
                if ((1 - z) > 0)                 gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1,  1)), draw_calculation(new Quaternion( 1, -1,  1)));
                if ((1 - z) > 0)                 gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1,  1)), draw_calculation(new Quaternion(-1,  1,  1)));
                if ((1 - z) > 0)                 gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1,  1)), draw_calculation(new Quaternion( 1, -1,  1)));

                if ((-1 - z) > 0)                gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1, -1)), draw_calculation(new Quaternion(-1,  1, -1)));
                if ((-1 - z) > 0)                gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1, -1)), draw_calculation(new Quaternion( 1, -1, -1)));
                if ((-1 - z) > 0)                gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1, -1)), draw_calculation(new Quaternion(-1,  1, -1)));
                if ((-1 - z) > 0)                gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1, -1)), draw_calculation(new Quaternion( 1, -1, -1)));

                if ((1 - z) > 0 && (-1 - z) > 0) gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1,  1, -1)), draw_calculation(new Quaternion( 1,  1,  1)));
                if ((1 - z) > 0 && (-1 - z) > 0) gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion( 1, -1, -1)), draw_calculation(new Quaternion( 1, -1,  1)));
                if ((1 - z) > 0 && (-1 - z) > 0) gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1,  1, -1)), draw_calculation(new Quaternion(-1,  1,  1)));
                if ((1 - z) > 0 && (-1 - z) > 0) gr.DrawLine(new Pen(Color.Black), draw_calculation(new Quaternion(-1, -1, -1)), draw_calculation(new Quaternion(-1, -1,  1)));

                gr.DrawEllipse(new Pen(Color.Red), (float)(pictureBox1.Width / 2 - k), (float)(pictureBox1.Height / 2 - k), (float)(2 * k), (float)(2 * k));
            } catch { }
            pictureBox1.Image = bitmap;

            label1.Text = "x: " + x + "\ny: " + y + "\nz: " + z + "\nмасштаб: " + k + "\nглубина: " + a + "\nвремя: " + t;
            t++;
        }

        Point draw_calculation(Quaternion h)
        {
            Quaternion h0 = h.Rotate(new Quaternion(Math.Sin(0.01 * t), Math.Sin(0.01 * t + 2 * Math.PI / 3), Math.Sin(0.01 * t + 4 * Math.PI / 3)), 1);
            return new Point((int)( (h0.Im - x) / R(h0, new Quaternion(0, x, y, z)) * k) + pictureBox1.Width  / 2,
                             (int)(-(h0.Jm - y) / R(h0, new Quaternion(0, x, y, z)) * k) + pictureBox1.Height / 2);

            double R(Quaternion h1, Quaternion h2)
            {
                return Math.Sqrt((h1.Im - h2.Im) * (h1.Im - h2.Im) + (h1.Jm - h2.Jm) * (h1.Jm - h2.Jm) + (h1.Km - h2.Km) * (h1.Km - h2.Km) * a * a + (h1.Re - h2.Re) * (h1.Re - h2.Re) * a * a);
            }
        }
    }
}
