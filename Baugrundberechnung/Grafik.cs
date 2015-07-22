﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Baugrundberechnung
{
    /// <summary>
    /// Diese Klasse ist zur Grafischen ausgabe gedacht.
    /// </summary>
    class Grafik
    { 
        //Attribute
        private static double L;
        private static double B;
        private static double S;
        private static double H;
        private static Pen pen_grub;
        private static Pen pen_hint;
        private static Size Fenster;
        private static double xnull;
        private static double ynull;
        private static double xmax;
        private static double ymax;
        private static double max;
        /// <summary>
        /// Diese Methode ruft die anderen Methoden mit übergebenen Werten zum zeichnen auf.
        /// </summary>
        /// <param name="L"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <param name="S"></param>
        /// <param name="T_eben"></param>
        /// <param name="T_laengs"></param>
        /// <param name="T_stirn"></param>
        /// <param name="T_ecke"></param>
        /// <param name="f"></param>
        /// <param name="mitBe"></param>
        public static void zeichnemal(double L, double B, double H, double S, double T_eben, double T_laengs, double T_stirn, double T_ecke, Form1 f, bool mitBe, Size Fenster)
        {
            // für Zeichnungsrahmen
            Grafik.xnull = 660;
            Grafik.ynull = 50;
            Grafik.xmax = Fenster.Width - 20; // normale breite würde außerhalb des sichtfeldes sein
            Grafik.ymax = Fenster.Height - 45; // normale höhe würde außerhalb des Sichtfeldes sein
            Grafik.Fenster = Fenster;
            //setzen der Attribute
            Grafik.L = L;
            Grafik.B = B;
            Grafik.S = S;
            Grafik.H = H;
            //setzten der Farbe auf Braun, dicke auf 2.5
            Grafik.pen_grub = new Pen(Color.Black, (float)(2.5));
            Grafik.pen_hint = new Pen(Color.LightGray, (float)7.0);
            if ((1 / 2.0 * (xmax - xnull)) < (1 / 3.0 * (ymax - ynull)))
            {
                max = (1 / 2.0 * (xmax - xnull))-50;
            }
            else
            {
                max = (1 / 3.0 * (ymax - ynull))-50;
            }
            ZeicheBaugrube(L, B, f);

            ZeichneBaugrubeSeite(H, S, f);
            //die Zeichnungshöhe und mitte der Zeichnung in der Breite.
            //750, 230
            zeichne_einbindetiefe(f, new Point((int)(1/4.0*(xmax-xnull)+xnull), (int)(1 / 3.0 * (ymax - ynull) +ynull)-20), T_ecke, "T_ecke", mitBe,T_ecke);
            //1025, 230
            zeichne_einbindetiefe(f, new Point((int)((3 / 4.0 * (xmax - xnull) + xnull)), (int)(1 / 3.0 * (ymax - ynull) + ynull) - 20), T_laengs, "T_laengs", mitBe, T_ecke);
            //750, 410
            zeichne_einbindetiefe(f, new Point((int)(1 / 4.0 * (xmax - xnull) + xnull), (int)(2 / 3.0 * (ymax - ynull) + ynull) - 20), T_stirn, "T_stirn", mitBe, T_ecke);
            //1025, 410
            zeichne_einbindetiefe(f, new Point((int)((3 / 4.0 * (xmax - xnull) + xnull)), (int)(2 / 3.0 * (ymax - ynull) + ynull) - 20), T_eben, "T_eben", mitBe, T_ecke);

            Graphics Test = Graphics.FromHwnd(f.Handle);
             
            Point ObenLinksPunkt = new Point((int)xnull,(int)ynull);
            Point UntenRechtsPunkt = new Point((int)(1 / 2.0 * (xmax - xnull) + xnull), (int)(1 /3.0 * (ymax - ynull) + ynull));
          
            ObenLinksPunkt = new Point((int)(1/ 2.0 * (xmax - xnull) + xnull), (int)ynull);
            UntenRechtsPunkt = new Point((int)(xmax), (int)(1 / 3.0 * (ymax - ynull) + ynull));
            
            //einbindetiefe Ecke
            ObenLinksPunkt =  new Point((int)(xnull), (int)(1 / 3.0 * (ymax - ynull) +ynull));
            UntenRechtsPunkt = new Point((int)(1 / 2.0 * (xmax - xnull)+xnull), (int)(2 / 3.0 * (ymax - ynull) + ynull));

            //einbindetiefe laengsseite
            ObenLinksPunkt = new Point((int)(1 / 2.0 * (xmax - xnull) + xnull), (int)(1 / 3.0 * (ymax - ynull) + ynull) );
            UntenRechtsPunkt = new Point((int)xmax, (int)(2 / 3.0 * (ymax - ynull)+ynull));

            //einbindetiefe Stirnseite
            ObenLinksPunkt = new Point((int)xnull, (int)(2 / 3.0 * (ymax - ynull) + ynull));
            UntenRechtsPunkt = new Point((int)(1 / 2.0 * (xmax - xnull) + xnull), (int)(3 / 3.0 * (ymax - ynull) + ynull));

            //einbindetiefe eben
            ObenLinksPunkt = new Point((int)(1 / 2.0 * (xmax - xnull) + xnull), (int)(2 / 3.0 * (ymax - ynull) + ynull));
            UntenRechtsPunkt = new Point((int)xmax, (int)(3 / 3.0 * (ymax - ynull) + ynull));
            if (!mitBe)
            {
                f.ohneBeHinweis.Location = new System.Drawing.Point((int)(1 / 2.0 * (xmax - xnull) + xnull -50), (int)((ymax - ynull) + ynull-40));
                f.ohneBeHinweis.Text = "Berechnung ist ohne Bemessungsbeiwert (Be)";
                f.ohneBeHinweis.AutoSize = true;
                f.Controls.Add(f.ohneBeHinweis);
                f.ohneBeHinweis.ForeColor = Color.Red;
                f.ohneBeHinweis.Show();
                f.ohneBeHinweis.Visible = true;
            }
            else
            {
                f.ohneBeHinweis.Visible = false;
            }
   
        }
        /// <summary>
        /// Diese Methode Zeichnet die Baugrube und fügt labels ein.
        /// </summary>
        /// <param name="laenge"></param>
        /// <param name="breite"></param>
        /// <param name="f"></param>
        private static void ZeicheBaugrube(double laenge, double breite, Form1 f)
        {
            //Berechnet eine variable breite in abhängigkeit zur länge.
            //die Höhe soll unverändert bleiben.
            double var_breite = (max * breite) / laenge;
            Graphics Test = Graphics.FromHwnd(f.Handle);
            //erzeugt die äußeren 4 Punkte 
            Point ObenLinksPunkt = new Point((int)(1/4.0 *(xmax -xnull)+xnull-var_breite/2),(int)(1/6.0 *(ymax-ynull)+ynull-max/2));
            Point UntenLinksPunkt = new Point((int)(1/4.0 *(xmax -xnull)+xnull-var_breite/2),(int)(1/6.0 *(ymax-ynull)+ynull+max/2));
            Point ObenRechtsPunkt = new Point((int)(1/4.0 *(xmax -xnull)+xnull+var_breite/2),(int)(1/6.0 *(ymax-ynull)+ynull-max/2));
            Point UntenRechtsPunkt = new Point((int)(1 / 4.0 * (xmax - xnull) + xnull + var_breite / 2), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2));


            //fügt das Label der Breite hinzu mit der beschriftung und 24 über der Zeichnung, Mittig.
            
             f.B_label.Location = new System.Drawing.Point((int)(1 / 4.0 * (xmax - xnull) + xnull + var_breite / 2) -50,48);
             f.B_label.Text = "B = " + breite;
             f.B_label.AutoSize = true;
             f.Controls.Add(f.B_label);
             f.B_label.Show();
            //fügt das Label der länge hinzu, kann sich nach links/rechts verschieben je nach verhältnis der var_breite, ist stets rechts von der Zeichnung
            f.L_label.Location = new System.Drawing.Point((int)((1 / 4.0 * (xmax - xnull) + xnull + var_breite / 2) + 10), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2)-90);
            //f.L_label.Text = "L = " + laenge;
            f.L_label.NewText = "L = " + laenge;
            f.L_label.Invalidate();
            //f.L_label.AutoSize = true;
            f.L_label.AutoSize = false;
            f.L_label.Height = 110;
            f.L_label.RotateAngle = -90; 
            f.Controls.Add(f.L_label);
            f.L_label.Show();
            HintergrundZeichnen(Test, ObenLinksPunkt.X, ObenLinksPunkt.Y, UntenLinksPunkt.Y, ObenRechtsPunkt.X);
            //Die 4 Linien werden gezeichnet
            Test.DrawLine(pen_grub, ObenLinksPunkt, UntenLinksPunkt);
            Test.DrawLine(pen_grub, ObenLinksPunkt, ObenRechtsPunkt);
            Test.DrawLine(pen_grub, UntenLinksPunkt, UntenRechtsPunkt);
            Test.DrawLine(pen_grub, ObenRechtsPunkt, UntenRechtsPunkt);

            //Überschrift der Baugrube.
            f.Baugrube.Location = new System.Drawing.Point((int)(1/4.0 *(xmax -xnull)+xnull) - 60, 30);
            f.Baugrube.Text = "Zeichnung der Baugrube";
            f.Baugrube.AutoSize = true;
            f.Controls.Add(f.Baugrube);
            f.Baugrube.Show();


        }

        private static void HintergrundZeichnen(Graphics Test, int ObenLinksPunktX, int ObenLinksPunktY, int UntenLinksPunkt, int ObenRechtsPunkt)
        {
            int x = ObenLinksPunktX + 4;
            int yOben = ObenLinksPunktY;
            int yUnten = UntenLinksPunkt;
            //Hintergrund test
            while (x < ObenRechtsPunkt)
            {
                Test.DrawLine(pen_hint, new Point(x, yOben), new Point(x, yUnten));
                x += 4;
            }
        }
        /// <summary>
        /// Zeichnet die Seite der Baugrube neben die Baugrube
        /// </summary>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="f"></param>
        private static void ZeichneBaugrubeSeite(double h, double s, Form1 f)
        {
            Graphics Test = Graphics.FromHwnd(f.Handle);
            int htemp = 20;
            double lmbdhs = 1/((max - 20) );
            double lmbd = (max - 20) / (htemp+ s);
            //erzeugt die 4 äußeren Punkte der Baugrube
            //950, 50
            Point ObenLinksPunkt = new Point((int)(3 / 4.0 * (xmax - xnull) + xnull - max / 2), (int)((1 / 6.0 * (ymax - ynull) + ynull - max / 2)-20));
            //1100, 50
            Point ObenRechtsPunkt = new Point((int)(3 / 4.0 * (xmax - xnull) + xnull + max / 2), (int)(1 / 6.0 * (ymax - ynull) + ynull - max / 2-20));
            //950, 200
            Point UntenLinksPunkt = new Point((int)(3 / 4.0 * (xmax - xnull) + xnull - max / 2), ((int)( ObenRechtsPunkt.Y + max *(s/h)*lmbd/20 + max *htemp/100)));
            //1100, 200
            Point UntenRechtsPunkt = new Point((int)(3 / 4.0 * (xmax - xnull) + xnull + max / 2), ((int)( ObenRechtsPunkt.Y + max *(s/h)*lmbd/20 + max *htemp/100)));
            int xmitte = (int)(3 / 4.0 * (xmax - xnull) + xnull);
            HintergrundZeichnen(Test, ObenLinksPunkt.X, (int)(ObenRechtsPunkt.Y  + max *htemp/100), UntenLinksPunkt.Y, xmitte);
            HintergrundZeichnen(Test, xmitte, ObenRechtsPunkt.Y + 10, UntenLinksPunkt.Y, ObenRechtsPunkt.X);
            Test.DrawLine(Pens.Black, ObenLinksPunkt, ObenRechtsPunkt);
            //Zeichnet die obere und untere Linie und Doppeldach
            zeichneDoppelDach(new Point(ObenRechtsPunkt.X - 10, ObenRechtsPunkt.Y), f);
            Test.DrawLine(Pens.Black, UntenLinksPunkt, UntenRechtsPunkt);

            //Zeichnet die gestrichelte Linie ganz unten.
            // i = 950; i<=1100 ; i = i+15
            for (int i = ObenLinksPunkt.X; i <= ObenRechtsPunkt.X; i = i + 15)
            {
                //i, 200
                Point ObenRechts = new Point(i, ((int)( ObenRechtsPunkt.Y + max *(s/h)*lmbd/20 + max *htemp/100)));
                //i-10, 210
                Point UntenLinks = new Point(i -10, ((int)( ObenRechtsPunkt.Y + max *(s/h)*lmbd/20 + max *htemp/100))+10);
                Test.DrawLine(Pens.Black, ObenRechts, UntenLinks);
            }
            
            //Zeichnet die Restlichen Linien
            // 1025, 50  ; 1025, 150
            Test.DrawLine(pen_grub, new Point(xmitte, ObenLinksPunkt.Y), new Point(xmitte, UntenLinksPunkt.Y - 20));
            // 950, 200 - (int)(lmbd * s)  ; 1025, 200 - (int)(lmbd * s))
            Test.DrawLine(Pens.Black, new Point(ObenLinksPunkt.X, (int)(ObenRechtsPunkt.Y + max * htemp / 100)), new Point(xmitte, (int)(ObenRechtsPunkt.Y + max * htemp / 100)));
            // 1025, 200 - (int)(lmbd * (h + s))  ;  1100, 200 - (int)(lmbd * (h + s)))
            Test.DrawLine(Pens.Black, new Point(xmitte, ObenRechtsPunkt.Y + 10), new Point((int)(3 / 4.0 * (xmax - xnull) + xnull + max / 2), ObenRechtsPunkt.Y + 10));
            // 1035, 200 - (int)(lmbd * s) ;  1035, 200), f 
            ZeichnePfeil(new Point( xmitte + 10, (int)(ObenRechtsPunkt.Y + 10)) , new Point(xmitte + 10, (int)(ObenRechtsPunkt.Y  + max *htemp/100)), f);
            // 1035, 200 - (int)(lmbd * (h + s)) ;   1035, 200 - (int)(lmbd * s)), f
            ZeichnePfeil(new Point(xmitte + 10,(int)(ObenRechtsPunkt.Y  + max *htemp/100)), new Point(xmitte + 10, ((int)( ObenRechtsPunkt.Y + max *(s/h)*lmbd/20 + max *htemp/100))), f);


            f.H_Seite_stirn_ecke_eben_laengs_SeiteS[0].Location = new System.Drawing.Point((int)(3 / 4.0 * (xmax - xnull) + xnull) - 60, 30);
            f.H_Seite_stirn_ecke_eben_laengs_SeiteS[0].Text = "Zeichnung von der Seite";
            f.H_Seite_stirn_ecke_eben_laengs_SeiteS[0].AutoSize = true;
            f.Controls.Add(f.H_Seite_stirn_ecke_eben_laengs_SeiteS[0]);
            f.H_Seite_stirn_ecke_eben_laengs_SeiteS[0].Show();
            f.T_Seite_stirn_ecke_eben_laengs_SeiteB[0].Location = new System.Drawing.Point(xmitte + 5, (int)(ObenRechtsPunkt.Y + max * htemp / 100) + 10);
            f.T_Seite_stirn_ecke_eben_laengs_SeiteB[0].Text = "S = " + S;
            f.T_Seite_stirn_ecke_eben_laengs_SeiteB[0].BackColor = Color.LightGray;
            f.T_Seite_stirn_ecke_eben_laengs_SeiteB[0].AutoSize = true;
            f.Controls.Add(f.T_Seite_stirn_ecke_eben_laengs_SeiteB[0]);
            f.T_Seite_stirn_ecke_eben_laengs_SeiteB[0].Show();
        
        }
        /// <summary>
        /// Zeichnet einen doppelten Pfeil
        /// </summary>
        /// <param name="oben"></param>
        /// <param name="unten"></param>
        /// <param name="f"></param>
        private static void ZeichnePfeil(Point oben, Point unten, Form1 f)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            gr.DrawLine(Pens.Black, oben, unten);
            gr.DrawLine(Pens.Black, unten, new Point(unten.X - 3, unten.Y - 6));
            gr.DrawLine(Pens.Black, unten, new Point(unten.X + 3, unten.Y - 6));
            gr.DrawLine(Pens.Black, oben, new Point(oben.X - 3, oben.Y + 6));
            gr.DrawLine(Pens.Black, oben, new Point(oben.X + 3, oben.Y + 6));

        }
        /// <summary>
        /// Zeichnet einen relativen doppelten Pfeil
        /// </summary>
        /// <param name="oben"></param>
        /// <param name="unten"></param>
        /// <param name="f"></param>
        /// <param name="ursprung"></param>
        private static void ZeichnePfeil_rel(Point oben, Point unten, Form1 f, Point ursprung)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            gr.TranslateTransform(ursprung.X, ursprung.Y);
            gr.DrawLine(Pens.Black, oben, unten);
            gr.DrawLine(Pens.Black, unten, new Point(unten.X - 3, unten.Y - 6));
            gr.DrawLine(Pens.Black, unten, new Point(unten.X + 3, unten.Y - 6));
            gr.DrawLine(Pens.Black, oben, new Point(oben.X - 3, oben.Y + 6));
            gr.DrawLine(Pens.Black, oben, new Point(oben.X + 3, oben.Y + 6));

        }
        /// <summary>
        /// Zeichnet die Ergebnis Baugruben
        /// </summary>
        /// <param name="f"></param>
        /// <param name="ursprung"></param>
        /// <param name="T"></param>
        private static void zeichne_einbindetiefe(Form1 f, Point ursprung, double T, String zuZeichnen, bool mitBe,double T_eck)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            //vershiebt den Ursprung
            gr.TranslateTransform(ursprung.X, ursprung.Y + 20);
            int breite = (int)max;
            //variable größe lmbd
            double lmbd =  ( T_eck+10)/max;
            //Verschiebt den Ursprung
            int x =  (int)(-breite / 2)+4;
            int yOben = (int)((H / lmbd));
            int yOben2 = 0 ;
            int yUnten = (int)max;
            //Hintergrund test
            while (x < 0)
            {
                gr.DrawLine(pen_hint, new Point(x, yOben), new Point(x, yUnten));
                x += 4;
            }
            //Hintergrund test
            while (x < (int)(breite / 2))
            {
                gr.DrawLine(pen_hint, new Point(x, yOben2), new Point(x, yUnten));
                x += 4;
            }
            gr.DrawLine(pen_grub, new Point(0, 0), new Point(0, (int)(1 / lmbd * (H + T))));
            gr.DrawLine(Pens.Black, new Point(0, 0), new Point((int)(breite / 2), 0));
            gr.DrawLine(Pens.Black, new Point(0, (int)(H/lmbd)), new Point((int)(-breite / 2), (int)(H/lmbd)));

            int anfang = (int)(-breite / 2);
            int ende = (int)(breite / 2);
            ZeichnePfeil_rel(new Point((int)(ende / 2), (int)(1/lmbd * H)), new Point((int)(ende / 2), (int)(1/lmbd * (H + T))), f, new Point(ursprung.X, ursprung.Y + 20));
            ZeichnePfeil_rel(new Point((int)(ende / 2), 0), new Point((int)(ende / 2), (int)(1/lmbd * H)), f, new Point(ursprung.X, ursprung.Y + 20));
            zeichneWassersspiegel(new Point(ursprung.X + (int)(-breite / 2) + 10, ursprung.Y + (int)(1 / lmbd * H)+20), f);
            zeichneWassersspiegel(new Point(ursprung.X + (int)(breite / 2) - 10, ursprung.Y+20), f);
             
            if(zuZeichnen == "T_stirn" )
            {
                f.T_stirn_ecke_eben_laengs_Label[0].Location = new System.Drawing.Point(ursprung.X - 60, ursprung.Y);
                f.T_stirn_ecke_eben_laengs_Label[0].Text = "Stirnseite 🔍";
                f.T_stirn_ecke_eben_laengs_Label[0].AutoSize = true;
                f.Controls.Add(f.T_stirn_ecke_eben_laengs_Label[0]);
                f.T_stirn_ecke_eben_laengs_Label[0].Show();
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[1].Location = new System.Drawing.Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(1 / lmbd * H) - 20);
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[1].Text = "H = " + H;
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[1].BackColor = Color.LightGray;
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[1].AutoSize = true;
                f.Controls.Add(f.H_Seite_stirn_ecke_eben_laengs_SeiteS[1]);
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[1].Show();
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[1].Location = new System.Drawing.Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(1 / lmbd * H) + 30);
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[1].Text = "T = " + T;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[1].BackColor = Color.LightGray;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[1].AutoSize = true;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[1].ForeColor = Color.Black;
                f.Controls.Add(f.T_Seite_stirn_ecke_eben_laengs_SeiteB[1]);
                if(!mitBe)
                {
                    f.T_Seite_stirn_ecke_eben_laengs_SeiteB[1].ForeColor = Color.Red;
                }
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[1].Show();

            }
            else if (zuZeichnen == "T_ecke")
            {
                f.T_stirn_ecke_eben_laengs_Label[1].Location = new System.Drawing.Point(ursprung.X-60, ursprung.Y);
                f.T_stirn_ecke_eben_laengs_Label[1].Text = "Ecke 🔍";
                f.T_stirn_ecke_eben_laengs_Label[1].AutoSize = true;
                f.Controls.Add(f.T_stirn_ecke_eben_laengs_Label[1]);
                f.T_stirn_ecke_eben_laengs_Label[1].Show();
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[2].Location = new System.Drawing.Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(1 / lmbd * H) - 20);
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[2].Text = "H = " + H;
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[2].BackColor = Color.LightGray;
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[2].AutoSize = true;
                f.Controls.Add(f.H_Seite_stirn_ecke_eben_laengs_SeiteS[2]);
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[2].Show();
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[2].Location = new System.Drawing.Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(1 / lmbd * H) + 30);
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[2].Text = "T = " + T;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[2].BackColor = Color.LightGray;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[2].AutoSize = true;
                f.Controls.Add(f.T_Seite_stirn_ecke_eben_laengs_SeiteB[2]);
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[2].ForeColor = Color.Black;
                if (!mitBe)
                {
                    f.T_Seite_stirn_ecke_eben_laengs_SeiteB[2].ForeColor = Color.Red;
                }
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[2].Show();
            }
            else if(zuZeichnen == "T_eben")
            {
                f.T_stirn_ecke_eben_laengs_Label[2].Location = new System.Drawing.Point(ursprung.X-60, ursprung.Y );
                f.T_stirn_ecke_eben_laengs_Label[2].Text = "eben 🔍";
                f.T_stirn_ecke_eben_laengs_Label[2].AutoSize = true;
                f.Controls.Add(f.T_stirn_ecke_eben_laengs_Label[2]);
                f.T_stirn_ecke_eben_laengs_Label[2].Show();
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[3].Location = new System.Drawing.Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(1 / lmbd * H) - 20);
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[3].Text = "H = " + H;
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[3].BackColor = Color.LightGray;
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[3].AutoSize = true;
                f.Controls.Add(f.H_Seite_stirn_ecke_eben_laengs_SeiteS[3]);
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[3].Show();
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[3].Location = new System.Drawing.Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(1 / lmbd * H) + 30);
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[3].Text = "T = " + T;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[3].BackColor = Color.LightGray;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[3].AutoSize = true;
                f.Controls.Add(f.T_Seite_stirn_ecke_eben_laengs_SeiteB[3]);
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[3].ForeColor = Color.Black;
                if (!mitBe)
                {
                    f.T_Seite_stirn_ecke_eben_laengs_SeiteB[3].ForeColor = Color.Red;
                }
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[3].Show();
            }
            else if (zuZeichnen == "T_laengs")
            {
                f.T_stirn_ecke_eben_laengs_Label[2].Location = new System.Drawing.Point(ursprung.X - 60, ursprung.Y);
                f.T_stirn_ecke_eben_laengs_Label[2].Text = "Längs 🔍";
                f.T_stirn_ecke_eben_laengs_Label[2].AutoSize = true;
                f.Controls.Add(f.T_stirn_ecke_eben_laengs_Label[2]);
                f.T_stirn_ecke_eben_laengs_Label[2].Show();
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[4].Location = new System.Drawing.Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(1 / lmbd * H) - 20);
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[4].Text = "H = " + H;
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[4].BackColor = Color.LightGray;
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[4].AutoSize = true;
                f.Controls.Add(f.H_Seite_stirn_ecke_eben_laengs_SeiteS[4]);
                f.H_Seite_stirn_ecke_eben_laengs_SeiteS[4].Show();
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[4].Location = new System.Drawing.Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(1 / lmbd * H) +30);
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[4].Text = "T = " + T;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[4].BackColor = Color.LightGray;
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[4].AutoSize = true;
                f.Controls.Add(f.T_Seite_stirn_ecke_eben_laengs_SeiteB[4]);
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[4].ForeColor = Color.Black;
                if (!mitBe)
                {
                    f.T_Seite_stirn_ecke_eben_laengs_SeiteB[4].ForeColor = Color.Red;
                }
                f.T_Seite_stirn_ecke_eben_laengs_SeiteB[4].Show();
            }
        }
        /// <summary>
        /// diese Methode zeichnet ein doppeltes Dach
        /// </summary>
        /// <param name="ursprung"></param>
        /// <param name="f"></param>
        public static void zeichneDoppelDach(Point ursprung, Form1 f)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            gr.TranslateTransform(ursprung.X, ursprung.Y);
            Point untenLinks = new Point(-10, 8);
            Point untenRechts = new Point(2, 8);
            gr.DrawLine(Pens.Black, untenLinks, new Point(-4, 0));
            gr.DrawLine(Pens.Black, untenRechts, new Point(-4, 0));
            Point untenL = new Point(10, 8);
            Point untenR = new Point(-2, 8);
            gr.DrawLine(Pens.Black, untenL, new Point(4, 0));
            gr.DrawLine(Pens.Black, untenR, new Point(4, 0));

        }
        public static void zeichneWassersspiegel(Point ursprung, Form1 f)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            gr.TranslateTransform(ursprung.X, ursprung.Y);
            Point mitte = new Point(0, 0);
            Point ersteLinieRechts = new Point(4, 2);
            Point ersteLinieLinks = new Point(-4, 2);
            Point zweiteLinieRechts = new Point(2, 4);
            Point zweiteLinieLinks = new Point(-2, 4);
            Point obenRechts = new Point(-5, -5);
            Point obenLinks = new Point(5, -5);
            gr.DrawLine(Pens.Black, ersteLinieLinks, ersteLinieRechts);
            gr.DrawLine(Pens.Black, zweiteLinieLinks, zweiteLinieRechts);
            gr.DrawLine(Pens.Black, obenLinks, obenRechts);
            gr.DrawLine(Pens.Black, obenRechts, mitte);
            gr.DrawLine(Pens.Black, obenLinks, mitte);
        }
    }
}
