using System;
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
            zeichne_einbindetiefe(f, new Point((int)(1/4.0*(xmax-xnull)+xnull), (int)(1 / 3.0 * (ymax - ynull) +ynull)-20), T_ecke, "T_ecke", mitBe);
            //1025, 230
            zeichne_einbindetiefe(f, new Point((int)((3 / 4.0 * (xmax - xnull) + xnull)), (int)(1 / 3.0 * (ymax - ynull) + ynull)-20), T_laengs, "T_laengs", mitBe);
            //750, 410
            zeichne_einbindetiefe(f, new Point((int)(1/4.0*(xmax-xnull)+xnull), (int)(2 / 3.0 * (ymax - ynull) + ynull)-20), T_stirn,"T_stirn", mitBe);
            //1025, 410
            zeichne_einbindetiefe(f, new Point((int)((3/ 4.0 * (xmax - xnull) + xnull)), (int)(2 / 3.0 * (ymax - ynull) + ynull)-20), T_eben, "T_eben", mitBe);

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
            /*
             * f.B_label.Location = new System.Drawing.Point(730, 26);
             * f.B_label.Text = "B = " + breite;
             * f.B_label.AutoSize = true;
             * f.Controls.Add(f.B_label);
             * f.B_label.Show();
             *   newlbl.Text = "";           
             *   newlbl.AutoSize = false;      
             *   newlbl.NewText = "ravindra";     
             *   newlbl.ForeColor = Color.Green;  
             *   newlbl.RotateAngle = -90;  
             * */
            //fügt das Label der länge hinzu, kann sich nach links/rechts verschieben je nach verhältnis der var_breite, ist stets rechts von der Zeichnung
            /*f.L_label.Location = new System.Drawing.Point((int)(750 + var_breite / 2 + 10), 125);
            //f.L_label.Text = "L = " + laenge;
            f.L_label.NewText = "L = " + laenge;
            //f.L_label.AutoSize = true;
            f.L_label.AutoSize = false;
            f.L_label.RotateAngle = -90; 
            f.Controls.Add(f.L_label);
            f.L_label.Show();*/
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
            //erzeugt die 4 äußeren Punkte der Baugrube
            //950, 50
            Point ObenLinksPunkt = new Point((int)(3 / 4.0 * (xmax - xnull) + xnull - max / 2), (int)(1 / 6.0 * (ymax - ynull) + ynull - max / 2));
            //950, 200
            Point UntenLinksPunkt = new Point((int)(3 / 4.0 * (xmax - xnull) + xnull - max / 2), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2)-10);
            //1100, 50
            Point ObenRechtsPunkt = new Point((int)(3 / 4.0 * (xmax - xnull) + xnull + max / 2), (int)(1 / 6.0 * (ymax - ynull) + ynull - max / 2));
            //1100, 200
            Point UntenRechtsPunkt = new Point((int)(3 / 4.0 * (xmax - xnull) + xnull + max / 2), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2)-10);
            //Zeichnet die obere und untere Linie und Doppeldach
            double lmbd = (max-20) / (h + s);
            HintergrundZeichnen(Test, ObenLinksPunkt.X, (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * s), UntenLinksPunkt.Y, (int)(3 / 4.0 * (xmax - xnull) + xnull));
            HintergrundZeichnen(Test, (int)(3 / 4.0 * (xmax - xnull) + xnull), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * (h + s)), UntenLinksPunkt.Y, ObenRechtsPunkt.X);
            Test.DrawLine(Pens.Black, ObenLinksPunkt, ObenRechtsPunkt);
            zeichneDoppelDach(new Point(ObenRechtsPunkt.X - 10, ObenRechtsPunkt.Y), f);
            Test.DrawLine(Pens.Black, UntenLinksPunkt, UntenRechtsPunkt);

            //Zeichnet die gestrichelte Linie ganz unten.
            // i = 950; i<=1100 ; i = i+15
            for (int i = (int)(3 / 4.0 * (xmax - xnull) + xnull - max / 2); i <= (int)(3 / 4.0 * (xmax - xnull) + xnull + max / 2); i = i + 15)
            {
                //i, 200
                Point ObenRechts = new Point(i, (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2)-10);
                //i-10, 210
                Point UntenLinks = new Point(i - 10, (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2));
                Test.DrawLine(Pens.Black, ObenRechts, UntenLinks);
            }
            
            //Zeichnet die Restlichen Linien
            // 1025, 50  ; 1025, 150
            Test.DrawLine(pen_grub, new Point((int)(3 / 4.0 * (xmax - xnull) + xnull), (int)(1 / 6.0 * (ymax - ynull) + ynull - max / 2)), new Point((int)(3 / 4.0 * (xmax - xnull) + xnull), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 20));
            // 950, 200 - (int)(lmbd * s)  ; 1025, 200 - (int)(lmbd * s))
            Test.DrawLine(Pens.Black, new Point((int)(3 / 4.0 * (xmax - xnull) + xnull - max / 2), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * s)), new Point((int)(3 / 4.0 * (xmax - xnull) + xnull), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * s)));
            // 1025, 200 - (int)(lmbd * (h + s))  ;  1100, 200 - (int)(lmbd * (h + s)))
            Test.DrawLine(Pens.Black, new Point((int)(3 / 4.0 * (xmax - xnull) + xnull), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * (h + s))), new Point((int)(3 / 4.0 * (xmax - xnull) + xnull + max / 2), (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * (h + s))));
            // 1035, 200 - (int)(lmbd * s) ;  1035, 200), f 
            ZeichnePfeil(new Point((int)(3 / 4.0 * (xmax - xnull) + xnull) + 10, (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * s)), new Point((int)(3 / 4.0 * (xmax - xnull) + xnull) + 10, (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10), f);
            // 1035, 200 - (int)(lmbd * (h + s)) ;   1035, 200 - (int)(lmbd * s)), f
            ZeichnePfeil(new Point((int)(3 / 4.0 * (xmax - xnull) + xnull) + 10, (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * (h + s))), new Point((int)(3 / 4.0 * (xmax - xnull) + xnull) + 10, (int)(1 / 6.0 * (ymax - ynull) + ynull + max / 2) - 10 - (int)(lmbd * s)), f);
            
            
            f.BaugrubeSeite.Location = new System.Drawing.Point((int)(3 / 4.0 * (xmax - xnull) + xnull) - 60, 30);
            f.BaugrubeSeite.Text = "Zeichnung von der Seite";
            f.BaugrubeSeite.AutoSize = true;
            f.Controls.Add(f.BaugrubeSeite);
            f.BaugrubeSeite.Show();
        
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
        private static void zeichne_einbindetiefe(Form1 f, Point ursprung, double T, String zuZeichnen, bool mitBe)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            //vershiebt den Ursprung
            gr.TranslateTransform(ursprung.X, ursprung.Y + 20);
            int breite = (int)max;
            //variable größe lmbd
            double lmbd =  (H + S)/(max);
            //Verschiebt den Ursprung
            int x =  (int)(-breite / 2)+4;
            int yOben = (int)((H / lmbd));
            int yOben2 =0 ;
            int yUnten = (int)(1 / lmbd * (H + S));
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
            gr.DrawLine(pen_grub, new Point(0, 0), new Point(0, (int)((H + T)/lmbd)));
            gr.DrawLine(Pens.Black, new Point(0, 0), new Point((int)(breite / 2), 0));
            gr.DrawLine(Pens.Black, new Point(0, (int)(H/lmbd)), new Point((int)(-breite / 2), (int)(H/lmbd)));
            gr.DrawLine(Pens.Black, new Point((int)(-breite / 2), (int)(1/lmbd * (H + S))), new Point((int)(breite / 2), (int)(1/lmbd * (H + S))));

            int anfang = (int)(-breite / 2);
            int ende = (int)(breite / 2);
            for (int i = anfang; i <= ende; i = i + 15)
            {
                Point ObenRechts = new Point(i, (int)(1/lmbd * (H + S)));
                Point UntenLinks = new Point(i - 10, (int)(1/lmbd * (H + S)) + 10);
                gr.DrawLine(Pens.Black, ObenRechts, UntenLinks);
            } 
            ZeichnePfeil_rel(new Point((int)(ende / 2), (int)(1/lmbd * H)), new Point((int)(ende / 2), (int)(1/lmbd * (H + T))), f, new Point(ursprung.X, ursprung.Y + 20));
            ZeichnePfeil_rel(new Point((int)(anfang / 2), (int)(1/lmbd * H)), new Point((int)(anfang / 2), (int)(1/lmbd * (H + S))), f, new Point(ursprung.X, ursprung.Y + 20));
            ZeichnePfeil_rel(new Point((int)(ende / 2), 0), new Point((int)(ende / 2), (int)(1/lmbd * H)), f, new Point(ursprung.X, ursprung.Y + 20));

            if(zuZeichnen == "T_stirn" )
            {
                f.T_stirn.Location = new System.Drawing.Point(ursprung.X-60, ursprung.Y);
                f.T_stirn.Text = "Stirnseite";
                f.T_stirn.AutoSize = true;
                f.Controls.Add(f.T_stirn);
                f.T_stirn.Show(); 

            }
            else if (zuZeichnen == "T_ecke")
            {
                f.T_ecke.Location = new System.Drawing.Point(ursprung.X-60, ursprung.Y);
                f.T_ecke.Text = "Ecke";
                f.T_ecke.AutoSize = true;
                f.Controls.Add(f.T_ecke);
                f.T_ecke.Show();
            }
            else if(zuZeichnen == "T_eben")
            {
                f.T_eben.Location = new System.Drawing.Point(ursprung.X-60, ursprung.Y );
                f.T_eben.Text = "eben";
                f.T_eben.AutoSize = true;
                f.Controls.Add(f.T_eben);
                f.T_eben.Show(); 
            }
            else if (zuZeichnen == "T_laengs")
            {
                f.T_laengs.Location = new System.Drawing.Point(ursprung.X - 60, ursprung.Y);
                f.T_laengs.Text = "Längs";
                f.T_laengs.AutoSize = true;
                f.Controls.Add(f.T_laengs);
                f.T_laengs.Show();
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
    }
}
