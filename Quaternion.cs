using System;
using System.Linq;

namespace растеризатор
{
    public class Quaternion
    {
        public double Re;
        public double Im;
        public double Jm;
        public double Km;

        public Quaternion(Quaternion c) // пересоздание
        {
            this.Re = c.Re;
            this.Im = c.Im;
            this.Jm = c.Jm;
            this.Km = c.Km;
        }
        public Quaternion(double Re, double Im, double Jm, double Km) // кватернион
        {
            this.Re = Re;
            this.Im = Im;
            this.Jm = Jm;
            this.Km = Km;
        }
        public Quaternion(double Re) // действительное число
        {
            this.Re = Re;
            this.Im = 0;
            this.Jm = 0;
            this.Km = 0;
        }
        public Quaternion(double x, double y, double z) // трёхмерный вектор
        {
            this.Re = 0;
            this.Im = x;
            this.Jm = y;
            this.Km = z;
        }

        public double Abs() // |h| модуль (абсолютное значение)
        {
            return Math.Sqrt(Re * Re + Im * Im + Jm * Jm + Km * Km);
        }
        public static double Abs(double a) // |h| модуль (абсолютное значение)
        {
            return Math.Abs(a);
        }
        public static double Abs(Quaternion a) // |h| модуль (абсолютное значение)
        {
            return Math.Sqrt(a.Re * a.Re + a.Im * a.Im + a.Jm * a.Jm + a.Km * a.Km);
        }

        public Quaternion Sign() // sgn() знак (направление)
        {
            if (Re == 0 && Im == 0 && Jm == 0 && Km == 0) return new Quaternion(1);
            else return new Quaternion(Re, Im, Jm, Km).Div(new Quaternion(Re, Im, Jm, Km).Abs());
        }
        public static double Sign(double a) // sgn() знак (направление)
        {
            if (a == 0) return 1;
            else return Math.Sign(a);
        }
        public static Quaternion Sign(Quaternion a) // sgn() знак (направление)
        {
            if (a.Re == 0 && a.Im == 0 && a.Jm == 0 && a.Km == 0) return new Quaternion(1);
            else return a.Div(a.Abs());
        }

        public Quaternion Conj() // conj() сопрежённое (Re() - Im())
        {
            return new Quaternion(Re, -Im, -Jm, -Km);
        }
        public static Quaternion Conj(double a)  // conj() сопрежённое (Re() - Im())
        {
            return new Quaternion(a);
        }
        public static Quaternion Conj(Quaternion a)  // conj() сопрежённое (Re() - Im())
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

        public Quaternion Rotate(Quaternion axis, double angle) // трёхмерный поворот
        {
            Quaternion h = new Quaternion(Re, Im, Jm, Km);
            Quaternion q = Exp(angle / 2, axis);
            return q.Mul(h).Mul(q.Conj());
        }
        public static Quaternion Rotate(Quaternion h, Quaternion axis, double angle) // трёхмерный поворот
        {
            Quaternion q = Exp(angle / 2, axis);
            return q.Mul(h).Mul(q.Conj());
        }
        
        public Quaternion RotateW(Quaternion axis, double angle) // четырёхмерный поворот
        {
            Quaternion h = new Quaternion(Re, Im, Jm, Km);
            Quaternion q = Exp(angle / 2, axis);
            return q.Mul(h).Mul(q);
        }
        public static Quaternion RotateW(Quaternion h, Quaternion axis, double angle) // четырёхмерный поворот
        {
            Quaternion q = Exp(angle / 2, axis);
            return q.Mul(h).Mul(q);
        }

        public double Arg() // arg() аргумент кватерниона
        {
        	return Math.Acos(Sign().Re);
        }
        public static double Arg(double a) // arg() аргумент кватерниона
        {
            return Math.Acos(Sign(a));
        }
        public static double Arg(Quaternion a) // arg() аргумент кватерниона
        {
            return Math.Acos(Sign(a).Re);
        }

