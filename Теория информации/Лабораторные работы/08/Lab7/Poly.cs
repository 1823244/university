using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7
{
    class Poly
    {
        public int[] coef;

        public Poly()
        {
            coef = new int[15];
        }

        public Poly setPoly(String coef)
        {
            for (int i = coef.Length - 1, j = 0; i >= 0; i--)
            {
                this.coef[j++] = coef[i] == '1' ? 1 : 0;
            }
            return this;
        }
        public Poly setStepen(int a)
        {
            this.coef = new int[15];
            this.coef[a] = 1;
            return this;
        }
        public Poly setStepens(int[] a)
        {
            foreach (int t in a)
            {
                this.coef[t] = 1;
            }
            return this;
        }

        public Poly plus(Poly a)
        {
            Poly newPoly = new Poly();
            this.coef.CopyTo(newPoly.coef, 0);

            for (int i = 0; i < 15; i++)
            {
                if (newPoly.coef[i] != a.coef[i]) {
                    newPoly.coef[i] = 1;
                } else {
                    newPoly.coef[i] = 0;
                }
            }

            return newPoly;
        }
        public Poly ymnoj(Poly a)
        {
            Poly newPoly = new Poly();

            for (int i = 0; i < 15; i++)
            {
                if (this.coef[i] == 0) continue;
                for (int j = 0; j < 15; j++)
                {
                    if (a.coef[j] == 0) continue;
                    if (i + j < 15)
                    {
                        newPoly.coef[i + j] = newPoly.coef[i + j] == 0 ? 1 : 0;
                    }
                }
            }

            return newPoly;
        }
        public Poly ostdelenie(Poly a)
        {
            Poly newPoly = new Poly();
            this.coef.CopyTo(newPoly.coef, 0);

            for (int i = 14; i >= 0; i--)
            {
                if (newPoly.coef[i] == 0) continue;
                for (int j = 14; j >= 0; j--)
                {
                    if (a.coef[j] == 0) continue;
                    if (i - j >= 0)
                    {
                        for (int k = 0; k <= j; k++)
                        {
                            newPoly.coef[i - k] = newPoly.coef[i - k] != a.coef[j - k] ? 1 : 0;
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return newPoly;
        }

        public String text()
        {
            String res = "";
            bool flag = false;

            for (int i = 14; i >= 0; i--)
            {
                if (!flag && this.coef[i] == 0) { continue; }
                flag = true;
                res += this.coef[i];
            }

            return res;
        }

    }
}
