using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baugrundberechnung
{
    /// <summary>
    /// Diese Klasse repräsentiert die Zeichnung im neuem Fenster wenn man Stirn, Ecke, Eben oder Längs öffnet.
    /// </summary>
    public partial class Form3 : Form
    {
        //Attribute
        public Label Aquivermächtigkeit = new Label();
        public Label Wasserspiegeldifferenz = new Label();
        public Label Einbindetiefe = new Label();
        private Pen pen_grube =new Pen(Color.Black, (float)(2)); 
        private Pen pen_hint =new Pen(Color.LightGray, (float)7.0);
        private double max;
        /// <summary>
        /// Initialisiert die Componenten
        /// </summary>
        public Form3()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Zeichnet mit den Übergebenenen Werten die Baugrube. Entweder Stirn, Ecke, Eben oder Längs
        /// </summary>
        /// <param name="welches"></param>
        /// <param name="L"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <param name="T"></param>
        /// <param name="Teck"></param>
        /// <param name="ohneBe"></param>
        public void öffnen(String welches, double L, double B, double H, double T, double Teck, bool ohneBe)
        {
            Graphics gr = Graphics.FromHwnd(Handle);
            this.Activate();
            gr.Clear(Form3.DefaultBackColor);
            //variablen Initialisieren
            if (this.Width - 20 < this.Height)
            {
                max = this.Width - 30;
            }
            else
            {
                max = this.Height - 10;
            }
            double lmbd = (max - 10) / (H + Teck);
            int x = 0 + 4;
            int yNull = 20;
            int yOben = (int)(H * lmbd);
            int yUnten = (int)max;
            int nullPunkt = (int)max /2;
            //Hintergrund
            while (x < nullPunkt)
            {
                gr.DrawLine(pen_hint, new Point(x, yOben), new Point(x, yUnten));
                x += 4;
            }
            while (x < max)
            {
                gr.DrawLine(pen_hint, new Point(x, yNull), new Point(x, yUnten));
                x += 4;
            }

            this.Text = welches;

            gr.DrawLine(new Pen(Color.Black, (float)(4)), new Point(nullPunkt, yNull), new Point(nullPunkt, (int)((H + T) * lmbd)));
            gr.DrawLine(pen_grube, new Point(nullPunkt, yNull), new Point((int)max, yNull));
            gr.DrawLine(pen_grube, new Point(0, yOben), new Point(nullPunkt, yOben));
            ZeichnePfeil(new Point(nullPunkt + 20, yNull), new Point(nullPunkt + 20, yOben), this);
            ZeichnePfeil(new Point(nullPunkt + 20, yOben), new Point(nullPunkt + 20, (int)((H + T) * lmbd)), this);
            zeichneWassersspiegelZeichen(new Point(70, yOben), this);
            zeichneWassersspiegelZeichen(new Point((int)max - 70, yNull), this);
            zeichneDoppelDach(new Point(20, yOben),this);
            zeichneDoppelDach(new Point((int)max - 20, yNull),this);
            //Wasserspiegel Label
            Wasserspiegeldifferenz.Location = new System.Drawing.Point(nullPunkt + 15, yNull+20);
            Wasserspiegeldifferenz.Text = "H = " + H;
            Wasserspiegeldifferenz.BackColor = Color.LightGray;
            Wasserspiegeldifferenz.AutoSize = true;
            this.Controls.Add(Wasserspiegeldifferenz);
            Wasserspiegeldifferenz.Show();
            //Einbindetiefe Label
            Einbindetiefe.Location = new System.Drawing.Point(nullPunkt + 15, yOben + 20);
            Einbindetiefe.Text = "Td = " + T;
            Einbindetiefe.BackColor = Color.LightGray;
            Einbindetiefe.AutoSize = true;
            this.Controls.Add(Einbindetiefe);
            Einbindetiefe.ForeColor = Color.Black;
            if (!ohneBe)
            {
                Einbindetiefe.Text = "T = " + T;
                Einbindetiefe.ForeColor = Color.Red;
            }
            Einbindetiefe.Show();
        }
        /// <summary>
        /// Diese Methode ist zum Zeichnen der Beschriftungspfeile gedacht
        /// </summary>
        /// <param name="oben"></param>
        /// <param name="unten"></param>
        /// <param name="f"></param>
        private static void ZeichnePfeil(Point oben, Point unten, Form3 f)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            gr.DrawLine(Pens.Black, oben, unten);
            gr.DrawLine(Pens.Black, unten, new Point(unten.X - 3, unten.Y - 6));
            gr.DrawLine(Pens.Black, unten, new Point(unten.X + 3, unten.Y - 6));
            gr.DrawLine(Pens.Black, oben, new Point(oben.X - 3, oben.Y + 6));
            gr.DrawLine(Pens.Black, oben, new Point(oben.X + 3, oben.Y + 6));
        }
        /// <summary>
        /// Wenn die Form schließt soll sie nicht Richtig geschlossen werden sondern nur versteckt werden/deaktiviert werden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text == "Stirn")
            {
                Form1.offen_stirn_ecke_eben_laengs[0] = false;
                Form1.wieoft_stirn_ecke_eben_laengs[0] = 0;
            }
            else if (this.Text == "Ecke")
            {
                Form1.offen_stirn_ecke_eben_laengs[1] = false;
                Form1.wieoft_stirn_ecke_eben_laengs[1] = 0;
            }
            else if (this.Text == "Eben")
            {
                Form1.offen_stirn_ecke_eben_laengs[2] = false;
                Form1.wieoft_stirn_ecke_eben_laengs[2] = 0;
            }
            else if (this.Text == "Längsseite")
            {
                Form1.offen_stirn_ecke_eben_laengs[3] = false;
                Form1.wieoft_stirn_ecke_eben_laengs[3] = 0;
            }
            this.Hide();
            e.Cancel = true;
        }
        /// <summary>
        /// Das Zeichen für den Wasserspiegel wird gezeichnet.
        /// </summary>
        /// <param name="ursprung"></param>
        /// <param name="f"></param>
        public static void zeichneWassersspiegelZeichen(Point ursprung, Form3 f)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            gr.TranslateTransform(ursprung.X, ursprung.Y);
            Point mitte = new Point(0, 0);
            Point ersteLinieRechts = new Point(8, 4);
            Point ersteLinieLinks = new Point(-8, 4);
            Point zweiteLinieRechts = new Point(4, 8);
            Point zweiteLinieLinks = new Point(-4, 8);
            Point obenRechts = new Point(-10, -10);
            Point obenLinks = new Point(10, -10);
            gr.DrawLine(Pens.Black, ersteLinieLinks, ersteLinieRechts);
            gr.DrawLine(Pens.Black, zweiteLinieLinks, zweiteLinieRechts);
            gr.DrawLine(Pens.Black, obenLinks, obenRechts);
            gr.DrawLine(Pens.Black, obenRechts, mitte);
            gr.DrawLine(Pens.Black, obenLinks, mitte);
        }
        public static void zeichneDoppelDach(Point ursprung, Form3 f)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            gr.TranslateTransform(ursprung.X, ursprung.Y);
            Point untenLinks = new Point(-20, 16);
            Point untenRechts = new Point(4, 16);
            gr.DrawLine(Pens.Black, untenLinks, new Point(-8, 0));
            gr.DrawLine(Pens.Black, untenRechts, new Point(-8, 0));
            Point untenL = new Point(20, 16);
            Point untenR = new Point(-4, 16);
            gr.DrawLine(Pens.Black, untenL, new Point(8, 0));
            gr.DrawLine(Pens.Black, untenR, new Point(8, 0));

        }
    }
}