        public Quaternion Exp() // exp() экспонента
        {
            Quaternion v = new Quaternion(Im, Jm, Km);
            return v.Sign().Mul(Math.Sin(v.Abs())).Sum(Math.Cos(v.Abs())).Mul(Math.Exp(Re)); 
        }
        public Quaternion Exp(double k) // exp() экспонента
        {
            Quaternion h = new Quaternion(Re, Im, Jm, Km);
            Quaternion v = new Quaternion(h.Mul(k).Im, h.Mul(k).Jm, h.Mul(k).Km);
            return v.Sign().Mul(Math.Sin(v.Abs())).Sum(Math.Cos(v.Abs())).Mul(Math.Exp(h.Mul(k).Re));
        }
        public Quaternion Exp(Quaternion q) // exp() экспонента
        {
        	Quaternion h = new Quaternion(Re, Im, Jm, Km);
            Quaternion v = new Quaternion(h.Mul(q).Im, h.Mul(q).Jm, h.Mul(q).Km);
            return v.Sign().Mul(Math.Sin(v.Abs())).Sum(Math.Cos(v.Abs())).Mul(Math.Exp(h.Mul(q).Re));
        }
        public static Quaternion Exp(double h, Quaternion q) // exp() экспонента
        {
            Quaternion v = new Quaternion(q.Mul(h).Im, q.Mul(h).Jm, q.Mul(h).Km);
            return v.Sign().Mul(Math.Sin(v.Abs())).Sum(Math.Cos(v.Abs())).Mul(Math.Exp(q.Mul(h).Re));
        }
        public static Quaternion Exp(Quaternion h, Quaternion q) // exp() экспонента
        {
            Quaternion v = new Quaternion(h.Mul(q).Im, h.Mul(q).Jm, h.Mul(q).Km);
            return v.Sign().Mul(Math.Sin(v.Abs())).Sum(Math.Cos(v.Abs())).Mul(Math.Exp(h.Mul(q).Re));
        }

        //public Quaternion Pow(int b) // ^ возведение в степень через предел
        //{
        //    Quaternion a = new Quaternion(Re, Im, Jm, Km);
        //    Quaternion c = new Quaternion(1);
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
        //public static Quaternion Pow(Quaternion a, int b) // ^ возведение в степень через предел
        //{
        //    Quaternion c = new Quaternion(1);
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
        public Quaternion Pow(double b) // ^ возведение в степень
        {
            Quaternion v = new Quaternion(Im, Jm, Km);
            double c1 = Math.Pow(Abs(), b);
            Quaternion c2 = Exp(Arg() * b, v.Sign());
            if (Abs() == 0) return new Quaternion(0);
            else return Mul(c1, c2);
        }
        public Quaternion Pow(Quaternion b) // ^ возведение в степень
        {
            Quaternion va = new Quaternion(Im, Jm, Km);
            Quaternion vb = new Quaternion(b.Im, b.Jm, b.Km);
            double c1 = Math.Pow(Abs(), b.Re);
            Quaternion c2 = Exp(Arg() * b.Re, va.Sign());
            Quaternion c3 = Exp(Math.Log(Abs()), vb);
            Quaternion c4 = Exp(va.Sign().Mul(Arg()), vb);
            if (Abs() == 0) return new Quaternion(0);
            else return Mul(c1, c2).Mul(c3).Mul(c4);
        }
        public static Quaternion Pow(double a, double b) // ^ возведение в степень
        {
            double c1 = Math.Pow(Abs(a), b);
            Quaternion c2 = Exp(Arg(a) * b, new Quaternion(1, 0, 0));
            if (a == 0) return new Quaternion(0);
            else return Mul(c1, c2);
        }
        public static Quaternion Pow(double a, Quaternion b) // ^ возведение в степень
        {
            Quaternion vb = new Quaternion(b.Im, b.Jm, b.Km);
            double c1 = Math.Pow(Abs(a), b.Re);
            Quaternion c2 = Exp(Arg(a) * b.Re, new Quaternion(1, 0, 0));
            Quaternion c3 = Exp(Math.Log(Abs(a)), vb);
            Quaternion c4 = Exp(Arg(a), vb);
            if (a == 0) return new Quaternion(0);
            else return Mul(c1, c2).Mul(c3).Mul(c4);
        }
        public static Quaternion Pow(Quaternion a, double b) // ^ возведение в степень
        {
            Quaternion v = new Quaternion(a.Im, a.Jm, a.Km);
            double c1 = Math.Pow(a.Abs(), b);
            Quaternion c2 = Exp(a.Arg() * b, v.Sign());
            if (a.Abs() == 0) return new Quaternion(0);
            else return Mul(c1, c2);
        }
        public static Quaternion Pow(Quaternion a, Quaternion b) // ^ возведение в степень
        {
            Quaternion va = new Quaternion(a.Im, a.Jm, a.Km);
            Quaternion vb = new Quaternion(b.Im, b.Jm, b.Km);
            double c1 = Math.Pow(a.Abs(), b.Re);
            Quaternion c2 = Exp(a.Arg() * b.Re, va.Sign());
            Quaternion c3 = Exp(Math.Log(a.Abs()), vb);
            Quaternion c4 = Exp(va.Sign().Mul(a.Arg()), vb);
            if (a.Abs() == 0) return new Quaternion(0);
            else return Mul(c1, c2).Mul(c3).Mul(c4);
        }

