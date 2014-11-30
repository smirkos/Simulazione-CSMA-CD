using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Simulazione_CSMA_CD
{
    public partial class Form1 : Form
    {
        const double casuale = 0.8;

        double t, ts, a, b, c, d, f;
        string canale;
        double n_collisioni, n_frame, n, 
            ta, tb, tc, td, tf, 
            statoa, statob, statoc, statod, statof,
            na, nb, nc, nd, nf, ra, rb, rc, rd, rf, ka, kb, kc, kd, kf, ya, yb, yc, yd, yf;

        public Form1()
        {
            InitializeComponent();

            txt1.Text = "";
            txt3.Text = "";
            txt2.Text = "";
        }

        /// <summary>
        /// Mette in pausa per i secondi che sono stati passati come parametro
        /// </summary>
        /// <param name="nSecond"></param>
        void Pause(int nSecond)
        {
            //double t0 = Timer;
            //do
            //{
            //    //int dummy;
            //    //dummy = Application.DoEvents();
            //    if (Timer < t0)
            //        t0 -= Convert.ToInt64(24) * Convert.ToInt64(60) * Convert.ToInt64(60);
            //} while (Timer - t0 < nSecond);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            n = 0;
            canale = "L";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pct1.BackColor = Color.Gray;
            pct2.BackColor = Color.Gray;
            pct3.BackColor = Color.Gray;
            pct4.BackColor = Color.Gray;
            pct5.BackColor = Color.Gray;
            n_collisioni = 0;
            n_frame = 0;
            txt1.Text = Convert.ToString(n_collisioni);
            txt2.Text = Convert.ToString(n_frame);
            ts = 0;
            t = 0;
            txt3.Text = "";
        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {
            txt1.Text = Convert.ToString(n_collisioni);
        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {
            txt2.Text = Convert.ToString(n_frame);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            n = 0;
            pctL.BackColor = Color.Green; //Canale LIBERO

            ta -= -1;
            tb -= -1;
            tc -= -1;
            td -= -1;
            tf -= -1;

            //genera numero casuale
            Random casuale = new Random();

            a = casuale.Next();
            if (a >= casuale.Next())
                statoa = 2;
            b = casuale.Next();
            if (b >= casuale.Next())
                statob = 2;
            c = casuale.Next();
            if (c >= casuale.Next())
                statoc = 2;
            d = casuale.Next();
            if (d >= casuale.Next())
                statod = 2;
            f = casuale.Next();
            if (f >= casuale.Next())
                statof = 2;
            
            t += 51.2;
            ts = t / 1000;
            txt3.Text = Convert.ToString(ts);

            if (statoa == 2)
            {
                pct1.BackColor = Color.Yellow;
                statoa = 1;
            }
            if (statob == 2)
            {
                pct2.BackColor = Color.Yellow;
                statob = 1;
            }
            if (statoc == 2)
            {
                pct3.BackColor = Color.Yellow;
                statoc = 1;
            }
            if (statod == 2)
            {
                pct4.BackColor = Color.Yellow;
                statod = 1;
            }
            if (statof == 2)
            {
                pct5.BackColor = Color.Yellow;
                statof = 1;
            }

            if (pct1.BackColor == Color.Yellow) 
                ta = 0;
            if (pct2.BackColor == Color.Yellow) 
                tb = 0;
            if (pct3.BackColor == Color.Yellow) 
                tc = 0;
            if (pct4.BackColor == Color.Yellow) 
                td = 0;
            if (pct5.BackColor == Color.Yellow) 
                tf = 0;

            Pause(2);

            if (canale == "L")
            {
                //A
                if (ta <= 0)
                {
                    if (statoa == 1)
                        pct1.BackColor = Color.Green;
                    if (pct1.BackColor == Color.Green)
                        n += 1;
                    statoa = 0;
                }

                //B
                if (tb <= 0)
                {
                    if (statob == 1)
                        pct2.BackColor = Color.Green;
                    if (pct2.BackColor == Color.Green)
                        n += 1;
                    statob = 0;
                }

                //C
                if (tc <= 0)
                {
                    if (statoc == 1)
                        pct3.BackColor = Color.Green;
                    if (pct3.BackColor == Color.Green)
                        n += 1;
                    statoc = 0;
                }

                //D
                if (td <= 0)
                {
                    if (statod == 1)
                        pct4.BackColor = Color.Green;
                    if (pct4.BackColor == Color.Green)
                        n += 1;
                    statod = 0;
                }

                //F
                if (tf <= 0)
                {
                    if (statof == 1)
                        pct5.BackColor = Color.Green;
                    if (pct5.BackColor == Color.Green)
                        n += 1;
                    statof = 0;
                }


                if (n > 1)
                    pctL.BackColor = Color.Red; //Canale in COLLISIONE
                if (pctL.BackColor == Color.Red)
                {
                    if (statoa == 0)
                        na += 1;
                    if (statob == 0)
                        nb += 1;
                    if (statoc == 0)
                        nc += 1;
                    if (statod == 0)
                        nd += 1;
                    if (statof == 0)
                        nf += 1;
                }

                if (n > 1)
                {
                    n_collisioni += 1;

                    //A
                    if (pct1.BackColor == Color.Green)
                    {
                        if (na > 10)
                            na = 10;
                        ka = na;
                        ya = Math.Pow(2, ka);
                        ra = Convert.ToInt32((ya - 0 + 1) * casuale.Next() + 0);
                        ta = ra;
                        pct1.BackColor = Color.Red;
                    }

                    //B
                    if (pct2.BackColor == Color.Green)
                    {
                        if (nb > 10)
                            nb = 10;
                        kb = nb;
                        yb = Math.Pow(2, kb);
                        rb = Convert.ToInt32((yb - 0 + 1) * casuale.Next() + 0);
                        tb = rb;
                        pct2.BackColor = Color.Red;
                    }
                    //C
                    if (pct3.BackColor == Color.Green)
                    {
                        if (nc > 10)
                            nc = 10;
                        kc = nc;
                        yc = Math.Pow(2, kc);
                        rc = Convert.ToInt32((yc - 0 + 1) * casuale.Next() + 0);
                        tc = rc;
                        pct3.BackColor = Color.Red;
                    }

                    //D
                    if (pct4.BackColor == Color.Green)
                    {
                        if (nd > 10)
                            nd = 10;
                        kd = nd;
                        yd = Math.Pow(2, kd);
                        rd = Convert.ToInt32((yd - 0 + 1) * casuale.Next() + 0);
                        td = rd;
                        pct4.BackColor = Color.Red;
                    }

                    //F
                    if (pct5.BackColor == Color.Green)
                    {
                        if (nf > 10)
                            nf = 10;
                        kf = nf;
                        yf = Math.Pow(2, kf);
                        rf = Convert.ToInt32((yf - 0 + 1) * casuale.Next() + 0);
                        tf = rf;
                        pct5.BackColor = Color.Red;
                    }
                }

                if (n == 1)
                {
                    canale = "O";

                    pctL.BackColor = Color.Yellow; //Canale OCCUPATO
                    n_frame += 1;

                    na = 0;
                    nb = 0;
                    nc = 0;
                    nd = 0;
                    nf = 0;
                }
                txt1.Text = Convert.ToString(n_collisioni);
                txt2.Text = Convert.ToString(n_frame);
                t += 51.2;
                ts = t / 1000;
                txt3.Text = Convert.ToString(ts);

                Pause(2);
                pctL.BackColor = Color.Green; //Canale LIBERO
                if (n == 1)
                {
                    if (pct1.BackColor == Color.Green)
                        pct1.BackColor = Color.Gray;
                    if (pct2.BackColor == Color.Green)
                        pct2.BackColor = Color.Gray;
                    if (pct3.BackColor == Color.Green)
                        pct3.BackColor = Color.Gray;
                    if (pct4.BackColor == Color.Green)
                        pct4.BackColor = Color.Gray;
                    if (pct5.BackColor == Color.Green)
                        pct5.BackColor = Color.Gray;
                    t += 51.2;
                    ts = t / 1000;
                    txt3.Text = Convert.ToString(ts);
                }
            }
            canale = "L";
            Pause(2);
        }
    }
}