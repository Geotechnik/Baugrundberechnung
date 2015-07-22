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
    public partial class Form5 : Form
    {
        public Label Wasserspiegeldifferenz = new Label();
        public Label Einbindetiefe = new Label();
        private Pen pen_grube;
        private Pen pen_hint;
        private int counter = 0;
        private double max;
        Graphics gr;

        public Form5(String welches, double L, double B, double H, double T, double Teck, bool ohneBe)
        {
            InitializeComponent();         
            this.öffnen(welches, L,B, H,T,Teck,ohneBe);
        }

        public void öffnen(String welches, double L, double B, double H, double T, double Teck, bool ohneBe)
        {
            int yOben2 = 20;

            Zeichnen(welches, H, T, Teck);
            Wasserspiegeldifferenz.Location = new System.Drawing.Point((int)(max / 2) + 20, yOben2 + 20);
            Wasserspiegeldifferenz.Text = "H = " + H;
            Wasserspiegeldifferenz.BackColor = Color.LightGray;
            Wasserspiegeldifferenz.AutoSize = true;
            Controls.Add(Wasserspiegeldifferenz);
            Wasserspiegeldifferenz.Show();

            if (!ohneBe)
            {

            }
        }

        private void Zeichnen(String welches, double H, double T, double Teck)
        {
            gr = Graphics.FromHwnd(Handle);
            this.TopMost = true;
            pen_hint = new Pen(Color.LightGray, (float)7.0);
            gr.Clear(Form3.DefaultBackColor);

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
            int yOben = (int)(H * lmbd);
            int yOben2 = 20;
            int yUnten = (int)max;
            //Hintergrund test
            while (x < (max / 2))
            {
                gr.DrawLine(pen_hint, new Point(x, yOben), new Point(x, yUnten));
                x += 4;
            }

            //Hintergrund test
            while (x < max)
            {
                gr.DrawLine(pen_hint, new Point(x, yOben2), new Point(x, yUnten));
                x += 4;
            }
            this.Text = welches;
            pen_grube = new Pen(Color.Black, (float)(2));
            gr.DrawLine(pen_grube, new Point((int)(max / 2), 20), new Point((int)max / 2, (int)((H + T) * lmbd)));
            gr.DrawLine(pen_grube, new Point((int)max / 2, 20), new Point((int)max, 20));
            gr.DrawLine(pen_grube, new Point(0, (int)(H * lmbd)), new Point((int)(max / 2), (int)(H * lmbd)));
            ZeichnePfeil(new Point((int)(max / 2) + 20, yOben2), new Point((int)(max / 2) + 20, yOben), this);
            ZeichnePfeil(new Point((int)(max / 2) + 20, yOben), new Point((int)(max / 2) + 20, yUnten), this);
            zeichneWassersspiegel(new Point(20, yOben), this);
            zeichneWassersspiegel(new Point((int)max - 20, yOben2), this);
        }

        private static void ZeichnePfeil(Point oben, Point unten, Form5 f)
        {
            Graphics gr = Graphics.FromHwnd(f.Handle);
            gr.DrawLine(Pens.Black, oben, unten);
            gr.DrawLine(Pens.Black, unten, new Point(unten.X - 3, unten.Y - 6));
            gr.DrawLine(Pens.Black, unten, new Point(unten.X + 3, unten.Y - 6));
            gr.DrawLine(Pens.Black, oben, new Point(oben.X - 3, oben.Y + 6));
            gr.DrawLine(Pens.Black, oben, new Point(oben.X + 3, oben.Y + 6));

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text == "Stirn")
            {
                Form1.offen_stirn_ecke_eben_laengs[0] = false;
            }
            else if (this.Text == "Ecke")
            {
                Form1.offen_stirn_ecke_eben_laengs[1] = false;
            }
            else if (this.Text == "Eben")
            {
                Form1.offen_stirn_ecke_eben_laengs[2] = false;
            }
            else if (this.Text == "Längs")
            {
                Form1.offen_stirn_ecke_eben_laengs[3] = false;
            }
            this.Hide();
            e.Cancel = true;
        }

        public static void zeichneWassersspiegel(Point ursprung, Form5 f)
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