        public Quaternion Ln() // ln() натуральный логарифм через предел
        {
            Quaternion h = new Quaternion(Re, Im, Jm, Km);
            return new Quaternion(Im, Jm, Km).Sign().Mul(h.Arg()).Sum(Math.Log(h.Abs()));
        }
        public static Quaternion Ln(double a) // ln() натуральный логарифм через предел
        {
            return new Quaternion(Math.Log(a));
        }
        public static Quaternion Ln(Quaternion h) // ln() натуральный логарифм через предел
        {
            return new Quaternion(h.Im, h.Jm, h.Km).Sign().Mul(h.Arg()).Sum(Math.Log(h.Abs()));
        }

        public Quaternion Log(double a) // log() логарифм
        {
            Quaternion b = new Quaternion(Re, Im, Jm, Km);
            return Div(Ln(b), Ln(a));
        }
        public Quaternion Log(Quaternion a) // log() логарифм
        {
            Quaternion b = new Quaternion(Re, Im, Jm, Km);
            return Div(Ln(b), Ln(a));
        }
        public static Quaternion Log(double a, double b) // log() логарифм
        {
            return Div(Ln(b), Ln(a));
        }
        public static Quaternion Log(double a, Quaternion b) // log() логарифм
        {
            return Div(Ln(b), Ln(a));
        }
        public static Quaternion Log(Quaternion a, double b) // log() логарифм
        {
            return Div(Ln(b), Ln(a));
        }
        public static Quaternion Log(Quaternion a, Quaternion b) // log() логарифм
        {
            return Div(Ln(b), Ln(a));
        }
        
        public Quaternion Cosh() // ch() гиперболический косинус
        {
            Quaternion h = new Quaternion(Re, Im, Jm, Km);
            return h.Exp().Sum(h.Exp(-1)).Div(2);
        }
        public static Quaternion Cosh(double h) // ch() гиперболический косинус
        {
            return new Quaternion(Math.Cosh(h));
        }
        public static Quaternion Cosh(Quaternion h) // ch() гиперболический косинус
        {
            return h.Exp().Sum(h.Exp(-1)).Div(2);
        }

        public Quaternion Sinh() // sh() гиперболический синус
        {
            Quaternion h = new Quaternion(Re, Im, Jm, Km);
            return h.Exp().Sub(h.Exp(-1)).Div(2);
        }
        public static Quaternion Sinh(double h) // sh() гиперболический синус
        {
            return new Quaternion(Math.Sinh(h));
        }
        public static Quaternion Sinh(Quaternion h) // sh() гиперболический синус
        {
            return h.Exp().Sub(h.Exp(-1)).Div(2);
        }
        
        public Quaternion Cos() // cos() косинус
        {
            Quaternion a = new Quaternion(Re, Im, Jm, Km);
            return Cosh(a.Mul(new Quaternion(Im, Jm, Km).Sign()));
        }
        public static Quaternion Cos(double a) // cos() косинус
        {
            return new Quaternion(Math.Cos(a));
        }
        public static Quaternion Cos(Quaternion a) // cos() косинус
        {
        	return Cosh(a.Mul(new Quaternion(a.Im, a.Jm, a.Km).Sign()));
        }
        
        public Quaternion Sin() // sin() синус
        {
            Quaternion a = new Quaternion(Re, Im, Jm, Km);
            return Sinh(a.Mul(new Quaternion(Im, Jm, Km).Sign())).Div(new Quaternion(Im, Jm, Km).Sign());
        }
        public static Quaternion Sin(double a) // sin() синус
        {
            return new Quaternion(Math.Sin(a));
        }
        public static Quaternion Sin(Quaternion a) // sin() синус
        {
        	return Sinh(a.Mul(new Quaternion(a.Im, a.Jm, a.Km).Sign())).Div(new Quaternion(a.Im, a.Jm, a.Km).Sign());
        }
    }
}