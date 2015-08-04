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
    public partial class Form1 : Form
    {
        //Attribute
        public Label Baugrube = new Label();
        public Label B_label = new Label();
        public Label H_label_SeiteBaugrube = new Label();
        public Label S_label_SeiteBaugrube = new Label();
        public Label T_label_Ecke = new Label();
        public Label H_label_Ecke = new Label();
        public Label T_label_Stirn = new Label();
        public Label H_label_Stirn = new Label();
        public Label T_label_Längs = new Label();
        public Label H_label_Längs = new Label();
        public Label T_label_Eben = new Label();
        public Label H_label_Eben = new Label();
        public Label S_labelSeite = new Label();
        public Label BaugrubeSeite = new Label();
        public Label T_stirn = new Label();
        public Label T_ecke = new Label();
        public Label T_eben = new Label();
        public Label T_laengs = new Label();
        public Label ohneBeHinweis = new Label();
        public DurchgedrehtesLabel L_label = new DurchgedrehtesLabel();
        private Form3 Stirn = new Form3();
        private Form3 Eben = new Form3();
        private Form3 Laengs = new Form3();
        private Form3 Ecke = new Form3();
        public static int[] wieoft_stirn_ecke_eben_laengs = new int[4];
        private int[] counter_stirn_ecke_eben_laengs = new int[5];
        private double[] T_stirn_ecke_eben_laengs = new double[4];
        private double laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit, anströmung_eben, anströmung_laengs, anströmung_stirn, anströmung_ecke, umfeld_eben, umfeld_laengs, umfeld_stirn, umfeld_ecke, bemessungsbeiwert;
        public static bool aufAlleUebernehmen = false;
        public static bool[] offen_stirn_ecke_eben_laengs = new bool[4];
        /// <summary>
        /// Initialisiert alle Componenten
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Validierung.setFormReference(this);
            this.T_stirn.Click += new System.EventHandler(this.T_stirnClick);
            this.T_ecke.Click += new System.EventHandler(this.T_eckeClick);
            this.T_eben.Click += new System.EventHandler(this.T_ebenClick);
            this.T_laengs.Click += new System.EventHandler(this.T_laengsClick);
        }
        /// <summary>
        /// Berechnet alle 8 möglichen Werte (4 mit be, 4 ohne Be) und ruft dann Grafik auf. Überprüft ob die Eingabe gültig ist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Berechnen_Click(object sender, EventArgs e)
        {
            if (gueltigeNummern())
            {
                variablenSetzen();
                if (breite <= laenge && aquifermächtigkeit >= wasserspiegeldifferenz && breite / laenge >= 0.3)
                {
                    if (Form1.ActiveForm == this)
                    {
                        Graphics g = this.CreateGraphics();
                        g.Clear(Form1.ActiveForm.BackColor);
                        g.Dispose();
                    }
                    ergebnisseInLabelSchreiben();
                    if (berechnungOhneBe.Checked)
                    {
                        zeichnungenZeichnenOhneBe();
                    }
                    else
                    {
                        zeichnungenZeichnenMitBe();
                    }
                }
                else //Wenn breite größer als laenge ist, oder aquifermächtigkeit kleiner wasserspiegeldifferenz
                {
                    if (breite > laenge)
                    {
                        System.Windows.Forms.MessageBox.Show("Breite der Baugrube (B) muss kleiner oder gleich Länge der Baugrube (L) sein. B wird nun maximal gesetzt", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (laenge % 1 == 0)
                        {
                            BreiteBaugrube.Text = laenge + ",00";
                            BreiteBaugrube_Leave(BreiteBaugrube, e);
                        }
                        else
                        {
                            BreiteBaugrube.Text = laenge + "";
                            BreiteBaugrube_Leave(BreiteBaugrube, e);
                        }
                    }
                    else if (aquifermächtigkeit < wasserspiegeldifferenz)
                    {
                        System.Windows.Forms.MessageBox.Show("Aquivermächtigkeit (S) muss größer oder gleich der Wasserspiegeldifferenz (H) sein. H wird nun maximal gesetzt", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (aquifermächtigkeit % 1 == 0)
                        {
                            Wasserspiegeldifferenz.Text = aquifermächtigkeit + ",00";
                            Wasserspiegeldifferenz_Leave(Wasserspiegeldifferenz, e);
                        }
                        else
                        {
                            Wasserspiegeldifferenz.Text = aquifermächtigkeit + "";
                            Wasserspiegeldifferenz_Leave(Wasserspiegeldifferenz, e);
                        }
                    }
                    else if (breite / laenge < 0.3)
                    {
                        System.Windows.Forms.MessageBox.Show("Breite der Baugrube (B) muss größer oder gleich 0,3 * Länge der Baugrube (L) sein. L wird nun maximal gesetzt", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (breite * 3 % 1 == 0)
                        {
                            LängeBaugrube.Text = (breite * 3) + ",00";
                            LängeBaugrube_Leave(LängeBaugrube, e);
                        }
                        else
                        {
                            LängeBaugrube.Text = (breite * 3) + "";
                            LängeBaugrube_Leave(LängeBaugrube, e);
                        }
                    }
                }
                LängeBaugrube.BackColor = Color.White;
                BreiteBaugrube.BackColor = Color.White;
                Wasserspiegeldifferenz.BackColor = Color.White;
                GlobaleSicherheit.BackColor = Color.White;
                WichteAuftrieb.BackColor = Color.White;
                Aquivermächtigkeit.BackColor = Color.White;
            }
            else
            {
                int ind = 0;
                Form1 send = (Form1)this;
                foreach (Control tb in send.Controls)
                {
                    if (tb.Focused)
                    {
                        ind = (int)this.Controls.IndexOf(tb);
                    }
                }
                System.Windows.Forms.MessageBox.Show("Eingabe ist nicht  gültig. Keine Berechnung möglich!", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        /// <summary>
        /// Zeichnet im gleichem und neuem Fenster Mit Be
        /// </summary>
        private void zeichnungenZeichnenMitBe()
        {
            Grafik.zeichnemal(laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], this, true, this.Size);
            if (offen_stirn_ecke_eben_laengs[0])
            {
                Stirn.Activate();
                Stirn.öffnen("Stirn", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            if (offen_stirn_ecke_eben_laengs[1])
            {
                Ecke.Activate();
                Ecke.öffnen("Ecke", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[1], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            if (offen_stirn_ecke_eben_laengs[2])
            {
                Eben.Activate();
                Eben.öffnen("Eben", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            if (offen_stirn_ecke_eben_laengs[3])
            {

                Laengs.Activate();
                Laengs.öffnen("Längsseite", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            this.Activate();
        }
        /// <summary>
        /// Zeichnet im gleichem und neuem Fenster Ohne Be
        /// </summary>
        private void zeichnungenZeichnenOhneBe()
        {
            Grafik.zeichnemal(laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], this, false, this.Size);
            berechnungOhneBe.Checked = false;
            if (offen_stirn_ecke_eben_laengs[0])
            {
                Stirn.Activate();
                Stirn.öffnen("Stirn", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            if (offen_stirn_ecke_eben_laengs[1])
            {
                Ecke.Activate();
                Ecke.öffnen("Ecke", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[1], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            if (offen_stirn_ecke_eben_laengs[2])
            {
                Eben.Activate();
                Eben.öffnen("Eben", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            if (offen_stirn_ecke_eben_laengs[3])
            {
                Laengs.Activate();
                Laengs.öffnen("Längsseite", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            this.Activate();
        }
        /// <summary>
        /// Zeichnet die Form für Stirn im neuem Fenster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_stirnClick(object sender, EventArgs e)
        {
            if (!offen_stirn_ecke_eben_laengs[0])
            {
                Stirn.Show();
                Stirn.öffnen("Stirn", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
                offen_stirn_ecke_eben_laengs[0] = true;
                wieoft_stirn_ecke_eben_laengs[0] = 1;
                Stirn.Activate();
            }
            if (wieoft_stirn_ecke_eben_laengs[0] == 1)
            {
                Stirn.Activate();
                Stirn.öffnen("Stirn", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }

        }
        /// <summary>
        /// Zeichnet die Form für Ecke im neuem Fenster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_eckeClick(object sender, EventArgs e)
        {

            if (!offen_stirn_ecke_eben_laengs[1])
            {
                Ecke.Show();
                Ecke.öffnen("Ecke", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[1], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
                offen_stirn_ecke_eben_laengs[1] = true;
                wieoft_stirn_ecke_eben_laengs[1] = 1;
                Ecke.Activate();
            }
            if (wieoft_stirn_ecke_eben_laengs[1] == 1)
            {
                Ecke.Activate();
                Ecke.öffnen("Ecke", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[1], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
        }
        /// <summary>
        /// Zeichnet die Form für Eben im neuem Fenster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_ebenClick(object sender, EventArgs e)
        {
            if (!offen_stirn_ecke_eben_laengs[2])
            {
                Eben.Show();
                Eben.öffnen("Eben", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
                offen_stirn_ecke_eben_laengs[2] = true;
                wieoft_stirn_ecke_eben_laengs[2] = 1;
                Eben.Activate();
            }
            if (wieoft_stirn_ecke_eben_laengs[2] == 1)
            {
                Eben.Activate();
                Eben.öffnen("Eben", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
        }
        /// <summary>
        /// Zeichnet die Form für Längs im neuem Fenster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_laengsClick(object sender, EventArgs e)
        {
            if (!offen_stirn_ecke_eben_laengs[3])
            {

                Laengs.Show();
                Laengs.öffnen("Längsseite", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
                offen_stirn_ecke_eben_laengs[3] = true;
                wieoft_stirn_ecke_eben_laengs[3] = 1;
                Laengs.Activate();
            }
            if (wieoft_stirn_ecke_eben_laengs[3] == 1)
            {
                Laengs.Activate();
                Laengs.öffnen("Längsseite", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
        }
        /// <summary>
        /// Schreibt die Ergebnisse in die Entsprechende Labels
        /// </summary>
        private void ergebnisseInLabelSchreiben()
        {
            Ergebnis_eben_mi_Be_Label.Text = "" + Berechnung.berechnungebenMitBe(anströmung_eben, umfeld_eben, bemessungsbeiwert, laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit);
            Ergebnis_eben_ohne_Be_Label.Text = "" + Berechnung.berechnungebenOhneBe(anströmung_eben, umfeld_eben, laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit);
            Ergebnis_Länge_mit_Be_Label.Text = "" + Berechnung.berechnungMitBe(anströmung_laengs, umfeld_laengs, bemessungsbeiwert, laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit);
            Ergebnis_Längs_ohne_Be_Label.Text = "" + Berechnung.berechnungOhneBe(anströmung_laengs, umfeld_laengs, laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit);
            Ergebnis_Stirn_mit_Be_Label.Text = "" + Berechnung.berechnungMitBe(anströmung_stirn, umfeld_stirn, bemessungsbeiwert, laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit);
            Ergebnis_Stirn_ohne_Be_Label.Text = "" + Berechnung.berechnungOhneBe(anströmung_stirn, umfeld_stirn, laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit);
            Ergebnis_Ecke_mit_Be_Label.Text = "" + Berechnung.berechnungMitBe(anströmung_ecke, umfeld_ecke, bemessungsbeiwert, laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit);
            Ergebnis_Ecke_ohne_Be_Label.Text = "" + Berechnung.berechnungOhneBe(anströmung_ecke, umfeld_ecke, laenge, breite, wasserspiegeldifferenz, aquifermächtigkeit, wichteAuftrieb, globaleSicherheit);
            if (berechnungOhneBe.Checked)
            {
                T_stirn_ecke_eben_laengs[2] = Convert.ToDouble(Ergebnis_eben_ohne_Be_Label.Text);
                T_stirn_ecke_eben_laengs[3] = Convert.ToDouble(Ergebnis_Längs_ohne_Be_Label.Text);
                T_stirn_ecke_eben_laengs[0] = Convert.ToDouble(Ergebnis_Stirn_ohne_Be_Label.Text);
                T_stirn_ecke_eben_laengs[1] = Convert.ToDouble(Ergebnis_Ecke_ohne_Be_Label.Text);
            }
            else
            {
                T_stirn_ecke_eben_laengs[2] = Convert.ToDouble(Ergebnis_eben_mi_Be_Label.Text);
                T_stirn_ecke_eben_laengs[3] = Convert.ToDouble(Ergebnis_Länge_mit_Be_Label.Text);
                T_stirn_ecke_eben_laengs[0] = Convert.ToDouble(Ergebnis_Stirn_mit_Be_Label.Text);
                T_stirn_ecke_eben_laengs[1] = Convert.ToDouble(Ergebnis_Ecke_mit_Be_Label.Text);
            }
        }
        /// <summary>
        /// Setzt die Attribute laenge, B, H, S, Y, n, anströmung_eben, anströmung_laengs, anströmung_stirn, anströmung_ecke, Umfeld_eben, umfeld_laengs, umfeld_stirn, umfeld_ecke und bemessungsbeiwert
        /// </summary>
        private void variablenSetzen()
        {
            laenge = Convertieren.convertToDouble(LängeBaugrube.Text);
            breite = Convertieren.convertToDouble(BreiteBaugrube.Text);
            wasserspiegeldifferenz = Convertieren.convertToDouble(Wasserspiegeldifferenz.Text);
            aquifermächtigkeit = Convertieren.convertToDouble(Aquivermächtigkeit.Text);
            wichteAuftrieb = Convertieren.convertToDouble(WichteAuftrieb.Text);
            globaleSicherheit = Convertieren.convertToDouble(GlobaleSicherheit.Text);
            anströmung_eben = Convertieren.convertToDouble(Anströmung_eben.Text);
            umfeld_eben = Convertieren.convertToDouble(Umfeld_eben.Text);
            anströmung_laengs = Convertieren.convertToDouble(Anströmung_Längs.Text);
            umfeld_laengs = Convertieren.convertToDouble(Umfeld_Längs.Text);
            anströmung_stirn = Convertieren.convertToDouble(Anströmung_Stirn.Text);
            umfeld_stirn = Convertieren.convertToDouble(Umfeld_Stirn.Text);
            anströmung_ecke = Convertieren.convertToDouble(Anströmung_Ecke.Text);
            umfeld_ecke = Convertieren.convertToDouble(Umfeld_Ecke.Text);
            bemessungsbeiwert = Convertieren.convertToDouble(Bemessungsbeiwert.Text);
        }
        /// <summary>
        /// Überprüft ob die Eingaben gültig sind
        /// </summary>
        /// <returns></returns>
        private bool gueltigeNummern()
        {
            bool laengevalid = Validierung.isValidNumber(LängeBaugrube.Text, "L");
            bool baugrubevalid = Validierung.isValidNumber(BreiteBaugrube.Text, "B");
            bool wasserspiegelvalid = Validierung.isValidNumber(Wasserspiegeldifferenz.Text, "H");
            bool sicherheitvalid = Validierung.isValidNumber(GlobaleSicherheit.Text, "n");
            bool auftriebvalid = Validierung.isValidNumber(WichteAuftrieb.Text, "γ");
            bool aquifervalid = Validierung.isValidNumber(Aquivermächtigkeit.Text, "S");
            return (laengevalid && baugrubevalid && wasserspiegelvalid && sicherheitvalid && auftriebvalid && aquifervalid);
        }
        /// <summary>
        /// Überprüft ob die Eingabe gültig ist
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        private bool gueltigeNummern(TextBox active)
        {
            if (active.Name == "LängeBaugrube")
            {
                return Validierung.isValidNumber(LängeBaugrube.Text, "L");
            }
            else if (active.Name == "BreiteBaugrube")
            {
                return Validierung.isValidNumber(BreiteBaugrube.Text, "B");
            }
            else if (active.Name == "Wasserspiegeldifferenz")
            {
                return Validierung.isValidNumber(Wasserspiegeldifferenz.Text, "H");
            }
            else if (active.Name == "GlobaleSicherheit")
            {
                return Validierung.isValidNumber(GlobaleSicherheit.Text, "n");
            }
            else if (active.Name == "WichteAuftrieb")
            {
                return Validierung.isValidNumber(WichteAuftrieb.Text, "γ");
            }
            else if (active.Name == "Aquivermächtigkeit")
            {
                return Validierung.isValidNumber(Aquivermächtigkeit.Text, "S");
            }
            return false;
        }
        /// <summary>
        /// koppelt laengeScroll mit der Textbox LängeBaugrube wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void laengeScroll_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, LängeBaugrube);
        }
        /// <summary>
        /// koppelt breiteScroll mit der textBox BreiteBaugrube wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void breiteScroll_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, BreiteBaugrube);
        }
        /// <summary>
        /// koppelt wasserspiegelScroll mit der textBox Wasserspiegeldifferenz wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void wasserspiegelScroll_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, Wasserspiegeldifferenz);
        }
        /// <summary>
        /// koppelt aquiferScroll mit der textBox Aquifermächtigkeit wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void aquiferScroll_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, Aquivermächtigkeit);
        }
        /// <summary>
        /// koppelt auftriebScroll mit der textBox WichteAuftrieb wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void auftriebScroll_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, WichteAuftrieb);
        }
        /// <summary>
        /// Koppelt die übergebene Scrollbar mit der Übergebenen Textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bezugstextbox"></param>
        private void scallierungDerWerte(object sender, EventArgs e, TextBox bezugstextbox)
        {
            ScrollBar tempScrollBar = (ScrollBar)sender;
            if ((tempScrollBar.Value / 100.0) % 1 == 0)
            {
                bezugstextbox.Text = tempScrollBar.Value / 100.0 + ",00";
            }
            else
            {
                bezugstextbox.Text = tempScrollBar.Value / 100.0 + "";
            }
            Berechnen_Click(sender, e);
        }
        /// <summary>
        /// koppelt sicherheitScroll mit der textBox GlobaleSicherheit wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void sicherheitScroll_ValueChanged(object sender, EventArgs e)
        {
            // Überprüft ob die Zahl eine Nachkommastelle hat
            if ((sicherheitScroll.Value / 1000.0) % 1 == 0)
            {
                GlobaleSicherheit.Text = (sicherheitScroll.Value / 1000.0) + ",000";
            }
            else
            {
                GlobaleSicherheit.Text = (sicherheitScroll.Value / 1000.0) + "";
            }

            Berechnen_Click(sender, e);
        }
        /// <summary>
        /// Öffnet den Infobereich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form infoForm = new Form2();
            infoForm.Text = "Info";
            infoForm.ShowDialog();
        }
        /// <summary>
        /// lässt die Werte einmalig berechnen sobald die Form geöffnet wurde.
        /// lässt die werte aus der textBox in die Scrollbar überschreiben.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //ohne geht das zeichnen im neuem Fenster beim erstmaligem öffnen Schief.
            if (offen_stirn_ecke_eben_laengs[0] && counter_stirn_ecke_eben_laengs[1] == 0)
            {
                counter_stirn_ecke_eben_laengs[1]++;
                Stirn.Activate();
                Stirn.öffnen("Stirn", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
                
            }
            if (offen_stirn_ecke_eben_laengs[1] && counter_stirn_ecke_eben_laengs[2] == 0)
            {
                counter_stirn_ecke_eben_laengs[2]++;
                Ecke.Activate();
                Ecke.öffnen("Ecke", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[1], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            if (offen_stirn_ecke_eben_laengs[2] && counter_stirn_ecke_eben_laengs[3] == 0)
            {
                counter_stirn_ecke_eben_laengs[3]++;
                Eben.Activate();
                Eben.öffnen("Eben", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            if (offen_stirn_ecke_eben_laengs[3] && counter_stirn_ecke_eben_laengs[4] == 0)
            {
                counter_stirn_ecke_eben_laengs[4]++;
                Laengs.Activate();
                Laengs.öffnen("Längsseite", laenge, breite, wasserspiegeldifferenz, T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
            }
            //this.Activate();
            if (counter_stirn_ecke_eben_laengs[0] == 0)
            {
                Berechnen_Click(null, null);
                counter_stirn_ecke_eben_laengs[0]++;
                LängeBaugrube_Leave(LängeBaugrube, e);
                BreiteBaugrube_Leave(BreiteBaugrube, e);
                Wasserspiegeldifferenz_Leave(Wasserspiegeldifferenz, e);
                GlobaleSicherheit_Leave(GlobaleSicherheit, e);
                WichteAuftrieb_Leave(WichteAuftrieb, e);
                Aquivermächtigkeit_Leave(Aquivermächtigkeit, e);
            }
        }
        /// <summary>
        /// Setzt die Werte zurück auf Standardwerte
        /// ruft Berechnen auf und lässt die Werte auf die Scrollbar überschreiben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZurücksetzenButton_Click(object sender, EventArgs e)
        {
            LängeBaugrube.Text = "30,00";
            BreiteBaugrube.Text = "15,00";
            Wasserspiegeldifferenz.Text = "10,00";
            Aquivermächtigkeit.Text = "30,00";
            WichteAuftrieb.Text = "11,00";
            GlobaleSicherheit.Text = "1,368";
            LängeBaugrube_Leave(LängeBaugrube, e);
            BreiteBaugrube_Leave(BreiteBaugrube, e);
            Wasserspiegeldifferenz_Leave(Wasserspiegeldifferenz, e);
            GlobaleSicherheit_Leave(GlobaleSicherheit, e);
            WichteAuftrieb_Leave(WichteAuftrieb, e);
            Aquivermächtigkeit_Leave(Aquivermächtigkeit, e);
            Berechnen_Click(sender, e);
            aufAlleUebernehmen = false;
        }
        /// <summary>
        /// Überschreibt die Werte von der Textbox auf die Scrollbar.
        /// wenn der Wert gleich ist macht er nichts um endlosschleife zu verhindern.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="bezugsScrollBar"></param>
        /// <param name="s"></param>
        private void Runden_und_Ueberschreiben(object sender, ScrollBar bezugsScrollBar, string s)
        {
            //überprüft ob String in der Textbox eine gültige Zahl ist
            if (gueltigeNummern((TextBox)sender))
            {
                TextBox tempTextBox = (TextBox)sender;
                // Überprüft ob die Checkbox angehakt ist und ob es ScrollBar5 ist.
                if (aufAlleUebernehmen && bezugsScrollBar.Name == "sicherheitScroll")
                {
                    //Überprüft ob der gerundete Wert der Textbox gleich dem Wert der Scrollbar ist oder ob nicht mehr als 3 Nachkommastellen zum runden da sind.
                    if (bezugsScrollBar.Value != (int)(Convertieren.convertToDouble(tempTextBox.Text) * 1000 + 0.5) || (Convertieren.convertToDouble(tempTextBox.Text) * 1000) % 1 != 0)
                    {
                        //Überschreibt den gerundeten Wert in die ScrollBar
                        bezugsScrollBar.Value = (int)(Convertieren.convertToDouble(tempTextBox.Text) * 1000 + 0.5);
                        //lässt den Wert zurücküberschreiben für eine änderung von 11,00 zu 11,0004 zurück zu 11,000 da sonst Werte gleich.
                        sicherheitScroll_ValueChanged(sender, null);
                    }
                }
                // Überprüft ob Checkbox nicht angehakt wurde und ob es Scrollbar5 ist.
                else if (!aufAlleUebernehmen && bezugsScrollBar.Name == "sicherheitScroll")
                {
                    //Überprüft ob gerundet werden muss
                    if ((Convertieren.convertToDouble(tempTextBox.Text) * 1000) % 1 != 0)
                    {
                        //gibt MessageBox aus.
                        Form message = new Baugrundberechnung.MessageBox("Es kann maximal mit einer Genauigkeit von\n3 Nachkommastellen gerechnet werden.\nEs wird automatisch gerundet");
                        message.Show();
                    }
                    //Überprüft ob der gerundete Wert der Textbox gleich dem Wert der Scrollbar ist oder ob nicht mehr als 3 Nachkommastellen zum runden da sind.
                    if (bezugsScrollBar.Value != (int)(Convertieren.convertToDouble(tempTextBox.Text) * 1000 + 0.5) || (Convertieren.convertToDouble(tempTextBox.Text) * 1000) % 1 != 0)
                    {
                        //Überschreibt den gerundeten Wert in die ScrollBar
                        bezugsScrollBar.Value = (int)(Convertieren.convertToDouble(tempTextBox.Text) * 1000 + 0.5);
                        sicherheitScroll_ValueChanged(sender, null);
                    }
                }
                // Überprüft ob Checkbox angehakt ist und ob es nicht Scrollbar5 ist.
                else if (aufAlleUebernehmen && bezugsScrollBar.Name != "sicherheitScroll")
                {
                    //Überprüft ob der gerundete Wert der Textbox gleich dem Wert der Scrollbar ist oder ob nicht mehr als 2 Nachkommastellen zum runden da sind.
                    if (bezugsScrollBar.Value != (int)(Convertieren.convertToDouble(tempTextBox.Text) * 100 + 0.5) || (Convertieren.convertToDouble(tempTextBox.Text) * 100) % 1 != 0)
                    {
                        //Überschreibt den gerundeten Wert in die ScrollBar
                        bezugsScrollBar.Value = (int)(Convertieren.convertToDouble(tempTextBox.Text) * 100 + 0.5);
                        //lässt den Wert zurücküberschreiben für eine änderung von 11,000 zu 11,0004 zurück zu 11,000 da sonst Werte gleich.
                        scallierungDerWerte(bezugsScrollBar, null, tempTextBox);
                    }
                }
                // Überprüft ob Checkbox nicht angehakt wurde und ob es nicht Scrollbar5 ist.
                else if (!aufAlleUebernehmen && bezugsScrollBar.Name != "sicherheitScroll")
                {
                    //Überprüft ob gerundet werden muss
                    if ((Convertieren.convertToDouble(tempTextBox.Text) * 100) % 1 != 0)
                    {
                        //gibt MessageBox aus.
                        Form message = new Baugrundberechnung.MessageBox("Es kann maximal mit einer Genauigkeit von\n2 Nachkommastellen gerechnet werden.\nEs wird automatisch gerundet");
                        message.Show(); 

                    }
                    //Überprüft ob der gerundete Wert der Textbox gleich dem Wert der Scrollbar ist oder ob nicht mehr als 2 Nachkommastellen zum runden da sind.
                    if (bezugsScrollBar.Value != (int)(Convertieren.convertToDouble(tempTextBox.Text) * 100 + 0.5) || (Convertieren.convertToDouble(tempTextBox.Text) * 100) % 1 != 0)
                    {
                        //Überschreibt den gerundeten Wert in die ScrollBar
                        bezugsScrollBar.Value = (int)(Convertieren.convertToDouble(tempTextBox.Text) * 100 + 0.5);
                        scallierungDerWerte(bezugsScrollBar, null, tempTextBox);
                    }
                }
            }
            else // wenn nicht alle True sind gibt er eine Warnung aus
            {
                System.Windows.Forms.MessageBox.Show("Eingabe ist nicht  gültig. Keine Berechnung möglich!", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// textBox1, hScrollBar1 und L
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LängeBaugrube_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, laengeScroll, "L");
        }
        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// BreiteBaugrube, hScrollBar2 und B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BreiteBaugrube_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, breiteScroll, "B");
        }
        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// Wasserspiegeldifferenz, hScrollBar4 und H
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wasserspiegeldifferenz_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, wasserspiegelScroll, "H");
        }
        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// Aquivermächtigkeit, hScrollBar3 und S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Aquivermächtigkeit_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, aquiferScroll, "S");
        }
        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// WichteAuftrieb, hScrollBar6 und γ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WichteAuftrieb_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, auftriebScroll, "γ");
        }
        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// GlobaleSicherheit, hScrollBar5 und n
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlobaleSicherheit_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, sicherheitScroll, "n");
        }
        /// <summary>
        /// ruft Berechnen_Click auf um neu zu Zeichnen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Berechnen_Click(sender, e);
            horizontal.Height = this.Size.Height;
        }
        /// <summary>
        /// Wenn die Checkbocx ohne Be angekreuzt ist wird es rot eingefärbt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void berechnungOhneBe_CheckedChanged(object sender, EventArgs e)
        {
            if (berechnungOhneBe.Checked)
            {
                berechnungOhneBe.ForeColor = Color.Red;
            }
            else
            {
                berechnungOhneBe.ForeColor = Color.Black;
            }
        }
       /// <summary>
       /// öffnet die Form für den Haftungsausschluss
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void haftungsausschlussToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form haftungsausschluss = new Form4();
            haftungsausschluss.ShowDialog();
        }
        /// <summary>
        /// beendet das Programm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void programmBeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}