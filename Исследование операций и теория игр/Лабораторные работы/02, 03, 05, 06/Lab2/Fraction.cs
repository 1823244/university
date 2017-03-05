using System;

namespace FractionNameSpace
{
    public class Fraction
    {
        /*
         * Поля
        */
        //Числитель
        int numerator;
        //Знаменатель
        int denominator;
        /*
         * Конструкторы
        */
        public Fraction()
        {
            numerator = 0;
            denominator = 1;
        }

        public Fraction(int number)
        {
            numerator = number;
            denominator = 1;
        }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new Exception("Denominator == 0!");
            }
            this.numerator = denominator < 0 ? -numerator : numerator;
            this.denominator = denominator < 0 ? -denominator : denominator;

            Reduce();
        }
        /*
         * Свойства
        */
        public int Numerator
        {
            get
            {
                return this.numerator;
            }
        }

        public int Denominator
        {
            get
            {
                return this.denominator;
            }
        }
        /*
         * Методы
         * public
        */
        /*
         * private
        */
        //Сокращение дроби
        private void Reduce()
        {
            if (Math.Abs(numerator) == 1)
            {
                return;
            }
            if (numerator == 0)
            {
                denominator = 1;
                return;
            }

            int gcd = GCD(numerator, denominator);
            if (gcd != 1)
            {
                numerator /= gcd;
                denominator /= gcd;
            }

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
        }
        /*
         * static
        */
        /*
         * Наибольший общий делитель (Greatest Common Divisor)
         * 
         * @a	Первое число
         * @b	Второе число
         * 
         * Установим эти два числа в порядке убывания
         * Поучаем остаток от деления a на b, как только остаток равен нулю
         * значит b - результат
        */
        public static int GCD(int a, int b)
        {
            if (a < b)
            {
                int c = a;
                a = b;
                b = c;
            }

            int result = a % b;
            while (result != 0)
            {
                a = b;
                b = result;
                result = a % b;
            }

            return b;
        }
        /*
         * Наименьшее общее кратное (Least Common Multiple)
         * 
         * @a	Первое число
         * @b	Второе число
         * 
         * Модуль произведения a и b делим на наибольший общий делитель
        */
        public static int LCM(int a, int b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }
        /*
         * Приведение типов
        */
        public static explicit operator String(Fraction x)
        {
            String temp;
            if (x.Denominator != 1)
            {
                temp = x.Numerator + "/" + x.Denominator;
            }
            else
            {
                temp = "" + x.Numerator;
            }
            return temp;
        }

        public static explicit operator int(Fraction x)
        {
            return (int)(x.Numerator / (double)x.Denominator);
        }

        public static explicit operator double(Fraction x)
        {
            return x.Numerator / (double)x.Denominator;
        }

        public static explicit operator Fraction(String s)
        {
            String[] tmp = s.Split('/');
            int a = Int32.Parse(tmp[0]);
            int b = Int32.Parse(tmp[1]);
            return new Fraction(a, b);
        }
        /*
         * Перегрузка операторов
        */
        public static Fraction operator +(Fraction a, Fraction b)
        {
            if (a.Denominator == b.Denominator)
            {
                return new Fraction(a.Numerator + b.Numerator, a.Denominator);
            }
            else
            {
                int lcm = LCM(a.Denominator, b.Denominator);
                int aNumerator = a.Numerator * lcm / a.Denominator;
                int bNumerator = b.Numerator * lcm / b.Denominator;
                return new Fraction(aNumerator + bNumerator, lcm);
            }
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            if (a.Denominator == b.Denominator)
            {
                return new Fraction(a.Numerator - b.Numerator, a.Denominator);
            }
            else
            {
                int lcm = LCM(a.Denominator, b.Denominator);
                int aNumerator = a.Numerator * lcm / a.Denominator;
                int bNumerator = b.Numerator * lcm / b.Denominator;
                return new Fraction(aNumerator - bNumerator, lcm);
            }
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public static Fraction operator *(Fraction a, int b)
        {
            return new Fraction(a.Numerator * b, a.Denominator);
        }

        public static Fraction operator /(Fraction a, int b)
        {
            return new Fraction(a.Numerator, a.Denominator * b);
        }

        public static Fraction operator -(Fraction a)
        {
            return new Fraction(-a.Numerator, a.Denominator);
        }
        public static bool operator <(Fraction a, Fraction b)
        {
            return (a.Numerator / (double)a.Denominator) < (b.Numerator / (double)b.Denominator);
        }
        public static bool operator >(Fraction a, Fraction b)
        {
            return (a.Numerator / (double)a.Denominator) > (b.Numerator / (double)b.Denominator);
        }
        public static bool operator ==(Fraction a, Fraction b)
        {
            return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            return (a.Numerator / (double)a.Denominator) != (b.Numerator / (double)b.Denominator);
        }
        public static bool operator >=(Fraction a, Fraction b)
        {
            return (a.Numerator / (double)a.Denominator) >= (b.Numerator / (double)b.Denominator);
        }
        public static bool operator <=(Fraction a, Fraction b)
        {
            return (a.Numerator / (double)a.Denominator) <= (b.Numerator / (double)b.Denominator);
        }
        //int
        public static bool operator <(Fraction a, int b)
        {
            return a.Numerator / (double)a.Denominator < b;
        }
        public static bool operator >(Fraction a, int b)
        {
            return a.Numerator / (double)a.Denominator > b;
        }
        public static bool operator ==(Fraction a, int b)
        {
            return a.Numerator / (double)a.Denominator == b;
        }
        public static bool operator !=(Fraction a, int b)
        {
            return a.Numerator / (double)a.Denominator != b;
        }
        public static bool operator >=(Fraction a, int b)
        {
            return a.Numerator / (double)a.Denominator >= b;
        }
        public static bool operator <=(Fraction a, int b)
        {
            return a.Numerator / (double)a.Denominator <= b;
        }
    }
}