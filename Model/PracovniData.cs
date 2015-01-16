using System;
using System.Collections;
//using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{

    public class PracovniData : IEnumerable
    {

        public virtual IEnumerator GetEnumerator()
        {
            return new PracovniData.Enumerator(this);
        }

        private class Enumerator : IEnumerator
        {
            PracovniData pracData;
            int actualIndex = -1;

            internal Enumerator(PracovniData pracData)
            {
                this.pracData = pracData;
            }

            public object Current
            {
                get
                {
                    if (actualIndex == pracData.pocetPolozek) throw new InvalidOperationException();
                    return pracData.itemArray[actualIndex];
                }
            }

            public bool MoveNext()
            {
                if (actualIndex > pracData.pocetPolozek) throw new InvalidOperationException();
                return ++actualIndex < pracData.pocetPolozek;
            }

            public void Reset()
            {
                actualIndex = -1;
            }
        }


        public class Item
        {
            private double _r, _fi, _re, _fie, _xe, _ye;

            public Item(double r, double fi, double re, double fie, double xe, double ye)
            {
                this._r = r;
                this._fi = fi;
                this._re = re;
                this._fie = fie;
                this._xe = xe;
                this._ye = ye;
            }

            public Item()
            {
                this._r = 0;
                this._fi = 0;
                this._re = 0;
                this._fie = 0;
                this._xe = 0;
                this._ye = 0;
            }


            public double r
            {
                get { return _r; }
                set { _r = value; }
            }
                
            public double fi 
            {
                get { return _fi; }
                set { _fi = value; }
            }

            public double re
            {
                get { return _re; }
                set { _re = value; }
            }

            public double fie
            {
                get { return _fie; }
                set { _fie = value; }
            }

            public double xe
            {
                get { return _xe; }
                set { _xe = value; }
            }

            public double ye
            {
                get { return _ye; }
                set { _ye = value; }
            }

        }





        public decimal DP;
        public decimal V;
        public decimal T;
        public decimal SS;
        public decimal E;
        public decimal PT;
        public decimal PS;
        public decimal PD;
        public decimal DD;
        public decimal P0;
        public decimal P2;
        public decimal P3;
        public decimal U;
        public decimal POZNEK;
        public decimal RT;
        //pom2 = POVRCH  true = vnejsi
        public Boolean POVRCH;


        private double ro;
        private double zo;
        private double vr;
        private double px;
        private double pmr;
        private double mr;
        private double m;

        Item[] itemArray;

        double malyPrumer, velkyPrumer, nekruhovitost;

        private Int32 pocetPolozek;

        public PracovniData()
        {
            defaultData();
        }

        private void defaultData()
        {

            DP = 1;
            V = 1;
            T = 1;
            SS = 0;
            E = 1;
            PT = 1;
            PS = 0;
            PD = 0;
            DD = 1;
            P0 = 1;
            P2 = 0;
            P3 = 0;

            U = 1;
            POZNEK = 0;
            RT = 1;
            POVRCH = true;

            pocetPolozek = 0;

        }


        private double sqr(double x)
        {
            return x * x;
        }


        public double MalyPrumer
        {
            get { return malyPrumer; }
            set { }
        }

        public double VelkyPrumer
        {
            get { return velkyPrumer; }
            set { }
        }

        public double Nekruhovitost
        {
            get { return nekruhovitost; }
            set { }
        }


        private void hlavniVypocet(Int32 i, Int32 j, Int32 n, double k1, double k2, double zv, double fr, double rm)
        {
            double fir, fiep, ed, tau, gama;
            double v1, v2, v3, v4, v5, v6, v7, v8;
            double v1d, v2d, v3d, v4d, v6d, v7d, v8d;
            double sdelta, cdelta, delta;
            double alfa;

            if (itemArray[i] == null)
            {
                itemArray[i] = new Item();
            }

            fir = ((double)j /n) * Math.PI;
            itemArray[i].fi = (fir / Math.PI) * 180;

            ed = zv + (Decimal.ToDouble(PD / 2)) + fr;
            //  if not pom2 then ed:=fr+rt-zv-pd/2;
            if (POVRCH) ed = fr + Decimal.ToDouble(RT) - zv - Decimal.ToDouble(PD) / 2;
            v1 = k1 * fir * fir * Math.Cos(fir);
            v2 = -k1 * fir * fir * Math.Cos(fir) * Math.Cos(fir) / 2;
            v3 = k1 * fir * Math.Sin(fir);
            v4 = k1 * Math.Sin(fir) * Math.Sin(fir) / 2;
            v5 = k2;
            v6 = k2 * fir * Math.Sin(fir) / 2;
            v7 = k2 * 0.111 * Decimal.ToDouble(P2) * Math.Cos(2 * fir) / Decimal.ToDouble(P0);
            v8 = k2 * 0.016 * Decimal.ToDouble(P3) * Math.Cos(3 * fir) / Decimal.ToDouble(P0);
            itemArray[i].r = rm + v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8;
            v1d = k1 * (2 * fir * Math.Cos(fir) - fir * fir * Math.Sin(fir));
            v2d = -k1 / 2 * (2 * fir * Math.Cos(fir) * Math.Cos(fir) - fir * fir * 2 * Math.Cos(fir) * Math.Sin(fir));
            v3d = k1 * (Math.Sin(fir) + fir * Math.Cos(fir));
            v4d = k1 * (Math.Sin(fir) * Math.Cos(fir));
            v6d = k2 / 2 * (Math.Sin(fir) + fir * Math.Cos(fir));
            v7d = -k2 * 0.111 * Decimal.ToDouble(P2) * Math.Sin(2 * fir) * 2 / Decimal.ToDouble(P0);
            v8d = -k2 * 0.016 * Decimal.ToDouble(P3) * Math.Sin(3 * fir) * 3 / Decimal.ToDouble(P0);
            double rd = v1d + v2d + v3d + v4d + v6d + v7d + v8d;

            if (POVRCH)
            {
                if (rd != 0)
                {


                    tau = Math.Atan(itemArray[i].r / rd);
                    gama = (Math.PI / 2) + Math.Abs(tau);
                    itemArray[i].re = Math.Sqrt(sqr(itemArray[i].r) + sqr(ed) - 2 * itemArray[i].r * ed * Math.Cos(gama));
                    sdelta = ed / itemArray[i].re  * Math.Sin(gama);
                    cdelta = (sqr(itemArray[i].re) + sqr(itemArray[i].r) - sqr(ed)) / (2 * itemArray[i].re * itemArray[i].r);
                    delta = Math.Atan(sdelta / cdelta);
                    if (tau > 0)
                    {
                        fiep = fir - delta;
                    }
                    else
                    {
                        fiep = fir + delta;
                    }
                }
                else
                {
                    fiep = fir;
                    itemArray[i].re = itemArray[i].r + ed;
                    tau = Math.PI / 2;
                }
                itemArray[i].fie  = fiep / (Math.PI * 180);
                alfa = fiep;
            }

            else
            {
                if (rd != 0)
                {
                    tau = Math.Atan(itemArray[i].r / rd);
                    gama = Math.PI / 2 - Math.Abs(tau);
                    itemArray[i].re = Math.Sqrt(sqr(itemArray[i].r) + sqr(ed) - 2 * itemArray[i].r * ed * Math.Cos(gama));
                    sdelta = ed / itemArray[i].re * Math.Sin(gama);
                    cdelta = (sqr(itemArray[i].re) + sqr(itemArray[i].r) - sqr(ed)) / (2 * itemArray[i].re * itemArray[i].r);
                    delta = Math.Atan(sdelta / cdelta);
                    if (tau > 0)
                    {
                        fiep = fir + delta;
                    }
                    else
                    {
                        fiep = fir - delta;
                    }
                }
                else
                {
                    fiep = fir;
                    itemArray[i].re = itemArray[i].r - ed;
                    tau = Math.PI / 2;
                }
                itemArray[i].fie = fiep / (Math.PI * 180);
                alfa = fiep;
            }
            itemArray[i].xe = itemArray[i].re * Math.Cos(alfa);
            itemArray[i].ye = itemArray[i].re * Math.Sin(alfa);
            //  {IF XE[i]>vr THEN vr = re[i];}
            //  {IF (PX>XE[I]) AND (XE[I] <= 0) THEN BEGIN PMR:=RE[I];PX:=XE[I];END;}
            //  {if trunc(alfa)=0 then mr:=xe[i];}
            if (itemArray[i].ye > vr) vr = itemArray[i].ye ;
            if ((itemArray[i].xe > mr) && (itemArray[i].xe > 0)) mr = itemArray[i].xe;
            if ((itemArray[i].xe < pmr) && (itemArray[i].xe < 0)) pmr = itemArray[i].xe;
            {

                //    str(vr:8:4,pomstr);write (pomstr,'  ');
                //    str(mr:8:4,pomstr);write (pomstr,'  ');
                //    str(xe[i]:8:4,pomstr);write (pomstr,'  ');
                //    str(pmr:8:4,pomstr);writeln (pomstr);
            }


        }

        public void vypocet()
        {
            double fr = Decimal.ToDouble(DD) / 2;
            vr = 0; px = 0; pmr = 0; mr = 0;

            if ((P2 == 0) && (P3 == 0)) P0 = 1;
            else
            {
                if (P3 == 0) P3 = (decimal)0.0001;
            }
            ro = (Decimal.ToDouble(DP + T)) / 2;
            zo = (Decimal.ToDouble(T * T)) / (12 * ro);
            double p = 2 * (Decimal.ToDouble(PT)) / (Decimal.ToDouble(V * DP));

//            decimal sss = ((DP / T - 1) * (DP / T - 1) * (DP / T - 1)) * PT/(E * V * PS);

//            s:=14.14 * Decimal.ToDouble(((DP/T-1)*(DP/T-1)*(DP/T-1))*PT/(E*V*ps)+SS+0.02*DP);

            double s = 14.14 * ((Decimal.ToDouble(DP) / Decimal.ToDouble(T) - 1) * (Decimal.ToDouble(DP) / Decimal.ToDouble(T) - 1) * (Decimal.ToDouble(DP) / Decimal.ToDouble(T) - 1)) * Decimal.ToDouble(PT) / (Decimal.ToDouble(E * V * PS)) + Decimal.ToDouble(SS) + 0.02 * Decimal.ToDouble(DP);
            double zv = System.Convert.ToDouble(T) / 2 + zo;
            double rm = Decimal.ToDouble(DP) / 2 - zv;
            double k1 = s * s / (18 * Math.PI * Math.PI * rm);
            double k2 = s / (3 * Math.PI);
            Int32 n = (Int32)Math.Truncate(180 / U);
            pocetPolozek = n * 2 +1;

            itemArray = new Item[pocetPolozek+1];

            Int32 i = 0;
            if (POVRCH == true)
            {
                for (Int32 j = -n; j <= n; j++)
                {
                    hlavniVypocet(i++, j, n, k1, k2, zv, fr, rm);
                }
                //  for j:=-n to n do hlavni_vypocet
            }
            else
            {
                for (Int32 j = n; j >= -n; j--)
                {
                    hlavniVypocet(i++, j, n, k1, k2, zv, fr, rm);
                }
                //  for j:=n downto -n do hlavni_vypocet
            }
            pmr = -pmr;
            //  openwindow (20,5,60,12,'',14,1,14,1,1,true,true);
            //  konec :=false;
            //  gotoxy (1,1);
            //  textcolor (14);
            //  gotoxy (3,2);
            //  write ('Maly prumer   : ');
            //  str((mr+pmr):8:4,pomstr);write (pomstr);
            malyPrumer = mr + pmr;
            //  gotoxy (3,3);
            // write ('Velky prumer  : ');
            //  str((vr*2):8:4,pomstr);write (pomstr);
            velkyPrumer = vr * 2;
            //  gotoxy (3,4);
            //  write ('Nekruhovitost  : ');
            //  str((vr*2-mr-pmr):8:4,pomstr);write (pomstr);
            nekruhovitost = (vr * 2) - mr - pmr;
            m = Decimal.ToDouble(POZNEK) - ((vr * 2) - mr - pmr);
            m = m * m;


        }

    }


}