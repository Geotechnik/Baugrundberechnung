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
        public Label BaugrubeSeite = new Label();
        public Label T_stirn = new Label();
        public Label T_ecke = new Label();
        public Label T_eben = new Label();
        public Label T_laengs = new Label();
        private Form3 Stirn = new Form3();
        private Form3 Ecke = new Form3();
        private Form3 Eben = new Form3();
        private Form3 Laengs = new Form3();
        private double[] T_stirn_ecke_eben_laengs = new double[4];
        //public Label L_label = new Label();
        public DurchgedrehtesLabel L_label = new DurchgedrehtesLabel();
        public static bool aufAlleUebernehmen = false;
        private double L, B, H, S, Y, n, A_eb, A_L, A_S, A_Ec, U_eb, U_L, U_S, U_Ec, Bei;
        private int counter = 0;
        public static bool[] offen_stirn_ecke_eben_laengs= new bool[4];
 
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
        /// Berechnet alle 8 möglichen Werte und ruft dann Grafik auf. Überprüft ob die Eingabe gültig ist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Berechnen_Click(object sender, EventArgs e)
        {
            //schaut ob die eingabe Gültig ist
            if (gueltigeNummern())
            {
                //Wandelt die Strings in double um.
                variablenSetzen();
                //Überprüft ob B<= L ist
                if (B <= L && S>=H && B/L >= 0.3)
                {
                    this.Activate();
                    if (Form1.ActiveForm == this)
                    {
                        Graphics g = this.CreateGraphics();
                        g.Clear(Form1.ActiveForm.BackColor);
                        g.Dispose();
                    }
                    //Graphics g= this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);
                    ergebnisseInLabelSchreiben();
                    if (berechnungOhneBe.Checked)
                    {
                        T_stirn_ecke_eben_laengs[2] = Berechnung.berechnungebenOhneBe(A_eb, U_eb, L, B, H, S, Y, n);
                        T_stirn_ecke_eben_laengs[3] = Berechnung.berechnungOhneBe(A_L, U_L, L, B, H, S, Y, n);
                        T_stirn_ecke_eben_laengs[0] = Berechnung.berechnungOhneBe(A_S, U_S, L, B, H, S, Y, n);
                        T_stirn_ecke_eben_laengs[1] = Berechnung.berechnungOhneBe(A_Ec, U_Ec, L, B, H, S, Y, n);
                        Grafik.zeichnemal(L, B, H, S, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], this, false, this.Size);
                        berechnungOhneBe.Checked = false;
                        if (offen_stirn_ecke_eben_laengs[0])
                        {
                            Stirn.Activate();
                            Stirn.öffnen("Stirn", L, B, H, S, T_stirn_ecke_eben_laengs[0], !berechnungOhneBe.Checked);
                        }
                        if (offen_stirn_ecke_eben_laengs[1])
                        {
                            Ecke.Activate();
                            Ecke.öffnen("Ecke", L, B, H, S, T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
                        }
                        if (offen_stirn_ecke_eben_laengs[2])
                        {
                            Eben.Activate();
                            Eben.öffnen("Eben", L, B, H, S, T_stirn_ecke_eben_laengs[2], !berechnungOhneBe.Checked);
                        }
                        if (offen_stirn_ecke_eben_laengs[3])
                        {
                            Laengs.Activate();
                            Laengs.öffnen("Längs", L, B, H, S, T_stirn_ecke_eben_laengs[3], !berechnungOhneBe.Checked);
                        }
                    }
                    else
                    {
                        T_stirn_ecke_eben_laengs[2] = Berechnung.berechnungebenMitBe(A_eb, U_eb, Bei, L, B, H, S, Y, n);
                        T_stirn_ecke_eben_laengs[3] = Berechnung.berechnungMitBe(A_L, U_L, Bei, L, B, H, S, Y, n);
                        T_stirn_ecke_eben_laengs[0] = Berechnung.berechnungMitBe(A_S, U_S, Bei, L, B, H, S, Y, n);
                        T_stirn_ecke_eben_laengs[1] = Berechnung.berechnungMitBe(A_Ec, U_Ec, Bei, L, B, H, S, Y, n);
                        Grafik.zeichnemal(L, B, H, S, T_stirn_ecke_eben_laengs[2], T_stirn_ecke_eben_laengs[3], T_stirn_ecke_eben_laengs[0], T_stirn_ecke_eben_laengs[1], this, true, this.Size);
                        if (offen_stirn_ecke_eben_laengs[0])
                        {
                            Stirn.Activate();
                            Stirn.öffnen("Stirn", L, B, H, S, T_stirn_ecke_eben_laengs[0], !berechnungOhneBe.Checked);
                        }
                        if (offen_stirn_ecke_eben_laengs[1])
                        {
                            Ecke.Activate();
                            Ecke.öffnen("Ecke", L, B, H, S, T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
                        }
                        if (offen_stirn_ecke_eben_laengs[2])
                        {
                            Eben.Activate();
                            Eben.öffnen("Eben", L, B, H, S, T_stirn_ecke_eben_laengs[2], !berechnungOhneBe.Checked);
                        }
                        if (offen_stirn_ecke_eben_laengs[3])
                        {
                            Laengs.Activate();
                            Laengs.öffnen("Längs", L, B, H, S, T_stirn_ecke_eben_laengs[3], !berechnungOhneBe.Checked);
                        }
                    }
                }
                else //Wenn B größer als L ist:, oder S kleiner H
                {
                    if (B > L)
                    {
                        System.Windows.Forms.MessageBox.Show("Breite der Baugrube(B) muss kleiner oder gleich Länge der Baugrube(L) sein. B wird nun Maximal gesetzt", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (L % 1 == 0)
                        {
                            textBox2.Text = L + ",00";
                            textBox2_Leave(textBox2, e);
                        }
                        else
                        {
                            textBox2.Text = L + "";
                            textBox2_Leave(textBox2, e);
                        }
                    }
                    else if(S<H)
                    {
                        System.Windows.Forms.MessageBox.Show("Aquivermächtigkeit (S) muss größer oder gleich der Wasserspiegeldifferenz (H) sein. H wird nun Maximal gesetzt", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (S % 1 == 0)
                        {
                            textBox3.Text = S + ",00";
                            textBox3_Leave(textBox3, e);
                        }
                        else
                        {
                            textBox3.Text = S + "";
                            textBox3_Leave(textBox3, e);
                        }
                    }
                    else if(B/L<0.3)
                    {
                        System.Windows.Forms.MessageBox.Show("Breite der Baugrube (B) muss größer oder gleich 0,3 * Länge der Baugrube(L) sein. L wird nun Maximal gesetzt", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (B*3 % 1 == 0)
                        {
                            textBox1.Text = (B*3) + ",00";
                            textBox1_Leave(textBox1, e);
                        }
                        else
                        {
                            textBox1.Text = (B*3) + "";
                            textBox1_Leave(textBox1, e);
                        }
                    }
                }
                textBox1.BackColor = Color.White;
                textBox2.BackColor = Color.White;
                textBox3.BackColor = Color.White;
                textBox4.BackColor = Color.White;
                textBox5.BackColor = Color.White;
                textBox6.BackColor = Color.White;
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
        private void T_stirnClick(object sender, EventArgs e)
        {
            if(!offen_stirn_ecke_eben_laengs[0])
            {
                Stirn.Show();
                Stirn.öffnen("Stirn", L, B, H, S, T_stirn_ecke_eben_laengs[0], !berechnungOhneBe.Checked);
                offen_stirn_ecke_eben_laengs[0] = true;
            }
            Stirn.Activate();
            
        }
        private void T_eckeClick(object sender, EventArgs e)
        {
            if (!offen_stirn_ecke_eben_laengs[1])
            {
                Ecke.Show();
                Ecke.öffnen("Ecke", L, B, H, S, T_stirn_ecke_eben_laengs[1], !berechnungOhneBe.Checked);
                offen_stirn_ecke_eben_laengs[1] = true;
            }
            Ecke.Activate();
        }
        private void T_ebenClick(object sender, EventArgs e)
        {
            if (!offen_stirn_ecke_eben_laengs[2])
            {
                Eben.Show();
                Eben.öffnen("Eben", L, B, H, S, T_stirn_ecke_eben_laengs[2], !berechnungOhneBe.Checked);
                offen_stirn_ecke_eben_laengs[2] = true;
            }
            Eben.Activate();
        }
        private void T_laengsClick(object sender, EventArgs e)
        {
            if (!offen_stirn_ecke_eben_laengs[3])
            {
                Laengs.Show();
                Laengs.öffnen("Längs", L, B, H, S, T_stirn_ecke_eben_laengs[3], !berechnungOhneBe.Checked);
                offen_stirn_ecke_eben_laengs[3] = true;
            }
            Laengs.Activate();
        }
        /// <summary>
        /// Schreibt die Ergebnisse in die Entsprechende Labels
        /// </summary>
        private void ergebnisseInLabelSchreiben()
        {
            Ergebnis_eben_mi_Be_Label.Text = "" + Berechnung.berechnungebenMitBe(A_eb, U_eb, Bei, L, B, H, S, Y, n);
            Ergebnis_eben_ohne_Be_Label.Text = "" + Berechnung.berechnungebenOhneBe(A_eb, U_eb, L, B, H, S, Y, n);
            Ergebnis_Länge_mit_Be_Label.Text = "" + Berechnung.berechnungMitBe(A_L, U_L, Bei, L, B, H, S, Y, n);
            Ergebnis_Längs_ohne_Be_Label.Text = "" + Berechnung.berechnungOhneBe(A_L, U_L, L, B, H, S, Y, n);
            Ergebnis_Stirn_mit_Be_Label.Text = "" + Berechnung.berechnungMitBe(A_S, U_S, Bei, L, B, H, S, Y, n);
            Ergebnis_Stirn_ohne_Be_Label.Text = "" + Berechnung.berechnungOhneBe(A_S, U_S, L, B, H, S, Y, n);
            Ergebnis_Ecke_mit_Be_Label.Text = "" + Berechnung.berechnungMitBe(A_Ec, U_Ec, Bei, L, B, H, S, Y, n);
            Ergebnis_Ecke_ohne_Be_Label.Text = "" + Berechnung.berechnungOhneBe(A_Ec, U_Ec, L, B, H, S, Y, n);
        }
        /// <summary>
        /// Setzt die Attribute L, B, H, S, Y, n, A_eb, A_L, A_S, A_Ec, U_eb, U_L, U_S, U_Ec und Bei
        /// </summary>
        private void variablenSetzen()
        {
            L = Convert.ToDouble(textBox1.Text);
            B = Convert.ToDouble(textBox2.Text);
            H = Convert.ToDouble(textBox3.Text);
            S = Convert.ToDouble(textBox6.Text);
            Y = Convert.ToDouble(textBox5.Text);
            n = Convert.ToDouble(textBox4.Text);
            A_eb = Convert.ToDouble(A_eben.Text);
            U_eb = Convert.ToDouble(U_eben.Text);
            A_L = Convert.ToDouble(A_Längs.Text);
            U_L = Convert.ToDouble(U_Längs.Text);
            A_S = Convert.ToDouble(A_Stirn.Text);
            U_S = Convert.ToDouble(U_Stirn.Text);
            A_Ec = Convert.ToDouble(A_Ecke.Text);
            U_Ec = Convert.ToDouble(U_Ecke.Text);
            Bei = Convert.ToDouble(Be.Text);
        }

        /// <summary>
        /// Überprüft ob die Eingaben gültig sind
        /// </summary>
        /// <returns></returns>
        private bool gueltigeNummern()
        {
            bool laengevalid = Validierung.isValidNumber(textBox1.Text, "L");
            bool baugrubevalid = Validierung.isValidNumber(textBox2.Text, "B");
            bool wasserspiegelvalid = Validierung.isValidNumber(textBox3.Text, "H");
            bool sicherheitvalid = Validierung.isValidNumber(textBox4.Text, "n");
            bool auftriebvalid = Validierung.isValidNumber(textBox5.Text, "γ");
            bool aquifervalid = Validierung.isValidNumber(textBox6.Text, "S");
            return (laengevalid && baugrubevalid && wasserspiegelvalid && sicherheitvalid && auftriebvalid && aquifervalid);
        }

        /// <summary>
        /// Überprüft ob die Eingabe gültig ist
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        private bool gueltigeNummern(TextBox active)
        {
            if (active.Name == "textBox1")
            {
               return Validierung.isValidNumber(textBox1.Text, "L");
            }
            else if (active.Name == "textBox2")
            {
                return Validierung.isValidNumber(textBox2.Text, "B");
            }
            else if (active.Name == "textBox3")
            {
                return Validierung.isValidNumber(textBox3.Text, "H");
            }
            else if (active.Name == "textBox4")
            {
                return Validierung.isValidNumber(textBox4.Text, "n");
            }
            else if (active.Name == "textBox5")
            {
                return Validierung.isValidNumber(textBox5.Text, "γ");
            }
            else if (active.Name == "textBox6")
            {
                return Validierung.isValidNumber(textBox6.Text, "S");
            }
            return false;
        }
        /// <summary>
        /// koppelt hScrollBar1 mit textBox1 wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, textBox1);
        }
        /// <summary>
        /// koppelt hScrollBar2 mit textBox2 wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        
        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, textBox2);
        }

        /// <summary>
        /// koppelt hScrollBar4 mit textBox3 wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void hScrollBar4_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, textBox3);
        }

        /// <summary>
        /// koppelt hScrollBar3 mit textBox6 wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void hScrollBar3_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, textBox6);
        }

        /// <summary>
        /// koppelt hScrollBar6 mit textBox5 wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void hScrollBar6_ValueChanged(object sender, EventArgs e)
        {
            scallierungDerWerte(sender, e, textBox5);
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
        /// koppelt hScrollBar5 mit textBox4 wenn der Wert der Scrollbar verändert wurde.
        /// </summary>
        private void hScrollBar5_ValueChanged(object sender, EventArgs e)
        {
            // Überprüft ob die Zahl eine Nachkommastelle hat
            if ((hScrollBar5.Value / 1000.0) % 1 == 0)
            {
                textBox4.Text = (hScrollBar5.Value / 1000.0) + ",000";
            }
            else
            {
                textBox4.Text = (hScrollBar5.Value / 1000.0) + "";
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
            infoForm.Show();
        }

        /// <summary>
        /// beendet das Programm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void programmBeendenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// lässt die Werte einmalig berechnen sobald die Form geöffnet wurde.
        /// lässt die werte aus der textBox in die Scrollbar überschreiben.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(counter == 0)
            { 
                Berechnen_Click(null, null);
                counter++;
                textBox1_Leave(textBox1, e);
                textBox2_Leave(textBox2, e);
                textBox3_Leave(textBox3, e);
                textBox4_Leave(textBox4, e);
                textBox5_Leave(textBox5, e);
                textBox6_Leave(textBox6, e);
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
            textBox1.Text = "30,00";
            textBox2.Text = "15,00";
            textBox3.Text = "10,00";
            textBox6.Text = "30,00";
            textBox5.Text = "11,00";
            textBox4.Text = "1,368";
            textBox1_Leave(textBox1, e);
            textBox2_Leave(textBox2, e);
            textBox3_Leave(textBox3, e);
            textBox4_Leave(textBox4, e);
            textBox5_Leave(textBox5, e);
            textBox6_Leave(textBox6, e);
            Berechnen_Click(sender, e);
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
                if (aufAlleUebernehmen && bezugsScrollBar.Name == "hScrollBar5")
                {
                    //Überprüft ob der gerundete Wert der Textbox gleich dem Wert der Scrollbar ist oder ob nicht mehr als 3 Nachkommastellen zum runden da sind.
                    if (bezugsScrollBar.Value != (int)(Convert.ToDouble(tempTextBox.Text) * 1000 + 0.5) || (Convert.ToDouble(tempTextBox.Text) * 1000) % 1 != 0)
                    {
                        //Überschreibt den gerundeten Wert in die ScrollBar
                        bezugsScrollBar.Value = (int)(Convert.ToDouble(tempTextBox.Text) * 1000 + 0.5);
                        //lässt den Wert zurücküberschreiben für eine änderung von 11,00 zu 11,0004 zurück zu 11,000 da sonst Werte gleich.
                        hScrollBar5_ValueChanged(sender, null);
                    }
                }
                // Überprüft ob Checkbox nicht angehakt wurde und ob es Scrollbar5 ist.
                else if (!aufAlleUebernehmen && bezugsScrollBar.Name == "hScrollBar5")
                {
                    //Überprüft ob gerundet werden muss
                    if ((Convert.ToDouble(tempTextBox.Text) * 1000) % 1 != 0)
                    {
                        //gibt MessageBox aus.
                        Form message = new Baugrundberechnung.MessageBox("Es kann maximal mit einer Genauigkeit von\n3 Nachkommastellen gerechnet werden.\nEs wird automatisch gerundet");
                        message.Show();
                    }
                    //Überprüft ob der gerundete Wert der Textbox gleich dem Wert der Scrollbar ist oder ob nicht mehr als 3 Nachkommastellen zum runden da sind.
                    if (bezugsScrollBar.Value != (int)(Convert.ToDouble(tempTextBox.Text) * 1000 + 0.5) || (Convert.ToDouble(tempTextBox.Text) * 1000) % 1 != 0)
                    {
                        //Überschreibt den gerundeten Wert in die ScrollBar
                        bezugsScrollBar.Value = (int)(Convert.ToDouble(tempTextBox.Text) * 1000 + 0.5);
                        hScrollBar5_ValueChanged(sender, null);
                    }
                }
                // Überprüft ob Checkbox angehakt ist und ob es nicht Scrollbar5 ist.
                else if (aufAlleUebernehmen && bezugsScrollBar.Name != "hScrollBar5")
                {
                    //Überprüft ob der gerundete Wert der Textbox gleich dem Wert der Scrollbar ist oder ob nicht mehr als 2 Nachkommastellen zum runden da sind.
                    if (bezugsScrollBar.Value != (int)(Convert.ToDouble(tempTextBox.Text) * 100 + 0.5) || (Convert.ToDouble(tempTextBox.Text) * 100) % 1 != 0)
                    {
                        //Überschreibt den gerundeten Wert in die ScrollBar
                        bezugsScrollBar.Value = (int)(Convert.ToDouble(tempTextBox.Text) * 100 + 0.5);
                        //lässt den Wert zurücküberschreiben für eine änderung von 11,000 zu 11,0004 zurück zu 11,000 da sonst Werte gleich.
                        scallierungDerWerte(bezugsScrollBar, null, tempTextBox);
                    }
                }
                // Überprüft ob Checkbox nicht angehakt wurde und ob es nicht Scrollbar5 ist.
                else if (!aufAlleUebernehmen && bezugsScrollBar.Name != "hScrollBar5")
                {
                    //Überprüft ob gerundet werden muss
                    if ((Convert.ToDouble(tempTextBox.Text) * 100) % 1 != 0)
                    {
                        //gibt MessageBox aus.
                        Form message = new Baugrundberechnung.MessageBox("Es kann maximal mit einer Genauigkeit von\n2 Nachkommastellen gerechnet werden.\nEs wird automatisch gerundet");
                        message.Show();

                    }
                    //Überprüft ob der gerundete Wert der Textbox gleich dem Wert der Scrollbar ist oder ob nicht mehr als 2 Nachkommastellen zum runden da sind.
                    if (bezugsScrollBar.Value != (int)(Convert.ToDouble(tempTextBox.Text) * 100 + 0.5) || (Convert.ToDouble(tempTextBox.Text) * 100) % 1 != 0)
                    {
                        //Überschreibt den gerundeten Wert in die ScrollBar
                        bezugsScrollBar.Value = (int)(Convert.ToDouble(tempTextBox.Text) * 100 + 0.5);
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
        private void textBox1_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, hScrollBar1, "L");
        }

        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// textBox2, hScrollBar2 und B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, hScrollBar2, "B");
        }

        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// textBox3, hScrollBar4 und H
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, hScrollBar4, "H");
        }

        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// textBox6, hScrollBar3 und S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox6_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, hScrollBar3, "S");
        }

        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// textBox5, hScrollBar6 und γ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox5_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, hScrollBar6, "γ");
        }

        /// <summary>
        /// Ruft Runden und Ueberschreiben auf.
        /// textBox4, hScrollBar5 und n
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox4_Leave(object sender, EventArgs e)
        {
            Runden_und_Ueberschreiben(sender, hScrollBar5, "n");
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

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

    }
}