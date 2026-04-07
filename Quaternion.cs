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
    public class Quaternion
    {
        public double Re;
        public double Im;
        public double Jm;
        public double Km;

        public Quaternion(Quaternion c)
        {
            this.Re = c.Re;
            this.Im = c.Im;
            this.Jm = c.Jm;
            this.Km = c.Km;
        }
        public Quaternion(double Re, double Im, double Jm, double Km)
        {
            this.Re = Re;
            this.Im = Im;
            this.Jm = Jm;
            this.Km = Km;
        }
        public Quaternion(double Re)
        {
            this.Re = Re;
            this.Im = 0;
            this.Jm = 0;
            this.Km = 0;
        }
        public Quaternion(double x, double y, double z)
        {
            this.Re = 0;
            this.Im = x;
            this.Jm = y;
            this.Km = z;
        }

        public double Abs() // |z| модуль (абсолютное значение)
        {
            return Math.Sqrt(Re * Re + Im * Im + Jm * Jm + Km * Km);
        }
        public static double Abs(double a) // |z| модуль (абсолютное значение)
        {
            return Math.Abs(a);
        }
        public static double Abs(Quaternion a) // |z| модуль (абсолютное значение)
        {
            return Math.Sqrt(a.Re * a.Re + a.Im * a.Im + a.Jm * a.Jm + a.Km * a.Km);
        }

        public Quaternion Sign() // sign(z) направление
        {
            if (Re == 0 && Im == 0 && Jm == 0 && Km == 0) return new Quaternion(1);
            else return new Quaternion(Re, Im, Jm, Km).Div(new Quaternion(Re, Im, Jm, Km).Abs());
        }
        public static double Sign(double a) // sign(z) направление
        {
            if (a == 0) return 1;
            else return Math.Sign(a);
        }
        public static Quaternion Sign(Quaternion a) // sign(z) направление
        {
            if (a.Re == 0 && a.Im == 0 && a.Jm == 0 && a.Km == 0) return new Quaternion(1);
            else return a.Div(a.Abs());
        }

        public Quaternion Conj() // conj(z) комплексносопрежённое (Re(z) - Im(z))
        {
            return new Quaternion(Re, -Im, -Jm, -Km);
        }
        public static Quaternion Conj(double a)  // conj(z) комплексносопрежённое (Re(z) - Im(z))
        {
            return new Quaternion(a);
        }
        public static Quaternion Conj(Quaternion a)  // conj(z) комплексносопрежённое (Re(z) - Im(z))
        {
            return new Quaternion(a.Re, -a.Im, -a.Jm, -a.Km);
        }

        public Quaternion Sum(double b) // + сложение
        {
            return new Quaternion((Re + b), Im, Jm, Km);
        }
        public Quaternion Sum(Quaternion b) // + сложение
        {
            return new Quaternion((Re + b.Re), (Im + b.Im), (Jm + b.Jm), (Km + b.Km));
        }
        public static double Sum(double a, double b) // + сложение
        {
            return a + b;
        }
        public static Quaternion Sum(Quaternion a, double b) // + сложение
        {
            return new Quaternion((a.Re + b), a.Im, a.Jm, a.Km);
        }
        public static Quaternion Sum(double a, Quaternion b) // + сложение
        {
            return new Quaternion((a + b.Re), b.Im, b.Jm, b.Km);
        }
        public static Quaternion Sum(Quaternion a, Quaternion b) // + сложение
        {
            return new Quaternion((a.Re + b.Re), (a.Im + b.Im), (a.Jm + b.Jm), (a.Km + b.Km));
        }

        public Quaternion Sub(double b) // - вычитание
        {
            return new Quaternion((Re - b), Im, Jm, Km);
        }
        public Quaternion Sub(Quaternion b) // - вычитание
        {
            return new Quaternion((Re - b.Re), (Im - b.Im), (Jm - b.Jm), (Km - b.Km));
        }
        public static double Sub(double a, double b) // - вычитание
        {
            return a - b;
        }
        public static Quaternion Sub(Quaternion a, double b) // - вычитание
        {
            return new Quaternion((a.Re - b), a.Im, a.Jm, a.Km);
        }
        public static Quaternion Sub(double a, Quaternion b) // - вычитание
        {
            return new Quaternion((a - b.Re), -b.Im, -b.Jm, -b.Km);
        }
        public static Quaternion Sub(Quaternion a, Quaternion b) // - вычитание
        {
            return new Quaternion((a.Re - b.Re), (a.Im - b.Im), (a.Jm - b.Jm), (a.Km - b.Km));
        }

        public Quaternion Mul(double b) // * умножение
        {
            return new Quaternion((Re * b), (Im * b), (Jm * b), (Km * b));
        }
        public Quaternion Mul(Quaternion b) // * умножение
        {
            return new Quaternion((Re * b.Re - Im * b.Im - Jm * b.Jm - Km * b.Km), 
                                  (Re * b.Im + Im * b.Re + Jm * b.Km - Km * b.Jm),
                                  (Re * b.Jm + Jm * b.Re + Km * b.Im - Im * b.Km),
                                  (Re * b.Km + Km * b.Re + Im * b.Jm - Jm * b.Im));
        }
        public static double Mul(double a, double b) // * умножение
        {
            return a * b;
        }
        public static Quaternion Mul(Quaternion a, double b) // * умножение
        {
            return new Quaternion((a.Re * b), (a.Im * b), (a.Jm * b), (a.Km * b));
        }
        public static Quaternion Mul(double a, Quaternion b) // * умножение
        {
            return new Quaternion((a * b.Re), (a * b.Im), (a * b.Jm), (a * b.Km));
        }
        public static Quaternion Mul(Quaternion a, Quaternion b) // * умножение
        {
            return new Quaternion((a.Re * b.Re - a.Im * b.Im - a.Jm * b.Jm - a.Km * b.Km),
                                  (a.Re * b.Im + a.Im * b.Re + a.Jm * b.Km - a.Km * b.Jm),
                                  (a.Re * b.Jm + a.Jm * b.Re + a.Km * b.Im - a.Im * b.Km),
                                  (a.Re * b.Km + a.Km * b.Re + a.Im * b.Jm - a.Jm * b.Im));
        }

        public Quaternion Div(double b) // / деление
        {
            return new Quaternion((Re / b), (Im / b), (Jm / b), (Km / b));
        }
        public Quaternion Div(Quaternion b) // / деление
        {
            Quaternion a = new Quaternion(Re, Im, Jm, Km);
            return a.Mul(b.Conj()).Div(b.Abs() * b.Abs());
        }
        public static double Div(double a, double b) // / деление
        {
            return a / b;
        }
        public static Quaternion Div(Quaternion a, double b) // / деление
        {
            return new Quaternion((a.Re / b), (a.Im / b), (a.Jm / b), (a.Km / b));
        }
        public static Quaternion Div(double a, Quaternion b) // / деление
        {
            return new Quaternion(a).Mul(b.Conj()).Div(b.Abs() * b.Abs());
        }
        public static Quaternion Div(Quaternion a, Quaternion b) // / деление
        {
            return a.Mul(b.Conj()).Div(b.Abs() * b.Abs());
        }

        public Quaternion Rotate(Quaternion axis, double angle)
        {
            Quaternion h = new Quaternion(Re, Im, Jm, Km);
            Quaternion q = new Quaternion(Math.Cos(angle / 2 * axis.Abs()),
                                          Math.Sin(angle / 2 * axis.Abs()) * axis.Sign().Im,
                                          Math.Sin(angle / 2 * axis.Abs()) * axis.Sign().Jm,
                                          Math.Sin(angle / 2 * axis.Abs()) * axis.Sign().Km);
            return q.Mul(h).Mul(q.Conj());
        }
        public static Quaternion Rotate(Quaternion h, Quaternion axis, double angle)
        {
            Quaternion q = new Quaternion(Math.Cos(angle / 2 * axis.Abs()),
                                          Math.Sin(angle / 2 * axis.Abs()) * axis.Sign().Im, 
                                          Math.Sin(angle / 2 * axis.Abs()) * axis.Sign().Jm, 
                                          Math.Sin(angle / 2 * axis.Abs()) * axis.Sign().Km);
            return q.Mul(h).Mul(q.Conj());
        }

        //public double Angle() // угол направления числа
        //{
        //    return Math.Acos(Sign().Re) * Sign(Math.Asin(Sign().Im));
        //}
        //public static double Angle(double a) // угол направления числа
        //{
        //    return Math.Acos(Sign(a));
        //}
        //public static double Angle(Complex a) // угол направления числа
        //{
        //    return Math.Acos(Sign(a).Re) * Sign(Math.Asin(Sign(a).Im));
        //}

        //public Complex Drct() // числовое направление угла
        //{
        //    return Sum(Cos(), Mul(Sin(), new Complex(0, 1)));
        //}
        //public static Complex Drct(double a) // числовое направление угла
        //{
        //    return new Complex(Math.Cos(a), Math.Sin(a));
        //}
        //public static Complex Drct(Complex a) // числовое направление угла
        //{
        //    return Sum(Cos(a), Mul(Sin(a), new Complex(0, 1)));
        //}

        //public Complex Exp(int b) // ^ возведение в степень через предел
        //{
        //    Complex a = new Complex(Re, Im);
        //    Complex c = new Complex(1);
        //    if (b >= 0) for (int i = 0; i < b; i++)
        //        {
        //            c = c.Mul(a);
        //        }
        //    else for (int i = 0; i < -b; i++)
        //        {
        //            c = c.Div(a);
        //        }
        //    return c;
        //}
        //public Complex Exp(double b) // ^ возведение в степень через предел
        //{
        //    Complex a = new Complex(Re, Im);
        //    Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
        //    Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
        //    Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
        //    return c3;
        //}
        //public Complex Exp(Complex b) // ^ возведение в степень через предел
        //{
        //    Complex a = new Complex(Re, Im);
        //    Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
        //    Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
        //    Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
        //    return c3;
        //}
        //public static Complex Exp(Complex a, int b) // ^ возведение в степень через предел
        //{
        //    Complex c = new Complex(1);
        //    if (b >= 0) for (int i = 0; i < b; i++)
        //        {
        //            c = c.Mul(a);
        //        }
        //    else for (int i = 0; i < -b; i++)
        //        {
        //            c = c.Div(a);
        //        }
        //    return c;
        //}
        //public static Complex Exp(double a, double b) // ^ возведение в степень через предел
        //{
        //    Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(new Complex(a)) * Math.Pow(0.1, d)));
        //    Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
        //    Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
        //    return c3;
        //}
        //public static Complex Exp(double a, Complex b) // ^ возведение в степень через предел
        //{
        //    Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(new Complex(a)) * Math.Pow(0.1, d)));
        //    Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
        //    Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
        //    return c3;
        //}
        //public static Complex Exp(Complex a, double b) // ^ возведение в степень через предел
        //{
        //    Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
        //    Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
        //    Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
        //    return c3;
        //}
        //public static Complex Exp(Complex a, Complex b) // ^ возведение в степень через предел
        //{
        //    Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
        //    Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
        //    Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
        //    return c3;
        //}

        //public Complex Pow(double b) // ^ возведение в степень
        //{
        //    Complex a = new Complex(Re, Im);
        //    double c1 = Math.Pow(a.Abs(), b);
        //    Complex c2 = Drct(a.Angle() * b);
        //    return Mul(c1, c2);
        //}
        //public Complex Pow(Complex b) // ^ возведение в степень
        //{
        //    Complex a = new Complex(Re, Im);
        //    double c1 = Math.Pow(a.Abs(), b.Re);
        //    Complex c2 = Drct(a.Angle() * b.Re);
        //    Complex c3 = Drct(Math.Log(a.Abs()) * b.Im);
        //    double c4 = Math.Pow(Math.E, -b.Im * a.Angle());
        //    if (a.Re == 0 && a.Im == 0) return new Complex(0);
        //    else return Mul(Mul(c1, c2), Mul(c3, c4));
        //}
        //public static Complex Pow(double a, double b) // ^ возведение в степень
        //{
        //    double c1 = Math.Pow(Abs(a), b);
        //    Complex c2 = Drct(Angle(a) * b);
        //    return Mul(c1, c2);
        //}
        //public static Complex Pow(double a, Complex b) // ^ возведение в степень
        //{
        //    double c1 = Math.Pow(a, b.Re);
        //    Complex c2 = Drct(Angle(a) * b.Re);
        //    Complex c3 = Drct(Math.Log(Abs(a)) * b.Im);
        //    double c4 = Math.Pow(Math.E, -b.Im * Angle(a));
        //    if (a == 0) return new Complex(0);
        //    else return Mul(Mul(c1, c2), Mul(c3, c4));
        //}
        //public static Complex Pow(Complex a, double b) // ^ возведение в степень
        //{
        //    double c1 = Math.Pow(a.Abs(), b);
        //    Complex c2 = Drct(a.Angle() * b);
        //    return Mul(c1, c2);
        //}
        //public static Complex Pow(Complex a, Complex b) // ^ возведение в степень
        //{
        //    double c1 = Math.Pow(a.Abs(), b.Re);
        //    Complex c2 = Drct(a.Angle() * b.Re);
        //    Complex c3 = Drct(Math.Log(a.Abs()) * b.Im);
        //    double c4 = Math.Pow(Math.E, -b.Im * a.Angle());
        //    if (a.Re == 0 && a.Im == 0) return new Complex(0);
        //    else return Mul(Mul(c1, c2), Mul(c3, c4));
        //}

        //public Complex Ln() // ln() натуральный логарифм через предел
        //{
        //    Complex a = new Complex(Re, Im);
        //    Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
        //    Complex c2 = Mul(Math.Pow(10, d), Sub(c1, 1));
        //
        //    return c2;
        //}
        //public static Complex Ln(double a) // ln() натуральный логарифм через предел
        //{
        //    return new Complex(Math.Log(a));
        //}
        //public static Complex Ln(Complex a) // ln() натуральный логарифм через предел
        //{
        //    Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
        //    Complex c2 = Mul(Math.Pow(10, d), Sub(c1, 1));
        //
        //    return c2;
        //}

        //public Complex Log(double a) // log() логарифм
        //{
        //    Complex b = new Complex(Re, Im);
        //    return Div(Ln(b), Ln(a));
        //}
        //public Complex Log(Complex a) // log() логарифм
        //{
        //    Complex b = new Complex(Re, Im);
        //    return Div(Ln(b), Ln(a));
        //}
        //public static Complex Log(double a, double b) // log() логарифм
        //{
        //    return Div(Ln(b), Ln(a));
        //}
        //public static Complex Log(double a, Complex b) // log() логарифм
        //{
        //    return Div(Ln(b), Ln(a));
        //}
        //public static Complex Log(Complex a, double b) // log() логарифм
        //{
        //    return Div(Ln(b), Ln(a));
        //}
        //public static Complex Log(Complex a, Complex b) // log() логарифм
        //{
        //    return Div(Ln(b), Ln(a));
        //}

        //public Complex Sin() // sin() синус
        //{
        //    Complex a = new Complex(Re, Im);
        //    return Div(Sub(Pow(Math.E, Mul(a, new Complex(0, 1))), Pow(Math.E, Mul(a, new Complex(0, -1)))), new Complex(0, 2));
        //}
        //public static Complex Sin(double a) // sin() синус
        //{
        //    return new Complex(Math.Sin(a));
        //}
        //public static Complex Sin(Complex a) // sin() синус
        //{
        //    return Div(Sub(Pow(Math.E, Mul(a, new Complex(0, 1))), Pow(Math.E, Mul(a, new Complex(0, -1)))), new Complex(0, 2));
        //}

        //public Complex Cos() // cos() косинус
        //{
        //    Complex a = new Complex(Re, Im);
        //    return Div(Sum(Pow(Math.E, Mul(a, new Complex(0, 1))), Pow(Math.E, Mul(a, new Complex(0, -1)))), 2);
        //}
        //public static Complex Cos(double a) // cos() косинус
        //{
        //    return new Complex(Math.Cos(a));
        //}
        //public static Complex Cos(Complex a) // cos() косинус
        //{
        //    return Div(Sum(Pow(Math.E, Mul(a, new Complex(0, 1))), Pow(Math.E, Mul(a, new Complex(0, -1)))), 2);
        //}
    }
}
