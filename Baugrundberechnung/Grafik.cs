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
            Grafik.pen_grub = new Pen(Color.Black, (float)(2.5));
            Grafik.pen_hint = new Pen(Color.LightGray, (float)7.0);
            //Maximale Zeichnungsfläche
            if ((1 / 2.0 * (xmax - xnull)) < (1 / 3.0 * (ymax - ynull)))
            {
                max = (1 / 2.0 * (xmax - xnull)) - 50;
            }
            else
            {
                max = (1 / 3.0 * (ymax - ynull)) - 50;
            }
            ZeicheBaugrube(L, B, f);
            ZeichneBaugrubeSeite(H, S, f);
            zeichne_einbindetiefe(f, new Point(variablesX(1), variablesY(2) - 20), T_ecke, "T_ecke", mitBe, T_ecke);
            zeichne_einbindetiefe(f, new Point(variablesX(3), variablesY(2) - 20), T_laengs, "T_laengs", mitBe, T_ecke);
            zeichne_einbindetiefe(f, new Point(variablesX(1), variablesY(4) - 20), T_stirn, "T_stirn", mitBe, T_ecke);
            zeichne_einbindetiefe(f, new Point(variablesX(3), variablesY(4) - 20), T_eben, "T_eben", mitBe, T_ecke);
            if (!mitBe)
            {
                f.ohneBeHinweis.Location = new System.Drawing.Point(variablesX(2) - 50, variablesY(6) - 40);
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
        /// Berechnet y abhängig von der Fenstergröße
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int variablesY(int x)
        {
            return (int)(x / 6.0 * (ymax - ynull) + ynull);
        }
        /// <summary>
        /// Berechnet x abhängig von der Fenstergröße. 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int variablesX(int x)
        {
            return (int)(x / 4.0 * (xmax - xnull) + xnull);
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
            Point ObenLinksPunkt = new Point((int)(variablesX(1) - var_breite / 2), (int)(variablesY(1) - max / 2));
            Point UntenLinksPunkt = new Point((int)(variablesX(1) - var_breite / 2), (int)(variablesY(1) + max / 2));
            Point ObenRechtsPunkt = new Point((int)(variablesX(1) + var_breite / 2), (int)(variablesY(1) - max / 2));
            Point UntenRechtsPunkt = new Point((int)(variablesX(1) + var_breite / 2), (int)(variablesY(1) + max / 2));
            HintergrundZeichnen(Test, ObenLinksPunkt.X, ObenLinksPunkt.Y, UntenLinksPunkt.Y, ObenRechtsPunkt.X);
            //Die 4 Linien werden gezeichnet
            Test.DrawLine(pen_grub, ObenLinksPunkt, UntenLinksPunkt);
            Test.DrawLine(pen_grub, ObenLinksPunkt, ObenRechtsPunkt);
            Test.DrawLine(pen_grub, UntenLinksPunkt, UntenRechtsPunkt);
            Test.DrawLine(pen_grub, ObenRechtsPunkt, UntenRechtsPunkt);
            //Überschrift der Baugrube.
            f.Baugrube.Location = new System.Drawing.Point(variablesX(1) - 60, 30);
            f.Baugrube.Text = "Zeichnung der Baugrube";
            f.Baugrube.AutoSize = true;
            f.Controls.Add(f.Baugrube);
            f.Baugrube.Show();
            f.B_label.Location = new System.Drawing.Point((int)(variablesX(1) + var_breite / 2) - 50, 48);
            f.B_label.Text = "B = " + breite;
            f.B_label.AutoSize = true;
            f.Controls.Add(f.B_label);
            f.B_label.Show();
            Test.Dispose();
            System.Drawing.Graphics formGraphics = f.CreateGraphics();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 8);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionVertical);
            formGraphics.DrawString("L = " + laenge, drawFont, drawBrush, (int)(variablesX(1) + var_breite / 2 + 10), (int)((variablesY(1) + max / 2) - 90), drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }
       /// <summary>
       /// Zeichnet den Wasserspiegel der Zeichnung etwas dunkler als der eigentliche Hintergrund
       /// </summary>
       /// <param name="Test"></param>
       /// <param name="ObenLinksPunktX"></param>
       /// <param name="ObenLinksPunktY"></param>
       /// <param name="UntenLinksPunkt"></param>
       /// <param name="ObenRechtsPunkt"></param>
        private static void HintergrundZeichnen(Graphics Test, int ObenLinksX, int ObenLinksY, int UntenLinksY, int ObenRechtsX)
        {
            int x = ObenLinksX + 4;
            int yOben = ObenLinksY;
            int yUnten = UntenLinksY;
            //Hintergrund test
            while (x < ObenRechtsX)
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
            double lmbdhs = 1 / ((max - 20));
            double lmbd = (max - 20) / (htemp + s);
            double sdurchh;
            Point ObenLinksPunkt = new Point((int)(variablesX(3) - max / 2), (int)(variablesY(1) - max / 2 - 20));
            Point ObenRechtsPunkt = new Point((int)(variablesX(3) + max / 2), (int)(variablesY(1) - max / 2 - 20));
            if(s/h > 10)
            {
                sdurchh = 10;
            }
            else
            {
                sdurchh = s / h;
            }
            Point UntenLinksPunkt = new Point((int)(variablesX(3) - max / 2), ((int)(ObenRechtsPunkt.Y + max * (sdurchh) * lmbd / 20 + max * htemp / 100)));
            Point UntenRechtsPunkt = new Point((int)(variablesX(3) + max / 2), ((int)(ObenRechtsPunkt.Y + max * (sdurchh) * lmbd / 20 + max * htemp / 100)));
            int xmitte = variablesX(3);
            HintergrundZeichnen(Test, ObenLinksPunkt.X, (int)(ObenRechtsPunkt.Y + max * htemp / 100), UntenLinksPunkt.Y, xmitte);
            HintergrundZeichnen(Test, xmitte, ObenRechtsPunkt.Y + 10, UntenLinksPunkt.Y, ObenRechtsPunkt.X);
            Test.DrawLine(Pens.Black, ObenLinksPunkt, ObenRechtsPunkt);
            zeichneDoppelDach(new Point(ObenRechtsPunkt.X - 10, ObenRechtsPunkt.Y), f);
            zeichneDoppelDach(new Point(ObenLinksPunkt.X + 10, (int)(ObenRechtsPunkt.Y + max * htemp / 100)), f);
            zeichneWassersspiegel(new Point(ObenRechtsPunkt.X - 30, ObenRechtsPunkt.Y + 10), f);
            zeichneWassersspiegel(new Point(ObenLinksPunkt.X + 30, (int)(ObenRechtsPunkt.Y + max * htemp / 100)), f);
            Test.DrawLine(Pens.Black, UntenLinksPunkt, UntenRechtsPunkt);
            for (int i = ObenLinksPunkt.X; i <= ObenRechtsPunkt.X; i = i + 15)
            {
                Point ObenRechts = new Point(i, ((int)(ObenRechtsPunkt.Y + max * (sdurchh) * lmbd / 20 + max * htemp / 100)));
                Point UntenLinks = new Point(i - 10, ((int)(ObenRechtsPunkt.Y + max * (sdurchh) * lmbd / 20 + max * htemp / 100)) + 10);
                Test.DrawLine(Pens.Black, ObenRechts, UntenLinks);
            }
            Test.DrawLine(pen_grub, new Point(xmitte, ObenLinksPunkt.Y), new Point(xmitte, (int)(ObenRechtsPunkt.Y + max * htemp / 100) + (int)((ObenRechtsPunkt.Y + max * (sdurchh) * lmbd / 20 + max * htemp / 100) - (ObenRechtsPunkt.Y + max * htemp / 100)) / 2));
            Test.DrawLine(Pens.Black, new Point(ObenLinksPunkt.X, (int)(ObenRechtsPunkt.Y + max * htemp / 100)), new Point(xmitte, (int)(ObenRechtsPunkt.Y + max * htemp / 100)));
            Test.DrawLine(Pens.Black, new Point(xmitte, ObenRechtsPunkt.Y + 10), new Point((int)(variablesX(3) + max / 2), ObenRechtsPunkt.Y + 10));
            ZeichnePfeil(new Point(xmitte + 10, (int)(ObenRechtsPunkt.Y + 10)), new Point(xmitte + 10, (int)(ObenRechtsPunkt.Y + max * htemp / 100)), f);
            ZeichnePfeil(new Point(xmitte + 10, (int)(ObenRechtsPunkt.Y + max * htemp / 100)), new Point(xmitte + 10, ((int)(ObenRechtsPunkt.Y + max * (sdurchh) * lmbd / 20 + max * htemp / 100))), f);
            f.BaugrubeSeite.Location = new System.Drawing.Point(variablesX(3) - 60, 30);
            f.BaugrubeSeite.Text = "Zeichnung von der Seite";
            f.BaugrubeSeite.AutoSize = true;
            f.Controls.Add(f.BaugrubeSeite);
            f.BaugrubeSeite.Show();
            Test.Dispose();
            drawText("S = " + S, new Point(xmitte + 10, (int)(ObenRechtsPunkt.Y + max * htemp / 100) + 10), f, Color.Black);
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
            gr.Dispose();
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
            gr.Dispose();
        }
        /// <summary>
        /// Zeichnet die Ergebnis Baugruben
        /// </summary>
        /// <param name="f"></param>
        /// <param name="ursprung"></param>
        /// <param name="T"></param>
        private static void zeichne_einbindetiefe(Form1 f, Point ursprung, double T, String zuZeichnen, bool mitBe, double T_eck)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            //verschiebt den Ursprung
            gr.TranslateTransform(ursprung.X, ursprung.Y + 20);
            int breite = (int)max;
            double lmbd = (max - 10) / (H + T_eck);
            int x = (int)(-breite / 2) + 4;
            int yOben = (int)((H * lmbd));
            int yOben2 = 0;
            int yUnten = (int)max;
            HintergrundZeichnen(gr, x-4, yOben, yUnten, 0);
            HintergrundZeichnen(gr, 0, yOben2, yUnten, (int)(breite / 2));
            gr.DrawLine(pen_grub, new Point(0, 0), new Point(0, (int)((H + T) * lmbd)));
            gr.DrawLine(Pens.Black, new Point(0, 0), new Point((int)(breite / 2), 0));
            gr.DrawLine(Pens.Black, new Point(0, yOben), new Point((int)(-breite / 2), yOben));
            int anfang = (int)(-breite / 2);
            int ende = (int)(breite / 2);
            ZeichnePfeil_rel(new Point((int)(ende / 2) - 20, (int)yOben), new Point((int)(ende / 2) - 20, (int)(lmbd * (H + T))), f, new Point(ursprung.X, ursprung.Y + 20));
            ZeichnePfeil_rel(new Point((int)(ende / 2) - 20, 0), new Point((int)(ende / 2) - 20, (int)(yOben)), f, new Point(ursprung.X, ursprung.Y + 20));
            zeichneWassersspiegel(new Point(ursprung.X + (int)(-breite / 2) + 30, ursprung.Y + (int)(yOben) + 20), f);
            zeichneWassersspiegel(new Point(ursprung.X + (int)(breite / 2) - 30, ursprung.Y + 20), f);
            zeichneDoppelDach(new Point(ursprung.X + (int)(-breite / 2) + 10, ursprung.Y + (int)(yOben) + 20), f);
            zeichneDoppelDach(new Point(ursprung.X + (int)(breite / 2) - 10, ursprung.Y + 20), f);
            if (zuZeichnen == "T_stirn")
            {
                f.T_stirn.Location = new System.Drawing.Point(ursprung.X - 60, ursprung.Y);
                f.T_stirn.Text = "Stirnseite 🔍";
                f.T_stirn.AutoSize = true;
                f.Controls.Add(f.T_stirn);
                f.T_stirn.Show();
                drawText("H = " + H, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) - 20), f, Color.Black);
                if (!mitBe)
                {
                    drawText("T = " + T, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) + 30), f, Color.Red);
                }
                else
                {
                    drawText("Td = " + T, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) + 30), f, Color.Black);
                }
            }
            else if (zuZeichnen == "T_ecke")
            {
                f.T_ecke.Location = new System.Drawing.Point(ursprung.X - 60, ursprung.Y);
                f.T_ecke.Text = "Ecke 🔍";
                f.T_ecke.AutoSize = true;
                f.Controls.Add(f.T_ecke);
                f.T_ecke.Show();
                drawText("H = " + H, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) - 20), f, Color.Black);
                if (!mitBe)
                {
                    drawText("T = " + T, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) + 30), f, Color.Red);
                }
                else
                {
                    drawText("Td = " + T, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) + 30), f, Color.Black);
                }
            }
            else if (zuZeichnen == "T_eben")
            {
                f.T_eben.Location = new System.Drawing.Point(ursprung.X - 60, ursprung.Y);
                f.T_eben.Text = "eben 🔍";
                f.T_eben.AutoSize = true;
                f.Controls.Add(f.T_eben);
                f.T_eben.Show();
                drawText("H = " + H, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) - 20), f, Color.Black);
                if (!mitBe)
                {
                    drawText("T = " + T, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) + 30), f, Color.Red);
                }
                else
                {
                    drawText("Td = " + T, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) + 30), f, Color.Black);
                }
            }
            else if (zuZeichnen == "T_laengs")
            {
                f.T_laengs.Location = new System.Drawing.Point(ursprung.X - 60, ursprung.Y);
                f.T_laengs.Text = "Längsseite 🔍";
                f.T_laengs.AutoSize = true;
                f.Controls.Add(f.T_laengs);
                f.T_laengs.Show();
                drawText("H = " + H, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) - 20), f, Color.Black);
                if (!mitBe)
                {
                    drawText("T = " + T, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) + 30), f, Color.Red);
                }
                else
                {
                    drawText("Td = " + T, new Point(ursprung.X + (int)(ende / 2) - 20, ursprung.Y + (int)(yOben) + 30), f, Color.Black);
                }
            }
            gr.Dispose();
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
            gr.Dispose();
        }
        /// <summary>
        /// Zeichnet das Zeichen für den Wasserspiegel.
        /// </summary>
        /// <param name="ursprung"></param>
        /// <param name="f"></param>
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
            gr.Dispose();
        }
        /// <summary>
        /// Zeichnet ein Label statt eines Richtigen Labels.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="f"></param>
        /// <param name="farbe"></param>
        private static void drawText(String text, Point position, Form1 f, Color farbe)
        {
            System.Drawing.Graphics formGraphics = f.CreateGraphics();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 8);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(farbe);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(text, drawFont, drawBrush, position.X, position.Y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }
    }
}
